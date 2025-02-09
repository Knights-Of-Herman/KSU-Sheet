using KnightsOfHerman.Backend.Database.Azure.Character;
using KnightsOfHerman.Backend.Database.Testing.UnitTesting;
using KnightsOfHerman.Common.Sanctum.Abstract.Character.Abilities;
using KnightsOfHerman.Common.Sanctum.Abstract.Character.Items;
using KnightsOfHerman.Common.Sanctum.Abstract.Character.Notes;
using KnightsOfHerman.Common.Sanctum.Abstract.Character.Stats;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;
using System;

namespace KnightsOfHerman.XUnitTesting.AzureDatabase.CharacterTests
{
    public class CharacterDBTests
    {
        int userID = 1;

        public CharacterDBTests()
        {
        }
        #region HelperFunctions
        async Task CreateStatedCharacter()
        {
            throw new NotImplementedException();
        }

        async Task DeleteUserCharacters()
        {
            await DBFactory.GetContext().Database.ExecuteSqlRawAsync($"DELETE FROM Sanctum.Characters WHERE OwnerID = {userID}");
        }

        async Task DeleteCharacter(int id)
        {
            await DBFactory.GetContext().Database.ExecuteSqlRawAsync($"DELETE FROM Sanctum.Characters WHERE CharacterID = {id}");
        }
        #endregion

        #region Bio

        [Fact]
        [Trait("Bio", "")]

        public async void CanGetCharacterBio()
        {
            var db = new CharacterDB(DBFactory.GetContext());
            var characterid = (await db.CreateBlankCharacter(userID)).Value;

            var result = await db.GetBio(characterid);
            Assert.True(result.IsSuccess);

            Assert.NotNull(result.Value);
        }

        [Fact]
        [Trait("Bio", "")]
        public async void CanSaveCharacterBio()
        {
            var db = new CharacterDB(DBFactory.GetContext());
            var characterid = (await db.CreateBlankCharacter(userID)).Value;
            var bio = (await db.GetBio(characterid)).Value;

            bio.Background = "Test Background";
            bio.Name = "Test Name";
            bio.Race = "Test Race";
            bio.TotalXP = 123;
            bio.UnspentXP = 100;
            bio.Conflict = 50;
            bio.Destiny = 2;
            bio.Languages = "Vadeshi";

            var result = await db.SaveBio(characterid, bio);
            Assert.True(result.IsSuccess);

            var newbio = (await db.GetBio(characterid)).Value;

            Assert.Equal(bio.Background, newbio.Background);
            Assert.Equal(bio.Name, newbio.Name);
            Assert.Equal(bio.Race, newbio.Race);
            Assert.Equal(bio.TotalXP, newbio.TotalXP);
            Assert.Equal(bio.UnspentXP, newbio.UnspentXP);
            Assert.Equal(bio.Conflict, newbio.Conflict);
            Assert.Equal(bio.Destiny, newbio.Destiny);
            Assert.Equal(bio.Languages, newbio.Languages);

        }
        #endregion

        #region Stats
        [Fact]
        [Trait("Stats", "")]

        public async void CanGetStats()
        {
            var db = new CharacterDB(DBFactory.GetContext());
            var characterid = (await db.CreateBlankCharacter(userID)).Value;

            var result = await db.GetStats(characterid);

            Assert.True(result.IsSuccess);

            Assert.Equal(74, result.Value.Count);

            await DeleteCharacter(characterid);
        }

        [Fact]
        [Trait("Stats", "")]

        public async void CanModifyStat()
        {
            var db = new CharacterDB(DBFactory.GetContext());
            var characterid = (await db.CreateBlankCharacter(userID)).Value;

            var result = await db.GetStats(characterid);

            var stat = result.Value.Find(x => x.StatID == CharacterStats.Intelligence);

            Assert.NotNull(stat);

            stat.Base = 2;
            stat.OverrideValue = 3;
            stat.DoOverride = true;
            stat.CustomMod = 5;

            var saveresult = await db.SaveStat(characterid, stat);
            Assert.True(saveresult.IsSuccess);

            var newresult = await db.GetStats(characterid);

            var stat2 = result.Value.Find(x => x.StatID == CharacterStats.Intelligence);

            Assert.Equal(stat, stat2);

            await DeleteCharacter(characterid);
        }

        #endregion

        #region Resources
        [Fact]
        [Trait("Resources", "")]

        public async void CanGetResources()
        {
            var db = new CharacterDB(DBFactory.GetContext());
            var characterid = (await db.CreateBlankCharacter(userID)).Value;

            var result = await db.GetResources(characterid);

            Assert.True(result.IsSuccess);

            Assert.Equal(4, result.Value.Count);

            await DeleteCharacter(characterid);
        }

        [Fact]
        [Trait("Resources", "")]

        public async void CanModifyResource()
        {
            var db = new CharacterDB(DBFactory.GetContext());
            var characterid = (await db.CreateBlankCharacter(userID)).Value;

            var result = await db.GetResources(characterid);

            var stat = result.Value.Find(x => x.ResourceID == CharacterResources.Health);

            Assert.NotNull(stat);

            stat.Attribute = 1;


            stat.Modifier = 4;


            stat.DoOverrideMax = true;
            stat.OverrideMaxValue = 7;

            stat.CurrentValue = 8;

            var saveresult = await db.SaveResource(characterid, stat);
            Assert.True(saveresult.IsSuccess);

            var newresult = await db.GetStats(characterid);

            var stat2 = result.Value.Find(x => x.ResourceID == CharacterResources.Health);

            Assert.Equal(stat, stat2);

            await DeleteCharacter(characterid);
        }
        #endregion

        #region Management
        [Fact]
        [Trait("Management", "")]

        public async void CanDeleteCharacter()
        {
            var db = new CharacterDB(DBFactory.GetContext());
            var result = await db.CreateBlankCharacter(userID);
            Assert.True(result.IsSuccess);
            var id = result.Value;

            //Create some items

            for (int i = 0; i < 5; i++)
            {
                await db.CreateNewItem(id);
            }


            var deleteresult = await db.DeleteCharacter(1, id);
            Assert.True(deleteresult.IsSuccess);

            //Check For Character
            var character = await DBFactory.GetContext().Database.SqlQueryRaw<int>($"SELECT COUNT(*) AS Value FROM Sanctum.Characters WHERE CharacterID = {id}").FirstAsync();
            Assert.Equal(0, character);

            //Check For Journal
            var stats = await DBFactory.GetContext().Database.SqlQueryRaw<int>($"SELECT COUNT(*) AS Value FROM Sanctum.CharacterStats WHERE CharacterID = {id}").FirstAsync();
            Assert.Equal(0, stats);

            //Check For Weapons

            //Check For Items
            var items = await DBFactory.GetContext().Database.SqlQueryRaw<int>($"SELECT COUNT(*) AS Value FROM Sanctum.CharacterItems WHERE CharacterID = {id}").FirstAsync();
            Assert.Equal(0, items);

            //Check for Armor


            //Check for Abilities
        }

        [Fact]
        [Trait("Management", "")]

        public async void CanCreateCharacter()
        {
            var db = new CharacterDB(DBFactory.GetContext());

            var result = await db.CreateBlankCharacter(userID);

            Assert.True(result.IsSuccess);

            var id = result.Value;

            var dbId = await DBFactory.GetContext().Database.SqlQueryRaw<int>($"SELECT CharacterID AS Value FROM Sanctum.Characters WHERE CharacterID = {id}").FirstAsync();

            Assert.Equal(id, dbId);

            //CleanUp

            await DeleteCharacter(id);
        }
        #endregion

        #region Profiles
        [Fact]
        [Trait("CharacterProfiles", "")]

        public async void CanGetProfiles()
        {
            await DeleteUserCharacters();

            var db = new CharacterDB(DBFactory.GetContext());

            int num = 5;

            List<int> ids = new();
            for (int i = 0; i < num; i++)
            {
                ids.Add((await db.CreateBlankCharacter(userID)).Value);
            }

            var result = await db.GetProfiles(userID);
            Assert.True(result.IsSuccess);

            var profiles = result.Value;

            Assert.Equal(num, profiles.Count);

            bool allIdsPresent = ids.All(id => profiles.Any(profile => profile.CharacterID == id));

            Assert.True(allIdsPresent, "Not all CharacterIDs are present in the profiles.");
        }
        #endregion

        #region Abilities
        [Fact]
        [Trait("Abilities", "")]
        public async void CanCreateCharacterAbility()
        {
            var db = new CharacterDB(DBFactory.GetContext());
            var characterId = (await db.CreateBlankCharacter(userID)).Value;

            // Assuming an enumeration or similar exists for AbilityType
            var result = await db.CreateAbility(characterId, AbilityType.Spell);
            Assert.True(result.IsSuccess);

            await DeleteCharacter(characterId);
        }

        [Fact]
        [Trait("Abilities", "")]
        public async void CanGetCharacterAbilities()
        {
            var db = new CharacterDB(DBFactory.GetContext());
            var characterId = (await db.CreateBlankCharacter(userID)).Value;

            // Create multiple abilities
            for (int i = 0; i < 3; i++)
            {
                await db.CreateAbility(characterId, AbilityType.Spell);
            }

            var result = await db.GetAbilities(characterId);
            Assert.True(result.IsSuccess);
            Assert.Equal(3, result.Value.Count);

            await DeleteCharacter(characterId);
        }

        [Fact]
        [Trait("Abilities", "")]
        public async void CanModifyCharacterAbility()
        {
            var db = new CharacterDB(DBFactory.GetContext());
            var characterId = (await db.CreateBlankCharacter(userID)).Value;

            var createResult = await db.CreateAbility(characterId, AbilityType.Spell);
            Assert.True(createResult.IsSuccess);

            var ability = createResult.Value;
            ability.Title = "Enhanced Magic";
            ability.Content = "More powerful than before";
            ability.Cost = "50 Mana";
            ability.Memorized = true;

            var saveResult = await db.SaveAbility(ability);
            Assert.True(saveResult.IsSuccess);

            var getResult = await db.GetAbilities(characterId);
            var updatedAbility = getResult.Value.FirstOrDefault(a => a.AbilityID == ability.AbilityID);

            Assert.NotNull(updatedAbility);
            Assert.Equal("Enhanced Magic", updatedAbility.Title);
            Assert.Equal("More powerful than before", updatedAbility.Content);
            Assert.Equal("50 Mana", updatedAbility.Cost);
            Assert.True(updatedAbility.Memorized);

            await DeleteCharacter(characterId);
        }

        [Fact]
        [Trait("Abilities", "")]
        public async void CanDeleteCharacterAbility()
        {
            var db = new CharacterDB(DBFactory.GetContext());
            var characterId = (await db.CreateBlankCharacter(userID)).Value;

            var ability = (await db.CreateAbility(characterId, AbilityType.Spell)).Value;

            var deleteResult = await db.DeleteAbility(ability.AbilityID);
            Assert.True(deleteResult.IsSuccess);

            var abilities = (await db.GetAbilities(characterId)).Value;
            Assert.DoesNotContain(abilities, a => a.AbilityID == ability.AbilityID);

            await DeleteCharacter(characterId);
        }
        #endregion

        #region Journal
        [Fact]
        [Trait("Journal", "")]
        public async void CanCreateJournalEntry()
        {
            var db = new CharacterDB(DBFactory.GetContext());
            var characterId = (await db.CreateBlankCharacter(userID)).Value;

            var result = await db.CreateJournalEntry(characterId, JournalCategory.General);
            Assert.True(result.IsSuccess);

            await DeleteCharacter(characterId);
        }

        [Fact]
        [Trait("Journal", "")]
        public async void CanGetJournalEntries()
        {
            var db = new CharacterDB(DBFactory.GetContext());
            var characterId = (await db.CreateBlankCharacter(userID)).Value;

            // Create multiple journal entries
            for (int i = 0; i < 5; i++)
            {
                await db.CreateJournalEntry(characterId, JournalCategory.General);
            }

            var result = await db.GetJournalEntries(characterId);
            Assert.True(result.IsSuccess);
            Assert.Equal(5, result.Value.Count);

            await DeleteCharacter(characterId);
        }

        [Fact]
        [Trait("Journal", "")]
        public async void CanModifyJournalEntry()
        {
            var db = new CharacterDB(DBFactory.GetContext());
            var characterId = (await db.CreateBlankCharacter(userID)).Value;

            var createResult = await db.CreateJournalEntry(characterId, JournalCategory.General);
            Assert.True(createResult.IsSuccess);

            var journalEntry = createResult.Value;
            journalEntry.Title = "Updated Title";
            journalEntry.Content = "Updated content";

            var saveResult = await db.SaveJournalEntry(journalEntry);
            Assert.True(saveResult.IsSuccess);

            var getResult = await db.GetJournalEntries(characterId);
            var updatedJournalEntry = getResult.Value.FirstOrDefault(e => e.JournalID == journalEntry.JournalID);

            Assert.NotNull(updatedJournalEntry);
            Assert.Equal("Updated Title", updatedJournalEntry.Title);
            Assert.Equal("Updated content", updatedJournalEntry.Content);
            Assert.Equal(JournalCategory.General, updatedJournalEntry.Category);

            await DeleteCharacter(characterId);
        }

        [Fact]
        [Trait("Journal", "")]
        public async void CanDeleteJournalEntry()
        {
            var db = new CharacterDB(DBFactory.GetContext());
            var characterId = (await db.CreateBlankCharacter(userID)).Value;

            var journalEntry = (await db.CreateJournalEntry(characterId, 0)).Value;

            var deleteResult = await db.DeleteJournalEntry(journalEntry.JournalID);
            Assert.True(deleteResult.IsSuccess);

            var entries = (await db.GetJournalEntries(characterId)).Value;
            Assert.DoesNotContain(entries, e => e.JournalID == journalEntry.JournalID);

            await DeleteCharacter(characterId);
        }
        #endregion

        #region Items

        [Fact]
        [Trait("Inventory", "Item")]
        public async void CanCreateItem()
        {
            var db = new CharacterDB(DBFactory.GetContext());
            var characterid = (await db.CreateBlankCharacter(userID)).Value;

            var result = await db.CreateNewItem(characterid);
            Assert.True(result.IsSuccess);

            await DeleteCharacter(characterid);
        }

        [Fact]
        [Trait("Inventory", "Item")]

        public async void CanGetItems()
        {
            var db = new CharacterDB(DBFactory.GetContext());
            var characterid = (await db.CreateBlankCharacter(userID)).Value;

            List<int> ids = new();
            for (int i = 0; i < 5; i++)
            {
                ids.Add((await db.CreateNewItem(characterid)).Value.ItemID);
            }

            var result = await db.GetItems(characterid);
            Assert.True(result.IsSuccess);

            Assert.Equal(5, result.Value.Count);

            bool allIdsPresent = ids.All(id => result.Value.Any(profile => profile.ItemID == id));

            await DeleteCharacter(characterid);
        }

        [Fact]
        [Trait("Inventory", "Item")]

        public async void CanModifyItem()
        {
            var db = new CharacterDB(DBFactory.GetContext());
            var characterid = (await db.CreateBlankCharacter(userID)).Value;

            var result = await db.CreateNewItem(characterid);
            Assert.True(result.IsSuccess);

            var item = result.Value;
            item.Name = "Test Item";
            item.Description = "Test Description";
            item.Weight = 2.5m;
            item.Quantity = 25;

            var saveresult = await db.SaveItem(item);
            Assert.True(saveresult.IsSuccess);

            var items = (await db.GetItems(characterid)).Value;

            var item2 = items.Find(x => x.ItemID == item.ItemID);
            Assert.NotNull(item2);

            Assert.Equal(item.Name, item2.Name);
            Assert.Equal(item.Description, item2.Description);
            Assert.Equal(item.Weight, item2.Weight);
            Assert.Equal(item.Quantity, item2.Quantity);

            await DeleteCharacter(characterid);
        }

        [Fact]
        [Trait("Inventory", "Item")]

        public async void CanDeleteItem()
        {
            var db = new CharacterDB(DBFactory.GetContext());
            var characterid = (await db.CreateBlankCharacter(userID)).Value;

            var item = (await db.CreateNewItem(characterid)).Value;

            var result = await db.DeleteItem(item.ItemID);
            Assert.True(result.IsSuccess);

            var items = (await db.GetItems(characterid)).Value;

            Assert.DoesNotContain(items, x => x.ItemID == item.ItemID);

            await DeleteCharacter(characterid);
        }
        #endregion

        #region Weapons

        [Fact]
        [Trait("Inventory", "Weapon")]
        public async void CanCreateWeapon()
        {
            var db = new CharacterDB(DBFactory.GetContext());
            var characterid = (await db.CreateBlankCharacter(userID)).Value;

            var result = await db.CreateNewWeapon(characterid);
            Assert.True(result.IsSuccess);

            await DeleteCharacter(characterid);
        }

        [Fact]
        [Trait("Inventory", "Weapon")]
        public async void CanGetWeapons()
        {
            var db = new CharacterDB(DBFactory.GetContext());
            var characterid = (await db.CreateBlankCharacter(userID)).Value;

            List<int> ids = new();
            for (int i = 0; i < 5; i++)
            {
                ids.Add((await db.CreateNewWeapon(characterid)).Value.ItemID);
            }

            var result = await db.GetWeapons(characterid);
            Assert.True(result.IsSuccess);

            Assert.Equal(5, result.Value.Count);

            bool allIdsPresent = ids.All(id => result.Value.Any(weapon => weapon.ItemID == id));

            await DeleteCharacter(characterid);
        }

        [Fact]
        [Trait("Inventory", "Weapon")]
        public async void CanModifyWeapon()
        {
            var db = new CharacterDB(DBFactory.GetContext());
            var characterid = (await db.CreateBlankCharacter(userID)).Value;

            var result = await db.CreateNewWeapon(characterid);
            Assert.True(result.IsSuccess);

            var weapon = result.Value;
            weapon.Name = "Test Weapon";
            weapon.Description = "Test Description";
            weapon.Weight = 3.5m;
            weapon.Quantity = 1;
            weapon.Damage = "1d8";
            weapon.Accuracy = 10;

            var saveresult = await db.SaveWeapon(weapon);
            Assert.True(saveresult.IsSuccess);

            var weapons = (await db.GetWeapons(characterid)).Value;

            var weapon2 = weapons.Find(x => x.ItemID == weapon.ItemID);
            Assert.NotNull(weapon2);

            Assert.Equal(weapon.Name, weapon2.Name);
            Assert.Equal(weapon.Description, weapon2.Description);
            Assert.Equal(weapon.Weight, weapon2.Weight);
            Assert.Equal(weapon.Quantity, weapon2.Quantity);
            Assert.Equal(weapon.Damage, weapon2.Damage);
            Assert.Equal(weapon.Accuracy, weapon2.Accuracy);

            await DeleteCharacter(characterid);
        }

        [Fact]
        [Trait("Inventory", "Weapon")]
        public async void CanDeleteWeapon()
        {
            var db = new CharacterDB(DBFactory.GetContext());
            var characterid = (await db.CreateBlankCharacter(userID)).Value;

            var weapon = (await db.CreateNewWeapon(characterid)).Value;

            var result = await db.DeleteWeapon(weapon.ItemID);
            Assert.True(result.IsSuccess);

            var weapons = (await db.GetWeapons(characterid)).Value;

            Assert.DoesNotContain(weapons, x => x.ItemID == weapon.ItemID);

            await DeleteCharacter(characterid);
        }
        #endregion

        #region Armor
        [Fact]
        [Trait("Inventory", "Armor")]
        public async void CanCreateArmor()
        {
            var db = new CharacterDB(DBFactory.GetContext());
            var characterid = (await db.CreateBlankCharacter(userID)).Value;

            var result = await db.CreateNewArmor(characterid);
            Assert.True(result.IsSuccess);

            await DeleteCharacter(characterid);
        }

        [Fact]
        [Trait("Inventory", "Armor")]
        public async void CanGetArmor()
        {
            var db = new CharacterDB(DBFactory.GetContext());
            var characterid = (await db.CreateBlankCharacter(userID)).Value;

            List<int> ids = new();
            for (int i = 0; i < 5; i++)
            {
                ids.Add((await db.CreateNewArmor(characterid)).Value.ItemID);
            }

            var result = await db.GetArmor(characterid);
            Assert.True(result.IsSuccess);

            Assert.Equal(5, result.Value.Count);

            bool allIdsPresent = ids.All(id => result.Value.Any(armor => armor.ItemID == id));

            await DeleteCharacter(characterid);
        }

        [Fact]
        [Trait("Inventory", "Armor")]
        public async void CanModifyArmor()
        {
            var db = new CharacterDB(DBFactory.GetContext());
            var characterid = (await db.CreateBlankCharacter(userID)).Value;

            var result = await db.CreateNewArmor(characterid);
            Assert.True(result.IsSuccess);

            var armor = result.Value;
            armor.Name = "Test Armor";
            armor.Description = "Test Description";
            armor.Weight = 15.0m;
            armor.Hindrance = 2;
            armor.Equipped = true;
            armor.Layer = ArmorLayer.Heavy;
            armor.Slot = ArmorSlot.Back | ArmorSlot.Chest;
            armor.Bludgeoning = 5;
            armor.Piercing = 5;
            armor.Slashing = 5;

            var saveresult = await db.SaveArmor(armor);
            Assert.True(saveresult.IsSuccess);

            var armors = (await db.GetArmor(characterid)).Value;

            var armor2 = armors.Find(x => x.ItemID == armor.ItemID);
            Assert.NotNull(armor2);

            Assert.Equal(armor.Name, armor2.Name);
            Assert.Equal(armor.Description, armor2.Description);
            Assert.Equal(armor.Weight, armor2.Weight);
            Assert.Equal(armor.Hindrance, armor2.Hindrance);
            Assert.Equal(armor.Equipped, armor2.Equipped);
            Assert.Equal(armor.Layer, armor2.Layer);
            Assert.Equal(armor.Slot, armor2.Slot);
            Assert.Equal(armor.Bludgeoning, armor2.Bludgeoning);
            Assert.Equal(armor.Piercing, armor2.Piercing);
            Assert.Equal(armor.Slashing, armor2.Slashing);

            await DeleteCharacter(characterid);
        }

        [Fact]
        [Trait("Inventory", "Armor")]
        public async void CanDeleteArmor()
        {
            var db = new CharacterDB(DBFactory.GetContext());
            var characterid = (await db.CreateBlankCharacter(userID)).Value;

            var armor = (await db.CreateNewArmor(characterid)).Value;

            var result = await db.DeleteArmor(armor.ItemID);
            Assert.True(result.IsSuccess);

            var armors = (await db.GetArmor(characterid)).Value;

            Assert.DoesNotContain(armors, x => x.ItemID == armor.ItemID);

            await DeleteCharacter(characterid);
        }

        #endregion
    }
}