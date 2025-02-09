using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnightsOfHerman.Common.Sanctum.Abstract.Character
{
    /// <summary>
    /// Result of a share operation
    /// </summary>
    public enum CharacterShareResult
    {
        Success = 0,
        UserNotFound = 1,
        CharacterNotFound = 2,
        NotAuthorized = 3,
        UnknownError = 4
    }
}
