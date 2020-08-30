using UnityEngine;

namespace Mechanics
{
    [CreateAssetMenu(fileName = "Item", menuName = "Inventory/Item", order = 0)]
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