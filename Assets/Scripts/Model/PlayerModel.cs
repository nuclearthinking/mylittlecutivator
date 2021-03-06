﻿using Mechanics.Inventory;
using Units;
using UnityEngine;

namespace Model
{
    public class PlayerModel
    {
        public int currentHealth;
        public int maximumHealth = 100;

        // MOVEMENT
        public Vector3 position;
        public Vector2 movement;
        public Vector2 lastMove = new Vector2(0, 0);
        public float xInput;
        public float yInput;

        public Enemy selectedTarget;

        public float nextFireTime;
        public float arrowForce = 20f;
        public float fireRate = 0.3f;

        // LEVELING
        public int level = 1;
        public int currentXp;
        public int nextLevelXp = 100;

        // STATS
        public int baseDamage = 20;
        public float criticalHitChance = 5.0f;

        public void SetXInput(float x)
        {
            xInput = x;
        }

        public void SetYInput(float y)
        {
            yInput = y;
        }

        public void AddXp(int xpAmount)
        {
            currentXp += xpAmount;
        }

        public int GetDamageDeal()
        {
            int damage = baseDamage + (int) (baseDamage * (level * 0.3));
            return damage + EquipementController.Instance.damageBonus;
        }

        public void IncrementHealth(int health)
        {
            currentHealth += health;
            currentHealth = currentHealth >= maximumHealth ? maximumHealth : currentHealth;
        }

        public void DecrementHealth(int health)
        {
            currentHealth -= health;
        }

        public int GetHealth()
        {
            return currentHealth;
        }
    }
}