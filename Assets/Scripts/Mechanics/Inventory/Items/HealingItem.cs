using Core;
using Gameplay;
using UnityEngine;

namespace Mechanics.Inventory.Items
{
    [CreateAssetMenu(fileName = "Healing Item", menuName = "Inventory/Healing Item", order = 0)]
    public class HealingItem : Item
    {
        public int healingAmount = 30;
        public bool usable;


        public override bool Use()
        {
            if (usable)
            {
                Simulation.Schedule<HealPlayer>().healingAmount = healingAmount;
                return true;
            }

            return false;
        }
    }
}