using Core;
using Model;

namespace Gameplay

{
    public class EnemyKilled : Simulation.Event<EnemyKilled>
    {

        public TreantController treant;
        private readonly InputModel inputModel = Simulation.GetModel<InputModel>();
        
        public override bool Precondition()
        {
            return treant != null;
        }

        public override void Execute()
        {
            if (treant == null) 
                return;
            if (treant.gameObject.GetHashCode() == inputModel.selectedTarget.GetHashCode())
            {
                inputModel.selectedTarget = null;
            }
            
            PlayerModel player = Simulation.GetModel<PlayerModel>();
            player.AddXp(30);
        }
    }
}