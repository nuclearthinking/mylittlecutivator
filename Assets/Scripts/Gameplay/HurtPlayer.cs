using Core;
using Model;

namespace Gameplay
{
    public class HurtPlayer: Simulation.Event<HurtPlayer>
    {

        public int damageTaken;
        private readonly GameModel gameModel = Simulation.GetModel<GameModel>();
        public override void Execute()
        {
            gameModel.player.TakeDamage(damageTaken);
        }
    }
}