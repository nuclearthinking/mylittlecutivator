using UnityEngine;

namespace Model
{
    [System.Serializable]
    public class PlayerModel
    {
        public int currentHealth;
        public int maximumHealth;
        public float movementSpeed = 4f;
        public Vector2 movement;
        public Vector2 lastMove;
        public Quaternion firePointRotation;
        public float xInput;
        public float yInput;
        public float nextFireTime = .0f;
        public float arrowForce = 20f;
        public float fireRate = 0.3f;

        public void SetFirePointRotation(Quaternion rotation)
        {
            firePointRotation = rotation;
        }

        public Quaternion GetFirePointRotation()
        {
            return firePointRotation;
        }

        public void SetXInput(float x)
        {
            xInput = x;
        }

        public void SetYInput(float y)
        {
            yInput = y;
        }
        
    }
}