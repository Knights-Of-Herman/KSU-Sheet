using KnightsOfHerman.Common.Sanctum.Abstract.Character.Stats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnightsOfHerman.Common.Sanctum.Communication.DTO
{
    /// <summary>
    /// Data Transfer Obejct of CharacterStat
    /// </summary>
    public class CharacterStatDTO : ICharacterStat
    {
        public CharacterStats StatID { get; set; }
        public short Base { get; set; }
        public short CustomMod { get; set; }
        public short OverrideValue { get; set; }
        public bool DoOverride { get; set; }

        public CharacterStatDTO() { }

        public CharacterStatDTO(ICharacterStat stat)
        {
            StatID = stat.StatID;
            Base = stat.Base;
            CustomMod = stat.CustomMod;
            OverrideValue = stat.OverrideValue;
            DoOverride = stat.DoOverride;
        }
    }
}
