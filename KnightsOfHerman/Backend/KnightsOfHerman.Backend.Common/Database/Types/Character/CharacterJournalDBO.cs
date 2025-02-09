using KnightsOfHerman.Common.Sanctum.Abstract.Character.Notes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnightsOfHerman.Backend.Common.Database.Types.Character
{
    /// <summary>
    /// CharacterJournal Object for Database operation
    /// </summary>
    public class CharacterJournalDBO : ICharacterJournal
    {
        public int JournalID { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public JournalCategory Category { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
