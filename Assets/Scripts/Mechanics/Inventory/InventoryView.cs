using System;
using Mechanics;
using Mechanics.Inventory;
using UnityEngine;

namespace Ui
{
    public class InventoryView : MonoBehaviour
    {
        public Transform slotsContainer;

        private InventoryController inventoryController;
        private InventorySlot[] slots;

        private void Start()
        {
            inventoryController = InventoryController.Instance;
            inventoryController.onInventoryChangedCallback += UpdateUi;

            slots = slotsContainer.GetComponentsInChildren<InventorySlot>();
        }

        private void Update()
        {
            //
        }

        void UpdateUi()
        {
            for (int i = 0; i < slots.Length; i++)
            {
                if (i < inventoryController.inventory.Count)
                {
                    slots[i].AddItem(inventoryController.inventory[i]);
                }
                else
                {
                    slots[i].ClearSlot();
                }
            }
        }
    }
}