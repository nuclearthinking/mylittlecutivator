using System;
using System.Collections.Generic;
using UnityEngine;

namespace Mechanics
{
    public class InventoryController : MonoBehaviour
    {
        public static InventoryController Instance { get; private set; }


        private void Awake()
        {
            if (Instance == null)
                Instance = new InventoryController();;
        }

        private static List<Item> inventory = new List<Item>();

        public void Add(Item item)
        {
            inventory.Add(item);
        }
    }
}