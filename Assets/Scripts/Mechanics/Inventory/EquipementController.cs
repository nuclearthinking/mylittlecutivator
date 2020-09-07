using System;
using Enums;
using Mechanics.Inventory.Items;
using UnityEditor;
using UnityEngine;
using Utils;

namespace Mechanics.Inventory
{
    public class EquipementController : MonoBehaviour
    {
        #region Singleton
        public static EquipementController Instance { get; private set; }
        private void Awake()
        {
            if (Instance == null)
                Instance = this;
        }
        #endregion

        private Equipment head;
        private Equipment armor;
        private Equipment mainHand;
        private Equipment boots;
        private Equipment offHand;
        
        public delegate void OnEquipementChanged();
        public OnEquipementChanged onEquipementChangedCallback;
        
        public void EquipItem(Equipment item)
        {
            switch (item.type)
            {
                case ItemType.Armor:
                    armor = item;
                    onEquipementChangedCallback?.Invoke();
                    break;
                case ItemType.Boots:
                    boots = item;
                    onEquipementChangedCallback?.Invoke();
                    break;
                case ItemType.Helmet:
                    head = item;
                    onEquipementChangedCallback?.Invoke();
                    break;
                case ItemType.MainHand:
                    mainHand = item;
                    onEquipementChangedCallback?.Invoke();
                    break;
                case ItemType.OffHand:
                    offHand = item;
                    onEquipementChangedCallback?.Invoke();
                    break;
                default:
                    return;
            }
        }
    }
}