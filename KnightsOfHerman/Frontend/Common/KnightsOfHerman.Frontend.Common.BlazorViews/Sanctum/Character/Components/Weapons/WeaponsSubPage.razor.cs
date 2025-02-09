using KnightsOfHerman.Frontend.Common.Model.Sanctum.Character.Implementation;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnightsOfHerman.Frontend.Common.BlazorViews.Sanctum.Character.Components.Weapons
{
    public partial class WeaponsSubPage
    {
        [Parameter]
        public required CharacterViewModel Model { get; set; }
    }
}
