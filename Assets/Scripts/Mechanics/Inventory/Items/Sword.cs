using Enums;
using Player;
using UnityEngine;

namespace Mechanics.Inventory.Items
{
    [CreateAssetMenu(fileName = "Sword", menuName = "Inventory/Sword")]
    public class Sword : WeaponItem
    {
        Sword()
        {
            type = ItemType.MainHand;
            weaponType = WeaponType.Sword;
        }
    }
}