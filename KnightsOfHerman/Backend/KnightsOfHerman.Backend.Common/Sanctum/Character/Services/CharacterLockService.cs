using KnightsOfHerman.Backend.Common.Sanctum.Character.Interfaces;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnightsOfHerman.Backend.Common.Sanctum.Character.Services
{
    /// <summary>
    /// Implementation of ICharacterLockSErvice
    /// </summary>
    public class CharacterLockService : ICharacterLockService
    {
        ConcurrentDictionary<int, SemaphoreSlim> _locks;

        public CharacterLockService()
        {
            _locks = new();
        }

        public SemaphoreSlim GetLock(int characterId)
        {
            return _locks.GetOrAdd(characterId, _ => new SemaphoreSlim(1,1));
        }
    }
}
