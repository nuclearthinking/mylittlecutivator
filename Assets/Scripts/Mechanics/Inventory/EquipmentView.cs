using System;
using UnityEngine;

namespace Mechanics.Inventory
{
    public class EquipmentView : MonoBehaviour
    {
        public EquipementSlot armorSlot;
        public EquipementSlot bootsSlot;
        public EquipementSlot helmetSlot;
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
            
        }
    }
}