using System;
using Core;
using Model;
using UnityEngine;

namespace Player
{
    public class PlayerView : MonoBehaviour
    {
        public Joystick joystick;
        public Transform firePoint;
        private readonly PlayerModel model = Simulation.GetModel<PlayerModel>();

        private Animator animator;

        private void Awake()
        {
            animator = GetComponent<Animator>();
        }

        void Update()
        {
            // GET INPUT
            model.xInput = joystick.Horizontal;
            model.yInput = joystick.Vertical;

            // MODIFY VIEW
            firePoint.transform.rotation = model.firePointRotation;
            animator.SetFloat("Horizontal", model.movement.x);
            animator.SetFloat("Vertical", model.movement.y);
            animator.SetFloat("Speed", model.movement.sqrMagnitude);
            animator.SetFloat("MoveX", model.movement.x);
            animator.SetFloat("MoveY", model.movement.y);
            animator.SetFloat("LastMoveX", model.lastMove.x);
            animator.SetFloat("LastMoveY", model.lastMove.y);
        }
    }
}