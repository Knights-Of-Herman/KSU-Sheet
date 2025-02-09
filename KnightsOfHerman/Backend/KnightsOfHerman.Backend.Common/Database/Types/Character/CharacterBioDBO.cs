using KnightsOfHerman.Common.Sanctum.Abstract.Character;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnightsOfHerman.Backend.Common.Database.Types.Character
{
    /// <summary>
    /// CharacterBIo Object for Database operation
    /// </summary>
    public class CharacterBioDBO : ICharacterBio
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

    }
}
