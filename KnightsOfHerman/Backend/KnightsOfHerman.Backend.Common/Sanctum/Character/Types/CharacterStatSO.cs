using KnightsOfHerman.Backend.Common.Database.Types.Character;
using KnightsOfHerman.Common.Abstract.Modification;
using KnightsOfHerman.Common.Sanctum.Abstract.Character.Stats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnightsOfHerman.Backend.Common.Sanctum.Character.Types
{
    /// <summary>
    /// Server Side Implmentation of ICharacterStat
    /// </summary>
    public class CharacterStatSO : ITrackModifed, ICharacterStat
    {
        public bool Modified { get; set; }

        public CharacterStats StatID { get; }

        short _base;
        public short Base {
            get => _base;
            set 
            {
                if(_base != value)
                {
                    _base = value;
                    Modified = true;
                }
            }
        }

        private short _customMod;
        public short CustomMod
        {
            get => _customMod;
            set
            {
                if (_customMod != value)
                {
                    _customMod = value;
                    Modified = true;
                }
            }
        }

        private short _overrideValue;
        public short OverrideValue
        {
            get => _overrideValue;
            set
            {
                if (_overrideValue != value)
                {
                    _overrideValue = value;
                    Modified = true;
                }
            }
        }

        private bool _doOverride;
        public bool DoOverride
        {
            get => _doOverride;
            set
            {
                if (_doOverride != value)
                {
                    _doOverride = value;
                    Modified = true;
                }
            }
        }

        public CharacterStatSO()
        {

        }

        public CharacterStatSO(CharacterStats stat)
        {
            StatID = stat;
        }

        public CharacterStatSO(CharacterStatDBO dbo)
        {
            Modified = false;
            StatID = dbo.StatID;
            _base = dbo.Base;
            CustomMod = dbo.CustomMod;
            OverrideValue = dbo.OverrideValue;
            DoOverride = dbo.DoOverride;
        } 
    }
}
