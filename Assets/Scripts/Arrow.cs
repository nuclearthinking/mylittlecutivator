using Core;
using Gameplay;
using UnityEngine;

public class Arrow : MonoBehaviour
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