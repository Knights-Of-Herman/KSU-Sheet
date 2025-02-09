using KnightsOfHerman.Common.Sanctum.Abstract.Character;
using KnightsOfHerman.Common.Sanctum.Abstract.Character.Abilities;
using KnightsOfHerman.Common.Sanctum.Abstract.Character.Items;
using KnightsOfHerman.Common.Sanctum.Abstract.Character.Notes;
using KnightsOfHerman.Common.Sanctum.Abstract.Character.Stats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace KnightsOfHerman.Common.Sanctum.Communication.DTO
{
    /// <summary>
    /// Data Transfer Obejct of Character
    /// </summary>
    public class CharacterDTO
    {
        public int CharacterID { get; set; }

        public CharacterAccess Access { get; set; }

        public Dictionary<int, CharacterWeaponDTO> Weapons { get; set; }

        public Dictionary<int, CharacterItemDTO> Items { get; set; }

        public Dictionary<int, CharacterArmorDTO> Armor { get; set; }

        public Dictionary<int, CharacterJournalDTO> Journal { get; set; }
        public Dictionary<int, CharacterAbilityDTO> Abilities { get; set; }

        public CharacterBioDTO Bio { get; set; }

        public Dictionary<CharacterStats, CharacterStatDTO> Stats { get; set; }
        public Dictionary<CharacterResources, CharacterResourceDTO> Resources { get; set; }
    }
}
