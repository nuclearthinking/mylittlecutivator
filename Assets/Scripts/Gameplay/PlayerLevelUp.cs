using Core;
using Mechanics;
using Model;
using Player;
using UnityEngine;

namespace Gameplay
{
    public class PlayerLevelUp : Simulation.Event<PlayerLevelUp>
    {
        private PlayerModel playerModel = Simulation.GetModel<PlayerModel>();
        public PlayerController player;

        public override void Execute()
        {
            playerModel.currentXp -= playerModel.nextLevelXp;
            playerModel.level += 1;
            playerModel.nextLevelXp = Game.Instance.GetExpToNextLevel(playerModel.level);
            player.GetComponentInChildren<ParticleSystem>().Play();
        }
    }
}