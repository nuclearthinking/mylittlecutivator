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
            if (item == null)
                return;
            if (item.isEquipable)
            {
                EquipementController.Instance.EquipItem(item as Equipment);
            }

            var removeItem = item.Use();

            if (removeItem)
                InventoryController.Instance.RemoveItem(item);
        }
    }
}