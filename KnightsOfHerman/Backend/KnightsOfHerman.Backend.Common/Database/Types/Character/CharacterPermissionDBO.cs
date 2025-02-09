using KnightsOfHerman.Common.Sanctum.Abstract.Character;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnightsOfHerman.Backend.Common.Database.Types.Character
{
    /// <summary>
    /// CharacterPermissions Object for Database operation
    /// </summary>
    public class CharacterPermissionDBO
    {
        public int UserID { get; set; }
        public int AccessLevel { get; set; }
    }

    /// <summary>
    /// CharacterShareInfo Object for Database operation
    /// </summary>
    public class CharacterShareInfoDBO
    {
        public string Username { get; set; }
        public CharacterAccess AccessLevel { get; set; }
    }
}
