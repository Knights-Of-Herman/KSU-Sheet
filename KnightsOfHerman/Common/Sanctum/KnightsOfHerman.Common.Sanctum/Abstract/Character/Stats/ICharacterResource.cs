using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnightsOfHerman.Common.Sanctum.Abstract.Character.Stats
{
    /// <summary>
    /// A Character's Attribute that acts as a pool of points such as Health Points
    /// </summary>
    public interface ICharacterResource
    {
        /// <summary>
        /// Which Resource it is
        /// </summary>
        CharacterResources ResourceID { get; }

        /// <summary>
        /// The attribute value
        /// </summary>
        short Attribute { get; set; }

        /// <summary>
        /// The Modifier Value
        /// </summary>
        short Modifier { get; set; }

        /// <summary>
        /// Whether to override the resource's max value
        /// </summary>
        bool DoOverrideMax { get; set; }

        /// <summary>
        /// The amount to override max to
        /// </summary>
        short OverrideMaxValue { get; set; }

        /// <summary>
        /// The current value the resource is at
        /// </summary>
        short CurrentValue { get; set; }
    }
}
