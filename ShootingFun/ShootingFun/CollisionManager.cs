using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShootingFun
{
    class CollisionManager
    {
        private readonly PlayerShip playerShip;
        private readonly ShotManager shotManager;
        private readonly EnemyManager enemyManager;

        public CollisionManager(PlayerShip playerShip, ShotManager shotManager, EnemyManager enemyManager)
        {
            this.playerShip = playerShip;
            this.shotManager = shotManager;
            this.enemyManager = enemyManager;
        }

        public void Update(GameTime gameTime)
        {
            CheckCollisions();
        }

        private void CheckCollisions()
        {
            CheckShotToPlayer();
            CheckShotToEnemy();
        }

        private void CheckShotToEnemy()
        {
            foreach (var shot in shotManager.PlayerShots)
            {
                foreach (var enemy in enemyManager.Enemies)
                {
                    if (shot.BoundingBox.Intersects(enemy.BoundingBox))
                        enemy.Hit();
                }
            }
        }

        private void CheckShotToPlayer()
        {
            foreach (var shot in shotManager.EnemyShots)
            {
                if (shot.BoundingBox.Intersects(playerShip.BoundingBox))
                    playerShip.Hit();
            }
        }
    }
}
