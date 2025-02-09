using KnightsOfHerman.Backend.Common.Database.Types.Character;
using KnightsOfHerman.Backend.Common.Sanctum.Character.Types;
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
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnightsOfHerman.Backend.Database.Azure.Character.Querries
{
    /// <summary>
    /// Helper class to handle Basic Character Querries
    /// </summary>
    internal class BasicQueries
    {
        AzureDBContext _dbContext;
        public BasicQueries(AzureDBContext dBContext)
        {
            _dbContext = dBContext;    
        }

        public async Task<CharacterShareResult> ShareCharacter(int ownerID, string shareuser, int characterId, CharacterAccess access)
        {
            try
            {
                var parameters = new SqlParameter[]
                {
                    new SqlParameter("@OwnerID", ownerID),
                    new SqlParameter("@ShareUsername", shareuser),
                    new SqlParameter("@CharacterID", characterId),
                    new SqlParameter("@AccessLevel", access),
                };
                var resultRaw = await _dbContext.Database.SqlQueryRaw<int>("EXEC ShareCharacter @OwnerID, @ShareUsername, @CharacterID, @AccessLevel", parameters).ToListAsync();
                var result = resultRaw.FirstOrDefault();
                return (CharacterShareResult)result;
            }
            catch (Exception ex)
            {
                return CharacterShareResult.UnknownError;
            }
        }

        public async Task<TryResult<int>> GetCharacterCount(int userID)
        {
            try
            {
                var parameter = new SqlParameter("@UserID", userID);
                var ids = await _dbContext.Database.SqlQueryRaw<int>("EXEC GetCharacterCount @UserID", parameter).ToListAsync();

                var id = ids.First();

                return TryResult<int>.Success(id);
            }
            catch (Exception ex)
            {
                return TryResult<int>.Fail(ex.Message);
            }

        }
        public async Task<TryResult<int>> CreateCharacter(int userID)
        {
            try
            {
                var parameter = new SqlParameter("@UserID", userID);
                var ids = await _dbContext.Database.SqlQueryRaw<int>("EXEC CreateBlankCharacter @UserID", parameter).ToListAsync();

                var id = ids.First();


                //Build Stats
                StringBuilder sb = new StringBuilder();
                foreach (CharacterStats stat in Enum.GetValues(typeof(CharacterStats)))
                {
                    sb.Append((int)stat).Append(",");
                }

                // Remove the last comma
                if (sb.Length > 0)
                {
                    sb.Length--;
                }

                string statIds = sb.ToString();

                await _dbContext.Database.ExecuteSqlRawAsync("EXEC CreateCharacterStats @CharacterID, @StatIDs",
                        new SqlParameter("@CharacterID", id),
                        new SqlParameter("@StatIDs", statIds));

                //Build Resources
                sb = new StringBuilder();
                foreach (CharacterResources stat in Enum.GetValues(typeof(CharacterResources)))
                {
                    sb.Append((int)stat).Append(",");
                }

                // Remove the last comma
                if (sb.Length > 0)
                {
                    sb.Length--;
                }

                string resourceIds = sb.ToString();

                await _dbContext.Database.ExecuteSqlRawAsync("EXEC CreateCharacterResources @CharacterID, @ResourceIDs",
                        new SqlParameter("@CharacterID", id),
                        new SqlParameter("@ResourceIDs", resourceIds));

                return TryResult<int>.Success(id);
            }
            catch (Exception ex)
            {
                return TryResult<int>.Fail(ex.Message);
            }
        }

        public async Task<TryResult> DeleteCharacterAsync(int userID, int characterID)
        {
            try
            {
                var user = new SqlParameter("@UserID", userID);
                var character = new SqlParameter("@CharacterID", characterID);
                var rows = await _dbContext.Database.ExecuteSqlRawAsync("EXEC DeleteCharacter @UserID, @CharacterID", user, character);
                if (rows > 0) return TryResult.Success();
                else return TryResult.Fail("Deletion Failed");
            }
            catch (Exception ex)
            {
                return TryResult<int>.Fail(ex.Message);
            }
        }

        public async Task<TryResult<List<CharacterProfile>>> GetProfiles(int userID)
        {
            try
            {
                var parameter = new SqlParameter("@UserID", userID);
                var profiles = await _dbContext.Database.SqlQueryRaw<CharacterProfile>("EXEC GetCharacterProfiles @UserID",parameter).ToListAsync();
                if(profiles != null) return TryResult<List<CharacterProfile>>.Success(profiles);
                return TryResult<List<CharacterProfile>>.Fail("Null List");
            }
            catch (Exception ex)
            {
                return TryResult<List<CharacterProfile>>.Fail(ex.Message);
            }
        }

        public async Task<TryResult<CharacterBioDBO>> GetCharacterBio(int characterID)
        {
            try
            {
                var parameter = new SqlParameter("@CharacterID", characterID);
                var bios = await _dbContext.Database.SqlQueryRaw<CharacterBioDBO>("EXEC GetCharacterBio @CharacterID", parameter).ToListAsync();
                if (bios != null)
                {
                    return TryResult<CharacterBioDBO>.Success(bios.First());

                }

                return TryResult<CharacterBioDBO>.Fail("Null List");
            }
            catch (Exception ex)
            {
                return TryResult<CharacterBioDBO>.Fail(ex.Message);
            }
        }


        public async Task<TryResult<Dictionary<int, CharacterAccess>>> GetCharacterPermissions(int characterID) 
        {
            try
            {
                var parameter = new SqlParameter("@CharacterID", characterID);
                var permissions = await _dbContext.Database.SqlQueryRaw<CharacterPermissionDBO>("EXEC GetCharacterPermissions @CharacterID", parameter).ToListAsync();
                if (permissions != null)
                {
                    var perms = permissions.ToDictionary(x => x.UserID, x => (CharacterAccess)x.AccessLevel);
                    return TryResult<Dictionary<int, CharacterAccess>>.Success(perms);
                }
                return TryResult<Dictionary<int, CharacterAccess>>.Fail("Null Bio");
            }
            catch (Exception ex)
            {
                return TryResult<Dictionary<int, CharacterAccess>>.Fail(ex.Message);
            }
        }

        public async Task<CharacterSharePermissions> GetCharacterSharePermissions(int ownerID, int characterID)
        {
            var permsObject = new CharacterSharePermissions()
            {
                Permissions = new()
            };
            try
            {
                var parameters = new SqlParameter[]
                {
                    new SqlParameter("@OwnerID", ownerID),
                    new SqlParameter("@CharacterID", characterID)
                };          
                var permissions = await _dbContext.Database.SqlQueryRaw<CharacterShareInfoDBO>("EXEC GetCharacterShareInfo @OwnerID, @CharacterID", parameters).ToListAsync();
                if (permissions != null)
                {
                    var perms = permissions.Select(x=>new CharacterShareDTO() { Access = x.AccessLevel, CharacterID = characterID, ShareUser = x.Username}).ToList();
                    permsObject.Permissions = perms;
                }
            }
            catch (Exception ex)
            {
            }
            return permsObject;
        }

        public async Task<TryResult<CharacterBioDTO>> GetBio(int characterID)
        {
            try
            {
                var parameter = new SqlParameter("@CharacterID", characterID);
                var bio = await _dbContext.Database.SqlQueryRaw<CharacterBioDTO>("EXEC GetCharacterBio @CharacterID", parameter).FirstAsync();
                if (bio != null) return TryResult<CharacterBioDTO>.Success(bio);
                return TryResult<CharacterBioDTO>.Fail("Null Bio");
            }
            catch (Exception ex)
            {
                return TryResult<CharacterBioDTO>.Fail(ex.Message);
            }
        }

        public async Task<TryResult> SaveCharacterBio(int characterid, ICharacterBio bio)
        {
            try
            {
                await _dbContext.Database.ExecuteSqlRawAsync("EXEC SaveCharacterBio @CharacterID, @Name, @Race, @Background, @TotalXP, @UnspentXP, @Destiny, @Conflict, @Languages",
                    new SqlParameter("@CharacterID", characterid),
                    new SqlParameter("@Name", bio.Name),
                    new SqlParameter("@Race", bio.Race),
                    new SqlParameter("@Background", bio.Background),
                    new SqlParameter("@TotalXP", bio.TotalXP),
                    new SqlParameter("@UnspentXP", bio.UnspentXP),
                    new SqlParameter("@Destiny", bio.Destiny),
                    new SqlParameter("@Languages", bio.Languages),
                    new SqlParameter("@Conflict", bio.Conflict));
                return TryResult.Success();
            }
            catch (Exception ex)
            {
                return TryResult.Fail(ex.Message);
            }
        }

        public async Task<TryResult> UnshareCharacter(int userId, int characterID)
        {
            try
            {
                var parameters = new SqlParameter[]
                {
                    new SqlParameter("@UserID", userId),
                    new SqlParameter("@CharacterID", characterID),
                };
                await _dbContext.Database.ExecuteSqlRawAsync("EXEC UnShareCharacter @UserID, @CharacterID", parameters);
                return TryResult.Success();
            }
            catch (Exception ex)
            {
                return TryResult.Fail(ex.Message);
            }
        }

    }
}
