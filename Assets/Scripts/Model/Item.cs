using UnityEngine;

namespace Mechanics
{
    [CreateAssetMenu(fileName = "Item", menuName = "Inventory/Item", order = 0)]
    public class Item : ScriptableObject
    {
        new public string name = "New Item";
        public Sprite icon = null;
    }
}