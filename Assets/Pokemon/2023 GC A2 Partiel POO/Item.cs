using UnityEngine;

public class Item
{
    public enum ItemEffect
    {
        Heal, 
        Speed, 
        Strength, 
        Defense, 
    }
    
    public ItemEffect Effect { get; protected set; }
    
    public Item(ItemEffect effect, int value)
    {
        Effect = effect;
        BonusValue = value;
    }

    public int BonusValue { get; protected set; }
}
