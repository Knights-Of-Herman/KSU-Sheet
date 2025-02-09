using KnightsOfHerman.Backend.Common.Database.Types.Character;
using KnightsOfHerman.Common.Abstract.Modification;
using KnightsOfHerman.Common.Sanctum.Abstract.Character;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnightsOfHerman.Backend.Common.Sanctum.Character.Types
{
    /// <summary>
    /// Server Side Implmentation of ICharacterBio
    /// </summary>
    public class CharacterBioSO : ITrackModifed, ICharacterBio
    {
        public bool Modified { get; set; }

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

        private string _race;
        public string Race
        {
            get => _race;
            set
            {
                if (_race != value)
                {
                    _race = value;
                    Modified = true;
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
                    Modified = true;
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
                    Modified = true;
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
                    Modified = true;
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
                    Modified = true;
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
                    Modified = true;
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
                    Modified = true;
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
                    Modified = true;
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
                    Modified = true;
                }
            }
        }

        public CharacterBioSO(ICharacterBio dbo)
        {
            _name = dbo.Name;
            _race = dbo.Race;
            _background = dbo.Background;
            _campaign = dbo.Campaign;
            _campaignID = dbo.CampaignID;
            _totalXP = dbo.TotalXP;
            _unspentXP = dbo.UnspentXP;
            _destiny = dbo.Destiny;
            _conflict = dbo.Conflict;
            _languages = dbo.Languages;
            Modified = false;
        }

        public CharacterBioSO()
        {
        }
    }
}
