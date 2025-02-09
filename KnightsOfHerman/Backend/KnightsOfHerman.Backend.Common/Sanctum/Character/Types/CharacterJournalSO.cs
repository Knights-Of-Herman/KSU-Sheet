using KnightsOfHerman.Common.Abstract.Modification;
using KnightsOfHerman.Common.Sanctum.Abstract.Character.Notes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace KnightsOfHerman.Backend.Common.Sanctum.Character.Types
{
    /// <summary>
    /// Server Side Implmentation of ICharacterJournal
    /// </summary>
    public class CharacterJournalSO : ITrackModifed, ICharacterJournal
    {
        public bool Modified { get; set; }

        public int JournalID { get; }

        private string _title;
        public string Title
        {
            get => _title;
            set
            {
                if (_title != value)
                {
                    _title = value;
                    Modified = true;
                }
            }
        }

        private string _content;
        public string Content
        {
            get => _content;
            set
            {
                if (_content != value)
                {
                    _content = value;
                    Modified = true;
                }
            }
        }

        private DateTime _createDate;
        public DateTime CreateDate
        {
            get => _createDate;
            set
            {
                if (_createDate != value)
                {
                    _createDate = value;
                    Modified = true;
                }
            }
        }
        public JournalCategory Category { get; }

        public CharacterJournalSO(ICharacterJournal entry)
        {
            Title = entry.Title;
            Content = entry.Content;
            CreateDate = entry.CreateDate;
            JournalID = entry.JournalID;
            Category = entry.Category;
            Modified = false;
        }
    }
}
