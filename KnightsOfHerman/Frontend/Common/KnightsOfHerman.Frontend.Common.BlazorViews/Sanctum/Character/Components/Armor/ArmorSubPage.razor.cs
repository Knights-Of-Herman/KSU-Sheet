using KnightsOfHerman.Common.Abstract.Modification;
using KnightsOfHerman.Frontend.Common.Model.Sanctum.Character.Implementation;
using KnightsOfHerman.Frontend.Common.Model.Sanctum.Character.Types;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnightsOfHerman.Frontend.Common.BlazorViews.Sanctum.Character.Components.Armor
{
    public partial class ArmorSubPage
    {
        [Parameter]
        public required CharacterViewModel Model { get; set; }

        public ArmorSubPage()
        {
			var x = Icons.Material.Filled.Abc;
		}

        private async void RefreshViewAsync()
        {
            _dropper.Refresh();
            await InvokeAsync(StateHasChanged);
        }

		MudDropContainer<CharacterArmorCO> _dropper;

        protected override void OnParametersSet()
        {
            base.OnParametersSet();
            Model.Character.Armor.OnTrackedModification += (x,y) => OnChange(x,y);
            Model.OnRefreshView += RefreshViewAsync;
        }

        async void OnChange(object x, TrackedModificationEventArgs y)
        {
            //_dropper.Refresh();
            //await InvokeAsync(StateHasChanged);
        }

    }
}
