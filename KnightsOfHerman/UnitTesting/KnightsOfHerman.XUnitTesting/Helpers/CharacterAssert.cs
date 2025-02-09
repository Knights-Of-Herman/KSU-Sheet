using KnightsOfHerman.Common.Sanctum.Abstract.Character;
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
    public static class CharacterAssert
    {

        public static void BiosAreSame(ICharacterBio bio1, ICharacterBio bio2)
        {
            Assert.NotNull(bio1);
            Assert.NotNull(bio2);
            Assert.Equal(bio1.Name, bio2.Name);
            Assert.Equal(bio1.Race, bio2.Race);
            Assert.Equal(bio1.Background, bio2.Background);
            Assert.Equal(bio1.Campaign, bio2.Campaign);
            Assert.Equal(bio1.CampaignID, bio2.CampaignID);
            Assert.Equal(bio1.TotalXP, bio2.TotalXP);
            Assert.Equal(bio1.UnspentXP, bio2.UnspentXP);
            Assert.Equal(bio1.Destiny, bio2.Destiny);
            Assert.Equal(bio1.Conflict, bio2.Conflict);
            Assert.Equal(bio1.Languages, bio2.Languages);
        }

        public static void StatsAreSame(ICharacterStat stat1, ICharacterStat stat2)
        {
            Assert.NotNull(stat1);
            Assert.NotNull(stat2);

            Assert.Equal(stat1.StatID, stat2.StatID);
            Assert.Equal(stat1.Base, stat2.Base);
            Assert.Equal(stat1.CustomMod, stat2.CustomMod);
            Assert.Equal(stat1.OverrideValue, stat2.OverrideValue);
            Assert.Equal(stat1.DoOverride, stat2.DoOverride);
        }

        public static void ResourcesAreSame(ICharacterResource resource1, ICharacterResource resource2)
        {
            Assert.NotNull(resource1);
            Assert.NotNull(resource2);
            Assert.Equal(resource1.ResourceID, resource2.ResourceID);
            Assert.Equal(resource1.Attribute, resource2.Attribute);
            Assert.Equal(resource1.Modifier, resource2.Modifier);
            Assert.Equal(resource1.OverrideMaxValue, resource2.OverrideMaxValue);
            Assert.Equal(resource1.DoOverrideMax, resource2.DoOverrideMax);
            Assert.Equal(resource1.CurrentValue, resource2.CurrentValue);
        }

        public static void WeaponsAreSame(ICharacterWeapon weapon1, ICharacterWeapon weapon2)
        {
            Assert.NotNull(weapon1);
            Assert.NotNull(weapon2);

            Assert.Equal(weapon1.ItemID, weapon2.ItemID);
            Assert.Equal(weapon1.Name, weapon2.Name);
            Assert.Equal(weapon1.Description, weapon2.Description);
            Assert.Equal(weapon1.Damage, weapon2.Damage);
            Assert.Equal(weapon1.Accuracy, weapon2.Accuracy);
            Assert.Equal(weapon1.Quantity, weapon2.Quantity);
            Assert.Equal(weapon1.Weight, weapon2.Weight);
        }

        public static void ArmorsAreSame(ICharacterArmor armor1, ICharacterArmor armor2)
        {
            Assert.NotNull(armor1);
            Assert.NotNull(armor2);
            Assert.Equal(armor1.ItemID, armor2.ItemID);
            Assert.Equal(armor1.Name, armor2.Name);
            Assert.Equal(armor1.Description, armor2.Description);
            Assert.Equal(armor1.Weight, armor2.Weight);
            Assert.Equal(armor1.Hindrance, armor2.Hindrance);
            Assert.Equal(armor1.Equipped, armor2.Equipped);
            Assert.Equal(armor1.Layer, armor2.Layer);
            Assert.Equal(armor1.Slot, armor2.Slot);
            Assert.Equal(armor1.Bludgeoning, armor2.Bludgeoning);
            Assert.Equal(armor1.Piercing, armor2.Piercing);
            Assert.Equal(armor1.Slashing, armor2.Slashing);
        }

        public static void ItemsAreSame(ICharacterItem item1, ICharacterItem item2)
        {
            Assert.NotNull(item1);
            Assert.NotNull(item2);
            Assert.Equal(item1.ItemID, item2.ItemID);
            Assert.Equal(item1.Name, item2.Name);
            Assert.Equal(item1.Description, item2.Description);
            Assert.Equal(item1.Weight, item2.Weight);
            Assert.Equal(item1.Quantity, item2.Quantity);
        }

        public static void JournalsAreSame(ICharacterJournal journal1, ICharacterJournal journal2)
        {
            Assert.NotNull(journal1);
            Assert.NotNull(journal2);
            Assert.Equal(journal1.JournalID, journal2.JournalID);
            Assert.Equal(journal1.Title, journal2.Title);
            Assert.Equal(journal1.Content, journal2.Content);
            Assert.Equal(journal1.CreateDate, journal2.CreateDate);
            Assert.Equal(journal1.Category, journal2.Category);
        }

        public static void AbilitiesAreSame(ICharacterAbility ability1, ICharacterAbility ability2)
        {
            Assert.NotNull(ability1);
            Assert.NotNull(ability2);
            Assert.Equal(ability1.AbilityID, ability2.AbilityID);
            Assert.Equal(ability1.AbilityType, ability2.AbilityType);
            Assert.Equal(ability1.Title, ability2.Title);
            Assert.Equal(ability1.Content, ability2.Content);
            Assert.Equal(ability1.Cost, ability2.Cost);
            Assert.Equal(ability1.Memorized, ability2.Memorized);
        }
    }
}
