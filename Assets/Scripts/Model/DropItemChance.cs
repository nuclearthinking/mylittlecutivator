using JetBrains.Annotations;
using UnityEngine;

namespace Model
{
    [System.Serializable]
    public class DropItemChance
    {
        [SerializeField] private GameObject item;
        [SerializeField] private float chance;

        [CanBeNull]
        public GameObject Roll()
        {
            var diceRoll = Random.Range(0.0f, 100.0f);
            return diceRoll <= chance ? item : null;
        }
    }
}