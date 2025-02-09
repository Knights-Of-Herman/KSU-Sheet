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
    /// Client Implementation of CharacterStat
    /// </summary>
    public class CharacterStatCO : ICharacterStat, INotifyModificationPath
    {
        public int Total
        {
            get
            {
                if (DoOverride) return OverrideValue;
                return Base + CustomMod; 
            }
        }

        public event TrackedModificationEventHandler? OnTrackedModification;


        public short _base;

        public short Base
        {
            get => _base;
            set
            {
                if (_base != value)
                {
                    _base = value;
                    OnValueChange(nameof(Base), value);
                }
            }
        }

        short _customMod;
        public short CustomMod
        {
            get => _customMod;
            set
            {
                if (_customMod != value)
                {
                    _customMod = value;
                    OnValueChange(nameof(CustomMod), value);
                }
            }
        }

        short _override;
        public short OverrideValue
        {
            get => _override;
            set
            {
                if (_override != value)
                {
                    _override = value;
                    OnValueChange(nameof(OverrideValue), value);
                }
            }
        }

        bool _doOverride;
        public bool DoOverride
        {
            get => _doOverride;
            set
            {
                if (_doOverride != value)
                {
                    _doOverride = value;
                    OnValueChange(nameof(DoOverride), value);
                }
            }
        }

        public bool Modified { get; set; }

        public CharacterStats StatID { get; private set; }

        public CharacterStatCO(ICharacterStat stat)
        {
            _base = stat.Base;
            _customMod = stat.CustomMod;
            _doOverride = stat.DoOverride;
            _override = stat.OverrideValue;
            StatID = stat.StatID;
        }

        void OnValueChange(string name, object value)
        {
            Modified = true;
            OnTrackedModification?.Invoke(this, new TrackedModificationEventArgs(name, value, EditActions.Edit));
        }

        public bool TryModifyByPath(string path, object value, EditActions action)
        {
            if (path == nameof(Base) && value.GetType() == typeof(int))
            {
                _base = (sbyte)value;
                return true;
            }
            if (path == nameof(CustomMod) && value.GetType() == typeof(int))
            {
                _customMod = (sbyte)value;
                return true;
            }
            if (path == nameof(OverrideValue) && value.GetType() == typeof(int))
            {
                _override = (sbyte)value;
                return true;
            }
            if (path == nameof(DoOverride) && value.GetType() == typeof(bool))
            {
                _doOverride = (bool)value;
                return true;
            }
            return false;
        }
    }
}
