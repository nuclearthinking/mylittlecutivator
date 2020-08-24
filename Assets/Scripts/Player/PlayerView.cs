using System;
using Core;
using Gameplay;
using Model;
using UnityEngine;
using UnityEngine.UI;

namespace Player
{
    public class PlayerView : MonoBehaviour
    {
        public Button attackButton;
        private readonly PlayerModel model = Simulation.GetModel<PlayerModel>();

        public GameObject treant;
        private Animator animator;

        private void Awake()
        {
            animator = GetComponent<Animator>();
            attackButton.onClick.AddListener(AttackButtonPressed);
        }
        GameObject newTreant;

        void Update()
        {
            // MODIFY VIEW
            animator.SetFloat("Horizontal", model.movement.x);
            animator.SetFloat("Vertical", model.movement.y);
            animator.SetFloat("Speed", model.movement.sqrMagnitude);
            animator.SetFloat("MoveX", model.movement.x);
            animator.SetFloat("MoveY", model.movement.y);
            animator.SetFloat("LastMoveX", model.lastMove.x);
            animator.SetFloat("LastMoveY", model.lastMove.y);
        }

        private void AttackButtonPressed()
        {
            if (Time.time < model.nextFireTime)
                return;

            animator.SetTrigger("Attack");
            Simulation.Schedule<PlayerShooting>();
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Enemy"))
            {
                var hurtEvent = Simulation.Schedule<HurtPlayer>();
                hurtEvent.damageTaken = 10;
            }
        }
    }
}