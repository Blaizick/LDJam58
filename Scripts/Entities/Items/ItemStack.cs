namespace Banchy
{
    public class ItemStack
    {
        public ItemType ItemType { get; set; }
        public int Amount { get; set; }

        public ItemStack() : this(null, -1) { }
        public ItemStack(ItemType itemType, int amount)
        {
            ItemType = itemType;
            Amount = amount;
        }
    }
}