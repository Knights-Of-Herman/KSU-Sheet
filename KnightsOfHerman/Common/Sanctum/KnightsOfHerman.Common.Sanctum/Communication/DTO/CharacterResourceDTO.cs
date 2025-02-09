using KnightsOfHerman.Common.Sanctum.Abstract.Character.Stats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnightsOfHerman.Common.Sanctum.Communication.DTO
{
    /// <summary>
    /// Data Transfer Obejct of CharacterResource
    /// </summary>
    public class CharacterResourceDTO : ICharacterResource
    {

        public bool DoOverrideMax { get; set; }

        public short Attribute { get; set; }
        public short Modifier { get; set; }
        public short OverrideMaxValue { get; set; }

        public short CurrentValue { get; set; }

        public CharacterResources ResourceID { get; set; }

        public CharacterResourceDTO()
        {

        }

        public CharacterResourceDTO(ICharacterResource resource)
        {
            Attribute = resource.Attribute;
            Modifier = resource.Modifier;
            OverrideMaxValue = resource.OverrideMaxValue;
            DoOverrideMax = resource.DoOverrideMax;
            CurrentValue = resource.CurrentValue;
        }
    }
}
