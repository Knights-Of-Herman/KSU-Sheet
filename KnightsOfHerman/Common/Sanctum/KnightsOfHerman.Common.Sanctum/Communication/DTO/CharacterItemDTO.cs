using KnightsOfHerman.Common.Sanctum.Abstract.Character.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnightsOfHerman.Common.Sanctum.Communication.DTO
{
    /// <summary>
    /// Data Transfer Obejct of CharacterItem
    /// </summary>
    public class CharacterItemDTO : ICharacterItem
    {
        public int ItemID { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Weight { get; set; }
        public short Quantity { get; set; }

        public CharacterItemDTO(ICharacterItem item)
        {
            ItemID = item.ItemID;
            Name = item.Name;
            Description = item.Description;
            Weight = item.Weight;
            Quantity = item.Quantity;
        }

        public CharacterItemDTO() { }
    }
}
