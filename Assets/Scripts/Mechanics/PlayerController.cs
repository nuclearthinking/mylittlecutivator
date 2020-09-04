using System.Collections;
using Core;
using Gameplay;
using Mechanics;
using Model;
using UnityEngine;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        public PlayerModel model = Simulation.GetModel<PlayerModel>();
        public Transform firePoint;
        private readonly InputModel inputModel = Simulation.GetModel<InputModel>();

        private Rigidbody2D rb;

        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            model.IncrementHealth(model.maximumHealth);
        }

        private void Update()
        {
            model.movement.x = model.xInput;
            model.movement.y = model.yInput;

            var playerDirection = GetPlayerDirection(model.xInput, model.yInput);
            Quaternion firePointRotation;
            if (playerDirection == PlayerDirection.Zero)
            {
                var lastPlayerDirection = GetPlayerDirection(model.lastMove.x, model.lastMove.y);
                firePointRotation = GetFirePointRotation(lastPlayerDirection);
            }
            else
            {
                firePointRotation = GetFirePointRotation(playerDirection);
            }

            firePoint.transform.rotation = firePointRotation;


            if (model.movement.x > 0.1f || model.movement.x < -0.1f)
            {
                model.lastMove = new Vector2(model.movement.x, 0f);
            }

            if (model.movement.y > 0.1f || model.movement.y < -0.1f)
            {
                model.lastMove = new Vector2(0f, model.movement.y);
            }

            if (model.movement.x >= .2f)
            {
                model.movement.x = GameController.Instance.Config.MovementSpeed;
            }
            else if (model.movement.x <= -.2f)
            {
                model.movement.x = -GameController.Instance.Config.MovementSpeed;
            }
            else
            {
                model.movement.x = 0f;
            }

            if (model.movement.y >= .2f)
            {
                model.movement.y = GameController.Instance.Config.MovementSpeed;
            }
            else if (model.movement.y <= -.2f)
            {
                model.movement.y = -GameController.Instance.Config.MovementSpeed;
            }
            else
            {
                model.movement.y = 0f;
            }

            // LEVELING
            UpdatePlayerLevel();

            // OTHER
            CheckDistanceToTarget();
            LookAtSelectedTarget();
        }

        private void FixedUpdate()
        {
            rb.MovePosition(rb.position + model.movement * Time.fixedDeltaTime);
        }

        PlayerDirection GetPlayerDirection(float joystickX, float joystickY)
        {
            if (joystickX > 0.1f && joystickY < 0.1f && joystickY >= 0.0f)
                return PlayerDirection.Right;
            if (joystickX < -0.1f && joystickY < 0.1f && joystickY >= 0.0f)
                return PlayerDirection.Left;
            if (joystickY > 0.1f && joystickX < 0.1f && joystickX >= 0.0f)
                return PlayerDirection.Up;
            if (joystickY < -0.1f && joystickX < 0.1f && joystickX >= 0.0f)
                return PlayerDirection.Down;

            return PlayerDirection.Zero;
        }

        Quaternion GetFirePointRotation(PlayerDirection playerDirection)
        {
            if (inputModel.selectedTarget != null)
            {
                var firePointPosition = firePoint.position;
                var targetPos = inputModel.selectedTarget.transform.position;
                Vector2 lookAt = GetLookAtPosition(firePointPosition, targetPos);
                float angle = Mathf.Atan2(lookAt.y, lookAt.x) * Mathf.Rad2Deg - 90f;
                return Quaternion.Euler(0f, 0f, angle);
            }

            switch (playerDirection)
            {
                case PlayerDirection.Right:
                    return Quaternion.Euler(0f, 0f, -90f); // RIGHT
                case PlayerDirection.Left:
                    return Quaternion.Euler(0f, 0f, 90f); //LEFT
                case PlayerDirection.Up:
                    return Quaternion.Euler(0f, 0f, 0f); //UP
                case PlayerDirection.Down:
                    return Quaternion.Euler(0f, -0f, 180f); //DOWN
                default:
                    return Quaternion.identity;
            }
        }

        void CheckDistanceToTarget()
        {
            if (inputModel.selectedTarget == null)
                return;
            float distance = Vector3.Distance(
                gameObject.transform.position,
                inputModel.selectedTarget.transform.position
            );
            if (distance >= GameController.Instance.Config.DistanceToReleaseTarget)
            {
                Simulation.Schedule<ReleaseEnemySelection>();
            }
        }

        void LookAtSelectedTarget()
        {
            if (inputModel.selectedTarget == null)
                return;
            var lookAtPosition = GetLookAtPosition(
                firePoint.position, inputModel.selectedTarget.transform.position
            );
            model.lastMove.x = lookAtPosition.x;
            model.lastMove.y = lookAtPosition.y;
        }

        private Vector2 GetLookAtPosition(Vector3 subjectPos, Vector3 targetPos)
        {
            return new Vector2(targetPos.x, targetPos.y) - new Vector2(subjectPos.x, subjectPos.y);
        }

        void UpdatePlayerLevel()
        {
            if (model.currentXp < model.nextLevelXp)
                return;

            model.level += 1;
            model.currentXp = model.currentXp - model.nextLevelXp;
            model.nextLevelXp += 100 + (int) (model.nextLevelXp * 0.5) * model.level;
            Simulation.Schedule<PlayerLevelUp>().player = this;
        }

        public void TakeDamage(int damage)
        {
            model.DecrementHealth(damage);
        }
    }
}