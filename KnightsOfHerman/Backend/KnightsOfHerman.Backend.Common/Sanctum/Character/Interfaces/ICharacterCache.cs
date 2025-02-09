using KnightsOfHerman.Backend.Common.Sanctum.Character.Types;
using KnightsOfHerman.Common.Types;

namespace KnightsOfHerman.Backend.Common.Sanctum.Character.Interfaces
{
    /// <summary>
    /// Cache that stores characters in memory instead of the databases
    /// Saves periodically to the database
    /// </summary>
    public interface ICharacterCache
    {
        /// <summary>
        /// Tries to get a character from the cache
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<TryResult<CharacterSO>> TryLoadCharacter(int id);

        /// <summary>
        /// Tries to save a character to the cache
        /// </summary>
        /// <param name="character"></param>
        /// <returns></returns>
        public Task<TryResult> TrySaveCharacter(CharacterSO character);

        /// <summary>
        /// Removes a character from the cache
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task EvictCharacter(int id);
    }
}
