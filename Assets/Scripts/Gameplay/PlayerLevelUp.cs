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

        public override void Execute()
        {
            playerModel.currentXp -= playerModel.nextLevelXp;
            playerModel.level += 1;
            playerModel.nextLevelXp = GameController.Instance.GetExpToNextLevel(playerModel.level);
            Debug.Log("LEVELUP!!!");
        }
    }
}