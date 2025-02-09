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
    /// Client Implementation of CharacterArmor
    /// </summary>
    public class CharacterArmorCO : ICharacterArmor, INotifyModificationPath
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

        private byte _hindrance;
        public byte Hindrance
        {
            get => _hindrance;
            set
            {
                if (_hindrance != value)
                {
                    _hindrance = value;
                    OnValueChange(nameof(Hindrance), value);
                }
            }
        }

        private bool _equipped;
        public bool Equipped
        {
            get => _equipped;
            set
            {
                if (_equipped != value)
                {
                    _equipped = value;
                    OnValueChange(nameof(Equipped), value);
                }
            }
        }

        private ArmorLayer _layer;
        public ArmorLayer Layer
        {
            get => _layer;
            set
            {
                if (_layer != value)
                {
                    _layer = value;
                    OnValueChange(nameof(Layer), value);
                }
            }
        }

        private ArmorSlot _slot;
        public ArmorSlot Slot
        {
            get => _slot;
            set
            {
                if (_slot != value)
                {
                    _slot = value;
                    OnValueChange(nameof(Slot), value);
                }
            }
        }

        private byte _bludgeoning;
        public byte Bludgeoning
        {
            get => _bludgeoning;
            set
            {
                if (_bludgeoning != value)
                {
                    _bludgeoning = value;
                    OnValueChange(nameof(Bludgeoning), value);
                }
            }
        }

        private byte _piercing;
        public byte Piercing
        {
            get => _piercing;
            set
            {
                if (_piercing != value)
                {
                    _piercing = value;
                    OnValueChange(nameof(Piercing), value);
                }
            }
        }

        private byte _slashing;
        public byte Slashing
        {
            get => _slashing;
            set
            {
                if (_slashing != value)
                {
                    _slashing = value;
                    OnValueChange(nameof(Slashing), value);
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

        public CharacterArmorCO(ICharacterArmor armor)
        {
            ItemID = armor.ItemID; 
            _name = armor.Name;
            _description = armor.Description;
            _weight = armor.Weight;
            _hindrance = armor.Hindrance;
            _equipped = armor.Equipped;
            _layer = armor.Layer;
            _slot = armor.Slot;
            _bludgeoning = armor.Bludgeoning;
            _piercing = armor.Piercing;
            _slashing = armor.Slashing;
            Modified = false; 
        }
    }
}
