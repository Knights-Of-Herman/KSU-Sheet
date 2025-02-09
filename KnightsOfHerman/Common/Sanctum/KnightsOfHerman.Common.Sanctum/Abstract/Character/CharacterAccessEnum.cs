using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnightsOfHerman.Common.Sanctum.Abstract.Character
{
    /// <summary>
    /// The access level someone has to a character
    /// </summary>
    public enum CharacterAccess
    {
        None = 0,
        Owner = 1,
        Viewer = 2,
        Editor = 3,
    }
}
