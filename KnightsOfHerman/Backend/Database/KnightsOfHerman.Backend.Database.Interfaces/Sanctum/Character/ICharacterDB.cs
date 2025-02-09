using KnightsOfHerman.Backend.Common.Sanctum.Character.Model;
using KnightsOfHerman.Backend.Common.Sanctum.Character.Model.Notes;
using KnightsOfHerman.Common.Sanctum.Abstract.Character;
using KnightsOfHerman.Common.Sanctum.Abstract.Character.Notes;
using KnightsOfHerman.Common.Sanctum.Communication.DTO;
using KnightsOfHerman.Common.Types;


namespace KnightsOfHerman.Backend.Database.Interfaces.Sanctum.Character
{
    public interface ICharacterDB
    {
        Task<TryResult<int>> CreateBlankCharacter(int userID);
        Task<TryResult<List<CharacterProfile>>> GetProfiles(int userID);

        Task<TryResult<ServerCharacterBase>> GetCharacterBase(int userID, int characterID);

        Task<TryResult> DeleteCharacterAsync(int userID, int characterID);

        Task<TryResult> SaveCharacter(ServerCharacter character);

        Task<TryResult<ServerJournalEntry>> CreateBlankJournalEntry(int userID, int characterID, JournalCategory type);

        Task<TryResult> ModifyJournalEntry(int userId, int characterID, ServerJournalEntry entry);

        Task<TryResult> RemoveJournalEntry(int userID, int CharacterID, int entryID);

        Task<TryResult<ServerJournalEntry>> GetJournalEntry(int entryID);

        Task<TryResult<List<JournalProxy>>> GetJournalEntryProxies(int characterID);
    }
}
