using Core;
using Player;
using UnityEngine;

namespace Gameplay
{
    public class PlayerLevelUp: Simulation.Event<PlayerLevelUp>
    {

        public PlayerController player;
        
        public override void Execute()
        {
            Debug.Log("LEVELUP!!!");
        }
    }
}