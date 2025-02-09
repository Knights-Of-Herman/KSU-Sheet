using KnightsOfHerman.Backend.Common.User.Model;
using KnightsOfHerman.Common.Types;

namespace KnightsOfHerman.Backend.Database.Interfaces.User
{
    public interface IUserDB
    {
        Task<TryResult<string>> GetUsernameAsync(int userID);

        Task<TryResult<AuthModel>> TryGetUserAuthInfo(string email);

        /// <summary>
        /// Tries to create a user in the database
        /// </summary>
        /// <param name="username">User's username (unique)</param>
        /// <param name="email">User's email (unique)</param>
        /// <param name="encrypted_pwd">Salted password</param>
        /// <param name="salt">Password's salt</param>
        /// <returns>Whether the user was created</returns>
        Task<TryResult<int>> TryCreateUserAsync(string username, string email, string encrypted_pwd, string salt);
    }
}
