using Core;
using Player;
using UnityEngine;

namespace Gameplay
{
    public class PlayerShooting : Simulation.Event<PlayerShooting>
    {

        public PlayerView playerView;
        public override void Execute()
        {
            
            Debug.Log("Player SHOOTING");
        }
    }
}