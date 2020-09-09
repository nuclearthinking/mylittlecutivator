using Enums;
using UnityEngine;
using Utils;

namespace Mechanics.Inventory.Items
{
    public class Item : ScriptableObject
    {
        public new string name = "New Item";
        public Sprite icon = null;
        public ItemType type;
        public ItemQuality quality;

        public virtual bool Use()
        {
            return false;
        }

        public bool IsEquipable =>
            type.In(
                ItemType.Armor,
                ItemType.MainHand,
                ItemType.Boots,
                ItemType.Helmet,
                ItemType.OffHand
            );

        public bool IsConsumable => type == ItemType.Consumable;
    }    
        
}