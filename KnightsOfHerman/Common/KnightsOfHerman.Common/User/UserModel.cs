using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnightsOfHerman.Common.User
{
    /// <summary>
    /// Type representing the basic information about a user
    /// </summary>
    public class UserModel
    {
        public int ID { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }
    }
}
