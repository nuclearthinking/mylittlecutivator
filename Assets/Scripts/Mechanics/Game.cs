using System;
using System.Collections;
using System.Collections.Generic;
using Core;
using Mechanics.Inventory;
using Mechanics.Inventory.Items;
using Model;
using Save;
using UnityEngine;

namespace Mechanics
{
    public class NotUniqueItemIdException : Exception
    {
        public NotUniqueItemIdException(string message) : base(message)
        {
        }
    }

    public class Game : MonoBehaviour
    {
        public static Game Instance { get; private set; }
        public GameModel model = Simulation.GetModel<GameModel>();

        [SerializeField] protected Config gameConfig;
        [SerializeField] protected AnimationCurve levelingDifficulty;

        private GameObject lootItemPrefab;

        private Dictionary<int, Item> items = new Dictionary<int, Item>();

        private void Start()
        {
            lootItemPrefab = Resources.Load<GameObject>("Prefabs/LootItem");
            Physics2D.IgnoreLayerCollision(9, 8, true);
            LoadItemsResources();
            LoadGame();
        }

        public int GetExpToNextLevel(int currentLevel)
        {
            return (int) levelingDifficulty.Evaluate(currentLevel);
        }

        public Config Config => this.gameConfig;

        private void OnEnable()
        {
            Instance = this;
        }

        private void OnDisable()
        {
            if (Instance == this) Instance = null;
        }

        private void Update()
        {
            if (Instance == this) Simulation.Tick();
            StartCoroutine(SaveGameCoroutine());
        }


        public GameObject DropItem(Item item, Vector3 position)
        {
            var newItem = Instantiate(lootItemPrefab, position, Quaternion.identity);
            var renderer = newItem.GetComponent<SpriteRenderer>();
            renderer.sprite = item.icon;
            renderer.material.SetColor("OutlineColor", ItemUtils.ColorByQuality(item.quality));
            newItem.GetComponent<ItemController>().item = item;
            return newItem;
        }

        private void LoadItemsResources()
        {
            var loadedItems = Resources.LoadAll<Item>("Items");
            foreach (var item in loadedItems)
            {
                if (items.ContainsKey(item.id))
                {
                    throw new NotUniqueItemIdException($"Not unique item id for item {item}");
                }

                items.Add(item.id, item);
            }
        }

        public Item GetItemById(int id)
        {
            if (items.ContainsKey(id))
            {
                return (Item) items[id].Clone();
            }
            return null;
        }
        
        
        private SaveData GetSaveData()
        {
            var playerData = Simulation.GetModel<PlayerModel>();
            var inventory = InventoryController.Instance;
            var equipment = EquipementController.Instance;
            
            var saveData = new SaveData();

            saveData.level = playerData.level;
            saveData.currentExp = playerData.currentXp;
            saveData.currentHealth = playerData.currentHealth;
            
            saveData.inventory = inventory.GetItemsAsIds();
            saveData.equipment = equipment.GetEquipmentAsIds();

            return saveData;
        }


        private void LoadGameData(SaveData data)
        {
            var playerData = Simulation.GetModel<PlayerModel>();
            var inventory = InventoryController.Instance;
            var equipment = EquipementController.Instance;
            playerData.level = data.level;
            playerData.currentXp = data.currentExp;
            playerData.currentHealth = data.currentHealth;
            inventory.LoadInventory(data.inventory);
            equipment.LoadEquipment(data.equipment);
        }

        private IEnumerator SaveGameCoroutine()
        {
            yield return new WaitForSeconds(10f);
            SaveGame();
        }
        
        protected void SaveGame()
        {
            var saveData = GetSaveData();
            SaveSystem.Save(saveData);
        }

        private void LoadGame()
        {
            var loadedData = SaveSystem.Load();
            if (loadedData != null)
            {
                LoadGameData(loadedData);
            }
        }
    }
}