

public class ItemSlot 
{
    public ItemSO item { get; private set; }
    public int amount { get; set; }

    public ItemSlot(ItemSO item, int amount)
    {
        this.item = item;
        this.amount = amount;
    }
}
