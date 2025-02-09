using KnightsOfHerman.Backend.Common.Sanctum.Character.Interfaces;
using KnightsOfHerman.Backend.Server.SignalRHubs;
using Microsoft.AspNetCore.SignalR;

namespace KnightsOfHerman.Backend.Server.Services
{
    /// <summary>
    /// Implementents ICharacterNotificationService
    /// </summary>
    public class CharacterNotificationService : ICharacterNotificationService
    {

        IHubContext<CharacterHub> _hubContext;
        public CharacterNotificationService(IHubContext<CharacterHub> hub)
        {
            _hubContext = hub;
        }

        public event ICharacterNotificationService.CharacterDeletionHandler OnCharacterDeleted;

        public void NotifyCharacterDeletion(int id)
        {
            _hubContext.Clients.Group(id.ToString()).SendAsync("CharacterDeleted");
        }
    }
}
