using KnightsOfHerman.Frontend.Common.Model.Sanctum.Character.Types;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnightsOfHerman.Frontend.Common.BlazorViews.Sanctum.Character.SubPages
{
    public partial class MainPage
    {
        [Parameter]
        public required CharacterCO Character { get; set; }
    }
}
