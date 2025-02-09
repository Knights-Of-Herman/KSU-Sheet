using KnightsOfHerman.Common.Sanctum.Abstract.Character;
using KnightsOfHerman.Common.Sanctum.Communication.DTO;
using KnightsOfHerman.Frontend.Common.Model.Sanctum.Character.Types;
using KnightsOfHerman.Frontend.Common.Model.Sanctum.Interfaces;
using KnightsOfHerman.Frontend.Common.Model.User;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MudBlazor.CategoryTypes;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace KnightsOfHerman.Frontend.Common.BlazorViews.Sanctum.CharacterList
{
    public partial class ShareCharacterDialog
    {



        CharacterShareDTO _backup;

        string _searchString;

        private bool FilterFunc(CharacterShareDTO element) => FilterFuncHelper(element, _searchString);

        private bool FilterFuncHelper(CharacterShareDTO element, string searchString)
        {
            if (string.IsNullOrWhiteSpace(searchString))
                return true;
            if (element.ShareUser.Contains(searchString))
            {
                return true;
            }
            return false;
        }

        void BackupPerm(object perm)
        {
            if(perm is CharacterShareDTO p)
            {
                _backup = new CharacterShareDTO()
                {
                    ShareUser = p.ShareUser,
                    CharacterID = p.CharacterID,
                    Access = p.Access
                };
            }
        }

        async void CommitEdit(object perm)
        {
            if(perm is CharacterShareDTO p)
            {
                var result = await CharacterController.ShareCharacter(p.ShareUser, Profile.CharacterID, p.Access);
                await LoadPermissions();
            }
        }

        void CancelEdit(object perm)
        {
            if (perm is CharacterShareDTO p)
            {
                p.ShareUser = _backup.ShareUser;
                p.Access = _backup.Access;
                p.CharacterID = _backup.CharacterID;
                _backup = null;
            }
        }

        private MudTable<CharacterShareDTO> _table;
        bool _loading = true;

        private void PageChanged(int i)
        {
            _table.NavigateTo(i - 1);
        }

        string _username;
        string Username
        {
            get => _username;
            set
            {
                if (_username != value)
                {
                    _username = value;
                    _userFound = false;
                }
            }
        }

        [Inject]
        public required IUserController User { get; set; }
        
        [Inject]
        public required ICharacterListAccess CharacterController { get; set; }

        CharacterAccess _access = CharacterAccess.Viewer;

        [Parameter]
        public required CharacterProfile Profile { get; set; }

        bool _userFound = false;

        bool _disableShare => !_userFound;

        CharacterSharePermissions _permissions;

        public async Task SearchUser()
        {
            var result = await User.LookForUser(Username);
            _userFound = result;
            await InvokeAsync(StateHasChanged);
        }

        public async Task Share()
        {
            var result = await CharacterController.ShareCharacter(Username, Profile.CharacterID, _access);
            await LoadPermissions();
        }

        public async Task LoadPermissions()
        {
            _loading = true;
            await InvokeAsync(StateHasChanged);

            _permissions = await CharacterController.GetSharePermissions(Profile.CharacterID);
            _loading = false;
            await InvokeAsync(StateHasChanged);

        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);
            if (firstRender)
            {
                await LoadPermissions();
            }
        }
    }
}
