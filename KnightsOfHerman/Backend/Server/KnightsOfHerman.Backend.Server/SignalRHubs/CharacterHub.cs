using KnightsOfHerman.Backend.Common.JWT;
using KnightsOfHerman.Backend.Common.Sanctum.Character.Interfaces;
using KnightsOfHerman.Common.Abstract;
using KnightsOfHerman.Common.Abstract.Modification;
using KnightsOfHerman.Common.Sanctum.Abstract.Character.Abilities;
using KnightsOfHerman.Common.Sanctum.Abstract.Character.Notes;
using KnightsOfHerman.Common.Sanctum.Communication.DTO;
using KnightsOfHerman.Common.Types;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;

namespace KnightsOfHerman.Backend.Server.SignalRHubs
{
    /// <summary>
    /// SingalrR Hub for Character Realtime access
    /// </summary>
    [Authorize]
    public class CharacterHub : Hub
    {
        
        private class CharacterConnection
        {
            public int UserID;
            public string ConnectionString = "";
        }

        ICharacterService _characterService;

        ICharacterNotificationService _notify;

        JWTService _jwt;

        public CharacterHub(ICharacterService characterService, JWTService jwt, ICharacterNotificationService notify)
        {
            _characterService = characterService;
            _notify = notify;
            _jwt = jwt;
            _notify.OnCharacterDeleted += NotifyCharacterDeletion;
        }

        /// <summary>
        /// Attempts to modify a character
        /// </summary>
        /// <param name="id"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public async Task<TryResult> ModifyCharacterAsync(int id, string args)
        {
            if (_jwt.TryParseUserID(Context.User?.Identity, out int userID))
            {
                var argsObject = JsonConvert.DeserializeObject<TrackedModificationEventArgs>(args); // Will need to sanitize this.


                var result = await _characterService.ModifyCharacter(userID, id, argsObject);
                if (result.IsSuccess)
                {
                    await Clients.OthersInGroup(id.ToString()).SendAsync("ModifyCharacterAsync", args);
                    return TryResult.Success();
                }
                else
                {
                    return TryResult.Fail(result.ErrorMessage);
                }
            }
            else
            {
                return TryResult.Fail("No Permission");
            }
        }

        /// <summary>
        /// Gets a Character and subscribes to its changes
        /// </summary>
        /// <param name="characterID"></param>
        /// <returns></returns>
        public async Task<TryResult<CharacterDTO>> SubscribeToCharacterAsync(int characterID)
        {
            var user = Context.User;
            if (_jwt.TryParseUserID(user.Identity, out int userID))
            {
                var result = await _characterService.GetCharacterDTO(userID, characterID);

                if (result.IsSuccess)
                {
                    if (result.Value != null)
                    {
                        await Groups.AddToGroupAsync(Context.ConnectionId, characterID.ToString());
                        return TryResult<CharacterDTO>.Success(result.Value);                      
                    }
                }
                return TryResult<CharacterDTO>.Fail("Couldn't Load Character");
            }
            else
            {
                return TryResult<CharacterDTO>.Fail("No Permission");
            }
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            await base.OnDisconnectedAsync(exception);
        }

        /// <summary>
        /// Creates a armor and notifies other subscribers to add it
        /// </summary>
        /// <param name="characterid">ID of the character to add</param>
        /// <returns>Created Entry if sucecssful</returns>
        public async Task<TryResult<CharacterArmorDTO>> CreateArmorAsync(int characterid)
        {
            var user = Context.User;
            if (_jwt.TryParseUserID(user.Identity, out int userID))
            {
                var result = await _characterService.CreateArmor(userID, characterid);
                if (result.IsSuccess && result.Value != null)
                {
                    var entry = result.Value;
                    var args = new TrackedModificationEventArgs($"Armor.{entry.ItemID}", entry, EditActions.Add);
                    string argsJson = JsonConvert.SerializeObject(args);
                    await Clients.OthersInGroup(characterid.ToString()).SendAsync("ModifyCharacterAsync", argsJson);
                    return TryResult<CharacterArmorDTO>.Success(entry);
                }
                else
                {
                    return TryResult<CharacterArmorDTO>.Fail(result.ErrorMessage);
                }
            }
            else
            {
                return TryResult<CharacterArmorDTO>.Fail("No Permission");
            }
        }

        /// <summary>
        /// Creates a item and notifies other subscribers to add it
        /// </summary>
        /// <param name="characterid">ID of the character to add</param>
        /// <returns>Created Entry if sucecssful</returns>
        public async Task<TryResult<CharacterItemDTO>> CreateItemAsync(int characterid)
        {
            var user = Context.User;
            if (_jwt.TryParseUserID(user.Identity, out int userID))
            {
                var result = await _characterService.CreateItem(userID, characterid);
                if (result.IsSuccess && result.Value != null)
                {
                    var entry = result.Value;
                    var args = new TrackedModificationEventArgs($"Items.{entry.ItemID}", entry, EditActions.Add);
                    string argsJson = JsonConvert.SerializeObject(args);
                    await Clients.OthersInGroup(characterid.ToString()).SendAsync("ModifyCharacterAsync", argsJson);
                    return TryResult<CharacterItemDTO>.Success(entry);
                }
                else
                {
                    return TryResult<CharacterItemDTO>.Fail(result.ErrorMessage);
                }
            }
            else
            {
                return TryResult<CharacterItemDTO>.Fail("No Permission");
            }
        }

        /// <summary>
        /// Creates a journal entry and notifies other subscribers to add it
        /// </summary>
        /// <param name="characterid">ID of the character to add</param>
        /// <param name="category">Category of the journal entry</param>
        /// <returns>Created Entry if sucecssful</returns>
        public async Task<TryResult<CharacterJournalDTO>> CreateJournalEntryAsync(int characterid, JournalCategory category)
        {
            var user = Context.User;
            if (_jwt.TryParseUserID(user.Identity, out int userID))
            {
                var result = await _characterService.CreateJournal(userID, characterid, category);
                if (result.IsSuccess && result.Value != null)
                {
                    var entry = result.Value;
                    var args = new TrackedModificationEventArgs($"Journal.{entry.JournalID}", entry, EditActions.Add);
                    string argsJson = JsonConvert.SerializeObject(args);
                    await Clients.OthersInGroup(characterid.ToString()).SendAsync("ModifyCharacterAsync", argsJson);
                    return TryResult<CharacterJournalDTO>.Success(entry);
                }
                else
                {
                    return TryResult<CharacterJournalDTO>.Fail(result.ErrorMessage);
                }
            }
            else
            {
                return TryResult<CharacterJournalDTO>.Fail("No Permission");
            }
        }

        /// <summary>
        /// Creates a journal entry and notifies other subscribers to add it
        /// </summary>
        /// <param name="characterid">ID of the character to add</param>
        /// <param name="category">Category of the journal entry</param>
        /// <returns>Created Entry if sucecssful</returns>
        public async Task<TryResult<CharacterAbilityDTO>> CreateAbilityAsync(int characterid, AbilityType type)
        {
            var user = Context.User;
            if (_jwt.TryParseUserID(user.Identity, out int userID))
            {
                var result = await _characterService.CreateAbility(userID, characterid, type);
                if (result.IsSuccess && result.Value != null)
                {
                    var entry = result.Value;
                    var args = new TrackedModificationEventArgs($"Abilities.{entry.AbilityID}", entry, EditActions.Add);
                    string argsJson = JsonConvert.SerializeObject(args);
                    await Clients.OthersInGroup(characterid.ToString()).SendAsync("ModifyCharacterAsync", argsJson);
                    return TryResult<CharacterAbilityDTO>.Success(entry);
                }
                else
                {
                    return TryResult<CharacterAbilityDTO>.Fail(result.ErrorMessage);
                }
            }
            else
            {
                return TryResult<CharacterAbilityDTO>.Fail("No Permission");
            }
        }

        /// <summary>
        /// Creates a weapon for the character and notifies other subscribers
        /// </summary>
        /// <param name="characterid"></param>
        /// <returns></returns>
        public async Task<TryResult<CharacterWeaponDTO>> CreateWeaponAsync(int characterid)
        {
            var user = Context.User;
            if (_jwt.TryParseUserID(user.Identity, out int userID))
            {
                var result = await _characterService.CreateWeapon(userID, characterid);
                if (result.IsSuccess && result.Value != null)
                {
                    var entry = result.Value;
                    var args = new TrackedModificationEventArgs($"Weapons.{entry.ItemID}", entry, EditActions.Add);
                    string argsJson = JsonConvert.SerializeObject(args);
                    await Clients.OthersInGroup(characterid.ToString()).SendAsync("ModifyCharacterAsync", argsJson);
                    return TryResult<CharacterWeaponDTO>.Success(entry);
                }
                else
                {
                    return TryResult<CharacterWeaponDTO>.Fail(result.ErrorMessage);
                }
            }
            else
            {
                return TryResult<CharacterWeaponDTO>.Fail("No Permission");
            }
        }

        /// <summary>
        /// Notifies subscribers that the character has been deleted
        /// </summary>
        /// <param name="id"></param>
        public async void NotifyCharacterDeletion(int id)
        {
            await Clients.Group(id.ToString()).SendAsync("CharacterDeleted");
        }
    }
}
