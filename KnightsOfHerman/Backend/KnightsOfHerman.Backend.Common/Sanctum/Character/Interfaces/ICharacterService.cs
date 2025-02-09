using KnightsOfHerman.Backend.Common.Database.Types.Character;
using KnightsOfHerman.Common.Abstract.Modification;
using KnightsOfHerman.Common.Sanctum.Abstract.Character;
using KnightsOfHerman.Common.Sanctum.Abstract.Character.Abilities;
using KnightsOfHerman.Common.Sanctum.Abstract.Character.Notes;
using KnightsOfHerman.Common.Sanctum.Communication.DTO;
using KnightsOfHerman.Common.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnightsOfHerman.Backend.Common.Sanctum.Character.Interfaces
{
    /// <summary>
    /// The main logic hub for Character Operations
    /// </summary>
    public interface ICharacterService
    {
        /// <summary>
        /// Creates a Character
        /// </summary>
        /// <param name="userID">User's ID</param>
        /// <returns></returns>
        public Task<TryResult<int>> CreateCharacter(int userID);

        /// <summary>
        /// Delets a character if the UserID has permission to
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="characterID"></param>
        /// <returns></returns>
        public Task<TryResult> DeleteCharacter(int userID, int characterID);

        /// <summary>
        /// Gets the user's Character profiles
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public Task<TryResult<List<CharacterProfile>>> GetProfiles(int userID);

        /// <summary>
        /// Gets a character's transfer object
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="characterID"></param>
        /// <returns></returns>
        public Task<TryResult<CharacterDTO>> GetCharacterDTO(int userID, int characterID);

        /// <summary>
        /// Modified a character by the path to the modification, handles edits and deletion
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="characterID"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public Task<TryResult> ModifyCharacter(int userID, int characterID, TrackedModificationEventArgs args);

        /// <summary>
        /// Creates an item
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="characterID"></param>
        /// <returns></returns>
        public Task<TryResult<CharacterItemDTO>> CreateItem(int userID, int characterID);
        
        /// <summary>
        /// Creates a weapon
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="characterID"></param>
        /// <returns></returns>
        public Task<TryResult<CharacterWeaponDTO>> CreateWeapon(int userID, int characterID);

        /// <summary>
        /// Creates armor
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="characterID"></param>
        /// <returns></returns>
        public Task<TryResult<CharacterArmorDTO>> CreateArmor(int userID, int characterID);

        /// <summary>
        /// Creates Journal Entry
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="characterID"></param>
        /// <param name="category"></param>
        /// <returns></returns>
        public Task<TryResult<CharacterJournalDTO>> CreateJournal(int userID, int characterID, JournalCategory category);

        /// <summary>
        /// Creates Ability
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="characterID"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public Task<TryResult<CharacterAbilityDTO>> CreateAbility(int userID, int characterID, AbilityType type);

        //Shares a character to the shareUser with the Accesslevel
        public Task<CharacterShareResult> ShareCharacter(int userID, string shareUser, int characterID, CharacterAccess access);

        /// <summary>
        /// Gets the shares a character has
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="characterID"></param>
        /// <returns></returns>
        public Task<CharacterSharePermissions> GetCharacterSharePermissions(int userId, int characterID);

        /// <summary>
        /// Removes a share from a character
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="characterID"></param>
        /// <returns></returns>
        public Task<TryResult> UnshareCharacter(int userId, int characterID);
    }
}
