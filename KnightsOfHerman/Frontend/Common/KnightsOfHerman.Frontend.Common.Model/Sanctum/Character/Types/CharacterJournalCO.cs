using KnightsOfHerman.Common.Abstract;
using KnightsOfHerman.Common.Abstract.Modification;
using KnightsOfHerman.Common.Sanctum.Abstract.Character.Notes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnightsOfHerman.Frontend.Common.Model.Sanctum.Character.Types
{
    /// <summary>
    /// Client Implementation of CharacterJournal
    /// </summary>
    public class CharacterJournalCO : ICharacterJournal, INotifyModificationPath
    {
        public int JournalID { get; private set; }

        private string _title;
        public string Title
        {
            get => _title;
            set
            {
                if (_title != value)
                {
                    _title = value;
                    OnValueChange(nameof(Title), value);
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
                    OnValueChange(nameof(Content), value);
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
                    OnValueChange(nameof(CreateDate), value);
                }
            }
        }

        public JournalCategory Category { get; private set; }

        public bool Modified { get; set; }

        public event TrackedModificationEventHandler? OnTrackedModification;

        private void OnValueChange(string name, object value)
        {
            Modified = true;
            OnTrackedModification?.Invoke(this, new TrackedModificationEventArgs(name, value, EditActions.Edit));
        }

        public CharacterJournalCO(ICharacterJournal journal)
        {
            JournalID = journal.JournalID; 
            _title = journal.Title;
            _content = journal.Content;
            _createDate = journal.CreateDate; 
            Category = journal.Category;
            Modified = false; 
        }
    }
}
