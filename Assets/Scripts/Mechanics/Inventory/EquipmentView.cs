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
        
        private void Start()
        {
            equipementController = EquipementController.Instance;
            equipementController.onEquipementChangedCallback += UpdateUi;

        }

        void UpdateUi()
        {
            if (equipementController.Armor != null)
            {
                armorSlot.Add(equipementController.Armor);
            }

            if (equipementController.Boots != null)
            {
                bootsSlot.Add(equipementController.Armor);
            }

            if (equipementController.Head != null)
            {
                headSlot.Add(equipementController.Head);
            }

            if (equipementController.MainHand != null)
            {
                mainHandSlot.Add(equipementController.MainHand);
            }

            if (equipementController.OffHand != null)
            {
                offHandSlot.Add(equipementController.OffHand);
            }
        }
    }
}