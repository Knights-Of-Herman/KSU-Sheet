using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnightsOfHerman.Common.Sanctum.Abstract.Character.Items
{

    /// <summary>
    /// To make an armor piece cover more than 1 slot do ArmorSlot.Head | ArmorSlot.Neck
    /// Add a slot slot |= ArmorSlot.Slot
    /// Remove a slot slot &= ~ArmorSlot.Slot;
    /// armor.Slot & slot to test if armor.Slot contains slot
    /// Idea came from error codes
    /// </summary>
    [Flags]
    public enum ArmorSlot
    {
        Head = 1 << 0,       // 1
        Neck = 1 << 1,       // 2
        Chest = 1 << 2,      // 4
        Back = 1 << 3,       // 8
        Stomach = 1 << 4,    // 16
        LeftArm = 1 << 5,    // 32
        LeftHand = 1 << 6,   // 64
        LeftLeg = 1 << 7,    // 128
        LeftFoot = 1 << 8,   // 256
        RightArm = 1 << 9,   // 512
        RightHand = 1 << 10, // 1024
        RightLeg = 1 << 11,  // 2048
        RightFoot = 1 << 12  // 4096
    }

    /// <summary>
    /// The layer an armor piece takes up
    /// </summary>
    public enum ArmorLayer
    {
        Light = 0,
        Medium = 1,
        Heavy = 2,
    }

    /// <summary>
    /// Represents a piece of armor in Sanctum
    /// </summary>
    public interface ICharacterArmor
    {
        /// <summary>
        /// Armor's ID
        /// </summary>
        public int ItemID { get; }

        /// <summary>
        /// Name of the armor piece
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// General notes abou the armor
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Weight of the armor piece
        /// </summary>
        public decimal Weight { get; set; }

        /// <summary>
        /// How much the armor piece hinders movement
        /// </summary>
        public byte Hindrance { get; set; }

        /// <summary>
        /// Whehter the armor piece is equipped
        /// </summary>
        public bool Equipped { get; set; }

        /// <summary>
        /// The layer the armor piece is on
        /// </summary>
        public ArmorLayer Layer { get; set; }

        /// <summary>
        /// The collection of body part slots the armor piece takes up
        /// </summary>
        public ArmorSlot Slot { get; set; }

        /// <summary>
        /// The amount of bludegoning damage the armor resists
        /// </summary>
        public byte Bludgeoning { get; set; }

        /// <summary>
        /// The amount of piercing damage the armor resists
        /// </summary>
        public byte Piercing { get; set; }

        /// <summary>
        /// The amount of slashing damage the armor resists
        /// </summary>
        public byte Slashing { get; set; }
    }
}
