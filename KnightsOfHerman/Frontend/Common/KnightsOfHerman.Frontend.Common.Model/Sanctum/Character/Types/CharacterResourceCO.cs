using KnightsOfHerman.Common.Abstract;
using KnightsOfHerman.Common.Abstract.Modification;
using KnightsOfHerman.Common.Sanctum.Abstract.Character.Stats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnightsOfHerman.Frontend.Common.Model.Sanctum.Character.Types
{
    /// <summary>
    /// Client Implementation of CharacterResource
    /// </summary>
    public class CharacterResourceCO : ICharacterResource, INotifyModificationPath
    {
        private short _attribute;
        public short Attribute
        {
            get => _attribute;
            set
            {
                if (_attribute != value)
                {
                    _attribute = value;
                    OnValueChange(nameof(Attribute), value);
                }
            }
        }

        private short _modifier;
        public short Modifier
        {
            get => _modifier;
            set
            {
                if (_modifier != value)
                {
                    _modifier = value;
                    OnValueChange(nameof(Modifier), value);
                }
            }
        }

        private bool _doOverrideMax;
        public bool DoOverrideMax
        {
            get => _doOverrideMax;
            set
            {
                if (_doOverrideMax != value)
                {
                    _doOverrideMax = value;
                    OnValueChange(nameof(DoOverrideMax), value);
                }
            }
        }

        private short _overrideMaxValue;
        public short OverrideMaxValue
        {
            get => _overrideMaxValue;
            set
            {
                if (_overrideMaxValue != value)
                {
                    _overrideMaxValue = value;
                    OnValueChange(nameof(OverrideMaxValue), value);
                }
            }
        }

        public int Total
        {
            get
            {
                if (DoOverrideMax) return OverrideMaxValue;
                return Attribute * Modifier;
            }
        }

        private short _currentValue;
        public short CurrentValue
        {
            get => _currentValue;
            set
            {
                if (_currentValue != value)
                {
                    _currentValue = value;
                    OnValueChange(nameof(CurrentValue), value);
                }
            }
        }

        public bool Modified { get; set; }

        public CharacterResources ResourceID { get; private set; }

        public event TrackedModificationEventHandler? OnTrackedModification;

        private void OnValueChange(string name, object value)
        {
            Modified = true;
            OnTrackedModification?.Invoke(this, new TrackedModificationEventArgs(name, value, EditActions.Edit));
        }

        public CharacterResourceCO(ICharacterResource resource)
        {
            _attribute = resource.Attribute;
            _modifier = resource.Modifier;
            _doOverrideMax = resource.DoOverrideMax;
            _overrideMaxValue = resource.OverrideMaxValue;
            _currentValue = resource.CurrentValue;
            ResourceID = resource.ResourceID;
        }
    }
}
