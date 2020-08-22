using Core;
using Model;
using UnityEditor;
using UnityEngine;

namespace Gameplay
{
    public class PlayerShooting : Simulation.Event<PlayerShooting>
    {

        private GameModel gameModel;
        private PlayerModel playerModel;
        private GameObject arrowPrefab;
        
        
        public override void Execute()
        {
            arrowPrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Arrow.prefab");
            gameModel = Simulation.GetModel<GameModel>();
            playerModel = Simulation.GetModel<PlayerModel>();
            var arrow = GameObject.Instantiate(
                arrowPrefab, 
                gameModel.player.firePoint.position, 
                gameModel.player.firePoint.rotation
            );
            Physics2D.IgnoreCollision(
                arrow.GetComponent<Collider2D>(), 
                gameModel.player.GetComponent<Collider2D>()
            );
            Rigidbody2D rb = arrow.GetComponent<Rigidbody2D>();
            rb.AddForce(gameModel.player.firePoint.up * playerModel.arrowForce, ForceMode2D.Impulse);
            playerModel.nextFireTime = Time.time + playerModel.fireRate;
        }
    }
}