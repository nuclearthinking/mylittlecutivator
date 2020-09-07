using Core;
using Enums;
using Gameplay;
using UnityEngine;

namespace Mechanics.Inventory.Items
{
    [CreateAssetMenu(fileName = "Healing Item", menuName = "Inventory/Healing Item", order = 0)]
    public class HealingItem : Item
    {
        public int healingAmount = 30;

        HealingItem()
        {
            type = ItemType.Consumable;
        }

        public override bool Use()
        {
            if (type == ItemType.Consumable) 
            {
                Simulation.Schedule<HealPlayer>().healingAmount = healingAmount;
                return true;
            }

            return false;
        }
    }
}