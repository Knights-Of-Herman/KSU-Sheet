using KnightsOfHerman.Backend.Common.Database.Types.Character;
using KnightsOfHerman.Common.Sanctum.Abstract.Character.Abilities;
using KnightsOfHerman.Common.Sanctum.Abstract.Character.Notes;
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
    /// Helper class to handle Ability Querries
    /// </summary>
    internal class AbilityQueries
    {
        AzureDBContext _dbContext;

        public AbilityQueries(AzureDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<TryResult<CharacterAbilityDBO>> CreateAbility(int characterID, AbilityType type)
        {
            try
            {
                var parameter = new SqlParameter("@CharacterID", characterID);
                var parameter2 = new SqlParameter("@AbilityType", type);
                var entries = await _dbContext.Database.SqlQueryRaw<CharacterAbilityDBO>("EXEC CreateCharacterAbility @CharacterID, @AbilityType", parameter, parameter2).ToListAsync();

                var entry = entries.First();

                return TryResult<CharacterAbilityDBO>.Success(entry);
            }
            catch (Exception ex)
            {
                return TryResult<CharacterAbilityDBO>.Fail(ex.Message);
            }
        }

        public async Task<TryResult<List<CharacterAbilityDBO>>> GetAbilities(int characterID)
        {
            try
            {
                var parameter = new SqlParameter("@CharacterID", characterID);
                var entries = await _dbContext.Database.SqlQueryRaw<CharacterAbilityDBO>("EXEC GetCharacterAbilities @CharacterID", parameter).ToListAsync();

                return TryResult<List<CharacterAbilityDBO>>.Success(entries);
            }
            catch (Exception ex)
            {
                return TryResult<List<CharacterAbilityDBO>>.Fail(ex.Message);
            }
        }

        public async Task<TryResult> SaveAbility(ICharacterAbility ability)
        {
            try
            {
                await _dbContext.Database.ExecuteSqlRawAsync("EXEC SaveCharacterAbility @AbilityID, @Title, @Content, @Cost, @Memorized",
                    new SqlParameter("@AbilityID", ability.AbilityID),
                    new SqlParameter("@Title", ability.Title),
                    new SqlParameter("@Content", ability.Content),
                    new SqlParameter("@Cost", ability.Cost),
                    new SqlParameter("@Memorized", ability.Memorized));
                return TryResult.Success();
            }
            catch (Exception ex)
            {
                return TryResult.Fail(ex.Message);
            }
        }

        public async Task<TryResult> DeleteAbility(int abilityID)
        {
            try
            {
                await _dbContext.Database.ExecuteSqlRawAsync("EXEC DeleteCharacterAbility @AbilityID",
                    new SqlParameter("@AbilityID", abilityID));
                return TryResult.Success();
            }
            catch (Exception ex)
            {
                return TryResult.Fail(ex.Message);
            }
        }
    }
}
