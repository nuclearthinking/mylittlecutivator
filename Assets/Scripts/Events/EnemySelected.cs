using Core;
using Model;
using Units;

namespace Events
{
    public class EnemySelected : Simulation.Event<EnemySelected>
    {
        public Enemy target;

        private readonly PlayerModel playerModel = Simulation.GetModel<PlayerModel>();

        public override void Execute()
        {
            if (target == null)
                return;
            playerModel.selectedTarget.ReleaseAsTarget();
            playerModel.selectedTarget = target;
            target.SelectedAsTarget();
        }
    }
}