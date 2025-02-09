using KnightsOfHerman.Common.Sanctum.Abstract.Character.Notes;
using KnightsOfHerman.Frontend.Common.BlazorViews.Sanctum.Character.Components.Armor;
using KnightsOfHerman.Frontend.Common.Model.Sanctum.Character.Implementation;
using KnightsOfHerman.Frontend.Common.Model.Sanctum.Character.Types;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MudBlazor.CategoryTypes;

namespace KnightsOfHerman.Frontend.Common.BlazorViews.Sanctum.Character.Components.Journal
{
    public partial class JournalSubPage
    {
        public enum JournalSortBy
        {
            Oldest,
            Latest
        }

        [Inject]
        public required IDialogService Dialog { get; set; }

        [Parameter]
        public required CharacterViewModel Model { get; set; }

        MudPagination _pager;
        private int CurrentPage = 1;
        private int PageSize = 10;
        private int TotalJournals => _journals.Count;
        private int TotalPages => (int)Math.Ceiling(TotalJournals / (double)PageSize);
        IEnumerable<CharacterJournalCO> PaginatedJournals => (IEnumerable<CharacterJournalCO>)_journals.Skip((CurrentPage - 1) * PageSize).Take(PageSize);

        //Reference to Model.Character
        CharacterCO Character => Model.Character;

        //Reference to Model.Character.Journal
        IDictionary<int, CharacterJournalCO> Journal => Character.Journal;

        string _search = "";

        JournalSortBy _sort = JournalSortBy.Latest;

        JournalCategory _category = JournalCategory.General;

        bool _open = false;

        string _activeSearch = "";

        public List<CharacterJournalCO> _journals
        {
            get
            {
                var list = Journal.Values.Where(x => x.Category == _category && x.Title.ToLower().Contains(_activeSearch.ToLower()));

                if (_sort == JournalSortBy.Oldest)
                {
                    list = list.OrderBy(x => x.CreateDate);
                } else
                {
                    list = list.OrderByDescending(x => x.CreateDate);
                }
                return list.ToList();
            }
        }

        void Search()
        {
            _activeSearch = _search;
            StateHasChanged();
        }

        async Task AddJournal()
        {
            await Model.CreateJournal(_category);
        }


        public void OpenJournal(CharacterJournalCO journal)
        {
            DialogOptions _dialogOptions = new DialogOptions()
            {
                MaxWidth = MaxWidth.Large,
                CloseButton = true,
            };
            var parameters = new DialogParameters<JournalEntryDialog>();
            parameters.Add(x => x.Journal, journal);
            parameters.Add(x => x.Model, Model);
            Dialog.Show<JournalEntryDialog>($"Journal Entry", parameters, _dialogOptions);
        }
    }
}
