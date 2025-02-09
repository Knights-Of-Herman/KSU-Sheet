using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnightsOfHerman.Common.Sanctum.Abstract.Character.Abilities
{
    /// <summary>
    /// Talents, Miracles, Weaponarts and other Abilities a character can perform
    /// </summary>
    public interface ICharacterAbility
    {
        /// <summary>
        /// Ability ID
        /// </summary>
        public int AbilityID { get; }

        /// <summary>
        /// Type of the Ability
        /// </summary>
        public AbilityType AbilityType { get; }

        /// <summary>
        /// Name of the Ability
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Description of the ability
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// Cost of the Ability (materials, Resources etc)
        /// </summary>
        public string Cost { get; set; }

        /// <summary>
        /// Whether the ability is memorized
        /// </summary>
        public bool Memorized { get; set; }
    }
}
