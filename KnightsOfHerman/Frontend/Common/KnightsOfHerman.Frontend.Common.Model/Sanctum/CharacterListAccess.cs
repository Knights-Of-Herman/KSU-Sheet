using KnightsOfHerman.Common.Sanctum.Abstract.Character;
using KnightsOfHerman.Common.Sanctum.Communication.DTO;
using KnightsOfHerman.Common.Types;
using KnightsOfHerman.Frontend.Common.Model.Sanctum.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace KnightsOfHerman.Frontend.Common.Model.Sanctum
{
    /// <summary>
    /// Implementation of ICharacterListAccess
    /// </summary>
    public class CharacterListAccess : ICharacterListAccess
    {
        HttpClient _http;

        public CharacterListAccess(HttpClient http)
        {
            _http = http;
        }


        public async Task<TryResult<int>> CreateBlankChracter()
        {
            try
            {
                var response = await _http.PostAsync("characterapi/createblankcharacter", null);

                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    if (int.TryParse(data, out int id))
                    {
                        return TryResult<int>.Success(id);
                    }
                }
                else
                {
                    var data = await response.Content.ReadAsStringAsync();
                }
            }
            catch { }
            return TryResult<int>.Fail();
        }

        public async Task<bool> DeleteCharacterAsync(int id)
        {
            try
            {
                var response = await _http.PostAsJsonAsync("characterapi/deletecharacter", id);
                if (response.IsSuccessStatusCode)
                {
                    return true;
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
        public async Task<TryResult<List<CharacterProfile>>> GetProfilesAsync()
        {
            try
            {
                var response = await _http.PostAsync("characterapi/getprofiles", null);
                if (response.IsSuccessStatusCode)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();

                    var profiles = JsonConvert.DeserializeObject<List<CharacterProfile>>(jsonString);

                    if (profiles != null)
                    {
                        return TryResult<List<CharacterProfile>>.Success(profiles);
                    }
                }
#if DEBUG
                else if (response.StatusCode == HttpStatusCode.InternalServerError)
                {
					var error = await response.Content.ReadAsStringAsync();
				}
#endif
                return TryResult<List<CharacterProfile>>.Fail();
            }
            catch
            {
                return TryResult<List<CharacterProfile>>.Fail();
            }
        }

        public async Task<CharacterSharePermissions> GetSharePermissions(int characterID)
        {
            try
            {
                var response = await _http.PostAsJsonAsync("characterapi/getsharepermissions", characterID);
                if (response.IsSuccessStatusCode && response.Content != null)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    var permissions = JsonConvert.DeserializeObject<CharacterSharePermissions>(jsonString);
                    return permissions != null ? permissions : new();
                }
                else
                {
                    return new CharacterSharePermissions() { Permissions = new() };
                }
            }
            catch
            {
                return new CharacterSharePermissions() { Permissions = new() };
            }
        }

        public async Task<CharacterShareResult> ShareCharacter(string shareuser, int characterID, CharacterAccess access)
        {
            try
            {
                var dto = new CharacterShareDTO()
                {
                    ShareUser = shareuser,
                    CharacterID = characterID,
                    Access = access
                };
                var response = await _http.PostAsJsonAsync("characterapi/sharecharacter", dto);
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<CharacterShareResult>();
                }
                else
                {
                    return CharacterShareResult.UnknownError;
                }
            }
            catch
            {
                return CharacterShareResult.UnknownError;
            }
        }

        public async Task UnsubscribeCharacter(int characterID)
        {
            var result = await _http.PostAsJsonAsync("characterapi/unsharecharacter", characterID);
        }
    }        
}
