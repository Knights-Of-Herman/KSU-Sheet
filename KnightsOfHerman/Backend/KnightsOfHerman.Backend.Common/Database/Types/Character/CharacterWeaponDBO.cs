using KnightsOfHerman.Common.Sanctum.Abstract.Character.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnightsOfHerman.Backend.Common.Database.Types.Character
{
    /// <summary>
    /// CharacterWeapon Object for Database operation
    /// </summary>
    public class CharacterWeaponDBO : ICharacterWeapon
    {
        public int ItemID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Weight { get; set; }
        public short Quantity { get; set; }
        public string Damage { get; set; }
        public short Accuracy { get; set; }
    }
}
