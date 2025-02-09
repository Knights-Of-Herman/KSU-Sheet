using KnightsOfHerman.Common.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnightsOfHerman.Frontend.Common.Model.User
{
    /// <summary>
    /// Controller in charge of logging in,registering and handling JWT tokens
    /// </summary>
    public interface IUserController
    {
        /// <summary>
        /// Attempts to login
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Task<bool> TryLoginAsync(LoginModel model);

        /// <summary>
        /// Attempts to logout
        /// </summary>
        /// <returns></returns>
        public Task<bool> TryLogoutAsync();

        /// <summary>
        /// Loads the user information
        /// </summary>
        /// <returns></returns>
        public Task LoadUserAsync();

        /// <summary>
        /// Attempts to register a new user
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Task<bool> TryRegisterAsync(RegisterModel model); //Want to change this to tryresult

        /// <summary>
        /// Checks a username for availabiltiy and profanity checking
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public Task<List<string>> CheckUsername(string username);

        /// <summary>
        /// Checks an email for availability and formatting
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public Task<List<string>> CheckEmail(string email);

        /// <summary>
        /// Checks for a user to friend or share to
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public Task<bool> LookForUser(string username);
    }
}
