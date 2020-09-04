using UnityEngine;

namespace Mechanics.Inventory.Items
{
    public enum WeaponType
    {
        Bow,
        Sword,
        Axe
    }

    public class WeaponItem : Equipment
    {
        [SerializeField] protected WeaponType type;
        [SerializeField] protected float damage;
    }

    [CreateAssetMenu(fileName = "Axe", menuName = "Inventory/Axe")]
    public class Axe : WeaponItem
    {
        Axe()
        {
            type = WeaponType.Axe;
        }
    }

    [CreateAssetMenu(fileName = "Bow", menuName = "Inventory/Bow")]
    public class Bow : WeaponItem
    {
        Bow()
        {
            type = WeaponType.Bow;
        }
    }

    [CreateAssetMenu(fileName = "Sword", menuName = "Inventory/Sword")]
    public class Sword : WeaponItem
    {
        Sword()
        {
            type = WeaponType.Sword;
        }
    }
}