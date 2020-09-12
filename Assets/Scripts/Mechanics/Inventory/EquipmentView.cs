using System;
using System.Collections.Generic;
using UnityEngine;

namespace Mechanics.Inventory
{
    public class EquipmentView : MonoBehaviour
    {
        public EquipementSlot armorSlot;
        public EquipementSlot bootsSlot;
        public EquipementSlot headSlot;
        public EquipementSlot mainHandSlot;
        public EquipementSlot offHandSlot;

        private EquipementController equipementController;

        private List<EquipementSlot> equipementSlots;
        
        private void Start()
        {
            equipementController = EquipementController.Instance;
            equipementController.onEquipementChangedCallback += UpdateUi;
            equipementSlots = new List<EquipementSlot>{armorSlot, bootsSlot, headSlot, mainHandSlot, offHandSlot};
        }

        void UpdateUi()
        {
            foreach (var equipementSlot in equipementSlots)
            {
                CheckEquipment(equipementSlot);
            }
        }

        void CheckEquipment(EquipementSlot equipementSlot)
        {
            var equippedItem = equipementController.GetEquippedItem(equipementSlot.itemType);
            if (equippedItem != null)
            {
                equipementSlot.Add(equippedItem);
            }
        }
    }
}