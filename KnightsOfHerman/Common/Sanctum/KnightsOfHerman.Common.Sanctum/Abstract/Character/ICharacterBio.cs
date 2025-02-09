using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnightsOfHerman.Common.Sanctum.Abstract.Character
{
    /// <summary>
    /// Character's General information
    /// </summary>
    public interface ICharacterBio
    {
        /// <summary>
        /// Character's Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Character's Race
        /// </summary>
        public string Race { get; set; }

        /// <summary>
        /// Character's Background
        /// </summary>
        public string Background { get; set; }

        /// <summary>
        /// Name of the Campaign the character is associated with
        /// </summary>
        public string Campaign { get; set; }

        /// <summary>
        /// ID of the campaign the character is associated with
        /// </summary>
        public int CampaignID { get; set; }

        /// <summary>
        /// Character's Total amount of XP they've earned
        /// </summary>
        public int TotalXP { get; set; }

        /// <summary>
        /// Character's XP that has yet to be spent
        /// </summary>
        public int UnspentXP { get; set; }

        /// <summary>
        /// Character's Destiny Point amount
        /// </summary>
        public byte Destiny { get; set; }

        /// <summary>
        /// Amount of conflict the character has
        /// </summary>
        public byte Conflict { get; set; }

        /// <summary>
        /// The languages the character speaks
        /// </summary>
        public string Languages { get; set; }
    }
}
