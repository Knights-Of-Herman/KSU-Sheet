using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnightsOfHerman.Common.Sanctum.Abstract.Character.Items
{
    /// <summary>
    /// A Weapon in Sanctum
    /// </summary>
    public interface ICharacterWeapon
    {
        /// <summary>
        /// Weapon's ID
        /// </summary>
        public int ItemID { get; }

        /// <summary>
        /// Name of the weapon
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Description of the weapon
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Weight of the weapon
        /// </summary>
        public decimal Weight { get; set; }

        /// <summary>
        /// Quantity of the weapon
        /// </summary>
        public short Quantity { get; set; }

        /// <summary>
        /// Damage of the item
        /// </summary>
        public string Damage { get; set; }

        /// <summary>
        /// Accuracy of the item
        /// </summary>
        public short Accuracy { get; set; }
    }
}
