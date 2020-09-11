using Core;
using Model;
using Units;

namespace Events
{
    public class EnemyKilled : Simulation.Event<EnemyKilled>
    {
        public Enemy enemy;
        private readonly PlayerModel playerModel = Simulation.GetModel<PlayerModel>();


        public override bool Precondition()
        {
            return enemy != null;
        }

        public override void Execute()
        {
            if (enemy == null)
                return;
            if (enemy.gameObject.GetHashCode() == playerModel.selectedTarget.GetHashCode())
            {
                playerModel.selectedTarget = null;
            }

            playerModel.AddXp(30);
        }
    }
}