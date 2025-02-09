using KnightsOfHerman.Common.Sanctum.Abstract.Character;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace KnightsOfHerman.Common.Sanctum.Communication.DTO
{
    /// <summary>
    /// Data Transfer Obejct of CharacterProfile
    /// </summary>
    public class CharacterProfile
    {
        public CharacterProfile(int ID)
        {
            CharacterID = ID;
        }

        public CharacterProfile()
        {
            
        }

        public CharacterAccess AccessLevel { get; set; }
        public int CharacterID { get; set; }

        public string Name { get; set; }

        public string Race { get; set; }

        public int CampaignID { get; set; }

        public string Campaign { get; set; }

        public int TotalXP { get; set; }
    }
}
