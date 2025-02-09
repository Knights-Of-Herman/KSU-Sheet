using KnightsOfHerman.Common.Sanctum.Abstract.Character.Stats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnightsOfHerman.Backend.Common.Database.Types.Character
{
    /// <summary>
    /// CharacterStat Object for Database operation
    /// </summary>
    public class CharacterStatDBO : ICharacterStat
    {
        public CharacterStats StatID { get; set; }
        public short Base { get; set; }
        public short CustomMod { get; set; }
        public short OverrideValue { get; set; }
        public bool DoOverride { get; set; }
    }
}
