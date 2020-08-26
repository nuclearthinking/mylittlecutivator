using System;
using System.Collections.Generic;
using Core;
using Model;
using UnityEngine;

namespace Mechanics
{
    public class EnemyLocatorController : MonoBehaviour
    {

        private InputModel inputModel = Simulation.GetModel<InputModel>();


        private void Update()
        {
            var tempEnemies = new List<GameObject>();

            foreach (var enemy in inputModel.nearEnemies)
            {
                if (enemy != null && enemy.activeSelf)
                {
                    tempEnemies.Add(enemy);
                }
            }

            inputModel.nearEnemies = tempEnemies;

        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Enemy"))
            {
                inputModel.nearEnemies.Add(other.gameObject);
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Enemy") && inputModel.nearEnemies.Contains(other.gameObject))
            {
                inputModel.nearEnemies.Remove(other.gameObject);
            }
        }
    }
}