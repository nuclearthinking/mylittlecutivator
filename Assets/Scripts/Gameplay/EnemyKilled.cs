using Core;
using Model;

namespace Gameplay

{
    public class EnemyKilled : Simulation.Event<EnemyKilled>
    {

        public TreantController treant;

        private readonly GameModel gameModel = Simulation.GetModel<GameModel>();
        
        public override bool Precondition()
        {
            return treant != null;
        }

        public override void Execute()
        {
            if (treant == null) 
                return;
            if (treant.gameObject.GetHashCode() == gameModel.player.selectedTarget.GetHashCode())
            {
                gameModel.player.selectedTarget = null;
            }
            
            PlayerModel player = Simulation.GetModel<PlayerModel>();
            player.AddXp(30);
        }
    }
}