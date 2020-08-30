﻿using UnityEngine;
using UnityEngine.UI;

namespace Mechanics.Inventory
{
    public class InventorySlot : MonoBehaviour
    {
        public Image icon;

        private Item item;

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

    }
}