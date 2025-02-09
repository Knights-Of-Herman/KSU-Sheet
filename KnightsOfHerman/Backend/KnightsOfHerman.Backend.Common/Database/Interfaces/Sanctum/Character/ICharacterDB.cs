using KnightsOfHerman.Backend.Common.Database.Types.Character;
using KnightsOfHerman.Common.Sanctum.Abstract.Character;
using KnightsOfHerman.Common.Sanctum.Abstract.Character.Abilities;
using KnightsOfHerman.Common.Sanctum.Abstract.Character.Items;
using KnightsOfHerman.Common.Sanctum.Abstract.Character.Notes;
using KnightsOfHerman.Common.Sanctum.Abstract.Character.Stats;
using KnightsOfHerman.Common.Sanctum.Communication.DTO;
using KnightsOfHerman.Common.Types;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnightsOfHerman.Backend.Common.Database.Interfaces.Sanctum.Character
{
    /// <summary>
    /// Class that reperesent all the Database Operations that can be done on the Character Object
    /// </summary>
    public interface ICharacterDB
    {
        /// <summary>
        /// Gets the amount of Characters a user currently has
        /// </summary>
        Task<TryResult<int>> GetCharacterCount(int userID);

        /// <summary>
        /// Creates a blank character for the user
        /// </summary>
        Task<TryResult<int>> CreateBlankCharacter(int userID);

        /// <summary>
        /// Gets the user's character profiles
        /// </summary>
        Task<TryResult<List<CharacterProfile>>> GetProfiles(int userID);

        /// <summary>
        /// Deletes a character
        /// </summary>
        Task<TryResult> DeleteCharacter(int userID, int characterID);

        /// <summary>
        /// Gets the Character's Stats
        /// </summary>
        Task<TryResult<List<CharacterStatDBO>>> GetStats(int characterID);

        /// <summary>
        /// Saves a changed stat to the db
        /// </summary>
        Task<TryResult> SaveStat(int characterID, ICharacterStat stat);
        
        /// <summary>
        /// Gets the Character's Resources
        /// </summary>
        Task<TryResult<List<CharacterResourceDBO>>> GetResources(int characterID);


        /// <summary>
        /// Saves a changed resource to the db
        /// </summary>
        Task<TryResult> SaveResource(int characterID, ICharacterResource resource);

        /// <summary>
        /// Gets the Character's General information
        /// </summary>
        Task<TryResult<CharacterBioDBO>> GetBio(int characterID);

        Task<TryResult> SaveBio(int characterID, ICharacterBio bio);

        /// <summary>
        /// Creates an item
        /// </summary>
        Task<TryResult<CharacterItemDBO>> CreateNewItem(int characterID);
        
        //Gets all the items a character has
        Task<TryResult<List<CharacterItemDBO>>> GetItems(int characterID);

        /// <summary>
        /// Saves a modified item
        /// </summary>
        Task<TryResult> SaveItem(ICharacterItem item);

        /// <summary>
        /// Deletes a character item
        /// </summary>
        Task<TryResult> DeleteItem(int itemID);

        /// <summary>
        /// Creates a new Weapon
        /// </summary>
        Task<TryResult<CharacterWeaponDBO>> CreateNewWeapon(int characterID);

        /// <summary>
        /// Gets the character's weapons
        /// </summary>
        /// <param name="characterID"></param>
        /// <returns></returns>
        Task<TryResult<List<CharacterWeaponDBO>>> GetWeapons(int characterID);
        
        /// <summary>
        /// Saves a modified weapon
        /// </summary>
        /// <param name="weapon"></param>
        /// <returns></returns>
        Task<TryResult> SaveWeapon(ICharacterWeapon weapon);

        /// <summary>
        /// Deletes a weapon from the db
        /// </summary>
        /// <param name="itemID"></param>
        /// <returns></returns>
        Task<TryResult> DeleteWeapon(int itemID);

        /// <summary>
        /// Creates an armor piece for the character
        /// </summary>
        /// <param name="characterID"></param>
        /// <returns></returns>
        Task<TryResult<CharacterArmorDBO>> CreateNewArmor(int characterID);

        /// <summary>
        /// Gets a characters armor pieces
        /// </summary>
        /// <param name="characterID"></param>
        /// <returns></returns>
        Task<TryResult<List<CharacterArmorDBO>>> GetArmor(int characterID);

        /// <summary>
        /// saves a modified armor piece
        /// </summary>
        /// <param name="armor"></param>
        /// <returns></returns>
        Task<TryResult> SaveArmor(ICharacterArmor armor);

        /// <summary>
        /// Deletes an armor piece
        /// </summary>
        /// <param name="itemID"></param>
        /// <returns></returns>
        Task<TryResult> DeleteArmor(int itemID);

        /// <summary>
        /// Creates a journal entry
        /// </summary>
        /// <param name="characterID"></param>
        /// <param name="category"></param>
        /// <returns></returns>
        Task<TryResult<CharacterJournalDBO>> CreateJournalEntry(int characterID, JournalCategory category);

        /// <summary>
        /// Gets a characters total journal entries
        /// </summary>
        /// <param name="characterID"></param>
        /// <returns></returns>
        Task<TryResult<List<CharacterJournalDBO>>> GetJournalEntries(int characterID);

        /// <summary>
        /// Saves a modified journal
        /// </summary>
        /// <param name="entry"></param>
        /// <returns></returns>
        Task<TryResult> SaveJournalEntry(ICharacterJournal entry);

        /// <summary>
        /// Delets a journal entry
        /// </summary>
        /// <param name="entryID"></param>
        /// <returns></returns>
        Task<TryResult> DeleteJournalEntry(int entryID);

        /// <summary>
        /// Creates a character's ability
        /// </summary>
        /// <param name="characterID"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        Task<TryResult<CharacterAbilityDBO>> CreateAbility(int characterID, AbilityType type);

        /// <summary>
        /// Gets the character's abilities
        /// </summary>
        /// <param name="characterID"></param>
        /// <returns></returns>
        Task<TryResult<List<CharacterAbilityDBO>>> GetAbilities(int characterID);

        /// <summary>
        /// Saves the character's ability
        /// </summary>
        /// <param name="ability"></param>
        /// <returns></returns>
        Task<TryResult> SaveAbility(ICharacterAbility ability);


        /// <summary>
        /// Deletes the character's ability 
        /// </summary>
        /// <param name="abilityID"></param>
        /// <returns></returns>
        Task<TryResult> DeleteAbility(int abilityID);

        /// <summary>
        /// Gets a list of who can access this character and their access level
        /// </summary>
        /// <param name="characterID"></param>
        /// <returns></returns>
        Task<TryResult<Dictionary<int,CharacterAccess>>> GetCharacterPermissions(int characterID);


        /// <summary>
        /// Shares a character to the other use with the given access level
        /// </summary>
        /// <param name="ownerID"></param>
        /// <param name="shareuser"></param>
        /// <param name="characterID"></param>
        /// <param name="access"></param>
        /// <returns></returns>
        Task<CharacterShareResult> ShareCharacter(int ownerID, string shareuser, int characterID, CharacterAccess access);

        /// <summary>
        /// Gets who the character is shared with
        /// used for displaying the data
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="characterID"></param>
        /// <returns></returns>
        Task<CharacterSharePermissions> GetCharacterSharePermissions(int userID, int characterID);

        /// <summary>
        /// Revokes a user's access to a character
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="characterID"></param>
        /// <returns></returns>
        Task<TryResult> UnshareCharacter(int userId, int characterID);

    }
}
