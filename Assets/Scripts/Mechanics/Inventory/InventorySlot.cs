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
            if (item.IsEquipable)
            {
                EquipItem(item);
            } else if (item.IsConsumable)
            {
                ConsumeItem(item);
            }
        }

        private void ConsumeItem(Item itemToConsume)
        {
            var removeItem = itemToConsume.Use();

            if (removeItem)
                InventoryController.Instance.RemoveItem(itemToConsume);
        }

        private void EquipItem(Item itemToEquip)
        {
            EquipementController.Instance.EquipItem(itemToEquip as Equipment);
            InventoryController.Instance.RemoveItem(itemToEquip);
        }
    }
}