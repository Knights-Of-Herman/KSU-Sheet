using KnightsOfHerman.Backend.Common.Database.Interfaces.User;
using KnightsOfHerman.Backend.Common.User.Model;
using KnightsOfHerman.Common.Types;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnightsOfHerman.Backend.Database.Azure.User
{

    /// <summary>
    /// Implementation of IUserDB for the AzureDb
    /// </summary>
    public class UserDB : IUserDB
    {
        AzureDBContext _dbContext;
        public UserDB(AzureDBContext context)
        {
            _dbContext = context;
        }

        DbConnection GetConnection() => _dbContext.Database.GetDbConnection();

        public async Task<TryResult<string>> GetUsernameAsync(int userID)
        {
            throw new NotImplementedException(); //Legacy and not needed
        }

        public async Task<TryResult<int>> TryCreateUserAsync(string username, string email, string e_pwd, string salt)
        {
            //holy molly i need to make these into procedures asap
            string query = @"INSERT INTO KOH.Users (Username, Email, Password, Salt) 
                                OUTPUT INSERTED.UserID                
                                VALUES (@username, @email, @password, @salt)";
            try
            {
                using (var conn = GetConnection())
                {
                    using (var cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = query;

                        cmd.Parameters.Add(new SqlParameter("@username", username));
                        cmd.Parameters.Add(new SqlParameter("@email", email));
                        cmd.Parameters.Add(new SqlParameter("@password", e_pwd));
                        cmd.Parameters.Add(new SqlParameter("@salt", salt));

                        await conn.OpenAsync();
                        var result = await cmd.ExecuteScalarAsync();
                        if (result != null)
                        {
                            var id = Convert.ToInt32(result);
                            return TryResult<int>.Success(id);
                        }
                        else
                        {
                            return TryResult<int>.Fail();
                        }
                    }
                }
            }
            catch
            {
                return TryResult<int>.Fail();
            }
        }

        public async Task<TryResult<AuthModel>> TryGetUserAuthInfo(string email)
        {
            try
            {
                string query = "SELECT UserID, Password, Salt, Username FROM KOH.Users WHERE Email = @email";
                using (var conn = GetConnection())
                {
                    using (var cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = query;
                        cmd.Parameters.Add(new SqlParameter("@email", email));

                        await conn.OpenAsync();
                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                var id = reader.GetInt32(0);
                                var pwd = reader.GetString(1);
                                var salt = reader.GetString(2);
                                var username = reader.GetString(3);

                                var info = new AuthModel(username, pwd, salt, id);

                                return TryResult<AuthModel>.Success(info);
                            }
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                return TryResult<AuthModel>.Fail(ex.Message);
            }
            return TryResult<AuthModel>.Fail();
        }

        public async Task<bool> CheckUsernameExists(string username)
        {
            var exists = await _dbContext.Database.SqlQueryRaw<bool>("EXEC CheckForUsername @Username", new SqlParameter("@Username", username)).ToListAsync();
            return exists.FirstOrDefault();
        }

        public async Task<bool> CheckEmailExists(string email)
        {
            var exists = await _dbContext.Database.SqlQueryRaw<bool>("EXEC CheckForEmail @Email", new SqlParameter("@Email", email)).ToListAsync();
            return exists.FirstOrDefault();
        }
    }
}
