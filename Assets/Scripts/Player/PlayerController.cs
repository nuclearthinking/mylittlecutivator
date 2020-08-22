using UnityEngine;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
    
        public 
    
        // Start is called before the first frame update
        void Start()
        {
        
        }

        private void FixedUpdate()
        {
        
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
    }
}
