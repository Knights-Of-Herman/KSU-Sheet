using KnightsOfHerman.Backend.Common.Database.Types.Character;
using KnightsOfHerman.Common.Sanctum.Abstract.Character.Items;
using KnightsOfHerman.Common.Sanctum.Communication.DTO;
using KnightsOfHerman.Common.Types;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnightsOfHerman.Backend.Database.Azure.Character.Querries
{
    /// <summary>
    /// Helper class to handle Item Querries
    /// </summary>
    public class InventoryQueries
    {
        AzureDBContext _dbContext;
        public InventoryQueries(AzureDBContext dBContext)
        {
            _dbContext = dBContext;
        }
        public async Task<TryResult<CharacterItemDBO>> CreateItem(int characterID)
        {
            try
            {
                var parameter = new SqlParameter("@CharacterID", characterID);
                var entries = await _dbContext.Database.SqlQueryRaw<CharacterItemDBO>("EXEC CreateCharacterItem @CharacterID", parameter).ToListAsync();

                var entry = entries.First();

                return TryResult<CharacterItemDBO>.Success(entry);
            }
            catch (Exception ex)
            {
                return TryResult<CharacterItemDBO>.Fail(ex.Message);
            }
        }
        public async Task<TryResult<List<CharacterItemDBO>>> GetItems(int characterID)
        {
            try
            {
                var parameter = new SqlParameter("@CharacterID", characterID);
                var entries = await _dbContext.Database.SqlQueryRaw<CharacterItemDBO>("EXEC GetCharacterItems @CharacterID", parameter).ToListAsync();

                return TryResult<List<CharacterItemDBO>>.Success(entries);
            }
            catch (Exception ex)
            {
                return TryResult<List<CharacterItemDBO>>.Fail(ex.Message);
            }
        }

        public async Task<TryResult> SaveItem(ICharacterItem item)
        {
            try
            {
                await _dbContext.Database.ExecuteSqlRawAsync("EXEC SaveCharacterItem @ItemID, @Name, @Description, @Quantity, @Weight",
                    new SqlParameter("@ItemID", item.ItemID),
                    new SqlParameter("@Name", item.Name),
                    new SqlParameter("@Description", item.Description),
                    new SqlParameter("@Quantity", item.Quantity),
                    new SqlParameter("@Weight", item.Weight));
                return TryResult.Success();
            }
            catch (Exception ex)
            {
                return TryResult.Fail(ex.Message);
            }
        }

        public async Task<TryResult> DeleteItem(int itemid)
        {
            try
            {
                await _dbContext.Database.ExecuteSqlRawAsync("EXEC DeleteCharacterItem @ItemID",
                    new SqlParameter("@ItemID", itemid));
                return TryResult.Success();
            }
            catch (Exception ex)
            {
                return TryResult.Fail(ex.Message);
            }
        }

        public async Task<TryResult<CharacterWeaponDBO>> CreateWeapon(int characterID)
        {
            try
            {
                var parameter = new SqlParameter("@CharacterID", characterID);
                var entries = await _dbContext.Database.SqlQueryRaw<CharacterWeaponDBO>("EXEC CreateCharacterWeapon @CharacterID", parameter).ToListAsync();

                var entry = entries.First();

                return TryResult<CharacterWeaponDBO>.Success(entry);
            }
            catch (Exception ex)
            {
                return TryResult<CharacterWeaponDBO>.Fail(ex.Message);
            }
        }

        public async Task<TryResult<CharacterArmorDBO>> CreateArmor(int characterID)
        {
            try
            {
                var parameter = new SqlParameter("@CharacterID", characterID);
                var entries = await _dbContext.Database.SqlQueryRaw<CharacterArmorDBO>("EXEC CreateCharacterArmor @CharacterID", parameter).ToListAsync();

                return TryResult<CharacterArmorDBO>.Success(entries.First());
            }
            catch (Exception ex)
            {
                return TryResult<CharacterArmorDBO>.Fail(ex.Message);
            }
        }

        public async Task<TryResult<List<CharacterWeaponDBO>>> GetWeapons(int characterID)
        {
            try
            {
                var parameter = new SqlParameter("@CharacterID", characterID);
                var entries = await _dbContext.Database.SqlQueryRaw<CharacterWeaponDBO>("EXEC GetCharacterWeapons @CharacterID", parameter).ToListAsync();
                return TryResult<List<CharacterWeaponDBO>>.Success(entries);
            }
            catch (Exception ex)
            {
                return TryResult<List<CharacterWeaponDBO>>.Fail(ex.Message);
            }
        }

        public async Task<TryResult<List<CharacterArmorDBO>>> GetArmor(int characterID)
        {
            try
            {
                var parameter = new SqlParameter("@CharacterID", characterID);
                var entries = await _dbContext.Database.SqlQueryRaw<CharacterArmorDBO>("EXEC GetCharacterArmor @CharacterID", parameter).ToListAsync();
                return TryResult<List<CharacterArmorDBO>>.Success(entries);
            }
            catch (Exception ex)
            {
                return TryResult<List<CharacterArmorDBO>>.Fail(ex.Message);
            }
        }

        public async Task<TryResult> SaveWeapon(ICharacterWeapon weapon)
        {
            try
            {
                await _dbContext.Database.ExecuteSqlRawAsync("EXEC SaveCharacterWeapon @ItemID, @Name, @Description, @Quantity, @Weight,  @Damage, @Accuracy",
                    new SqlParameter("@ItemID", weapon.ItemID),
                    new SqlParameter("@Name", weapon.Name),
                    new SqlParameter("@Description", weapon.Description),
                    new SqlParameter("@Weight", weapon.Weight),
                    new SqlParameter("@Quantity", weapon.Quantity),
                    new SqlParameter("@Damage", weapon.Damage),
                    new SqlParameter("@Accuracy", weapon.Accuracy));
                return TryResult.Success();
            }
            catch (Exception ex)
            {
                return TryResult.Fail(ex.Message);
            }
        }

        public async Task<TryResult> SaveArmor(ICharacterArmor armor)
        {
            try
            {
                await _dbContext.Database.ExecuteSqlRawAsync("EXEC SaveCharacterArmor @ItemID, @Name, @Description, @Weight, @Hindrance, @Equipped, @Layer, @Slot, @Bludgeoning, @Piercing, @Slashing",
                    new SqlParameter("@ItemID", armor.ItemID),
                    new SqlParameter("@Name", armor.Name),
                    new SqlParameter("@Description", armor.Description),
                    new SqlParameter("@Weight", armor.Weight),
                    new SqlParameter("@Hindrance", armor.Hindrance),
                    new SqlParameter("@Equipped", armor.Equipped),
                    new SqlParameter("@Layer", armor.Layer),
                    new SqlParameter("@Slot", armor.Slot),
                    new SqlParameter("@Bludgeoning", armor.Bludgeoning),
                    new SqlParameter("@Piercing", armor.Piercing),
                    new SqlParameter("@Slashing", armor.Slashing));
                return TryResult.Success();
            }
            catch (Exception ex)
            {
                return TryResult.Fail(ex.Message);
            }
        }

        public async Task<TryResult> DeleteWeapon(int itemID)
        {
            try
            {
                await _dbContext.Database.ExecuteSqlRawAsync("EXEC DeleteCharacterWeapon @ItemID",
                    new SqlParameter("@ItemID", itemID));
                return TryResult.Success();
            }
            catch (Exception ex)
            {
                return TryResult.Fail(ex.Message);
            }
        }

        public async Task<TryResult> DeleteArmor(int itemID)
        {
            try
            {
                await _dbContext.Database.ExecuteSqlRawAsync("EXEC DeleteCharacterArmor @ItemID",
                    new SqlParameter("@ItemID", itemID));
                return TryResult.Success();
            }
            catch (Exception ex)
            {
                return TryResult.Fail(ex.Message);
            }
        }
    }
}
