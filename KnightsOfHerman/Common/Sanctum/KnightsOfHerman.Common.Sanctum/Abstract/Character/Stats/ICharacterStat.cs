using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnightsOfHerman.Common.Sanctum.Abstract.Character.Stats
{
    /// <summary>
    /// Represents the character's skills and attributes
    /// </summary>
    public interface ICharacterStat
    {
        /// <summary>
        /// Type of Stat
        /// </summary>
        public CharacterStats StatID { get; }

        /// <summary>
        /// Base Score prior to any modifiers
        /// </summary>
        short Base { get; set; }

        /// <summary>
        /// Custom Modifier to apply
        /// </summary>
        short CustomMod { get; set; }

        /// <summary>
        /// Total uses this value if DoOveride is flagged
        /// </summary>
        short OverrideValue { get; set; }

        /// <summary>
        /// Whether to use the Override Value
        /// </summary>
        bool DoOverride { get; set; }
    }
}
