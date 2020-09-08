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

        public Equipment Armor { get; private set; }
        public Equipment MainHand { get; private set; }
        public Equipment Boots { get; private set; }
        public Equipment OffHand { get; private set; }
        public Equipment Head { get; private set; }

        public delegate void OnEquipementChanged();

        public OnEquipementChanged onEquipementChangedCallback;

        public void EquipItem(Equipment item)
        {
            switch (item.type)
            {
                case ItemType.Armor:
                    Armor = item;
                    onEquipementChangedCallback?.Invoke();
                    break;
                case ItemType.Boots:
                    Boots = item;
                    onEquipementChangedCallback?.Invoke();
                    break;
                case ItemType.Helmet:
                    Head = item;
                    onEquipementChangedCallback?.Invoke();
                    break;
                case ItemType.MainHand:
                    MainHand = item;
                    onEquipementChangedCallback?.Invoke();
                    break;
                case ItemType.OffHand:
                    OffHand = item;
                    onEquipementChangedCallback?.Invoke();
                    break;
                default:
                    return;
            }
        }
    }
}