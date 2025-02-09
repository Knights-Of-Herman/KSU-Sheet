using KnightsOfHerman.Common.Sanctum.Abstract.Character;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnightsOfHerman.Common.Sanctum.Communication.DTO
{

    /// <summary>
    /// Data Transfer Obejct of CharacterBio
    /// </summary>
    public class CharacterBioDTO : ICharacterBio
    {
        public string Name { get; set; }
        public string Race { get; set; }
        public string Background { get; set; }
        public string Campaign { get; set; }
        public int CampaignID { get; set; }
        public int TotalXP { get; set; }
        public int UnspentXP { get; set; }
        public byte Destiny { get; set; }
        public byte Conflict { get; set; }
        public string Languages { get; set; }

        public CharacterBioDTO(ICharacterBio bio)
        {
            Name = bio.Name;
            Race = bio.Race;
            Background = bio.Background;
            Campaign = bio.Campaign;
            CampaignID = bio.CampaignID;
            TotalXP = bio.TotalXP;
            UnspentXP = bio.UnspentXP;
            Destiny = bio.Destiny;
            Conflict = bio.Conflict;
            Languages = bio.Languages;
        }
        public CharacterBioDTO() { }
    }
}
