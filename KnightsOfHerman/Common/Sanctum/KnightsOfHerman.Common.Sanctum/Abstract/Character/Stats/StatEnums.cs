using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnightsOfHerman.Common.Sanctum.Abstract.Character.Stats
{
    /// <summary>
    /// List of the Charater's Resources
    /// </summary>
    public enum CharacterResources
    {
        Health,
        Mana,
        Stamina,
        Speed
    }

    /// <summary>
    /// List of the Character's Attributes and Skills
    /// </summary>
    public enum CharacterStats
    {
        //Attributes
        Intelligence,
        Magic,
        Charisma,
        Constitution,
        Craftsmanship,
        Resolve,
        Strength,
        Finesse,
        Reflex,

        //Intelligence Skills
        Perception,
        Investigation,
        Tactics,
        Education,
        Economics,
        Lore,
        Formality,
        Streetwise,
        Teaching,
        Survival,
        //Magic Skills

        Terrathurgy,
        Aerothurgy,
        Pyromancy,
        Hydromancy,
        Necromancy,
        Mysticism,
        Agrokenesis,
        Transmutation,

        //Charisma Skills
        Persuassion,
        Deception,
        Leadership,
        Performance,
        Seduction,
        Style,
        Insight,

        //Constitution Skills
        Endurance,
        Immunity,

        //Craftsmanship Skills
        FirstAid,
        Smithing,
        Carpentry,
        Leatherworking,
        Tailoring,
        Enchantment,
        Alchemy,
        Cooking,
        FineArts,
        Forgery,
        TrapCrafting,
        Artillery,

        //Resolve Skills
        Courage,
        Intimidation,
        Determination,
        Concentration,
        Discipline,
        Faith,

        //Strength Skills
        Athletics,
        Physique,
        Bully,

        //Finesse Skills
        Escape,
        Acrobatics,
        AnimalHandling,
        LandVehicles,
        WaterVehicles,
        SleightOfHand,
        Stealth,
        Archery,
        Marksmanship,

        //Reflex Skills
        Evasion,
        LongBlades,
        MediumBlades,
        ShortBlades,
        PoleArms,
        Crude,
        Shield,
        Brawl
    }
}
