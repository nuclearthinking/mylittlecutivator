using System;
using UnityEngine;

namespace Mechanics
{
    public class ItemController : MonoBehaviour
    {
        public Item item;
        public Color outlineColor;


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