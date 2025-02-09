using KnightsOfHerman.Common.Abstract.Modification;
using KnightsOfHerman.Common.Sanctum.Abstract.Character.Abilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnightsOfHerman.Backend.Common.Sanctum.Character.Types
{
    /// <summary>
    /// Server Side Implmentation of ICharacterAbility
    /// </summary>
    public class CharacterAbilitySO : ITrackModifed, ICharacterAbility
    {
        public bool Modified { get; set; }

        private int _abilityID;
        public int AbilityID
        {
            get => _abilityID;
            set
            {
                if (_abilityID != value)
                {
                    _abilityID = value;
                    Modified = true;
                }
            }
        }

        private AbilityType _abilityType;
        public AbilityType AbilityType
        {
            get => _abilityType;
            set
            {
                if (_abilityType != value)
                {
                    _abilityType = value;
                    Modified = true;
                }
            }
        }

        private string _title;
        public string Title
        {
            get => _title;
            set
            {
                if (_title != value)
                {
                    _title = value;
                    Modified = true;
                }
            }
        }

        private string _content;
        public string Content
        {
            get => _content;
            set
            {
                if (_content != value)
                {
                    _content = value;
                    Modified = true;
                }
            }
        }

        private string _cost;
        public string Cost
        {
            get => _cost;
            set
            {
                if (_cost != value)
                {
                    _cost = value;
                    Modified = true;
                }
            }
        }

        private bool _memorized;
        public bool Memorized
        {
            get => _memorized;
            set
            {
                if (_memorized != value)
                {
                    _memorized = value;
                    Modified = true;
                }
            }
        }

        public CharacterAbilitySO(ICharacterAbility ability)
        {
            _abilityID = ability.AbilityID;
            _abilityType = ability.AbilityType;
            _title = ability.Title;
            _content = ability.Content;
            _cost = ability.Cost;
            _memorized = ability.Memorized;
            Modified = false;
        }
    }
}
