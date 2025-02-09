using KnightsOfHerman.Backend.Common.Database.Types.Character;
using KnightsOfHerman.Backend.Database.Azure.User;
using KnightsOfHerman.Common.Sanctum.Abstract.Character.Notes;
using KnightsOfHerman.Common.Sanctum.Communication.DTO;
using KnightsOfHerman.Common.Types;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnightsOfHerman.Backend.Database.Azure.Character.Querries
{
    /// <summary>
    /// Helper class to handle Journal Querries
    /// </summary>
    internal class JournalQueries
    {
        AzureDBContext _dbContext;

        public JournalQueries(AzureDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<TryResult<CharacterJournalDBO>> CreateJournalEntry(int characterID, JournalCategory category)
        {
            try
            {
                var parameter = new SqlParameter("@CharacterID", characterID);
                var parameter2 = new SqlParameter("@Category", category);
                var entries = await _dbContext.Database.SqlQueryRaw<CharacterJournalDBO>("EXEC CreateJournalEntry @CharacterID, @Category", parameter, parameter2).ToListAsync();

                var entry = entries.First();

                return TryResult<CharacterJournalDBO>.Success(entry);
            }
            catch (Exception ex)
            {
                return TryResult<CharacterJournalDBO>.Fail(ex.Message);
            }
        }

        public async Task<TryResult<List<CharacterJournalDBO>>> GetJournalEntries(int characterID)
        {
            try
            {
                var parameter = new SqlParameter("@CharacterID", characterID);
                var entries = await _dbContext.Database.SqlQueryRaw<CharacterJournalDBO>("EXEC GetJournalEntries @CharacterID", parameter).ToListAsync();

                return TryResult<List<CharacterJournalDBO>>.Success(entries);
            }
            catch (Exception ex)
            {
                return TryResult<List<CharacterJournalDBO>>.Fail(ex.Message);
            }
        }

        public async Task<TryResult> SaveJournalEntry(ICharacterJournal journalEntry)
        {
            try
            {
                await _dbContext.Database.ExecuteSqlRawAsync("EXEC SaveJournalEntry @NoteID, @Title, @Content",
                    new SqlParameter("@NoteID", journalEntry.JournalID),
                    new SqlParameter("@Title", journalEntry.Title),
                    new SqlParameter("@Content", journalEntry.Content));
                return TryResult.Success();
            }
            catch (Exception ex)
            {
                return TryResult.Fail(ex.Message);
            }
        }

        public async Task<TryResult> DeleteJournalEntry(int noteID)
        {
            try
            {
                await _dbContext.Database.ExecuteSqlRawAsync("EXEC DeleteJournalEntry @NoteID",
                    new SqlParameter("@NoteID", noteID));
                return TryResult.Success();
            }
            catch (Exception ex)
            {
                return TryResult.Fail(ex.Message);
            }
        }
    }
}
