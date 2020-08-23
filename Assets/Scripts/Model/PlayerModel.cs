using UnityEngine;

namespace Model
{
    [System.Serializable]
    public class PlayerModel
    {
        public int currentHealth;
        public int maximumHealth;
        
        // MOVEMENT
        public float movementSpeed = 4.0f;
        public Vector2 movement;
        public Vector2 lastMove;
        public float xInput;
        public float yInput;

        // SHOOTING
        public Quaternion firePointRotation;
        public float nextFireTime = .0f;
        public float arrowForce = 20f;
        public float fireRate = 0.3f;
        
        // LEVELING
        public int level = 1;
        public int currentXp;
        public int nextLevelXp = 100;

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

        public void AddXP(int xpAmount)
        {
            currentXp += xpAmount;
        }
        
    }
}