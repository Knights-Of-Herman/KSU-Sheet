using KnightsOfHerman.Backend.Common.Database.Interfaces.Sanctum.Character;
using KnightsOfHerman.Backend.Common.Database.Types.Character;
using KnightsOfHerman.Backend.Database.Azure.Character.Querries;
using KnightsOfHerman.Common.Sanctum.Abstract.Character;
using KnightsOfHerman.Common.Sanctum.Abstract.Character.Abilities;
using KnightsOfHerman.Common.Sanctum.Abstract.Character.Items;
using KnightsOfHerman.Common.Sanctum.Abstract.Character.Notes;
using KnightsOfHerman.Common.Sanctum.Abstract.Character.Stats;
using KnightsOfHerman.Common.Sanctum.Communication.DTO;
using KnightsOfHerman.Common.Types;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnightsOfHerman.Backend.Database.Azure.Character
{
    /// <summary>
    /// Implementation of ICharacterDB to an AzureDB
    /// </summary>
    public class CharacterDB : ICharacterDB
    {
        AzureDBContext dBContext;

        BasicQueries basic;

        JournalQueries journal;

        StatQueries stats;

        InventoryQueries inventory;

        AbilityQueries abilities;
        public CharacterDB(AzureDBContext context)
        {
            dBContext = context;
            basic = new(dBContext);
            journal = new(dBContext);
            stats = new(dBContext);
            inventory = new(dBContext);
            abilities = new(dBContext);
        }
        DbConnection GetConnection() => dBContext.Database.GetDbConnection();

        public async Task<TryResult<int>> CreateBlankCharacter(int userID) => await basic.CreateCharacter(userID);

        public async Task<TryResult<List<CharacterProfile>>> GetProfiles(int userID) => await basic.GetProfiles(userID);

        public async Task<TryResult> DeleteCharacter(int userID, int characterID) => await basic.DeleteCharacterAsync(userID, characterID);

        public async Task<TryResult<List<CharacterStatDBO>>> GetStats(int characterID) => await stats.GetCharacterStats(characterID);

        public async Task<TryResult> SaveStat(int characterID, ICharacterStat stat) => await stats.SaveCharacterStat(characterID, stat);

        public async Task<TryResult<List<CharacterResourceDBO>>> GetResources(int characterID) => await stats.GetCharacterResources(characterID);

        public async Task<TryResult> SaveResource(int characterID, ICharacterResource resource) => await stats.SaveCharacterResource(characterID, resource);

        public async Task<TryResult<CharacterBioDBO>> GetBio(int characterID) => await basic.GetCharacterBio(characterID);

        public async Task<TryResult> SaveBio(int characterid, ICharacterBio bio) => await basic.SaveCharacterBio(characterid, bio);

        public async Task<TryResult<CharacterItemDBO>> CreateNewItem(int characterID) => await inventory.CreateItem(characterID);
        public async Task<TryResult<List<CharacterItemDBO>>> GetItems(int characterID) => await inventory.GetItems(characterID);
        public async Task<TryResult> SaveItem(ICharacterItem item) => await inventory.SaveItem(item);
        public async Task<TryResult> DeleteItem(int itemid) => await inventory.DeleteItem(itemid);

        public async Task<TryResult<CharacterWeaponDBO>> CreateNewWeapon(int characterID) => await inventory.CreateWeapon(characterID);

        public async Task<TryResult<List<CharacterWeaponDBO>>> GetWeapons(int characterID) => await inventory.GetWeapons(characterID);

        public async Task<TryResult> SaveWeapon(ICharacterWeapon weapon) => await inventory.SaveWeapon(weapon);

        public async Task<TryResult> DeleteWeapon(int itemID) => await inventory.DeleteWeapon(itemID);

        public async Task<TryResult<CharacterArmorDBO>> CreateNewArmor(int characterID) => await inventory.CreateArmor(characterID);

        public async Task<TryResult<List<CharacterArmorDBO>>> GetArmor(int characterID) => await inventory.GetArmor(characterID);

        public async Task<TryResult> SaveArmor(ICharacterArmor armor) => await inventory.SaveArmor(armor);

        public async Task<TryResult> DeleteArmor(int itemID) => await inventory.DeleteArmor(itemID);

        public async Task<TryResult<CharacterJournalDBO>> CreateJournalEntry(int characterid, JournalCategory category) => await journal.CreateJournalEntry(characterid, category);
        public async Task<TryResult<List<CharacterJournalDBO>>> GetJournalEntries(int characterid) => await journal.GetJournalEntries(characterid);
        public async Task<TryResult> SaveJournalEntry(ICharacterJournal entry) => await journal.SaveJournalEntry(entry);
        public async Task<TryResult> DeleteJournalEntry(int entryid) => await journal.DeleteJournalEntry(entryid);

        public async Task<TryResult<CharacterAbilityDBO>> CreateAbility(int characterid, AbilityType type) => await abilities.CreateAbility(characterid, type);
        public async Task<TryResult<List<CharacterAbilityDBO>>> GetAbilities(int characterid) => await abilities.GetAbilities(characterid);
        public async Task<TryResult> SaveAbility(ICharacterAbility ability) => await abilities.SaveAbility(ability);
        public async Task<TryResult> DeleteAbility(int abilityid) => await abilities.DeleteAbility(abilityid);

        public async Task<TryResult<int>> GetCharacterCount(int userID) => await basic.GetCharacterCount(userID);

        public async Task<TryResult<Dictionary<int, CharacterAccess>>> GetCharacterPermissions(int characterID) => await basic.GetCharacterPermissions(characterID);

        public async Task<CharacterShareResult> ShareCharacter(int ownerID, string shareuser, int characterID, CharacterAccess access) => await basic.ShareCharacter(ownerID, shareuser, characterID, access);

        public async Task<CharacterSharePermissions> GetCharacterSharePermissions(int userID, int characterID) => await basic.GetCharacterSharePermissions(userID, characterID);

        public Task<TryResult> UnshareCharacter(int userId, int characterID) => basic.UnshareCharacter(userId, characterID);
    }
}
