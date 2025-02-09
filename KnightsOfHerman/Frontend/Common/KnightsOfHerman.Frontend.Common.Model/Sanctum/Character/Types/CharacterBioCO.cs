using KnightsOfHerman.Common.Abstract;
using KnightsOfHerman.Common.Abstract.Modification;
using KnightsOfHerman.Common.Sanctum.Abstract.Character;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnightsOfHerman.Frontend.Common.Model.Sanctum.Character.Types
{
    /// <summary>
    /// Client Implementation of CharacterBio
    /// </summary>
    public class CharacterBioCO : ICharacterBio, INotifyModificationPath
    {
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

        private string _race;
        public string Race
        {
            get => _race;
            set
            {
                if (_race != value)
                {
                    _race = value;
                    OnValueChange(nameof(Race), value);
                }
            }
        }

        private string _background;
        public string Background
        {
            get => _background;
            set
            {
                if (_background != value)
                {
                    _background = value;
                    OnValueChange(nameof(Background), value);
                }
            }
        }

        private string _campaign;
        public string Campaign
        {
            get => _campaign;
            set
            {
                if (_campaign != value)
                {
                    _campaign = value;
                    OnValueChange(nameof(Campaign), value);
                }
            }
        }

        private int _campaignID;
        public int CampaignID
        {
            get => _campaignID;
            set
            {
                if (_campaignID != value)
                {
                    _campaignID = value;
                    OnValueChange(nameof(CampaignID), value);
                }
            }
        }

        private int _totalXP;
        public int TotalXP
        {
            get => _totalXP;
            set
            {
                if (_totalXP != value)
                {
                    _totalXP = value;
                    OnValueChange(nameof(TotalXP), value);
                }
            }
        }

        private int _unspentXP;
        public int UnspentXP
        {
            get => _unspentXP;
            set
            {
                if (_unspentXP != value)
                {
                    _unspentXP = value;
                    OnValueChange(nameof(UnspentXP), value);
                }
            }
        }

        private byte _destiny;
        public byte Destiny
        {
            get => _destiny;
            set
            {
                if (_destiny != value)
                {
                    _destiny = value;
                    OnValueChange(nameof(Destiny), value);
                }
            }
        }

        private byte _conflict;
        public byte Conflict
        {
            get => _conflict;
            set
            {
                if (_conflict != value)
                {
                    _conflict = value;
                    OnValueChange(nameof(Conflict), value);
                }
            }
        }

        private string _languages;
        public string Languages
        {
            get => _languages;
            set
            {
                if (_languages != value)
                {
                    _languages = value;
                    OnValueChange(nameof(Languages), value);
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

        public CharacterBioCO(ICharacterBio bio)
        {
            _name = bio.Name;
            _race = bio.Race;
            _background = bio.Background;
            _campaign = bio.Campaign;
            _campaignID = bio.CampaignID;
            _totalXP = bio.TotalXP;
            _unspentXP = bio.UnspentXP;
            _destiny = bio.Destiny;
            _conflict = bio.Conflict;
            _languages = bio.Languages;
            Modified = false;
        }
    }
}
