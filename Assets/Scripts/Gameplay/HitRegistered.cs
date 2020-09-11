using Core;
using Model;
using UnityEngine;

namespace Gameplay
{
    public class HitRegistered : Simulation.Event<HitRegistered>
    {
        public Collider2D collision;
        public GameObject projectile;
        private PlayerModel playerModel = Simulation.GetModel<PlayerModel>();


        public override void Execute()
        {
            if (collision.gameObject.CompareTag("Enemy"))
            {
                Treant treant = collision.GetComponent<Treant>();
                treant.TakeDamage(playerModel.GetDamageDeal());
            }

            var hitEffect = Resources.Load<GameObject>("Prefabs/Impact");
            GameObject effect = GameObject.Instantiate(
                hitEffect,
                projectile.transform.position,
                Quaternion.identity
            );
            GameObject.Destroy(effect, 2f);
            GameObject.Destroy(projectile);
        }
    }
}