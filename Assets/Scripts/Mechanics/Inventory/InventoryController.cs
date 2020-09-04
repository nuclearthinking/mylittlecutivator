using System.Collections.Generic;
using Mechanics.Inventory.Items;
using UnityEngine;

namespace Mechanics.Inventory
{
    public class InventoryController : MonoBehaviour
    {
        #region Singleton
        public static InventoryController Instance { get; private set; }
        private void Awake()
        {
            if (Instance == null)
                Instance = this;
        }
        #endregion

        public delegate void OnInventoryChanged();
        public OnInventoryChanged onInventoryChangedCallback;
        
        public List<Item> inventory = new List<Item>();

        public void Add(Item item)
        {
            inventory.Add(item);
            onInventoryChangedCallback?.Invoke();
        }

        public void RemoveItem(Item item)
        {
            inventory.Remove(item);
            onInventoryChangedCallback?.Invoke();
        }
    }
}