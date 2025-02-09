using KnightsOfHerman.Common.Abstract.Modification;
using KnightsOfHerman.Common.Abstract;
using KnightsOfHerman.Common.Sanctum.Abstract.Character.Abilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnightsOfHerman.Frontend.Common.Model.Sanctum.Character.Types
{
    /// <summary>
    /// Client Implementation of CharacterAbility
    /// </summary>
    public class CharacterAbilityCO : ICharacterAbility, INotifyModificationPath
    {
        public int AbilityID { get; private set; }
        public AbilityType AbilityType { get; private set; }

        private string _title;
        public string Title
        {
            get => _title;
            set
            {
                if (_title != value)
                {
                    _title = value;
                    OnValueChange(nameof(Title), value);
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
                    OnValueChange(nameof(Content), value);
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
                    OnValueChange(nameof(Cost), value);
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
                    OnValueChange(nameof(Memorized), value);
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

        public CharacterAbilityCO(ICharacterAbility ability)
        {
            AbilityID = ability.AbilityID; 
            AbilityType = ability.AbilityType;
            _content = ability.Content;
            _cost = ability.Cost;
            _memorized = ability.Memorized;
            _title = ability.Title;
            Modified = false; 
        }
    }
}
