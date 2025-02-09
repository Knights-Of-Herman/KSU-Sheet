using KnightsOfHerman.Common.User;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace KnightsOfHerman.Frontend.Common.BlazorViews.Account
{
    public partial class LoginView
    {
        bool _checking = false;

        [Parameter]
        public required Action GoToPreviousPage { get; set; }

        [Parameter]
        public required Action GoToRegister { get; set; }

        [Parameter]
        public required Action Refresh { get; set; }

        string Email
        {
            get => _email;
            set
            {
                _email = value;
                StateHasChanged();
            }
        }

        string _email = string.Empty;

        string Password
        {
            get => _password;
            set
            {
                _password = value;
                StateHasChanged();
            }
        }

        string _password = string.Empty;

        bool _showAlert = false;

        bool _disableLogin => string.IsNullOrWhiteSpace(Email) || string.IsNullOrWhiteSpace(Password) || _checking;



        async Task TryLoginAsync()
        {
            _showAlert = false;
            _checking = true;
            await InvokeAsync(StateHasChanged);

            LoginModel model = new LoginModel()
            {
                Email = _email,
                Password = _password
            };

            var result = await _account.TryLoginAsync(model);
            if (result)
            {
                await InvokeAsync(StateHasChanged);
                _checking = false;
                GoToPreviousPage();
            }
            else
            {
                _showAlert = true;
                _checking = false;
                await InvokeAsync(StateHasChanged);
            }
        }

        async Task Logout()
        {
            await _account.TryLogoutAsync();
            Refresh();
        }

    }
}
