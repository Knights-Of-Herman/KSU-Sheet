using KnightsOfHerman.Backend.Common.Sanctum.Character.Interfaces;
using KnightsOfHerman.Backend.Common.Sanctum.Character.Types;
using KnightsOfHerman.Backend.Database.Azure.Character;
using KnightsOfHerman.Common.Types;
using Microsoft.Extensions.Caching.Memory;
using System.Collections.Concurrent;
using System.Reflection.Metadata.Ecma335;
using System.Threading;

namespace KnightsOfHerman.Backend.Server.Memory
{
    /// <summary>
    /// Background service implmentation of ICharacter Cache,
    /// Handles revolving saves of characters to the database
    /// </summary>
    public class CharacterCache : BackgroundService, ICharacterCache
    {
        public CharacterCache(IMemoryCache memory, IServiceProvider services, ICharacterLockService locks)
        {
            _memory = memory;
            _services = services;
            _locks = locks;
        }

        ICharacterLockService _locks;

        /// <summary>
        /// Memory to save active character data to
        /// </summary>
        IMemoryCache _memory;

        /// <summary>
        /// Way to access other services as a singleton
        /// </summary>
        IServiceProvider _services;

        /// <summary>
        /// Semaphore to limit the number of concurrent tasks
        /// </summary>
        private SemaphoreSlim _semaphore = new SemaphoreSlim(10);

        /// <summary>
        /// Time that a entry can exist without access
        /// </summary>
        TimeSpan _defaultTTL = TimeSpan.FromMinutes(10);

        /// <summary>
        /// How often to save a character to the DB
        /// </summary>
        TimeSpan _saveFreq = TimeSpan.FromSeconds(10);

        /// <summary>
        /// Concurrent Dictionary to use a sudo Unique Concurrent List, also stores the last save time
        /// </summary>
        static ConcurrentDictionary<int, DateTime> _activeCharacterIDs = new();
        
        /// <summary>
        /// Tracks the ids of character's that have been modified
        /// </summary>
        static ConcurrentDictionary<int,bool> _modifiedCharacters = new();

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var tasks = new List<Task>();

                foreach (var id in _modifiedCharacters.Keys) 
                {
                    tasks.Add(DoSaveAsync(id, stoppingToken));
                }

                await Task.WhenAll(tasks);

                await Task.Delay(2000, stoppingToken);
            }
        }

        public async Task<TryResult<CharacterSO>> TryLoadCharacter(int id)
        {
            if (_memory.TryGetValue<CharacterSO>(id, out var character))
            {
                if (character != null)
                {
                    return TryResult<CharacterSO>.Success(character);
                }
            }
            return TryResult<CharacterSO>.Fail("Character Not In Memory");
        }

        public async Task<TryResult> TrySaveCharacter(CharacterSO character)
        {
            var options = new MemoryCacheEntryOptions()
                .SetSlidingExpiration(_defaultTTL)
                .RegisterPostEvictionCallback(callback: OnCharacterEviction);

            _memory.Set<CharacterSO>(character.CharacterID, character, options);
            var result = _activeCharacterIDs[character.CharacterID] = DateTime.Now;

            if (character.Modified) _modifiedCharacters.TryAdd(character.CharacterID,true);
            else _modifiedCharacters.TryRemove(character.CharacterID, out _);

            return TryResult.Success();
        }

        /// <summary>
        /// Handles saving the character to the database one last time when it expires from the cache
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="reason"></param>
        /// <param name="state"></param>
        private async void OnCharacterEviction(object key, object? value, EvictionReason reason, object? state)
        {
            if(reason != EvictionReason.Replaced)
            {
                if (key is int id)
                {
                    _activeCharacterIDs.Remove(id, out _);
                    _modifiedCharacters.TryRemove(id, out _);

                    //Final save of data
                    if (value is CharacterSO character)
                    {
                        using (var scope = _services.CreateAsyncScope())
                        {
                            var db = scope.ServiceProvider.GetRequiredService<ICharacterDBService>();
                            await db.SaveCharacterToDatabase(character);
                        }
                    }
                }
            }
        }
        
        /// <summary>
        /// Handles saving a character to the database
        /// </summary>
        /// <param name="id"></param>
        /// <param name="stoppingToken"></param>
        /// <returns></returns>
        private async Task DoSaveAsync(int id, CancellationToken stoppingToken)
        {
            //Enter Semaphore
            await _semaphore.WaitAsync(stoppingToken);

            try
            {
                var span = DateTime.Now - _activeCharacterIDs[id];
                if (span > _saveFreq)
                {
                    var charLock = _locks.GetLock(id);
                    await charLock.WaitAsync(); //Wait to enter Character semaphore
                    try
                    {
                        //Load character from Cache
                        var result = await TryLoadCharacter(id);
                        if (result.IsSuccess && result.Value != null)
                        {
                            var character = result.Value;
                            if (character.Modified)
                            {
                                //Save character to db
                                using (var scope = _services.CreateAsyncScope())
                                {
                                    var db = scope.ServiceProvider.GetRequiredService<ICharacterDBService>();

                                    var dbresult = await db.SaveCharacterToDatabase(character);
                                    if (dbresult.IsSuccess)
                                    {
                                        //Put Character Back into cache
                                        _activeCharacterIDs[id] = DateTime.Now;
                                        var memresult = await TrySaveCharacter(character);
                                        if (!memresult.IsSuccess)
                                        {
                                            _activeCharacterIDs.Remove(id, out _);
                                            _modifiedCharacters.TryRemove(id, out _);
                                            _memory.Remove(id); // Make sure the memory is cleared
                                        }
                                    }
                                }
                            }
                        }
                        else //If can't get character no need to have access to it
                        {
                            _activeCharacterIDs.Remove(id, out _);
                            _modifiedCharacters.TryRemove(id, out _);
                            _memory.Remove(id); // Make sure the memory is cleared
                        }
                    }
                    catch
                    {
                        //Keep on going on.
                    }
                    finally
                    {
                        //Release character semaphore
                        charLock.Release();
                    }          
                }
            }
            finally
            {
                //Release the semaphore no matter what
                _semaphore.Release();
            }
        }

        public async Task EvictCharacter(int id)
        {
            await Task.Run(()=>_memory.Remove(id));
        }
    }
}
