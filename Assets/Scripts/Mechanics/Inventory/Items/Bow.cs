using Player;
using UnityEngine;

namespace Mechanics.Inventory.Items
{
    [CreateAssetMenu(fileName = "Bow", menuName = "Inventory/Bow")]
    public class Bow : WeaponItem
    {
        Bow()
        {
            type = WeaponType.Bow;
        }
    }
}