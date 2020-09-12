using System.Collections.Generic;
using Core;
using Model;
using Units;
using UnityEngine;

namespace Mechanics
{
    public class EnemyLocator : MonoBehaviour
    {
        public List<Enemy> nearEnemies = new List<Enemy>();
        private readonly PlayerModel playerModel = Simulation.GetModel<PlayerModel>();

        private void Update()
        {
            var refreshedEnemies = new List<Enemy>();

            foreach (var enemy in nearEnemies)
            {
                if (enemy != null && enemy.gameObject.activeSelf)
                {
                    refreshedEnemies.Add(enemy);
                }
            }
            nearEnemies = refreshedEnemies;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Enemy"))
            {
                nearEnemies.Add(other.gameObject.GetComponent<Enemy>());
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Enemy") && nearEnemies.Contains(other.gameObject.GetComponent<Enemy>()))
            {
                nearEnemies.Remove(other.gameObject.GetComponent<Enemy>());
            }
        }


        public void SelectNearestEnemy()
        {
            if (nearEnemies.Count == 0)
                return;

            Vector3 playerPosition = playerModel.position;
            Enemy closestEnemy = null;
            float minimalDistance = 999f;

            foreach (var enemy in nearEnemies)
            {
                var distance = Vector3.Distance(enemy.transform.position, playerPosition);
                if (distance < minimalDistance)
                {
                    closestEnemy = enemy;
                    minimalDistance = distance;
                }
            }

            if (closestEnemy == null)
                return;

            if (playerModel.selectedTarget == closestEnemy)
                return;

            if (playerModel.selectedTarget != null)
            {
                playerModel.selectedTarget.ReleaseAsTarget();
                playerModel.selectedTarget = null;
            }

            playerModel.selectedTarget = closestEnemy;
            playerModel.selectedTarget.SelectedAsTarget();
        }
    }
}