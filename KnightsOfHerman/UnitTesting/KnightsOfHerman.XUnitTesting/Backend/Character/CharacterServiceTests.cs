using KnightsOfHerman.Backend.Common.Database.Interfaces.Sanctum.Character;
using KnightsOfHerman.Backend.Common.Sanctum.Character.Interfaces;
using KnightsOfHerman.Backend.Common.Sanctum.Character.Services;
using KnightsOfHerman.Backend.Common.Sanctum.Character.Types;
using KnightsOfHerman.Backend.Database.Azure.Character;
using KnightsOfHerman.Backend.Database.Testing.UnitTesting;
using KnightsOfHerman.Backend.Server.Memory;
using KnightsOfHerman.Common.Abstract;
using KnightsOfHerman.Common.Abstract.Modification;
using KnightsOfHerman.Common.Types;
using KnightsOfHerman.XUnitTesting.Helpers;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using PeanutButter.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnightsOfHerman.XUnitTesting.Backend.Character
{
    public class CharacterServiceTests
    {
        /* These tests are outdated.
        ICharacterCache FakeCache = new FakeCache();

        public static CharacterDBService GetService()
        {
            return new CharacterDBService(GetDB());
        }
        public static CharacterDB GetDB()
        {
            return new CharacterDB(DBFactory.GetContext());
        }

        [Fact]
        [Trait("Character", "Delete")]
        public async void NonOwnerShouldNotDelete()
        {
            var fake = new FakeNotify();
            var service = new CharacterService(FakeCache, GetService(), fake, new CharacterLockService());

            await using (var character = await TestCharacter.DBCreateBlankAsync())
            {
                var result = await service.DeleteCharacter(2, character.CharacterID);
                Assert.False(fake.WasNotified);
                Assert.False(result.IsSuccess);
            }
        }

        [Fact]
        [Trait("Character", "Modify")]
        public async void OwnerShouldBeAbleToModify()
        {
            var fake = new FakeNotify();
            var service = new CharacterService(FakeCache, GetService(), fake, new CharacterLockService());

            await using (var tc = await TestCharacter.DBCreateComplexAsync())
            {
                var id = tc.Journal.First().Value.JournalID;
                var modifyResult = await service.ModifyCharacter(1, tc.CharacterID,
                    new TrackedModificationEventArgs($"Bio.Name", "Joe Mama", EditActions.Edit));
                Assert.True(modifyResult.IsSuccess);
                var newResult = await service.GetCharacterDTO(1, tc.CharacterID);

                Assert.Equal("Joe Mama", newResult.Value.Bio.Name);
            }
        }

        [Fact]
        [Trait("Character", "Modify")]
        public async void OwnerShouldBeAbleToModifyDictionary()
        {
            var fake = new FakeNotify();
            var service = new CharacterService(FakeCache, GetService(), fake, new CharacterLockService());

            await using (var tc = await TestCharacter.DBCreateComplexAsync())
            {
                var id = tc.Journal.First().Value.JournalID;
                var modifyResult = await service.ModifyCharacter(1, tc.CharacterID,
                    new TrackedModificationEventArgs($"Journal.{id}.Title", "Joe Mama", EditActions.Edit));
                Assert.True(modifyResult.IsSuccess);

                var newResult = await service.GetCharacterDTO(1, tc.CharacterID);

                Assert.Equal("Joe Mama", newResult.Value.Journal[id].Title);
            }
        }

        [Fact]
        [Trait("Character", "DictionaryRemove")]
        public async void ShouldBeAbleToRemoveJournalSO()
        {
            var fake = new FakeNotify();
            var service = new CharacterService(FakeCache, GetService(), fake, new CharacterLockService());
            await using (var tc = await TestCharacter.DBCreateComplexAsync())
            {
                var id = tc.Journal.First().Value.JournalID;
                var modifyResult = await service.ModifyCharacter(1,tc.CharacterID,
                    new TrackedModificationEventArgs($"Journal.{id}",null,EditActions.Remove));
                Assert.True(modifyResult.IsSuccess);
            }
        }

        [Fact]
        [Trait("Character", "DictionaryRemove")]
        public async void ShouldBeAbleToRemoveAbilitySO()
        {
            var fake = new FakeNotify();
            var service = new CharacterService(FakeCache, GetService(), fake, new CharacterLockService());
            await using (var tc = await TestCharacter.DBCreateComplexAsync())
            {
                var id = tc.Abilities.First().Value.AbilityID;
                var modifyResult = await service.ModifyCharacter(1, tc.CharacterID,
                    new TrackedModificationEventArgs($"Abilities.{id}", null, EditActions.Remove));
                Assert.True(modifyResult.IsSuccess);
            }
        }

        [Fact]
        [Trait("Character", "DictionaryRemove")]
        public async void ShouldBeAbleToRemoveWeaponSO()
        {
            var fake = new FakeNotify();
            var service = new CharacterService(FakeCache, GetService(), fake, new CharacterLockService());
            await using (var tc = await TestCharacter.DBCreateComplexAsync())
            {
                var id = tc.Weapons.First().Value.ItemID;
                var modifyResult = await service.ModifyCharacter(1, tc.CharacterID,
                    new TrackedModificationEventArgs($"Weapons.{id}", null, EditActions.Remove));
                Assert.True(modifyResult.IsSuccess);
            }
        }

        [Fact]
        [Trait("Character", "DictionaryRemove")]
        public async void ShouldBeAbleToRemoveArmorSO()
        {
            var fake = new FakeNotify();
            var service = new CharacterService(FakeCache, GetService(), fake, new CharacterLockService());
            await using (var tc = await TestCharacter.DBCreateComplexAsync())
            {
                var id = tc.Armor.First().Value.ItemID;
                var modifyResult = await service.ModifyCharacter(1, tc.CharacterID,
                    new TrackedModificationEventArgs($"Armor.{id}", null, EditActions.Remove));
                Assert.True(modifyResult.IsSuccess);
            }
        }

        [Fact]
        [Trait("Character", "DictionaryRemove")]
        public async void ShouldBeAbleToRemoveItemSO()
        {
            var fake = new FakeNotify();
            var service = new CharacterService(FakeCache, GetService(), fake, new CharacterLockService());
            await using (var tc = await TestCharacter.DBCreateComplexAsync())
            {
                var id = tc.Items.First().Value.ItemID;
                var modifyResult = await service.ModifyCharacter(1, tc.CharacterID,
                    new TrackedModificationEventArgs($"Items.{id}", null, EditActions.Remove));
                Assert.True(modifyResult.IsSuccess);
            }
        }
    }

    

    public class FakeNotify : ICharacterNotificationService
    {
        public bool WasNotified { get; set; }

        public event ICharacterNotificationService.CharacterDeletionHandler OnCharacterDeleted;

        public async Task NotifyCharacterDeletion(int id)
        {
            WasNotified = true;
        }

        void ICharacterNotificationService.NotifyCharacterDeletion(int id)
        {
            throw new NotImplementedException();
        }
    }

    public class FakeCache : ICharacterCache
    {
        Dictionary<int, CharacterSO> characters = new();

        public Task EvictCharacter(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<TryResult<CharacterSO>> TryLoadCharacter(int id)
        {
            if (characters.ContainsKey(id))
            {
                return TryResult<CharacterSO>.Success(characters[id]);
            } else
            {
                return TryResult<CharacterSO>.Fail();
            }
        }

        public async Task<TryResult> TrySaveCharacter(CharacterSO character)
        {
            characters[character.CharacterID] = character;
            return TryResult.Success();
        }*/
    }

} 
