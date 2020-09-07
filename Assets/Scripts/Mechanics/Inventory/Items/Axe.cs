using Enums;
using Player;
using UnityEngine;

namespace Mechanics.Inventory.Items
{
    [CreateAssetMenu(fileName = "Axe", menuName = "Inventory/Axe")]
    public class Axe : WeaponItem
    {
        Axe()
        {
            type = ItemType.MainHand;
            weaponType = WeaponType.Axe;
        }
    }
}