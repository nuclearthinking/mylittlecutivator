using JetBrains.Annotations;
using Mechanics.Inventory.Items;
using UnityEngine;

namespace Model
{
    [System.Serializable]
    public class DropItemChance
    {
        [SerializeField] private Item item;
        [SerializeField] private float chance;

        [CanBeNull]
        public Item Roll()
        {
            var diceRoll = Random.Range(0.0f, 100.0f);
            return diceRoll <= chance ? item : null;
        }
    }
}