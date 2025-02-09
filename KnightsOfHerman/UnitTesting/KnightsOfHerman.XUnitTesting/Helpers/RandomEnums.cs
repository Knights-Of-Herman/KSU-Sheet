using KnightsOfHerman.Common.Sanctum.Abstract.Character.Abilities;
using KnightsOfHerman.Common.Sanctum.Abstract.Character.Items;
using KnightsOfHerman.Common.Sanctum.Abstract.Character.Notes;
using KnightsOfHerman.Common.Sanctum.Abstract.Character.Stats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnightsOfHerman.XUnitTesting.Helpers
{
    public static class RandomEnums
    {
        public static CharacterStats GetRandomStat()
        {
            Random random = new Random();
            CharacterStats[] values = (CharacterStats[])Enum.GetValues(typeof(CharacterStats));
            CharacterStats randomValue = values[random.Next(values.Length)];
            return randomValue;
        }

        public static CharacterResources GetRandomResource()
        {
            Random random = new Random();
            CharacterResources[] values = (CharacterResources[])Enum.GetValues(typeof(CharacterResources));
            CharacterResources randomValue = values[random.Next(values.Length)];
            return randomValue;
        }

        public static ArmorSlot GetRandomArmorSlot()
        {
            Random random = new Random();
            ArmorSlot[] values = (ArmorSlot[])Enum.GetValues(typeof(ArmorSlot));
            ArmorSlot randomValue = values[random.Next(values.Length)];
            return randomValue;
        }

        public static ArmorLayer GetRandomArmorLayer()
        {
            Random random = new Random();
            ArmorLayer[] values = (ArmorLayer[])Enum.GetValues(typeof(ArmorLayer));
            ArmorLayer randomValue = values[random.Next(values.Length)];
            return randomValue;
        }

        public static JournalCategory GetRandomJournalCategory()
        {
            Random random = new Random();
            JournalCategory[] values = (JournalCategory[])Enum.GetValues(typeof(JournalCategory));
            JournalCategory randomValue = values[random.Next(values.Length)];
            return randomValue;
        }

        public static AbilityType GetRandomAbilityType()
        {
            Random random = new Random();
            AbilityType[] values = (AbilityType[])Enum.GetValues(typeof(AbilityType));
            AbilityType randomValue = values[random.Next(values.Length)];
            return randomValue;
        }
    }
}
