using KnightsOfHerman.Common.Sanctum.Abstract.Character.Abilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnightsOfHerman.Backend.Common.Database.Types.Character
{
    /// <summary>
    /// CharacterAbility Object for Database operation
    /// </summary>
    public class CharacterAbilityDBO : ICharacterAbility
    {
        public int AbilityID { get; set; }

        public AbilityType AbilityType { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public string Cost { get; set; }

        public bool Memorized { get; set; }
    }
}
