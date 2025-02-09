using KnightsOfHerman.Frontend.Common.BlazorViews.Sanctum.Character.Components.Abilities;
using KnightsOfHerman.Frontend.Common.Model.Sanctum.Character.Implementation;
using Microsoft.AspNetCore.Components;
using Microsoft.IdentityModel.Tokens;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnightsOfHerman.Frontend.Common.BlazorViews.Sanctum.Character
{
    public partial class CharacterView : IDisposable
    {
        [Inject]
        public required CharacterViewModelBuilder ModelBuilder { get; set; }

        [Parameter]
        public required int CharacterID { get; set; }

        [Parameter]
        public required Action GoToCharactersList { get; set; }

        [Parameter]
        public required Action GoToError { get; set; }

        CharacterViewModel Model;

        bool _loading = true;

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);

            if (firstRender)
            {
                var modelResult = await ModelBuilder.SetCharacterID(CharacterID)
                                    .BuildAsync();

                if (modelResult.IsSuccess && modelResult.Value != null)
                {
                    Model = modelResult.Value;
                    _loading = false;
                    Model.OnRefreshView += HandleRefresh;
                    Model.OnDelete += HandleDelete;
                    await InvokeAsync(StateHasChanged);
                }
                else
                {
                    GoToError();
                    //Somehow go to error page
                }
            }

        }

        [Inject]
        public required IDialogService Dialog { get; set; }

        void HandleDelete()
        {
            InvokeAsync(async () =>
            {
                var parameters = new DialogParameters<CharacterDeletedDialog>();
                parameters.Add(x => x.Name, Model.Character.Bio.Name);
                var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.Small, FullWidth = true };
                var dialog = Dialog.Show<CharacterDeletedDialog>("Character Deleted", parameters, options);
                var result = await dialog.Result;
                GoToCharactersList();
            });

        }

        async void HandleRefresh()
        {
            await InvokeAsync(StateHasChanged);
        }

        public void Dispose()
        {
            if(Model != null)
            {
                Model.OnRefreshView -= HandleRefresh;
                Model.OnDelete -= HandleDelete;
            }
            
        }
    }
}
