using KnightsOfHerman.Common.Sanctum.Abstract.Character.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnightsOfHerman.Backend.Common.Database.Types.Character
{
    /// <summary>
    /// CharacterArmor Object for Database operation
    /// </summary>
    public class CharacterArmorDBO : ICharacterArmor
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
    }
}
