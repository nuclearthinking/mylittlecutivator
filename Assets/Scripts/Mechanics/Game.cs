using Core;
using Mechanics.Inventory.Items;
using Model;
using UnityEngine;

namespace Mechanics
{
    public class Game : MonoBehaviour
    {
        public static Game Instance { get; private set; }
        public GameModel model = Simulation.GetModel<GameModel>();

        [SerializeField] protected Config gameConfig;
        [SerializeField] protected AnimationCurve levelingDifficulty;

        private GameObject lootItemPrefab;

        private void Start()
        {
            lootItemPrefab = Resources.Load<GameObject>("Items/LootItem");
            Physics2D.IgnoreLayerCollision(9,8, true);
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
    }
}