using Enums;
using Mechanics.Inventory.Items;
using UnityEngine;
using UnityEngine.UI;
using Utils;

namespace Mechanics.Inventory
{
    public class EquipementSlot : MonoBehaviour
    {
        [SerializeField] private ItemType itemType;
        public Image itemIcon;
        public Image defaultIcon;
        private Item item;

        private bool IsEquipped => item != null;

        public void Add(Item newItem)
        {
            item = newItem;
            defaultIcon.enabled = false;
            itemIcon.sprite = item.icon;
        }

        public void Remove()
        {
            item = null;
            defaultIcon.enabled = true;
            itemIcon.sprite = null;
            itemIcon.enabled = false;
        }

        public void SlotClick()
        {
            if (IsEquipped)
            {
                Remove();
            }
        }
    }
}