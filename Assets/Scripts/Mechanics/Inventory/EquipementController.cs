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

        public int damageBonus = 0;

        public delegate void OnEquipementChanged();

        public OnEquipementChanged onEquipementChangedCallback;

        private InventoryController inventory;

        private void Start()
        {
            inventory = InventoryController.Instance;
            onEquipementChangedCallback += CalcBonusDamage;
        }

        public void EquipItem(Equipment item)
        {
            switch (item.type)
            {
                case ItemType.Armor:
                    if (Armor != null)
                        inventory.Add(Armor);
                    Armor = item;
                    onEquipementChangedCallback?.Invoke();
                    break;
                case ItemType.Boots:
                    if (Boots != null)
                        inventory.Add(Boots);
                    Boots = item;
                    onEquipementChangedCallback?.Invoke();
                    break;
                case ItemType.Helmet:
                    if (Head != null)
                        inventory.Add(Head);
                    Head = item;
                    onEquipementChangedCallback?.Invoke();
                    break;
                case ItemType.MainHand:
                    if (MainHand != null)
                        inventory.Add(MainHand);
                    MainHand = item;
                    onEquipementChangedCallback?.Invoke();
                    break;
                case ItemType.OffHand:
                    if (OffHand != null)
                        inventory.Add(OffHand);
                    OffHand = item;
                    onEquipementChangedCallback?.Invoke();
                    break;
                default:
                    return;
            }
        }

        public void UnEquipItem(Item item)
        {
            switch (item.type)
            {
                case ItemType.Armor:
                    inventory.Add(item);
                    Armor = null;
                    onEquipementChangedCallback?.Invoke();
                    break;
                case ItemType.Boots:
                    inventory.Add(item);
                    Boots = null;
                    onEquipementChangedCallback?.Invoke();
                    break;
                case ItemType.Helmet:
                    inventory.Add(item);
                    Head = null;
                    onEquipementChangedCallback?.Invoke();
                    break;
                case ItemType.MainHand:
                    inventory.Add(item);
                    MainHand = null;
                    onEquipementChangedCallback?.Invoke();
                    break;
                case ItemType.OffHand:
                    inventory.Add(item);
                    OffHand = null;
                    onEquipementChangedCallback?.Invoke();
                    break;
                default:
                    return;
            }
        }

        private void CalcBonusDamage()
        {
            int damage = 0;
            if (MainHand != null) damage += MainHand.damageBonus;
            if (OffHand != null) damage += OffHand.damageBonus;
            if (Armor != null) damage += Armor.damageBonus;
            if (Boots != null) damage += Boots.damageBonus;
            if (Head != null) damage += Head.damageBonus;

            damageBonus = damage;
        }
    }
}