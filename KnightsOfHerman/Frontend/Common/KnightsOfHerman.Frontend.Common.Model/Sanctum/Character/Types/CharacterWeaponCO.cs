using KnightsOfHerman.Common.Abstract.Modification;
using KnightsOfHerman.Common.Abstract;
using KnightsOfHerman.Common.Sanctum.Abstract.Character.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnightsOfHerman.Frontend.Common.Model.Sanctum.Character.Types
{
    /// <summary>
    /// Client Implementation of CharacterWeapon
    /// </summary>
    public class CharacterWeaponCO : ICharacterWeapon, INotifyModificationPath
    {
        public int ItemID { get; private set; }

        private string _name;
        public string Name
        {
            get => _name;
            set
            {
                if (_name != value)
                {
                    _name = value;
                    OnValueChange(nameof(Name), value);
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
                    OnValueChange(nameof(Description), value);
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
                    OnValueChange(nameof(Weight), value);
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
                    OnValueChange(nameof(Quantity), value);
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
                    OnValueChange(nameof(Damage), value);
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
                    OnValueChange(nameof(Accuracy), value);
                }
            }
        }

        public bool Modified { get; set; }

        public event TrackedModificationEventHandler? OnTrackedModification;

        private void OnValueChange(string name, object value)
        {
            Modified = true;
            OnTrackedModification?.Invoke(this, new TrackedModificationEventArgs(name, value, EditActions.Edit));
        }

        public CharacterWeaponCO(ICharacterWeapon weapon)
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
