using KnightsOfHerman.Common.Sanctum.Abstract.Character.Notes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnightsOfHerman.Common.Sanctum.Communication.DTO
{
    /// <summary>
    /// Data Transfer Obejct of CharacterJournal
    /// </summary>
    public class CharacterJournalDTO : ICharacterJournal
    {
        public string Title { get; set; } = "";
        public string Content { get; set; } = "";
        public DateTime CreateDate { get; set; } = DateTime.Now;

        public int JournalID { get; set; }
        public JournalCategory Category { get; set; }

        public CharacterJournalDTO(ICharacterJournal entry)
        {
            Title = entry.Title;
            Content = entry.Content;
            CreateDate = entry.CreateDate;
            JournalID = entry.JournalID;
            Category = entry.Category;
        }

        public CharacterJournalDTO() { }
    }
}
