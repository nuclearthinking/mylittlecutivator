using Mechanics.Inventory.Items;
using UnityEngine;
using UnityEngine.UI;

namespace Mechanics.Inventory
{
    public class EquipmentSlot : MonoBehaviour
    {
        [SerializeField] private Equipment item;

        [SerializeField] private Image icon;
    }
}