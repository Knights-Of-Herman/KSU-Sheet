using KnightsOfHerman.Common.Abstract;
using KnightsOfHerman.Common.Sanctum.Abstract.Character.Notes;
using KnightsOfHerman.Common.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace KnightsOfHerman.Frontend.Common.Model.Sanctum.Delegates
{
    /// <summary>
    /// Delegate to send a modification to a remote entity
    /// </summary>
    /// <param name="path">Path to the change</param>
    /// <param name="value">Value of the change</param>
    /// <param name="action">Action of the change</param>
    /// <returns>TryResult of the task</returns>
    public delegate Task<bool> SendModificationAsync(string path, object value, EditActions action);

    public delegate Task<TryResult<List<ICharacterJournal>>> LoadJournalEntriesAsync(int startIndex, int amount, string criteria, string type);

    public delegate Task<TryResult<List<ICharacterJournal>>> LoadAbilitiesAsync(int startIndex, int amount, string criteria /*Add Ability Type Here*/);

    public delegate Task<TryResult<ICharacterJournal>> CreateJournalEntryAsync(string type);

    public delegate Task<TryResult<ICharacterJournal>> CreateAbilityAsync(/*Type*/);

}
