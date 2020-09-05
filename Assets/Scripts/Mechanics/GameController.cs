using System;
using Core;
using Model;
using UnityEngine;

namespace Mechanics
{
    public class GameController : MonoBehaviour
    {
        public static GameController Instance { get; private set; }
        public GameModel model = Simulation.GetModel<GameModel>();

        [SerializeField] protected Config gameConfig;
        [SerializeField] protected AnimationCurve levelingDifficulty;

        private void Start()
        {
            // Physics2D.IgnoreLayerCollision(9,8, true);
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
    }
}