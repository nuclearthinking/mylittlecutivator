using Enums;
using Player;
using UnityEngine;

namespace Mechanics.Inventory.Items
{

    public class WeaponItem : Equipment
    {
        [SerializeField] protected WeaponType weaponType;
        [SerializeField] protected float damage;
    }
}