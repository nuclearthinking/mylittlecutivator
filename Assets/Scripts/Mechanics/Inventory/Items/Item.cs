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

        public virtual bool Use()
        {
            return false;
        }

        public bool isEquipable =>
            type.In(
                ItemType.Armor,
                ItemType.MainHand,
                ItemType.Boots,
                ItemType.Helmet,
                ItemType.OffHand
            );
    }
}