using KnightsOfHerman.Backend.Common.Sanctum.Character.Types;
using KnightsOfHerman.Common.Abstract;
using KnightsOfHerman.Common.Sanctum.Abstract.Character;
using KnightsOfHerman.XUnitTesting.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tynamix.ObjectFiller;

namespace KnightsOfHerman.XUnitTesting
{
    public class ModifyCharacterTests
    {
        [Fact]
        public void ShouldBeAbleToModifyBioSO()
        {
            var tc = TestCharacter.CreateFakeCharacterSO();
            ModifyCharacter.ModifyCharacterRecursive(tc, "Bio.Name", "Joe", EditActions.Edit);
            Assert.Equal("Joe", tc.Bio.Name);
            
        }

        [Fact]
        public void ShouldBeAbleToModifyJournalSO()
        {
            var tc = TestCharacter.CreateFakeCharacterSO();
            var kvp = tc.Journal.First();
            ModifyCharacter.ModifyCharacterRecursive(tc, $"Journal.{kvp.Key}.Title", "New Title", EditActions.Edit);
            Assert.Equal("New Title", tc.Journal[kvp.Key].Title);       
        }

        [Fact]
        public async void ShouldBeAbleToRemoveJournalSO()
        {
            await using (var tc = await TestCharacter.DBCreateComplexAsync())
            {
                
            }
        }
    }
}
