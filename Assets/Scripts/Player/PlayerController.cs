using Core;
using Gameplay;
using Model;
using UnityEngine;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        public PlayerModel model = Simulation.GetModel<PlayerModel>();
        public Transform firePoint;

        public GameObject selectedTarget;
        private Rigidbody2D rb;

        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            model.currentHealth = model.maximumHealth;
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
                model.movement.x = model.movementSpeed;
            }
            else if (model.movement.x <= -.2f)
            {
                model.movement.x = -model.movementSpeed;
            }
            else
            {
                model.movement.x = 0f;
            }

            if (model.movement.y >= .2f)
            {
                model.movement.y = model.movementSpeed;
            }
            else if (model.movement.y <= -.2f)
            {
                model.movement.y = -model.movementSpeed;
            }
            else
            {
                model.movement.y = 0f;
            }

            // LEVELING
            UpdatePlayerLevel();
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
            if (selectedTarget != null)
            {
                var playerRbpos = firePoint.position;
                var targetPos = selectedTarget.transform.position;
                Vector2 lookAt = new Vector2(targetPos.x, targetPos.y) - new Vector2(playerRbpos.x, playerRbpos.y);
                float angle = Mathf.Atan2(lookAt.y, lookAt.x)* Mathf.Rad2Deg - 90f;
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

        void UpdatePlayerLevel()
        {
            if (model.currentXp < model.nextLevelXp)
                return;

            model.level += 1;
            model.currentXp = model.currentXp - model.nextLevelXp;
            model.nextLevelXp += 100 + (int) (model.nextLevelXp * 0.5) * model.level;
            Simulation.Schedule<PlayerLevelUp>().player = this;
        }

        void TakeDamage(int damage)
        {
            model.currentXp -= damage;
        }
    }
}