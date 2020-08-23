using Core;
using Model;

namespace Gameplay
{
    public class HurtPlayer: Simulation.Event<HurtPlayer>
    {

        public int damageTaken;
        private PlayerModel playerModel = Simulation.GetModel<PlayerModel>();
        public override void Execute()
        {
            playerModel.currentHealth -= damageTaken;
        }
    }
}