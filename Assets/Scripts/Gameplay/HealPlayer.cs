using Core;
using Model;

namespace Gameplay
{
    public class HealPlayer : Simulation.Event<HealPlayer>
    {
        public int healingAmount = 0;
        private readonly PlayerModel playerModel = Simulation.GetModel<PlayerModel>();
        public override void Execute()
        {
            if (healingAmount > 0)
            {
                playerModel.IncrementHealth(healingAmount);
            }
        }
    }
}