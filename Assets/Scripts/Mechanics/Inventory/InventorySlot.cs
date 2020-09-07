using Mechanics.Inventory.Items;
using UnityEngine;
using UnityEngine.UI;

namespace Mechanics.Inventory
{
    public class InventorySlot : MonoBehaviour
    {
        public Image icon;

        protected Item item;

        public void AddItem(Item newItem)
        {
            item = newItem;
            icon.sprite = item.icon;
            icon.enabled = true;
        }


        public void ClearSlot()
        {
            item = null;
            icon.sprite = null;
            icon.enabled = false;
        }

        public virtual void SlotClick()
        {
            var removeItem = item.Use();

            if (removeItem)
                InventoryController.Instance.RemoveItem(item);
        }
    }
}