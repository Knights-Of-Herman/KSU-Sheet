using KnightsOfHerman.Backend.Common.Database.Types.Character;
using KnightsOfHerman.Backend.Common.Sanctum.Character.Types;
using KnightsOfHerman.Backend.Database.Azure.Character;
using KnightsOfHerman.Backend.Database.Testing.UnitTesting;
using KnightsOfHerman.Common.Sanctum.Abstract.Character.Abilities;
using KnightsOfHerman.Common.Sanctum.Abstract.Character.Items;
using KnightsOfHerman.Common.Sanctum.Abstract.Character.Notes;
using KnightsOfHerman.Common.Sanctum.Abstract.Character.Stats;
using KnightsOfHerman.Common.Sanctum.Communication.DTO;
using Microsoft.AspNetCore.Razor.Hosting;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tynamix.ObjectFiller;

namespace KnightsOfHerman.XUnitTesting.Helpers
{
    /// <summary>
    /// Class used to simplify the creation and deletion of test characters.
    /// CreateAsync is meant to be wrapped in a using statement
    /// </summary>
    public class TestCharacter : IAsyncDisposable
    {
        public int CharacterID { get; }

        private TestCharacter(int id)
        {
            CharacterID = id;
        }

        public Dictionary<int, CharacterWeaponDBO> Weapons { get; set; }

        public Dictionary<int, CharacterItemDBO> Items { get; set; }

        public Dictionary<int, CharacterArmorDBO> Armor { get; set; }

        public Dictionary<int, CharacterJournalDBO> Journal { get; set; }
        public Dictionary<int, CharacterAbilityDBO> Abilities { get; set; }

        public CharacterBioDBO Bio { get; set; }

        public Dictionary<CharacterStats, CharacterStatDBO> Stats { get; set; }
        public Dictionary<CharacterResources, CharacterResourceDBO> Resources { get; set; }

        /// <summary>
        /// Creates a blank character 
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static async Task<TestCharacter> DBCreateBlankAsync()
        {
            var db = new CharacterDB(DBFactory.GetContext());
            var result = await db.CreateBlankCharacter(1);
            if (result.IsSuccess)
            {
                var id = result.Value;
                var tc =  new TestCharacter(id);

                //Get Stats
                var stats = (await db.GetStats(id)).Value;
                tc.Stats = stats.ToDictionary(x => x.StatID, x => x);
                //Get Resources
                var resources = (await db.GetResources(id)).Value;
                tc.Resources = resources.ToDictionary(x => x.ResourceID, x => x);
                //Get Bio
                var bio = (await db.GetBio(id)).Value;
                tc.Bio = bio;

                return tc;

            } else
            {
                throw new Exception("Couldn't Create Character");
            }

        }

        public static async Task<TestCharacter> DBCreateComplexAsync()
        {
            var db = new CharacterDB(DBFactory.GetContext());
            var result = await db.CreateBlankCharacter(1);
            if (result.IsSuccess)
            {
                var id = result.Value;
                var tc = new TestCharacter(id);

                var random = new Random();

                foreach (CharacterResources stat in Enum.GetValues(typeof(CharacterResources)))
                {
                    var res = new CharacterResourceDBO();
                    res.ResourceID = stat;
                    res.Attribute = ((short)random.Next(1, 20));
                    res.Modifier = ((short)random.Next(1, 20));
                    res.OverrideMaxValue = ((short)random.Next(1, 20));
                    res.CurrentValue = ((short)random.Next(1, 20));
                    res.DoOverrideMax = random.Next(2) == 1;
                    await db.SaveResource(id, res);
                }

                foreach (CharacterStats stat in Enum.GetValues(typeof(CharacterStats)))
                {
                    var s = new CharacterStatDBO();
                    s.StatID = stat;
                    s.Base = ((short)random.Next(1, 20));
                    s.CustomMod = ((short)random.Next(1, 20));
                    s.OverrideValue = ((short)random.Next(1, 20));
                    s.DoOverride = random.Next(2) == 1;
                    await db.SaveStat(id, s);
                }

                var randItems = random.Next(1, 10);
                var randWeapons = random.Next(1, 10);
                var randArmor = random.Next(1, 10);
                var randJournal = random.Next(1, 10);
                var randAbilities = random.Next(1, 10);

                for (int i = 0; i < randItems; i++)
                {
                    var item = (await db.CreateNewItem(id)).Value;
                    item.Weight = random.Next(100);
                    item.Quantity = (short)random.Next(100);
                    item.Name = PeanutButter.RandomGenerators.RandomValueGen.GetRandomString(30, 200);
                    item.Description = PeanutButter.RandomGenerators.RandomValueGen.GetRandomString(30, 400);
                    var sr = await db.SaveItem(item);
                }

                for (int i = 0; i < randWeapons; i++)
                {
                    var item = (await db.CreateNewWeapon(id)).Value;
                    item.Weight = random.Next(100);
                    item.Quantity = (short)random.Next(100);
                    item.Name = PeanutButter.RandomGenerators.RandomValueGen.GetRandomString(30, 200);
                    item.Description = PeanutButter.RandomGenerators.RandomValueGen.GetRandomString(30, 400);
                    item.Accuracy = (short)random.Next(100);
                    item.Damage = PeanutButter.RandomGenerators.RandomValueGen.GetRandomString(5,20);
                    var sr = await db.SaveWeapon(item);
                }

                for (int i = 0; i < randArmor; i++)
                {
                    var item = (await db.CreateNewArmor(id)).Value;
                    item.Weight = random.Next(100);
                    item.Name = PeanutButter.RandomGenerators.RandomValueGen.GetRandomString(30, 200);
                    item.Description = PeanutButter.RandomGenerators.RandomValueGen.GetRandomString(30, 400);
                    item.Bludgeoning = (byte)random.Next(100);
                    item.Piercing = (byte)random.Next(100);
                    item.Slashing = (byte)random.Next(100);
                    item.Slot = RandomEnums.GetRandomArmorSlot();
                    item.Layer = RandomEnums.GetRandomArmorLayer();
                    var sr = await db.SaveArmor(item);
                }

                for(int i = 0; i < randAbilities; i++)
                {
                    var ability = (await db.CreateAbility(id, RandomEnums.GetRandomAbilityType())).Value;
                    ability.Title = PeanutButter.RandomGenerators.RandomValueGen.GetRandomString(30, 200);
                    ability.Content = PeanutButter.RandomGenerators.RandomValueGen.GetRandomString(30, 400);
                    ability.Cost = PeanutButter.RandomGenerators.RandomValueGen.GetRandomString(30, 200);
                    ability.Memorized = random.Next(2) == 1;
                    await db.SaveAbility(ability);
                } 

                for(int i = 0; i < randJournal; i++) {
                    var journal = (await db.CreateJournalEntry(id, RandomEnums.GetRandomJournalCategory())).Value;
                    journal.Title = PeanutButter.RandomGenerators.RandomValueGen.GetRandomString(30, 200);
                    journal.Content = PeanutButter.RandomGenerators.RandomValueGen.GetRandomString(1000, 2500);
                    await db.SaveJournalEntry(journal);
                }

                var languagesSB = new StringBuilder();
                var randLanguages = random.Next(6) + 1;
                for(int i = 0; i < randLanguages; i++)
                {
                    languagesSB.Append(PeanutButter.RandomGenerators.RandomValueGen.GetRandomLanguageName());
                    languagesSB.Append(" ");
                }
                languagesSB.Length--;

                var genbio = (await db.GetBio(id)).Value;
                genbio.Name = PeanutButter.RandomGenerators.RandomValueGen.GetRandomName();
                genbio.Destiny = (byte)(random.Next(4) + 1);
                genbio.Languages = languagesSB.ToString();
                genbio.Race = PeanutButter.RandomGenerators.RandomValueGen.GetRandomLastName();
                genbio.Background = PeanutButter.RandomGenerators.RandomValueGen.GetRandomCountry();
                genbio.Conflict = (byte)random.Next(100);
                await db.SaveBio(id, genbio);

                //Get Stats
                var stats = (await db.GetStats(id)).Value;
                tc.Stats = stats.ToDictionary(x => x.StatID, x => x);

                //Get Resources
                var resources = (await db.GetResources(id)).Value;
                tc.Resources = resources.ToDictionary(x => x.ResourceID, x => x);
                //Get Bio
                var bio = (await db.GetBio(id)).Value;
                tc.Bio = bio;

                tc.Armor = (await db.GetArmor(id)).Value.ToDictionary(x => x.ItemID, x => x);
                tc.Weapons = (await db.GetWeapons(id)).Value.ToDictionary(x => x.ItemID, x => x);
                tc.Items = (await db.GetItems(id)).Value.ToDictionary(x => x.ItemID, x => x);
                tc.Journal = (await db.GetJournalEntries(id)).Value.ToDictionary(x => x.JournalID, x => x);
                tc.Abilities = (await db.GetAbilities(id)).Value.ToDictionary(x => x.AbilityID, x => x);


                return tc;

            }
            else
            {
                throw new Exception("Couldn't Create Character");
            }

        }

        public static CharacterSO CreateFakeCharacterSO()
        {
            CharacterSO so = new(1);
            var random = new Random();

            foreach (CharacterResources stat in Enum.GetValues(typeof(CharacterResources)))
            {
                var res = new CharacterResourceDBO();
                res.ResourceID = stat;
                res.Attribute = 5;
                res.Modifier = 6;
                res.OverrideMaxValue = 3;
                res.CurrentValue = 2;
                res.DoOverrideMax = false;
                so.Resources[stat] = new CharacterResourceSO(res);
            }

            foreach (CharacterStats stat in Enum.GetValues(typeof(CharacterStats)))
            {
                var s = new CharacterStatDBO();
                s.StatID = stat;
                s.Base = 4;
                s.CustomMod = 1;
                s.OverrideValue = 2;
                s.DoOverride = false;
                so.Stats[stat] = new CharacterStatSO(s);
            }

            for(int i = 1; i <= 5; i++)
            {
                var j = new CharacterJournalDBO();
                j.JournalID = i;
                j.Title = $"Title {i}";
                j.Content = $"Content {i}";
                j.CreateDate = DateTime.Now;
                j.Category = JournalCategory.General;
                so.Journal[i] = new CharacterJournalSO(j);
            }

            for (int i = 1; i <= 5; i++)
            {
                var j = new CharacterAbilityDBO();
                j.AbilityID = i;
                j.Title = $"Ability {i}";
                j.Content = $"Content {i}";
                j.AbilityType = AbilityType.Talent;
                so.Abilities[i] = new CharacterAbilitySO(j);
            }

            for (int i = 1; i <= 5; i++)
            {
                var j = new CharacterWeaponDBO();
                j.ItemID = i;
                j.Name = $"Weapon {i}";
                j.Description = $"Content {i}";
                j.Quantity = 1;
                j.Weight = 1.0m;
                j.Damage = "1d4 + 2 Slashing";
                so.Weapons[i] = new CharacterWeaponSO(j);
            }

            for (int i = 1; i <= 5; i++)
            {
                var j = new CharacterItemDBO();
                j.ItemID = i;
                j.Name = $"Item {i}";
                j.Description = $"Content {i}";
                j.Quantity = 1;
                j.Weight = 1.0m;
                so.Items[i] = new CharacterItemSO(j);
            }

            for (int i = 1; i <= 5; i++)
            {
                var j = new CharacterArmorDBO();
                j.ItemID = i;
                j.Name = $"Weapon {i}";
                j.Description = $"Content {i}";
                j.Weight = 1.0m;
                j.Slot = ArmorSlot.Head;
                j.Layer = ArmorLayer.Heavy;
                j.Slashing = 5;
                j.Piercing = 5;
                j.Bludgeoning = 5;
                so.Armor[i] = new CharacterArmorSO(j);
            }

            so.Bio.Name = "John Doe";
            so.Bio.Background = "Adventurer";
            so.Bio.Conflict = 23;
            so.Bio.UnspentXP = 200;
            so.Bio.TotalXP = 2000;
            so.Bio.Languages = "German, American";
            so.Bio.Race = "Human";
            so.Bio.Destiny = 2;

            return so;
        }

        //Add one where it creates a more compelx character

        //Maybe have it load all the character dtails here as well

        public async ValueTask DisposeAsync()
        {
            var db = new CharacterDB(DBFactory.GetContext());
            await db.DeleteCharacter(1, CharacterID);
        }
    }
}
