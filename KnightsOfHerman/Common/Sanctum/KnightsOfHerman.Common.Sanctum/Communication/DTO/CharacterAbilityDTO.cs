using KnightsOfHerman.Common.Sanctum.Abstract.Character.Abilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnightsOfHerman.Common.Sanctum.Communication.DTO
{
    /// <summary>
    /// Data Transfer Obejct of CharacterAbility
    /// </summary>
    public class CharacterAbilityDTO : ICharacterAbility
    {
        public int AbilityID { get; set; }
        public AbilityType AbilityType { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Cost { get; set; }
        public bool Memorized { get; set; }

        public CharacterAbilityDTO(ICharacterAbility ability)
        {
            AbilityID = ability.AbilityID;
            AbilityType = ability.AbilityType;
            Title = ability.Title;
            Content = ability.Content;
            Cost = ability.Cost;
            Memorized = ability.Memorized;
        }

        public CharacterAbilityDTO() { }
    }
}
