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
    /// Server Side Implmentation of ICharacterWeapon
    /// </summary>
    public class CharacterWeaponSO : ITrackModifed, ICharacterWeapon
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

        private string _damage;
        public string Damage
        {
            get => _damage;
            set
            {
                if (_damage != value)
                {
                    _damage = value;
                    Modified = true;
                }
            }
        }

        private short _accuracy;
        public short Accuracy
        {
            get => _accuracy;
            set
            {
                if (_accuracy != value)
                {
                    _accuracy = value;
                    Modified = true;
                }
            }
        }

        public CharacterWeaponSO(ICharacterWeapon weapon)
        {
            ItemID = weapon.ItemID; 
            _name = weapon.Name;
            _description = weapon.Description;
            _weight = weapon.Weight;
            _quantity = weapon.Quantity;
            _damage = weapon.Damage;
            _accuracy = weapon.Accuracy;
            Modified = false; 
        }
    }
}
