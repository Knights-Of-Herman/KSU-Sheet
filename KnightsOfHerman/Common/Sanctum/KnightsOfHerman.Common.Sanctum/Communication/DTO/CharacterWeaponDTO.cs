using KnightsOfHerman.Common.Sanctum.Abstract.Character.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnightsOfHerman.Common.Sanctum.Communication.DTO
{
    /// <summary>
    /// Data Transfer Obejct of CharacterWeapon
    /// </summary>
    public class CharacterWeaponDTO : ICharacterWeapon
    {
        public int ItemID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Weight { get; set; }
        public short Quantity { get; set; }
        public string Damage { get; set; }
        public short Accuracy { get; set; }

        public CharacterWeaponDTO(ICharacterWeapon weapon)
        {
            ItemID = weapon.ItemID;
            Name = weapon.Name;
            Description = weapon.Description;
            Weight = weapon.Weight;
            Quantity = weapon.Quantity;
            Damage = weapon.Damage;
            Accuracy = weapon.Accuracy;
        }

        public CharacterWeaponDTO() { }
    }
}
