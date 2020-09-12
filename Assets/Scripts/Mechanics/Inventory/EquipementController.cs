using System;
using System.Collections.Generic;
using System.Linq;
using Enums;
using JetBrains.Annotations;
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

        class EquipmentItem
        {
            public ItemType type;
            public Item item;

            public EquipmentItem(ItemType type)
            {
                this.type = type;
            }

            public int GetId()
            {
                return item == null ? 0 : item.id;
            }
        }

        private readonly List<EquipmentItem> equipement = new List<EquipmentItem>
        {
            new EquipmentItem(ItemType.Armor),
            new EquipmentItem(ItemType.Helmet),
            new EquipmentItem(ItemType.Boots),
            new EquipmentItem(ItemType.MainHand),
            new EquipmentItem(ItemType.MainHand)
        };
        public int damageBonus = 0;

        public delegate void OnEquipementChanged();

        public OnEquipementChanged onEquipementChangedCallback;

        private InventoryController inventory;

        private void Start()
        {
            inventory = InventoryController.Instance;
            onEquipementChangedCallback += CalcBonusDamage;
        }

        public void EquipItem(Item item)
        {
            foreach (var equipmentItem in equipement)
            {
                if (equipmentItem.type == item.type)
                {
                    if (equipmentItem.item != null)
                    {
                        inventory.Add(item);
                    }

                    equipmentItem.item = item;
                    onEquipementChangedCallback?.Invoke();
                    return;
                }
            }
        }

        public void UnEquipItem(Item item)
        {
            foreach (var equipmentItem in equipement)
            {
                if (equipmentItem.item == item)
                {
                    inventory.Add(item);
                    equipmentItem.item = null;
                    onEquipementChangedCallback?.Invoke();
                    return;
                }
            }
        }

        private void CalcBonusDamage()
        {
            var damage = equipement.Where(
                equipmentItem => equipmentItem.item != null
            ).Sum(
                equipmentItem => equipmentItem.item.damageBonus
            );

            damageBonus = damage;
        }

        public Item GetEquippedItem(ItemType itemType)
        {
            foreach (var equipmentItem in equipement)
            {
                if (equipmentItem.type == itemType)
                {
                    return equipmentItem.item;
                }
            }
            return null;
        }

        public int[] GetEquipmentAsIds()
        {
            return new[]
            {
                equipement[0].GetId(),
                equipement[1].GetId(),
                equipement[2].GetId(),
                equipement[3].GetId(),
                equipement[4].GetId(),
            };
        }

        public void LoadEquipment(int[] itemIds)
        {
            for (int i = 0; i < itemIds.Length; i++)
            {
                if (itemIds[i] != 0)
                {
                    var item = Game.Instance.GetItemById(itemIds[i]);
                    equipement[i].item = item;
                }
            }
            onEquipementChangedCallback?.Invoke();
        }
    }
}