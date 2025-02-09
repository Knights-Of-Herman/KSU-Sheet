using KnightsOfHerman.Common.Sanctum.Abstract.Character;
using KnightsOfHerman.Common.Sanctum.Communication.DTO;
using KnightsOfHerman.Frontend.Common.BlazorViews.Sanctum.Character.Components.Armor;
using KnightsOfHerman.Frontend.Common.Model.Sanctum.Interfaces;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MudBlazor.CategoryTypes;

namespace KnightsOfHerman.Frontend.Common.BlazorViews.Sanctum.CharacterList
{
    public partial class CharacterListView
    {
        [Parameter]
        public required Action<int> GoToCharacter { get; set; }

        [Inject]
        public required AuthenticationStateProvider Auth { get; set; }

        [Inject]
        public required IDialogService Dialog { get; set; }

        [Inject]
        public required ISnackbar Snackbar { get; set; }

        [Inject]
        public required ICharacterListAccess CharactersAccess { get; set; }

        bool _loading = true;

        List<CharacterProfile> profiles = new();

        List<CharacterProfile> _myProfiles => profiles.Where(x => x.AccessLevel == CharacterAccess.Owner).ToList();

        List<CharacterProfile> _sharedProfiles => profiles.Where(x => x.AccessLevel != CharacterAccess.Owner).ToList();

        void GoToCharacterPage(int id)
        {
            var idString = id.ToString();
            GoToCharacter(id);
            //_nav.NavigateTo($"/character?id={Uri.EscapeDataString(idString)}");

        }

        void DuplicateCharacter(int id)
        {
            Snackbar.Add("Duplicate Not Implemented Yet", Severity.Warning);
        }

        async void UnsubscribeCharacter(int characterID)
        {
            await CharactersAccess.UnsubscribeCharacter(characterID);
            await LoadProfiles();
        }

        async Task LoadProfiles()
        {
            _loading = true;
            await InvokeAsync(StateHasChanged);

            var result2 = await CharactersAccess.GetProfilesAsync();
            if (result2.IsSuccess && result2.Value != null)
            {
                profiles = result2.Value;
            }
            else
            {
				Snackbar.Add($"Couldn't Load Characters", Severity.Error);
			}

			_loading = false;
            await InvokeAsync(StateHasChanged);
        }

        async void CreateCharacter()
        {
            var response = await CharactersAccess.CreateBlankChracter();

            await LoadProfiles();

            if (response.IsSuccess)
            {
            }
            else
            {
                Snackbar.Add($"Couldn't add character", Severity.Error);
            }
        }

        async void DeleteCharacter(int id, string name)
        {
            var result = await CharactersAccess.DeleteCharacterAsync(id);

            await LoadProfiles();
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                var state = await Auth.GetAuthenticationStateAsync();
                var user = state.User;

                if (user.Identity != null && user.Identity.IsAuthenticated)
                {
                    await LoadProfiles();
                }
            }
            await base.OnAfterRenderAsync(firstRender);

        }



        void OpenCharacterShare(CharacterProfile profile)
        {
            DialogOptions _dialogOptions = new DialogOptions()
            {
                MaxWidth = MaxWidth.Medium,
                CloseButton = true,
                Position = DialogPosition.TopCenter
            };
            var parameters = new DialogParameters<ShareCharacterDialog>();
            parameters.Add(x => x.Profile, profile);
            Dialog.Show<ShareCharacterDialog>($"{profile.Name} Sharing Settings", parameters, _dialogOptions);
        }
    }
}
