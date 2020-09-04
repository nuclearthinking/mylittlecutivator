using Player;
using UnityEngine;

namespace Mechanics.Inventory.Items
{

    public class WeaponItem : Equipment
    {
        [SerializeField] protected WeaponType type;
        [SerializeField] protected float damage;
    }
}