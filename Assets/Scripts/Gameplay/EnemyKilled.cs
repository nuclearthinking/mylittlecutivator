using Core;
using Model;

namespace Gameplay

{
    public class EnemyKilled : Simulation.Event<EnemyKilled>
    {

        public TreantController treant;

        public override bool Precondition()
        {
            return treant != null;
        }

        public override void Execute()
        {
            if (treant == null) 
                return;
            PlayerModel player = Simulation.GetModel<PlayerModel>();
            player.AddXP(30);
        }
    }
}