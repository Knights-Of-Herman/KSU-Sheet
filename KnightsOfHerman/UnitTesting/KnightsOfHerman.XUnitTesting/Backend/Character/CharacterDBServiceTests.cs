using KnightsOfHerman.Backend.Common.Database.Types.Character;
using KnightsOfHerman.Backend.Common.Sanctum.Character.Services;
using KnightsOfHerman.Backend.Common.Sanctum.Character.Types;
using KnightsOfHerman.Backend.Database.Azure.Character;
using KnightsOfHerman.Backend.Database.Testing.UnitTesting;
using KnightsOfHerman.Common.Sanctum.Abstract.Character;
using KnightsOfHerman.Common.Sanctum.Abstract.Character.Stats;
using KnightsOfHerman.XUnitTesting.Helpers;
using PeanutButter.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Tynamix.ObjectFiller;

namespace KnightsOfHerman.XUnitTesting.Backend.Character
{

    public class CharacterDBServiceTests
    {
        #region Helper Functions
        public static CharacterDBService GetService()
        {
            return new CharacterDBService(GetDB());
        }

        public static CharacterDB GetDB()
        {
            return new CharacterDB(DBFactory.GetContext());
        }

        public static void CompareStats(Dictionary<CharacterStats,CharacterStatSO> so, Dictionary<CharacterStats, CharacterStatDBO> dbo)
        {
            Assert.NotNull(so);
            Assert.NotNull(dbo);
            Assert.Equal(so.Count, dbo.Count);

            foreach (var kvp in so)
            {
                CharacterStats key = kvp.Key;
                ICharacterStat stat1 = kvp.Value;
                CharacterAssert.StatsAreSame(stat1, dbo[key]);
            }
        }

        public static void CompareResources(Dictionary<CharacterResources, CharacterResourceSO> so, Dictionary<CharacterResources, CharacterResourceDBO> dbo)
        {
            Assert.NotNull(so);
            Assert.NotNull(dbo);
            Assert.Equal(so.Count, dbo.Count);

            foreach (var kvp in so)
            {
                CharacterResources key = kvp.Key;
                ICharacterResource stat1 = kvp.Value;
                CharacterAssert.ResourcesAreSame(stat1, dbo[key]);
            }
        }

        #endregion

        [Fact]
        [Trait("Character", "Management")]

        public async void CharacterCountShouldBeCorrect()
        {
            await using (var tc1 = await TestCharacter.DBCreateBlankAsync())
            {
                await using (var tc2 = await TestCharacter.DBCreateBlankAsync())
                {
                    await using (var tc3 = await TestCharacter.DBCreateBlankAsync())
                    {
                        await using (var tc4 = await TestCharacter.DBCreateBlankAsync())
                        {
                            await using (var tc5 = await TestCharacter.DBCreateBlankAsync())
                            {

                                //Create 5 characters
                                var service = GetService();
                                var result = await service.GetCharacterCount(1);
                                Assert.True(result.IsSuccess);
                                Assert.Equal(5, result.Value);

                            }
                        }
                    }
                }
            }
        }


        [Fact]
        [Trait("Character", "Management")]

        public async void CanGetProfiles()
        {
            await using (var tc1 = await TestCharacter.DBCreateBlankAsync())
            {
                await using (var tc2 = await TestCharacter.DBCreateBlankAsync())
                {
                    await using (var tc3 = await TestCharacter.DBCreateBlankAsync())
                    {
                        await using (var tc4 = await TestCharacter.DBCreateBlankAsync())
                        {
                            await using (var tc5 = await TestCharacter.DBCreateBlankAsync())
                            {
                                //Create 5 characters
                                var service = GetService();
                                var result = await service.GetCharacterProfiles(1);
                                Assert.True(result.IsSuccess);
                            }
                        }
                    }
                }
            }
        }

        [Fact]
        [Trait("Character", "Management")]
        public async void OwnerShouldBeCorrect()
        {
            await using (var tc = await TestCharacter.DBCreateBlankAsync())
            {
                var service = GetService();
                var database = GetDB();

                var character = (await service.GetCharacterFromDatabase(tc.CharacterID)).Value;

                Assert.True(character.Permissions.ContainsKey(1));

                Assert.True(character.Permissions[1] == CharacterAccess.Owner);
            }
        }


        [Fact]
        [Trait("Character", "Management")]
        public async void CanGetBlankCharacterSO()
        {
            await using(var tc = await TestCharacter.DBCreateBlankAsync())
            {
                var service = GetService();
                var database = GetDB();

                var charSOResult = await service.GetCharacterFromDatabase(tc.CharacterID);


                Assert.True(charSOResult.IsSuccess);
                Assert.NotNull(charSOResult.Value);
                var character = charSOResult.Value;

                CompareResources(character.Resources, tc.Resources);
                CompareStats(character.Stats, tc.Stats);
                CharacterAssert.BiosAreSame(tc.Bio, character.Bio);
                
            }
        }

        [Trait("Character", "Weapon")]

        [Fact]
        public async void CanCreateWeapon()
        {
            await using (var tc = await TestCharacter.DBCreateBlankAsync())
            {
                var service = GetService();

                var weaponResult = await service.CreateWeapon(tc.CharacterID);

                Assert.True(weaponResult.IsSuccess);
                Assert.NotNull(weaponResult.Value);

                var newWeapon = weaponResult.Value;

                var character = (await service.GetCharacterFromDatabase(tc.CharacterID)).Value;

                Assert.True(character.Weapons.ContainsKey(newWeapon.ItemID));

                CharacterAssert.WeaponsAreSame(newWeapon, character.Weapons[newWeapon.ItemID]);

            }
        }

        [Trait("Character", "Item")]

        [Fact]
        public async void CanCreateItem()
        {
            await using (var tc = await TestCharacter.DBCreateBlankAsync())
            {
                var service = GetService();

                var result = await service.CreateItem(tc.CharacterID);

                Assert.True(result.IsSuccess);
                Assert.NotNull(result.Value);

                var newthing = result.Value;

                var character = (await service.GetCharacterFromDatabase(tc.CharacterID)).Value;

                Assert.True(character.Items.ContainsKey(newthing.ItemID));

                CharacterAssert.ItemsAreSame(newthing, character.Items[newthing.ItemID]);
            }
        }

        [Trait("Character", "Armor")]

        [Fact]
        public async void CanCreateArmor()
        {
            await using (var tc = await TestCharacter.DBCreateBlankAsync())
            {
                var service = GetService();

                var result = await service.CreateArmor(tc.CharacterID);

                Assert.True(result.IsSuccess);
                Assert.NotNull(result.Value);

                var newthing = result.Value;

                var character = (await service.GetCharacterFromDatabase(tc.CharacterID)).Value;

                Assert.True(character.Armor.ContainsKey(newthing.ItemID));

                CharacterAssert.ArmorsAreSame(newthing, character.Armor[newthing.ItemID]);
            }
        }

        [Trait("Character", "Ability")]

        [Fact]
        public async void CanCreateAbility()
        {
            await using (var tc = await TestCharacter.DBCreateBlankAsync())
            {
                var service = GetService();

                var result = await service.CreateAbility(tc.CharacterID, RandomEnums.GetRandomAbilityType());

                Assert.True(result.IsSuccess);
                Assert.NotNull(result.Value);

                var newthing = result.Value;

                var character = (await service.GetCharacterFromDatabase(tc.CharacterID)).Value;

                Assert.True(character.Abilities.ContainsKey(newthing.AbilityID));

                CharacterAssert.AbilitiesAreSame(newthing, character.Abilities[newthing.AbilityID]);
            }
        }

        [Trait("Character", "Journal")]

        [Fact]
        public async void CanCreateJournal()
        {
            await using (var tc = await TestCharacter.DBCreateBlankAsync())
            {
                var service = GetService();

                var result = await service.CreateJournal(tc.CharacterID, RandomEnums.GetRandomJournalCategory());

                Assert.True(result.IsSuccess);
                Assert.NotNull(result.Value);

                var newthing = result.Value;

                var character = (await service.GetCharacterFromDatabase(tc.CharacterID)).Value;

                Assert.True(character.Journal.ContainsKey(newthing.JournalID));

                CharacterAssert.JournalsAreSame(newthing, character.Journal[newthing.JournalID]);
            }
        }

        [Fact]
        [Trait("Character", "Management")]
        public async void CanSaveSingleCharacterSOChange()
        {
            await using (var tc = await TestCharacter.DBCreateBlankAsync())
            {

                var service = GetService();

                var character = (await service.GetCharacterFromDatabase(tc.CharacterID)).Value;

                character.Stats[CharacterStats.Intelligence].Base = 3;

                Assert.True(character.Stats[CharacterStats.Intelligence].Modified);

                var saveResult = await service.SaveCharacterToDatabase(character);
                Assert.True(saveResult.IsSuccess);


                var character2 = (await service.GetCharacterFromDatabase(tc.CharacterID)).Value;
                Assert.Equal(3, character2.Stats[CharacterStats.Intelligence].Base);


            }
        }

        [Fact]
        [Trait("Character", "Management")]
        public async void CanSaveManyCharacterSOChange()
        {
            await using (var tc = await TestCharacter.DBCreateComplexAsync())
            {
                var service = GetService();
                var character = (await service.GetCharacterFromDatabase(tc.CharacterID)).Value;

                character.Bio.Name = "John Herman";
                character.Bio.Languages = "Vadeshi and Clovtain";

                character.Armor.First().Value.Name = "Big Ole Helmet";
                character.Journal.First().Value.Content = "Some cool story about Joe";

                var result = await service.SaveCharacterToDatabase(character);
                Assert.True(result.IsSuccess);


            }

        }

    }
}
