using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnightsOfHerman.Backend.Common.Sanctum.Character.Interfaces
{
    /// <summary>
    /// Holds lock objects for character IDs, should be a singleton.
    /// </summary>
    public interface ICharacterLockService
    {
        /// <summary>
        /// Gets the lock associated with the caharacter
        /// </summary>
        /// <param name="characterId"></param>
        /// <returns></returns>
        public SemaphoreSlim GetLock(int characterId);
    }
}
