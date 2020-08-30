using Core;
using Gameplay;
using UnityEngine;

namespace Mechanics
{
    public class ArrowProjectileController : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D hitInfo)
        {
            if (hitInfo.gameObject.CompareTag("Locator"))
            {
                return;
            }
            var hitEvent = Simulation.Schedule<HitRegistered>();
            hitEvent.collision = hitInfo;
            hitEvent.projectile = gameObject;
        }
    }
}