using KnightsOfHerman.Common.Sanctum.Abstract.Character.Abilities;
using KnightsOfHerman.Common.Sanctum.Abstract.Character.Notes;
using KnightsOfHerman.Frontend.Common.BlazorViews.Sanctum.Character.Components.Journal;
using KnightsOfHerman.Frontend.Common.Model.Sanctum.Character.Implementation;
using KnightsOfHerman.Frontend.Common.Model.Sanctum.Character.Types;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static KnightsOfHerman.Frontend.Common.BlazorViews.Sanctum.Character.Components.Journal.JournalSubPage;

namespace KnightsOfHerman.Frontend.Common.BlazorViews.Sanctum.Character.Components.Abilities
{
    public partial class AbilitiesSubPage
    {
        [Inject]
        public required IDialogService Dialog { get; set; }

        [Parameter]
        public required CharacterViewModel Model { get; set; }

        MudPagination _pager;
        private int CurrentPage = 1;
        private int PageSize = 10;
        private int TotalJournals => _abilities.Count;
        private int TotalPages => (int)Math.Ceiling(TotalJournals / (double)PageSize);
        IEnumerable<CharacterAbilityCO> PaginatedAbilities => (IEnumerable<CharacterAbilityCO>)_abilities.Skip((CurrentPage - 1) * PageSize).Take(PageSize);

        //Reference to Model.Character
        CharacterCO Character => Model.Character;

        //Reference to Model.Character.Journal
        IDictionary<int, CharacterAbilityCO> Abilities => Character.Abilities;

        string _search = "";

        AbilityType _type = AbilityType.Spell;

        bool _open = false;

        string _activeSearch = "";

        public List<CharacterAbilityCO> _abilities
        {
            get
            {
                var list = Abilities.Values.Where(x => x.AbilityType == _type && x.Title.ToLower().Contains(_activeSearch.ToLower()));
                return list.ToList();
            }
        }

        void Search()
        {
            _activeSearch = _search;
            StateHasChanged();
        }

        async Task AddAbility()
        {
            await Model.CreateAbility(_type);
        }


        public void OpenAbility(CharacterAbilityCO ability)
        {
            DialogOptions _dialogOptions = new DialogOptions()
            {
                MaxWidth = MaxWidth.Large,
                CloseButton = true,
            };
            var parameters = new DialogParameters<AbilityDialog>();
            parameters.Add(x => x.Ability, ability);
            parameters.Add(x => x.Model, Model);
            Dialog.Show<AbilityDialog>($"Ability", parameters, _dialogOptions);
        }
    }
}

