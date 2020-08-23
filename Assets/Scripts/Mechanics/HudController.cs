using System;
using Core;
using Model;
using UnityEngine;
using UnityEngine.UI;

namespace Mechanics
{
    public class HudController: MonoBehaviour
    {    
        // XP INDICATION
        public Slider xpBar;
        public Text level;
        
        //HP INDICATION
        public Slider hpBar;
        public Gradient hpBarGradient;
        public Image hpBarFill;
        
        private PlayerModel playerModel = Simulation.GetModel<PlayerModel>();


        private void Start()
        {
            hpBar.maxValue = playerModel.maximumHealth;
            hpBar.value = playerModel.currentHealth;
        }

        private void Update()
        {
            xpBar.maxValue = playerModel.nextLevelXp;
            xpBar.value = playerModel.currentXp;
            level.text = playerModel.level.ToString();
            hpBar.value = playerModel.currentHealth;
            hpBarFill.color = hpBarGradient.Evaluate(hpBar.normalizedValue);
        }
    }
    
    
    
}