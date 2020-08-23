using System;
using Core;
using Model;
using UnityEngine;
using UnityEngine.UI;

namespace Mechanics
{
    public class HUDController: MonoBehaviour
    {
        public Slider xpBar;
        public Text level;

        private PlayerModel model = Simulation.GetModel<PlayerModel>();
        
        private void Update()
        {
            xpBar.maxValue = model.nextLevelXp;
            xpBar.value = model.currentXp;
            level.text = model.level.ToString();
        }
    }
    
    
    
}