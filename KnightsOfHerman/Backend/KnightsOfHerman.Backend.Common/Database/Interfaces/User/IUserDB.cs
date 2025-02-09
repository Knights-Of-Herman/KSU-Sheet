using KnightsOfHerman.Backend.Common.User.Model;
using KnightsOfHerman.Common.Types;

namespace KnightsOfHerman.Backend.Common.Database.Interfaces.User
{
    /// <summary>
    /// Represents all User Database options
    /// </summary>
    public interface IUserDB
    {
        /// <summary>
        /// Gets the Username from the Userid
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        Task<TryResult<string>> GetUsernameAsync(int userID);

        /// <summary>
        /// Gets the user's info for logging in
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Sees if the username already exists in the database
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        Task<bool> CheckUsernameExists(string username);

        /// <summary>
        /// Sees if the email already exists in the database
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public Task<bool> CheckEmailExists(string email);

    }
}
