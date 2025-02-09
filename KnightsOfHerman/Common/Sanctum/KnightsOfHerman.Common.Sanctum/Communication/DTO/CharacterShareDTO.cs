using KnightsOfHerman.Common.Sanctum.Abstract.Character;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnightsOfHerman.Common.Sanctum.Communication.DTO
{
    /// <summary>
    /// Data Transfer Obejct of CharacterShare
    /// </summary>
    public class CharacterShareDTO
    {
        public string ShareUser { get; set; }

        public int CharacterID { get; set; }

        public CharacterAccess Access { get; set; }
    }
}
