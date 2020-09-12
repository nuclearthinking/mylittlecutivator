using System.Collections.Generic;
using System.Linq;
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

        public int[] GetItemsAsIds()
        {
            return (from item in inventory select item.id).ToArray();
        }

        public void LoadInventory(int[] itemIds)
        {
            foreach (var itemId in itemIds)
            {
                var item = Game.Instance.GetItemById(itemId);
                if (item != null)
                {
                    inventory.Add(item);
                }
            }
            onInventoryChangedCallback?.Invoke();
        }
    }
}