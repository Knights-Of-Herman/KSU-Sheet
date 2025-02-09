using KnightsOfHerman.Backend.Common.Database.Interfaces.Sanctum.Character;
using KnightsOfHerman.Backend.Common.Sanctum.Character.Interfaces;
using KnightsOfHerman.Backend.Common.Sanctum.Character.Types;
using KnightsOfHerman.Common.Sanctum.Abstract.Character;
using KnightsOfHerman.Common.Sanctum.Abstract.Character.Abilities;
using KnightsOfHerman.Common.Sanctum.Abstract.Character.Notes;
using KnightsOfHerman.Common.Sanctum.Communication.DTO;
using KnightsOfHerman.Common.Types;

namespace KnightsOfHerman.Backend.Common.Sanctum.Character.Services
{
    /// <summary>
    /// Implemetnation of ICharacterDBService
    /// Wraps the Database to convert from DBO to SO
    /// </summary>
    public class CharacterDBService : ICharacterDBService
    {
        ICharacterDB _database;
        public CharacterDBService(ICharacterDB database)
        {
            _database = database;
        }

        public async Task<TryResult<CharacterAbilitySO>> CreateAbility(int characterID, AbilityType type)
        {
            var result = await _database.CreateAbility(characterID, type);
            if (result.IsSuccess && result.Value != null)
            {
                return TryResult<CharacterAbilitySO>.Success(new CharacterAbilitySO(result.Value));
            }
            else
            {
                return TryResult<CharacterAbilitySO>.Fail(result.ErrorMessage);
            }
        }

        public async Task<TryResult<CharacterArmorSO>> CreateArmor(int characterID)
        {
            var result = await _database.CreateNewArmor(characterID);
            if (result.IsSuccess && result.Value != null)
            {
                return TryResult<CharacterArmorSO>.Success(new CharacterArmorSO(result.Value));
            }
            else
            {
                return TryResult<CharacterArmorSO>.Fail(result.ErrorMessage);
            }
        }

        public async Task<TryResult<int>> CreateCharacter(int userid) => await _database.CreateBlankCharacter(userid);

        public async Task<TryResult<CharacterItemSO>> CreateItem(int characterID)
        {
            var result = await _database.CreateNewItem(characterID);
            if (result.IsSuccess && result.Value != null)
            {
                return TryResult<CharacterItemSO>.Success(new CharacterItemSO(result.Value));
            }
            else
            {
                return TryResult<CharacterItemSO>.Fail(result.ErrorMessage);
            }
        }

        public async Task<TryResult<CharacterJournalSO>> CreateJournal(int characterID, JournalCategory category)
        {
            var result = await _database.CreateJournalEntry(characterID, category);
            if (result.IsSuccess && result.Value != null)
            {
                return TryResult<CharacterJournalSO>.Success(new CharacterJournalSO(result.Value));
            }
            else
            {
                return TryResult<CharacterJournalSO>.Fail(result.ErrorMessage);
            }
        }

        public async Task<TryResult<CharacterWeaponSO>> CreateWeapon(int characterID)
        {
            var result = await _database.CreateNewWeapon(characterID);
            if(result.IsSuccess && result.Value != null)
            {
                return TryResult<CharacterWeaponSO>.Success(new CharacterWeaponSO(result.Value));
            } else
            {
                return TryResult<CharacterWeaponSO>.Fail(result.ErrorMessage);
            }
        }

        public async Task<TryResult> DeleteAbility(int abilityID)
        {
            return await _database.DeleteAbility(abilityID);
        }

        public async Task<TryResult> DeleteArmor(int itemID)
        {
            return await _database.DeleteArmor(itemID);
        }

        public async Task<TryResult> DeleteCharacter(int userid, int characterID)
        {
            return await _database.DeleteCharacter(userid, characterID);
        }

        public async Task<TryResult> DeleteItem(int itemID)
        {
            return await _database.DeleteItem(itemID);
        }

        public async Task<TryResult> DeleteJournal(int journalID)
        {
            return await _database.DeleteJournalEntry(journalID);
        }

        public async Task<TryResult> DeleteWeapon(int itemID)
        {
            return await _database.DeleteWeapon(itemID);
        }

        public async Task<TryResult<int>> GetCharacterCount(int userID) => await _database.GetCharacterCount(userID);

        public async Task<TryResult<CharacterSO>> GetCharacterFromDatabase(int characterID)
        {
            try
            {
                CharacterSO character = new(characterID);

                var bioResult = await _database.GetBio(characterID);
                if (!bioResult.IsSuccess || bioResult.Value == null) return TryResult<CharacterSO>.Fail("Bio Load Failed");

                var statResult = await _database.GetStats(characterID);
                if (!statResult.IsSuccess || statResult.Value == null) return TryResult<CharacterSO>.Fail("Stat Load Failed");

                var resourceResult = await _database.GetResources(characterID);
                if (!resourceResult.IsSuccess || resourceResult.Value == null) return TryResult<CharacterSO>.Fail("Resource Load Failed");

                var itemsResult = await _database.GetItems(characterID);
                if (!itemsResult.IsSuccess || itemsResult.Value == null) return TryResult<CharacterSO>.Fail("Items Load Failed");

                var weaponsResult = await _database.GetWeapons(characterID);
                if (!weaponsResult.IsSuccess || weaponsResult.Value == null) return TryResult<CharacterSO>.Fail("Weapons Load Failed");

                var armorResult = await _database.GetArmor(characterID);
                if (!armorResult.IsSuccess || armorResult.Value == null) return TryResult<CharacterSO>.Fail("Armor Load Failed");

                var journalResult = await _database.GetJournalEntries(characterID);
                if (!journalResult.IsSuccess || journalResult.Value == null) return TryResult<CharacterSO>.Fail("Journal Load Failed");

                var abilitiesResult = await _database.GetAbilities(characterID);
                if (!abilitiesResult.IsSuccess || abilitiesResult.Value == null) return TryResult<CharacterSO>.Fail("Abilities Load Failed");

                var permissionsResult = await _database.GetCharacterPermissions(characterID);
                if (!permissionsResult.IsSuccess || permissionsResult.Value == null) return TryResult<CharacterSO>.Fail("Permissions Load Failed");

                character.Stats = statResult.Value.ToDictionary(
                    s => s.StatID,
                    s => new CharacterStatSO(s));

                character.Resources = resourceResult.Value.ToDictionary(
                    s => s.ResourceID,
                    s => new CharacterResourceSO(s));

                character.Bio = new CharacterBioSO(bioResult.Value);

                character.Weapons = weaponsResult.Value.ToDictionary(
                    x => x.ItemID,
                    x => new CharacterWeaponSO(x));

                character.Armor = armorResult.Value.ToDictionary(
                    x => x.ItemID,
                    x => new CharacterArmorSO(x));

                character.Items = itemsResult.Value.ToDictionary(
                    x => x.ItemID,
                    x => new CharacterItemSO(x));

                character.Journal = journalResult.Value.ToDictionary(
                    x => x.JournalID,
                    x => new CharacterJournalSO(x)
                );

                character.Abilities = abilitiesResult.Value.ToDictionary(
                    x => x.AbilityID,
                    x => new CharacterAbilitySO(x)
                );

                character.Permissions = permissionsResult.Value;
                return TryResult<CharacterSO>.Success(character); 
            } catch(Exception ex)
            {
                return TryResult<CharacterSO>.Fail(ex.Message);
            }

        }


        public async Task<TryResult> SaveCharacterToDatabase(CharacterSO character)
        {
            bool fail = false;
            var id = character.CharacterID;
            foreach(var stat in character.Stats.Values.Where(x => x.Modified)) 
            {
                if(!(await _database.SaveStat(id, stat)).IsSuccess)
                {
                    fail = true;
                } else
                {
                    stat.Modified = false;
                }
            }

            foreach (var resource in character.Resources.Values.Where(x => x.Modified))
            {
                if (!(await _database.SaveResource(id, resource)).IsSuccess)
                {
                    fail = true;
                }
                else
                {
                    resource.Modified = false;
                }
            }

            foreach (var ability in character.Abilities.Values.Where(x => x.Modified))
            {
                if (!(await _database.SaveAbility(ability)).IsSuccess)
                {
                    fail = true;
                }
                else
                {
                    ability.Modified = false;
                }
            }

            foreach (var journal in character.Journal.Values.Where(x => x.Modified))
            {
                if (!(await _database.SaveJournalEntry(journal)).IsSuccess)
                {
                    fail = true;
                }
                else
                {
                    journal.Modified = false;
                }
            }

            foreach (var item in character.Items.Values.Where(x => x.Modified))
            {
                if (!(await _database.SaveItem(item)).IsSuccess)
                {
                    fail = true;
                }
                else
                {
                    item.Modified = false;
                }
            }

            foreach (var weapon in character.Weapons.Values.Where(x => x.Modified))
            {
                if (!(await _database.SaveWeapon(weapon)).IsSuccess)
                {
                    fail = true;
                }
                else
                {
                    weapon.Modified = false;
                }
            }

            foreach (var armor in character.Armor.Values.Where(x => x.Modified))
            {
                if (!(await _database.SaveArmor(armor)).IsSuccess)
                {
                    fail = true;
                }
                else
                {
                    armor.Modified = false;
                }
            }

            if(character.Bio.Modified)
            {
                if (!(await _database.SaveBio(id, character.Bio)).IsSuccess)
                {
                    fail = true;
                }
                else
                {
                    character.Bio.Modified = false;
                }
            }

            if(fail)
            {
                return TryResult.Fail("One or more saves failed");
            } else
            {
                character.Modified = false;
                return TryResult.Success();
            }
        }

        public async Task<TryResult<List<CharacterProfile>>> GetCharacterProfiles(int userID)
        {
            return await _database.GetProfiles(userID);
        }

        public async Task<CharacterShareResult> ShareCharacter(int ownerID, string shareuser, int characterID, CharacterAccess access)
        {
            return await _database.ShareCharacter(ownerID, shareuser, characterID, access);
        }

        public async Task<TryResult<Dictionary<int, CharacterAccess>>> GetPermissions(int characterID)
        {
            return await _database.GetCharacterPermissions(characterID);
        }

        public async Task<CharacterSharePermissions> GetCharacterSharePermissions(int userId, int characterID) => await _database.GetCharacterSharePermissions(userId, characterID);

        public async Task<TryResult> UnshareCharacter(int userId, int characterID) => await _database.UnshareCharacter(userId, characterID);
    }
}
