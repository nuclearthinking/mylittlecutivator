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
        public Joystick joystick;
        public Button attackButton;
        private readonly PlayerModel model = Simulation.GetModel<PlayerModel>();

        private Animator animator;

        private void Awake()
        {
            animator = GetComponent<Animator>();
            attackButton.onClick.AddListener(AttackButtonPressed);
        }

        void Update()
        {
            // GET INPUT
            model.xInput = joystick.Horizontal;
            model.yInput = joystick.Vertical;

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
    }
}