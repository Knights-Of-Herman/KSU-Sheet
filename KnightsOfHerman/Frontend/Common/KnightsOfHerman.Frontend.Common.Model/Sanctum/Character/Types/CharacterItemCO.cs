using KnightsOfHerman.Common.Abstract;
using KnightsOfHerman.Common.Abstract.Modification;
using KnightsOfHerman.Common.Sanctum.Abstract.Character.Items;
using KnightsOfHerman.Common.Sanctum.Communication.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnightsOfHerman.Frontend.Common.Model.Sanctum.Character.Types
{
    /// <summary>
    /// Client Implementation of CharacterItem
    /// </summary>
    public class CharacterItemCO : ICharacterItem, INotifyModificationPath
    {
        private int _itemID;
  
        public int ItemID
        {
            get => _itemID;
            private set
            {
                if (_itemID != value)
                {
                    _itemID = value;
                    OnValueChange(nameof(ItemID), value);
                }
            }
        }
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

        public bool Modified { get; set; }

        public event TrackedModificationEventHandler? OnTrackedModification;

        public CharacterItemCO(ICharacterItem dto)
        {
            _itemID = dto.ItemID;
            _name = dto.Name;
            _description = dto.Description;
            _weight = dto.Weight;
            _quantity = dto.Quantity;
            Modified = false;
        }

        private void OnValueChange(string name, object value)
        {
            Modified = true;
            OnTrackedModification?.Invoke(this, new TrackedModificationEventArgs(name, value, EditActions.Edit));
        }
    }
}
