namespace KnightsOfHerman.Common.Sanctum.Abstract.Character.Items
{
    /// <summary>
    /// An item representation
    /// </summary>
    public interface ICharacterItem
    {
        /// <summary>
        /// Item's ID
        /// </summary>
        public int ItemID { get; }

        /// <summary>
        /// Name of the item
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Brief description of the Item
        /// </summary>
        public string Description { get; set; }
        
        /// <summary>
        /// Weight of the item
        /// </summary>
        public decimal Weight { get; set; }

        /// <summary>
        /// Amount of the item
        /// </summary>
        public short Quantity { get; set; }
    }
}
