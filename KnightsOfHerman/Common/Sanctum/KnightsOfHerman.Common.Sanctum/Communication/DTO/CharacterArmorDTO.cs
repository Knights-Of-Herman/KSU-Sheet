using KnightsOfHerman.Common.Sanctum.Abstract.Character.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnightsOfHerman.Common.Sanctum.Communication.DTO
{
    /// <summary>
    /// Data Transfer Obejct of CharacterArmor
    /// </summary>
    public class CharacterArmorDTO : ICharacterArmor
    {
        public int ItemID { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Weight { get; set; }
        public byte Hindrance { get; set; }
        public bool Equipped { get; set; }
        public ArmorLayer Layer { get; set; }
        public ArmorSlot Slot { get; set; }
        public byte Bludgeoning { get; set; }
        public byte Piercing { get; set; }
        public byte Slashing { get; set; }

        public CharacterArmorDTO(ICharacterArmor armor)
        {
            ItemID = armor.ItemID;
            Name = armor.Name;
            Description = armor.Description;
            Weight = armor.Weight;
            Hindrance = armor.Hindrance;
            Equipped = armor.Equipped;
            Layer = armor.Layer;
            Slot = armor.Slot;
            Bludgeoning = armor.Bludgeoning;
            Piercing = armor.Piercing;
            Slashing = armor.Slashing;
        }

        public CharacterArmorDTO() { }
    }
}
