using KnightsOfHerman.Common.Types;
using KnightsOfHerman.Common.User;

namespace KnightsOfHerman.Backend.Common.User.Abstract
{
    /// <summary>
    /// Service in charge of User Operations
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Authenticates username and password. Returns a JWT token if successful
        /// </summary>
        /// <returns>A JWT Token if authenticated</returns>
        public Task<TryResult<string>> TryGetJWTTokenAsync(LoginModel credentials);

        /// <summary>
        /// Tries to create a new user with a unique Email and Username
        /// </summary>
        /// <returns>A JWT Token if created</returns>
        public Task<TryResult<string>> TryCreateUserAsync(RegisterModel model);

        /// <summary>
        /// Checks a username and returns a list of errors with it 
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public Task<List<string>> CheckUsername(string username);

        /// <summary>
        /// Checks a email and returns a list of errors with it 
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public Task<List<string>> CheckEmail(string email);

        /// <summary>
        /// Checks if a username exists
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public Task<bool> LookForUsername(string username);
    }
}
