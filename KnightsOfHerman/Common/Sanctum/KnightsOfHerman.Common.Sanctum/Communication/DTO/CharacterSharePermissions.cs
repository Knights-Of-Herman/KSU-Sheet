using KnightsOfHerman.Common.Sanctum.Abstract.Character;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnightsOfHerman.Common.Sanctum.Communication.DTO
{
    /// <summary>
    /// Data Transfer Obejct of a list of CharacterPermissions
    /// </summary>
    public class CharacterSharePermissions
    {
        public List<CharacterShareDTO> Permissions { get; set; } = new();
    }
}
