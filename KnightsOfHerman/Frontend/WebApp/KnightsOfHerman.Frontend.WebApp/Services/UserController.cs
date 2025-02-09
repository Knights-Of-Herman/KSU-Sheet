using KnightsOfHerman.Common.User;
using KnightsOfHerman.Frontend.Common.BlazorViews.Services;
using KnightsOfHerman.Frontend.Common.Model.User;
using Microsoft.AspNetCore.Components.Authorization;
using System.Reflection;

namespace KnightsOfHerman.Frontend.WebApp.Services
{
    /// <summary>
    /// Class that handles user account related services
    /// </summary>
    public class UserController : IUserController
    {

        UserStateProvider _state;

        HttpClient _http;

        /// <summary>
        /// Creates the account service
        /// </summary>
        /// <param name="state">AuthenticationStateProvider must be of type extended class AuthStateProvider</param>
        /// <param name="http"></param>
        public UserController(AuthenticationStateProvider state, HttpClient http)
        {
            _state = (UserStateProvider)state;
            _http = http;
        }

        public async Task LoadUserAsync()
        {
            await _state.LoadAuthStateAsync();
        }

        public async Task<bool> TryLoginAsync(LoginModel model)
        {
            var response = await _http.PostAsJsonAsync("userapi/login", model);
            if (response.IsSuccessStatusCode)
            {
                var jwtToken = await response.Content.ReadFromJsonAsync<JwtToken>();

                if (jwtToken != null && await _state.TrySetTokenAsync(jwtToken.Token))
                {
                    return true;
                }

            }
            return false;
        }

        public async Task<bool> TryLogoutAsync()
        {
            return await _state.TryClearTokenAsync();
        }


        public async Task<List<string>> CheckUsername(string username)
        {
            var response = await _http.PostAsJsonAsync("userapi/checkusername", username);
            if (response.IsSuccessStatusCode)
            {
                var errors = await response.Content.ReadFromJsonAsync<List<string>>();
                return errors;
            } else
            {
                return new List<string>() { "Couldn't Validate Username" };
            }
        }

        public async Task<List<string>> CheckEmail(string email)
        {
            var response = await _http.PostAsJsonAsync("userapi/checkemail", email);
            if (response.IsSuccessStatusCode)
            {
                var errors = await response.Content.ReadFromJsonAsync<List<string>>();
                return errors;
            }
            else
            {
                return new List<string>() { "Couldn't Validate Email" };
            }
        }

        public async Task<bool> TryRegisterAsync(RegisterModel model)
        {
            var response = await _http.PostAsJsonAsync("userapi/register", model);

            if (response.IsSuccessStatusCode)
            {
                var jwtToken = await response.Content.ReadFromJsonAsync<JwtToken>();
                if (jwtToken != null)
                {
                    await _state.TrySetTokenAsync(jwtToken.Token);
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> LookForUser(string username)
        {
            var response = await _http.PostAsJsonAsync("userapi/lookforusername", username);
            try
            {
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<bool>();
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}
