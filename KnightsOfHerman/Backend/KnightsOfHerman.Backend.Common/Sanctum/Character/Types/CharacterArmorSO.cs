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
    /// Server Side Implmentation of ICharacterArmor
    /// </summary>
    public class CharacterArmorSO : ITrackModifed, ICharacterArmor
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

        private byte _hindrance;
        public byte Hindrance
        {
            get => _hindrance;
            set
            {
                if (_hindrance != value)
                {
                    _hindrance = value;
                    Modified = true;
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
                    Modified = true;
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
                    Modified = true;
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
                    Modified = true;
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
                    Modified = true;
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
                    Modified = true;
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
                    Modified = true;
                }
            }
        }

        public CharacterArmorSO(ICharacterArmor armor)
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
