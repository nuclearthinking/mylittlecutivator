using Enums;
using UnityEngine;

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
    }
}