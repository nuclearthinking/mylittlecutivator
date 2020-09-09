using System;
using Mechanics.Inventory;
using Mechanics.Inventory.Items;
using UnityEngine;

namespace Mechanics
{
    public class ItemController : MonoBehaviour
    {
        public Item item;


        private void Awake()
        {
            var renderer = GetComponent<SpriteRenderer>();
            renderer.sprite = item.icon;
        }

        public void PickUp()
        {
            InventoryController.Instance.Add(item);
            Destroy(gameObject);
        }


        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                PickUp();
            }
        }
    }
}