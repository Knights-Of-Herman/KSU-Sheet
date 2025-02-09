using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnightsOfHerman.Backend.Common.User.Model
{
    /// <summary>
    /// Model for Authentication
    /// </summary>
    public class AuthModel
    {
        public string EncryptedPassword { get; }

        public string Salt { get; }

        public string Username { get; }

        public int UserID { get; }

        public AuthModel(string username, string password, string salt, int userID)
        {
            Username = username;
            EncryptedPassword = password;
            Salt = salt;
            UserID = userID;
        }
    }
}
