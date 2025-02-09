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
    /// Server Side Implmentation of ICharacterResource
    /// </summary>
    public class CharacterResourceSO : ITrackModifed, ICharacterResource
    {
        public bool Modified { get; set; }

        short _currentValue;
        public short CurrentValue 
        {
            get => _currentValue;
            set
            {
                if(_currentValue != value)
                {
                    _currentValue = value;
                    Modified = true;
                }
            }
        }

        short _overrideMaxValue;
        public short OverrideMaxValue 
        {   get => _overrideMaxValue;
            set
            { 
                if(_overrideMaxValue != value)
                {
                    _overrideMaxValue = value;
                    Modified = true;
                }
            } 
        }

        bool _doOverrideMax;
        public bool DoOverrideMax
        {
            get => _doOverrideMax;
            set
            {
                if(_doOverrideMax != value)
                {
                    _doOverrideMax = value;
                    Modified = true;
                }
            }
        }

        short _attribute;
        public short Attribute
        {
            get => _attribute;
            set
            {
                if (_attribute != value)
                {
                    Modified = true;
                    _attribute = value;
                }
            }
        }

        short _modifier;
        public short Modifier
        {
            get => _modifier;
            set
            {
                if (_modifier != value)
                {
                    Modified = true;
                    _modifier = value;
                }
            }
        }

        public short Total => throw new NotImplementedException();

        public CharacterResources ResourceID { get; }

        public CharacterResourceSO() { } 

        public CharacterResourceSO(CharacterResourceDBO dbo)
        {
            Modified = false;
            _currentValue = dbo.CurrentValue;
            _doOverrideMax = dbo.DoOverrideMax;
            _overrideMaxValue = dbo.OverrideMaxValue;
            _attribute = dbo.Attribute;
            _modifier = dbo.Modifier;
            ResourceID = dbo.ResourceID;
        }

    }
}
