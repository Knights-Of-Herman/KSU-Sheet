using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnightsOfHerman.Backend.Common.Sanctum.Character.Interfaces
{
    /// <summary>
    /// Service to handle character events
    /// </summary>
    public interface ICharacterNotificationService
    {
        /// <summary>
        /// Tell subscribers the character has been deleted
        /// </summary>
        /// <param name="id"></param>
        void NotifyCharacterDeletion(int id);

        /// <summary>
        /// Handler for Character Deletion events
        /// </summary>
        /// <param name="characterID"></param>
        public delegate void CharacterDeletionHandler(int characterID);

        /// <summary>
        /// Subscribe to for Character Deletion events
        /// </summary>
        public event CharacterDeletionHandler OnCharacterDeleted;
    }
}
