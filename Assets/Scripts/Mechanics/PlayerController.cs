using System.Collections;
using Core;
using Gameplay;
using Model;
using Units;
using UnityEngine;

namespace Mechanics
{
    public class PlayerController : MonoBehaviour
    {
        public Transform firePoint;
        
        private readonly PlayerModel playerModel = Simulation.GetModel<PlayerModel>();

        private Rigidbody2D rb;

        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            playerModel.IncrementHealth(playerModel.maximumHealth);
            playerModel.nextLevelXp = Game.Instance.GetExpToNextLevel(playerModel.level);
        }

        private void Update()
        {
            playerModel.movement.x = playerModel.xInput;
            playerModel.movement.y = playerModel.yInput;

            var playerDirection = GetPlayerDirection(playerModel.xInput, playerModel.yInput);
            Quaternion firePointRotation;
            if (playerDirection == Enums.PlayerDirection.Zero)
            {
                var lastPlayerDirection = GetPlayerDirection(playerModel.lastMove.x, playerModel.lastMove.y);
                firePointRotation = GetFirePointRotation(lastPlayerDirection);
            }
            else
            {
                firePointRotation = GetFirePointRotation(playerDirection);
            }

            firePoint.transform.rotation = firePointRotation;


            if (playerModel.movement.x > 0.1f || playerModel.movement.x < -0.1f)
            {
                playerModel.lastMove = new Vector2(playerModel.movement.x, 0f);
            }

            if (playerModel.movement.y > 0.1f || playerModel.movement.y < -0.1f)
            {
                playerModel.lastMove = new Vector2(0f, playerModel.movement.y);
            }

            if (playerModel.movement.x >= .2f)
            {
                playerModel.movement.x = Game.Instance.Config.MovementSpeed;
            }
            else if (playerModel.movement.x <= -.2f)
            {
                playerModel.movement.x = -Game.Instance.Config.MovementSpeed;
            }
            else
            {
                playerModel.movement.x = 0f;
            }

            if (playerModel.movement.y >= .2f)
            {
                playerModel.movement.y = Game.Instance.Config.MovementSpeed;
            }
            else if (playerModel.movement.y <= -.2f)
            {
                playerModel.movement.y = -Game.Instance.Config.MovementSpeed;
            }
            else
            {
                playerModel.movement.y = 0f;
            }

            // LEVELING
            StartCoroutine(UpdatePlayerLevel());

            // OTHER
            StartCoroutine(CheckDistanceToTarget());
            LookAtSelectedTarget();
        }

        private void FixedUpdate()
        {
            rb.MovePosition(rb.position + playerModel.movement * Time.fixedDeltaTime);
        }

        Enums.PlayerDirection GetPlayerDirection(float joystickX, float joystickY)
        {
            if (joystickX > 0.1f && joystickY < 0.1f && joystickY >= 0.0f)
                return Enums.PlayerDirection.Right;
            if (joystickX < -0.1f && joystickY < 0.1f && joystickY >= 0.0f)
                return Enums.PlayerDirection.Left;
            if (joystickY > 0.1f && joystickX < 0.1f && joystickX >= 0.0f)
                return Enums.PlayerDirection.Up;
            if (joystickY < -0.1f && joystickX < 0.1f && joystickX >= 0.0f)
                return Enums.PlayerDirection.Down;

            return Enums.PlayerDirection.Zero;
        }

        Quaternion GetFirePointRotation(Enums.PlayerDirection playerDirection)
        {
            if (playerModel.selectedTarget != null)
            {
                var firePointPosition = firePoint.position;
                var targetPos = playerModel.selectedTarget.transform.position;
                Vector2 lookAt = GetLookAtPosition(firePointPosition, targetPos);
                float angle = Mathf.Atan2(lookAt.y, lookAt.x) * Mathf.Rad2Deg - 90f;
                return Quaternion.Euler(0f, 0f, angle);
            }

            switch (playerDirection)
            {
                case Enums.PlayerDirection.Right:
                    return Quaternion.Euler(0f, 0f, -90f); // RIGHT
                case Enums.PlayerDirection.Left:
                    return Quaternion.Euler(0f, 0f, 90f); //LEFT
                case Enums.PlayerDirection.Up:
                    return Quaternion.Euler(0f, 0f, 0f); //UP
                case Enums.PlayerDirection.Down:
                    return Quaternion.Euler(0f, -0f, 180f); //DOWN
                default:
                    return Quaternion.identity;
            }
        }

        IEnumerator CheckDistanceToTarget()
        {
            yield return new WaitForSeconds(0.2f);
            if (playerModel.selectedTarget == null)
                yield break;
            float distance = Vector3.Distance(
                gameObject.transform.position,
                playerModel.selectedTarget.transform.position
            );
            if (distance >= Game.Instance.Config.DistanceToReleaseTarget)
            {
                playerModel.selectedTarget.GetComponent<Enemy>().ReleaseAsTarget();
            }
        }

        void LookAtSelectedTarget()
        {
            if (playerModel.selectedTarget == null)
                return;
            var lookAtPosition = GetLookAtPosition(
                firePoint.position, playerModel.selectedTarget.transform.position
            );
            playerModel.lastMove.x = lookAtPosition.x;
            playerModel.lastMove.y = lookAtPosition.y;
        }

        private Vector2 GetLookAtPosition(Vector3 subjectPos, Vector3 targetPos)
        {
            return new Vector2(targetPos.x, targetPos.y) - new Vector2(subjectPos.x, subjectPos.y);
        }

        IEnumerator UpdatePlayerLevel()
        {
            yield return new WaitForSeconds(.2f);
            if (playerModel.currentXp < playerModel.nextLevelXp)
                yield break;
            
            var levelUp = Simulation.Schedule<PlayerLevelUp>();
            levelUp.player = this;
        }

        public void TakeDamage(int damage)
        {
            playerModel.DecrementHealth(damage);
        }
    }
}