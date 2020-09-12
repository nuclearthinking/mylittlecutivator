using System;
using Enums;
using UnityEngine;
using Utils;

namespace Mechanics.Inventory.Items
{
    [CreateAssetMenu(fileName = "Healing Item", menuName = "Inventory/Item", order = 0)]
    public class Item : ScriptableObject, ICloneable
    {
        public int id;
        public new string name = "New Item";
        public Sprite icon = null;
        public ItemType type;
        public ItemQuality quality;
        public int damageBonus = 0;
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


        public object Clone()
        {
            var newItem = CreateInstance<Item>();
            newItem.id = id;
            newItem.name = name;
            newItem.icon = icon;
            newItem.type = type;
            newItem.quality = quality;
            newItem.damageBonus = damageBonus;
            return newItem;
        }
    }    
        
}