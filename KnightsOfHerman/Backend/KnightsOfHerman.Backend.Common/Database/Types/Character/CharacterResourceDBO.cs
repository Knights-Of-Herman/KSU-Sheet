using KnightsOfHerman.Common.Sanctum.Abstract.Character.Stats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnightsOfHerman.Backend.Common.Database.Types.Character
{
    /// <summary>
    /// CharacterResource Object for Database operation
    /// </summary>
    public class CharacterResourceDBO : ICharacterResource
    {
        public CharacterResources ResourceID { get; set; }
        public short Attribute { get; set; }
        public short Modifier { get; set; }
     
        public short OverrideMaxValue { get; set; } 
        public bool DoOverrideMax { get; set; }
        public short CurrentValue { get; set; }
        public short Total => throw new NotImplementedException();
    }
}
