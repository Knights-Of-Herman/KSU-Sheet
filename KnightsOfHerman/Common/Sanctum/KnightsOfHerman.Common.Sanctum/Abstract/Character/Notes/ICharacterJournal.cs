using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnightsOfHerman.Common.Sanctum.Abstract.Character.Notes
{
    /// <summary>
    /// Character's Journal Entry to take notes on
    /// </summary>
    public interface ICharacterJournal
    {
        /// <summary>
        /// ID of the Entry
        /// </summary>
        int JournalID { get; }

        /// <summary>
        /// Title of the entry
        /// </summary>
        string Title { get; set; }

        /// <summary>
        /// The content of the entry
        /// </summary>
        string Content { get; set; }

        /// <summary>
        /// When the entry was created
        /// </summary>
        DateTime CreateDate { get; set; }

        /// <summary>
        /// What type of entry
        /// </summary>
        JournalCategory Category { get; }
    }
}
