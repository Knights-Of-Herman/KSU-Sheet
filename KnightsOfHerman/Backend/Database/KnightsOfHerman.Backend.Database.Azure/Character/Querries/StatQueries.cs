using KnightsOfHerman.Backend.Common.Database.Types.Character;
using KnightsOfHerman.Backend.Database.Azure.User;
using KnightsOfHerman.Common.Sanctum.Abstract.Character;
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

namespace KnightsOfHerman.Backend.Database.Azure.Character.Querries
{
    /// <summary>
    /// Helper class to handle Stat/Resource Querries
    /// </summary>
    internal class StatQueries
    {
        AzureDBContext _dbContext;
        public StatQueries(AzureDBContext dBContext)
        {
            _dbContext = dBContext;
        }
        public async Task<TryResult<List<CharacterStatDBO>>> GetCharacterStats(int characterid)
        {
            try
            {
                var parameter = new SqlParameter("@CharacterID", characterid);
                var stats = await _dbContext.Database.SqlQueryRaw<CharacterStatDBO>("EXEC GetCharacterStats @CharacterID", parameter).ToListAsync();
                if (stats != null) return TryResult<List<CharacterStatDBO>>.Success(stats);
                return TryResult<List<CharacterStatDBO>>.Fail("Null List");
            }
            catch (Exception ex)
            {
                return TryResult<List<CharacterStatDBO>>.Fail(ex.Message);
            }
        }

        public async Task<TryResult> SaveCharacterStat(int characterid, ICharacterStat stat)
        {
            try
            {
                await _dbContext.Database.ExecuteSqlRawAsync("EXEC SaveCharacterStat @CharacterID, @StatID, @Base, @CustomMod, @OverrideValue, @DoOverride",
                    new SqlParameter("@CharacterID", characterid),
                    new SqlParameter("@StatID", stat.StatID),
                    new SqlParameter("@Base", stat.Base),
                    new SqlParameter("@CustomMod", stat.CustomMod),
                    new SqlParameter("@OverrideValue", stat.OverrideValue),
                    new SqlParameter("@DoOverride", stat.DoOverride));
                return TryResult.Success();
            } catch(Exception ex)
            {
                return TryResult.Fail(ex.Message);
            }
        }
     
        public async Task<TryResult<List<CharacterResourceDBO>>> GetCharacterResources(int characterid)
        {
            try
            {
                var parameter = new SqlParameter("@CharacterID", characterid);
                var stats = await _dbContext.Database.SqlQueryRaw<CharacterResourceDBO>("EXEC GetCharacterResources @CharacterID", parameter).ToListAsync();
                if (stats != null) return TryResult<List<CharacterResourceDBO>>.Success(stats);
                return TryResult<List<CharacterResourceDBO>>.Fail("Null List");
            }
            catch (Exception ex)
            {
                return TryResult<List<CharacterResourceDBO>>.Fail(ex.Message);
            }
        }


        public async Task<TryResult> SaveCharacterResource(int characterid, ICharacterResource resource)
        {
            try
            {
                await _dbContext.Database.ExecuteSqlRawAsync("EXEC SaveCharacterResource @CharacterID, @ResourceID, @Attribute, @Modifier, @OverrideMaxValue, @DoOverrideMax, @CurrentValue",
                        new SqlParameter("@CharacterID", characterid),
                        new SqlParameter("@ResourceID", resource.ResourceID),
                        new SqlParameter("@Attribute", resource.Attribute),
                        new SqlParameter("@Modifier", resource.Modifier),
                        new SqlParameter("@DoOverrideMax", resource.DoOverrideMax),
                        new SqlParameter("@OverrideMaxValue", resource.OverrideMaxValue),
                        new SqlParameter("@CurrentValue", resource.CurrentValue));
                return TryResult.Success();
            }
            catch (Exception ex)
            {
                return TryResult.Fail(ex.Message);
            }
        }
    }
}
