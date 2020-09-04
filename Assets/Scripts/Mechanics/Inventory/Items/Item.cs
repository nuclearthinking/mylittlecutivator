using UnityEngine;

namespace Mechanics.Inventory.Items
{
    public class Item : ScriptableObject
    {
        public new string name = "New Item";
        public Sprite icon = null;


        public virtual bool Use()
        {
            return false;
        }
    }
}