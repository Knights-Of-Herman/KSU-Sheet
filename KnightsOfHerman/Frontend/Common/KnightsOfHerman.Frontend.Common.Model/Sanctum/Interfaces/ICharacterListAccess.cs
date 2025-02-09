using KnightsOfHerman.Common.Sanctum.Abstract.Character;
using KnightsOfHerman.Common.Sanctum.Communication.DTO;
using KnightsOfHerman.Common.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnightsOfHerman.Frontend.Common.Model.Sanctum.Interfaces
{
    /// <summary>
    /// Handles operations for the Character List page
    /// </summary>
    public interface ICharacterListAccess
    {
        /// <summary>
        /// Loads teh character's profiles
        /// </summary>
        /// <returns></returns>
        public Task<TryResult<List<CharacterProfile>>> GetProfilesAsync();

        /// <summary>
        /// Creates a new blank character
        /// </summary>
        /// <returns></returns>
        public Task<TryResult<int>> CreateBlankChracter();

        /// <summary>
        /// Deletes a character
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<bool> DeleteCharacterAsync(int id);

        /// <summary>
        /// Shares a character
        /// </summary>
        /// <param name="shareuser"></param>
        /// <param name="characterID"></param>
        /// <param name="access"></param>
        /// <returns></returns>

        public Task<CharacterShareResult> ShareCharacter(string shareuser, int characterID, CharacterAccess access);

        /// <summary>
        /// Gets the current shares of a character
        /// </summary>
        /// <param name="characterID"></param>
        /// <returns></returns>

        public Task<CharacterSharePermissions> GetSharePermissions(int characterID);

        /// <summary>
        /// Removes a character that was shared to you
        /// </summary>
        /// <param name="characterID"></param>
        /// <returns></returns>
        public Task UnsubscribeCharacter(int characterID);
    }
}
