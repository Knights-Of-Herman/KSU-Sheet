using KnightsOfHerman.Common.Abstract.Modification;
using KnightsOfHerman.Common.Sanctum.Abstract.Character.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnightsOfHerman.Backend.Common.Sanctum.Character.Types
{
    /// <summary>
    /// Server Side Implmentation of ICharacterItem
    /// </summary>
    public class CharacterItemSO : ITrackModifed, ICharacterItem
    {
        public bool Modified { get; set; }

        public int ItemID { get; }

        private string _name;
        public string Name
        {
            get => _name;
            set
            {
                if (_name != value)
                {
                    _name = value;
                    Modified = true;
                }
            }
        }

        private string _description;
        public string Description
        {
            get => _description;
            set
            {
                if (_description != value)
                {
                    _description = value;
                    Modified = true;
                }
            }
        }

        private decimal _weight;
        public decimal Weight
        {
            get => _weight;
            set
            {
                if (_weight != value)
                {
                    _weight = value;
                    Modified = true;
                }
            }
        }

        private short _quantity;
        public short Quantity
        {
            get => _quantity;
            set
            {
                if (_quantity != value)
                {
                    _quantity = value;
                    Modified = true;
                }
            }
        }

        public CharacterItemSO(ICharacterItem item)
        {
            ItemID = item.ItemID;
            _name = item.Name;
            _description = item.Description;
            _weight = item.Weight;
            _quantity = item.Quantity; 
            Modified = false;
        }
    }
}
