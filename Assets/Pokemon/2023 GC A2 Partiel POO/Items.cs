using UnityEngine;

public class Items : MonoBehaviour
{

    public class HealPotion : Item
    {
        public HealPotion() : base(ItemEffect.Heal, 50)
        {
            
        }
    }
    public class SuperPotion : Item
    {
        public SuperPotion() : base(ItemEffect.Heal, 100)
        {
            
        }
    }
    public class StrenghPotion : Item
    {
        public StrenghPotion() : base(ItemEffect.Strength, 20)
        {
            
        }
    }
    public class DefensePotion : Item
    {
        public DefensePotion() : base(ItemEffect.Defense, 25)
        {
            
        }
    }
    public class SpeedPotion : Item
    {
        public SpeedPotion() : base(ItemEffect.Speed, 20)
        {
            
        }
    }
}
