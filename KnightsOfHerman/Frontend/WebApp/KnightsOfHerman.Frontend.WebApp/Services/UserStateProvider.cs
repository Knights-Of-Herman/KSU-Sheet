using KnightsOfHerman.Common.User;
using KnightsOfHerman.Frontend.Common.Model.Communication.Hubs;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace KnightsOfHerman.Frontend.Common.BlazorViews.Services
{
    /// <summary>
    /// Handles saving the user state inside of the browser storage and used for CascadingAuthenticatioView in blazor
    /// </summary>
    public class UserStateProvider : AuthenticationStateProvider
    {
        ProtectedSessionStorage _session;

        HttpClient _http;
        string _session_key = "user_session_token";
        HubFactory _hubs;
        public UserStateProvider(ProtectedSessionStorage session, HttpClient http, HubFactory hubs)
        {
            _session = session;
            _http = http;
            _hubs = hubs;
        }

        private AuthenticationState? _authState;

        private string _token;
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            if (_authState == null)
            {
                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
            }
            else
            {
                _http.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _token);
                _hubs.SetAccessToken(_token);
                return _authState;
            }
        }

        public async Task LoadAuthStateAsync()
        {
            var result = await _session.GetAsync<string>(_session_key);

            if (result.Success && result.Value != null && TryCreateAuthStateAsync(result.Value, out var state) && state != null)
            {
                _token = result.Value;
                _http.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _token);
                _hubs.SetAccessToken(_token);
                SetAuthState(state);
            }
            else
            {
                //Need to make sure this returns a non logged in user
                SetAuthState(new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity())));
            }
        }

        void SetAuthState(AuthenticationState state)
        {
            _authState = state;
            NotifyAuthenticationStateChanged(Task.FromResult(state));
        }

        private bool TryCreateAuthStateAsync(string token, out AuthenticationState? state)
        {
            if (JwtToken.TryParseToken(token, out var claims))
            {
                var identity = new ClaimsIdentity(claims, "jwt");

                var principal = new ClaimsPrincipal(identity);

                state = new AuthenticationState(principal);
                return true;
            }
            else
            {
                state = default;
                return false;
            }
        }

        public async Task<bool> TrySetTokenAsync(string token)
        {
            try
            {
                await _session.SetAsync(_session_key, token);

                if (TryCreateAuthStateAsync(token, out var state) && state != null)
                {
                    _token = token;
                    _http.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _token);
                    _hubs.SetAccessToken(_token);
                    SetAuthState(state);
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> TryClearTokenAsync()
        {
            try
            {
                await _session.DeleteAsync(_session_key);
                _token = string.Empty;
                _http.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", string.Empty);
                _hubs.SetAccessToken(null);
                NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()))));
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
