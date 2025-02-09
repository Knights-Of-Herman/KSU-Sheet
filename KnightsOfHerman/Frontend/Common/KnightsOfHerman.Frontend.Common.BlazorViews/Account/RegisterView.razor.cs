using KnightsOfHerman.Common.User;
using KnightsOfHerman.Frontend.Common.Model.User;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace KnightsOfHerman.Frontend.Common.BlazorViews.Account
{
    public partial class RegisterView
    {
        MudForm _form;

        [Inject]
        IUserController User { get; set; }

        [Inject]
        NavigationManager Nav { get; set; }

        [Parameter]
        public required Action GoToPrevious { get; set; }

        [Parameter]
        public required Action GoToLogin { get; set; }

        [Parameter]
        public required Action Refresh { get; set; }

        bool success;
        string[] errors = { };

        string _username = "";

        string _email = "";

        string _password = "";

        string _passwordConfirm = "";


        bool _alertShow;

        string _alertText = "";

        IEnumerable<string> ValidatePassword(string pw) => Password.CheckStrength(pw);

        string ValidateMatch(string pw)
        {
            if (_passwordConfirm != _password) return "Passwords must match";
            return null;
        }

        async Task<IEnumerable<string>> ValidateUsernameAsync(string username)
        {
            var result = await User.CheckUsername(username);
            return result;
        }

        async Task<IEnumerable<string>> ValidateEmailAsync(string email)
        {
            var result = await User.CheckEmail(email);
            return result;
        }

        async Task TryRegisterAsync()
        {
            if (!success)
            {
                _alertText = "Invalid Field(s)";
                _alertShow = true;
                return;
            }
            if (!(String.IsNullOrWhiteSpace(_username) || String.IsNullOrWhiteSpace(_email) || String.IsNullOrWhiteSpace(_password) || String.IsNullOrWhiteSpace(_passwordConfirm)))
            {
                if (_password == _passwordConfirm)
                {
                    //Need to check email

                    var model = new RegisterModel()
                    {
                        Username = _username,
                        Email = _email,
                        Password = _password
                    };

                    if(await User.TryRegisterAsync(model))
                    {
                        GoToPrevious();
                    } else
                    {
                        _alertText = "Register Failed";
                        _alertShow = true;
                        await InvokeAsync(StateHasChanged);
                    }

                } else
                {
                    _alertText = "Passwords Do Not Match";
                    _alertShow = true;
                    await InvokeAsync(StateHasChanged);
                }
            } else
            {
                _alertText = "Missing Required Field";
                _alertShow = true;
                await InvokeAsync(StateHasChanged);
            }
        }


        void CloseAlert()
        {
            _alertShow = false;
            _alertText = "";
            StateHasChanged();
        }

    }
}
