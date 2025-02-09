using KnightsOfHerman.Common.Abstract.Modification;
using KnightsOfHerman.Common.Sanctum.Abstract.Character;
using KnightsOfHerman.Common.Sanctum.Abstract.Character.Abilities;
using KnightsOfHerman.Common.Sanctum.Abstract.Character.Notes;
using KnightsOfHerman.Common.Sanctum.Communication.DTO;
using KnightsOfHerman.Common.Types;
using KnightsOfHerman.Frontend.Common.Model.Communication.Hubs;
using KnightsOfHerman.Frontend.Common.Model.Events;
using KnightsOfHerman.Frontend.Common.Model.Sanctum.Character.Types;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace KnightsOfHerman.Frontend.Common.Model.Sanctum.Character.Implementation
{
    public enum PopupSeverity
    {
        Normal,
        Success,
        Error,
        Warnign
    }

    public delegate void RefreshViewDelegate();
    public delegate Task RefreshViewAsyncDelegate();
    public delegate void DisplayPopupDelegate(string message, PopupSeverity severity);
    public delegate Task DisplayPopupAsyncDelegate(string message, PopupSeverity severity);

    /// <summary>
    /// View Model in charge of the Character App
    /// </summary>
    public class CharacterViewModel
    {
        /// <summary>
        /// Character's ID
        /// </summary>
        public int CharacterID { get; private set; }

        /// <summary>
        /// Character Object
        /// </summary>
        public CharacterCO Character { get; internal set; }

        /// <summary>
        /// Access level the current session has to the character
        /// </summary>
        public CharacterAccess Access { get; internal set; }

        /// <summary>
        /// Whether to display the view as readonly
        /// </summary>
        public bool IsReadOnly => !(Access == CharacterAccess.Owner || Access == CharacterAccess.Editor);

        /// <summary>
        /// Subscribe to this event to be notified when to redraw views.
        /// </summary>
        public event RefreshViewEventHandler? OnRefreshView;

        /// <summary>
        /// Subscribe to this to handle Character Deletions
        /// </summary>
        public event DeleteCharacterEventHandler? OnDelete;

        [Required]
        internal HubConnection Hub { get; set; }

        internal CharacterViewModel(int characterID)
        {
            CharacterID = characterID;
        }

        internal async void HandleDeleted()
        {
            OnDelete?.Invoke();
        }
        internal void SubscribeToCharacter()
        {
            Character.OnTrackedModification += HandleClientModification;
        }

        async void HandleClientModification(object sender, TrackedModificationEventArgs args)
        {
            if (!IsReadOnly)
            {
                string argsJson = JsonConvert.SerializeObject(args);

                var result = await Hub.InvokeAsync<TryResult>("ModifyCharacterAsync", CharacterID, argsJson);

                //StateHasChanged()
                OnRefreshView?.Invoke();
            }
        }

        /// <summary>
        /// Parses the change and then modifies the Character Object
        /// </summary>
        /// <param name="argsJson"></param>
        public async void HandleServerModification(string argsJson)
        {
            try
            {
                var args = JsonConvert.DeserializeObject<TrackedModificationEventArgs>(argsJson);

                if (args.Type.Contains("CharacterArmor"))
                {
                    CharacterArmorDTO dto = JsonConvert.DeserializeObject<CharacterArmorDTO>(args.Value.ToString());
                    args = new TrackedModificationEventArgs(args.Path, new CharacterArmorCO(dto), args.Action);
                }
                else if (args.Type.Contains("CharacterBio"))
                {
                    CharacterBioDTO dto = JsonConvert.DeserializeObject<CharacterBioDTO>(args.Value.ToString());
                    args = new TrackedModificationEventArgs(args.Path, new CharacterBioCO(dto), args.Action);
                }
                else if (args.Type.Contains("CharacterAbility"))
                {
                    CharacterAbilityDTO dto = JsonConvert.DeserializeObject<CharacterAbilityDTO>(args.Value.ToString());
                    args = new TrackedModificationEventArgs(args.Path, new CharacterAbilityCO(dto), args.Action);
                }

                else if (args.Type.Contains("CharacterItem"))
                {
                    CharacterItemDTO dto = JsonConvert.DeserializeObject<CharacterItemDTO>(args.Value.ToString());
                    args = new TrackedModificationEventArgs(args.Path, new CharacterItemCO(dto), args.Action);
                }
                else if (args.Type.Contains("CharacterJournal"))
                {
                    CharacterJournalDTO dto = JsonConvert.DeserializeObject<CharacterJournalDTO>(args.Value.ToString());
                    args = new TrackedModificationEventArgs(args.Path, new CharacterJournalCO(dto), args.Action);
                }
                else if (args.Type.Contains("CharacterResource"))
                {
                    //Shouldn't need to be handled
                }
                else if (args.Type.Contains("CharacterStat"))
                {
                    //Shouldn't need to be handled
                }
                else if (args.Type.Contains("CharacterWeapon"))
                {
                    CharacterWeaponDTO dto = JsonConvert.DeserializeObject<CharacterWeaponDTO>(args.Value.ToString());
                    args = new TrackedModificationEventArgs(args.Path, new CharacterWeaponCO(dto), args.Action);
                }
                else if (args.Type.EndsWith("CharacterDTO")) //Should only ever handle things with DTO's
                {
                    //Shouldn't need to be handled
                }

                //var newargs = new TrackedModificationEventArgs(args.Path, obj, args.Action);
                var result = Character.ModifyByPath(args);
                if (result.IsSuccess)
                {
                    OnRefreshView?.Invoke();
                }
            } catch
            {
                //log
            }
        }

        public async Task CreateArmor()
        {
            var result = await Hub.InvokeAsync<TryResult<CharacterArmorDTO>>("CreateArmorAsync", CharacterID);
            if (result.IsSuccess && result.Value != null)
            {
                Character.Armor[result.Value.ItemID] = new CharacterArmorCO(result.Value);
                //Message armor is made
                OnRefreshView?.Invoke();
            }
            else
            {
                //send error message
            }
        }

        public async Task CreateItem()
        {
            var result = await Hub.InvokeAsync<TryResult<CharacterItemDTO>>("CreateItemAsync", CharacterID);
            if (result.IsSuccess && result.Value != null)
            {
                Character.Items[result.Value.ItemID] = new CharacterItemCO(result.Value);
                //Message armor is made
                OnRefreshView?.Invoke();
            }
            else
            {
                //send error message
            }
        }

        public async Task CreateJournal(JournalCategory category)
        {
            var result = await Hub.InvokeAsync<TryResult<CharacterJournalDTO>>("CreateJournalEntryAsync", CharacterID, category);
            if (result.IsSuccess && result.Value != null)
            {
                Character.Journal[result.Value.JournalID] = new CharacterJournalCO(result.Value);
                //Message armor is made
                OnRefreshView?.Invoke();
            }
            else
            {
                //send error message
            }
        }

        public async Task CreateAbility(AbilityType type)
        {
            var result = await Hub.InvokeAsync<TryResult<CharacterAbilityDTO>>("CreateAbilityAsync", CharacterID, type);
            if (result.IsSuccess && result.Value != null)
            {
                Character.Abilities[result.Value.AbilityID] = new CharacterAbilityCO(result.Value);
                //Message armor is made
                OnRefreshView?.Invoke();
            }
            else
            {
                //send error message
            }
        }

        public async Task CreateWeapon()
        {
            var result = await Hub.InvokeAsync<TryResult<CharacterWeaponDTO>>("CreateWeaponAsync", CharacterID);
            if (result.IsSuccess && result.Value != null)
            {
                Character.Weapons[result.Value.ItemID] = new CharacterWeaponCO(result.Value);
                //Message weapon is made
                OnRefreshView?.Invoke();
            }
            else
            {
                //send error message
            }
        }
    }

    /// <summary>
    /// In charge of building the ViewModel
    /// </summary>
    public class CharacterViewModelBuilder
    {
        CharacterViewModel _model;
        DisplayPopupAsyncDelegate _popupAsync;
        DisplayPopupDelegate _popup;

        HubFactory _hubs;

        int? characterID;

        public CharacterViewModelBuilder(HubFactory hubs)
        {
            _hubs = hubs;
        }

        public CharacterViewModelBuilder SetCharacterID(int id)
        {
            characterID = id;
            return this;
        }

        public CharacterViewModelBuilder SetPopup(DisplayPopupDelegate popup)
        {
            _popup = popup;
            return this;
        }

        public CharacterViewModelBuilder SetPopupAsync(DisplayPopupAsyncDelegate popup)
        {
            _popupAsync = popup;
            return this;
        }

        /// <summary>
        /// Builds the viewmodel
        /// </summary>
        /// <returns></returns>
        public async Task<TryResult<CharacterViewModel>> BuildAsync()
        {
            try
            {
                if (characterID != null)
                {
                    var model = new CharacterViewModel((int)characterID);
                    var hub = _hubs.GetCharacterHub();
                    await hub.StartAsync();
                    model.Hub = hub;

                    var fetch = await hub.InvokeAsync<TryResult<CharacterDTO>>("SubscribeToCharacterAsync", (int)characterID);


                    if (fetch.IsSuccess && fetch.Value != null)
                    {
                        model.Character = new CharacterCO(fetch.Value);
                        model.Access = fetch.Value.Access;
                        hub.On<string>("ModifyCharacterAsync", model.HandleServerModification);
                        hub.On("CharacterDeleted", model.HandleDeleted);
                        model.SubscribeToCharacter();
                    }
                    else return TryResult<CharacterViewModel>.Fail(fetch.ErrorMessage);

                    return TryResult<CharacterViewModel>.Success(model);
                }
                return TryResult<CharacterViewModel>.Fail("Character ID Not Set");
            }
            catch (Exception ex)
            {
                //Probably set an if debug
                return TryResult<CharacterViewModel>.Fail(ex.Message);
            }
        }
    }

}
