﻿using Core;
using Model;
using UnityEngine;

namespace Gameplay
{
    public class PlayerShooting : Simulation.Event<PlayerShooting>
    {

        private GameModel gameModel;
        private PlayerModel playerModel;
        
        
        public override void Execute()
        {
            var arrowPrefab = Resources.Load<GameObject>("Prefabs/Arrow");
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