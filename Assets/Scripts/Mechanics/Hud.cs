using System;
using System.Collections;
using Core;
using Model;
using UnityEngine;
using UnityEngine.UI;

namespace Mechanics
{
    public class Hud : MonoBehaviour
    {
        // XP INDICATION
        public Slider xpBar;
        public Text level;

        //HP INDICATION
        public Slider hpBar;
        public Gradient hpBarGradient;
        public Image hpBarFill;

        private PlayerModel playerModel = Simulation.GetModel<PlayerModel>();

        private void Awake()
        {
            hpBar.maxValue = playerModel.maximumHealth;
            hpBar.value = playerModel.GetHealth();
        }

        private void Start()
        {
            UpdateHud();
        }

        private void Update()
        {
            StartCoroutine(UpdateHudCoroutine());
        }


        IEnumerator UpdateHudCoroutine()
        {
            yield return new WaitForSeconds(.5f);
            UpdateHud();
        }

        private void UpdateHud()
        {
            xpBar.maxValue = playerModel.nextLevelXp;
            xpBar.value = playerModel.currentXp;
            level.text = playerModel.level.ToString();
            hpBar.value = playerModel.GetHealth();
            hpBarFill.color = hpBarGradient.Evaluate(hpBar.normalizedValue);
        }
    }
}