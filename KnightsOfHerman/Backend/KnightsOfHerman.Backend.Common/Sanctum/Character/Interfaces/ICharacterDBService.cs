using KnightsOfHerman.Backend.Common.Sanctum.Character.Types;
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
    /// Wrapper for ICharacterDB that converts them to Server objects (SO)
    /// Only responsible for that conversion
    /// </summary>
    public interface ICharacterDBService
    {

        Task<TryResult<int>> CreateCharacter(int userid);
        Task<TryResult> DeleteCharacter(int userid, int characterID);

        Task<TryResult> SaveCharacterToDatabase(CharacterSO character);

        Task<TryResult<CharacterSO>> GetCharacterFromDatabase(int characterID);

        Task<TryResult<CharacterItemSO>> CreateItem(int characterID);
        Task<TryResult> DeleteItem(int itemID);
        Task<TryResult<CharacterWeaponSO>> CreateWeapon(int characterID);
        Task<TryResult> DeleteWeapon(int itemID);

        Task<TryResult<CharacterArmorSO>> CreateArmor(int characterID);
        Task<TryResult> DeleteArmor(int itemID);

        Task<TryResult<CharacterJournalSO>> CreateJournal(int characterID, JournalCategory category);
        Task<TryResult> DeleteJournal(int journalID);

        Task<TryResult<CharacterAbilitySO>> CreateAbility(int characterID, AbilityType type);
        Task<TryResult> DeleteAbility(int abilityID);

        Task<TryResult<int>> GetCharacterCount(int userID);

        Task<TryResult<List<CharacterProfile>>> GetCharacterProfiles(int userID);

        Task<CharacterShareResult> ShareCharacter(int ownerID, string shareuser, int characterID, CharacterAccess access);

        Task<TryResult<Dictionary<int, CharacterAccess>>> GetPermissions(int characterID);
        Task<CharacterSharePermissions> GetCharacterSharePermissions(int userId, int characterID);

        Task<TryResult> UnshareCharacter(int userId, int characterID);

    }
}
