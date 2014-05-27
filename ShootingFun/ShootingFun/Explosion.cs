using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShootingFun
{
    class Explosion : Sprite
    {
        private Texture2D texture;
        private Vector2 centerOfSprite;
        private Rectangle bounds;

        public Explosion(Texture2D texture, Vector2 centerOfSprite, Rectangle bounds)
            : base(texture, centerOfSprite, bounds, 2, 2, 10)
        {
        }

        public bool IsDone()
        {
            return animationPlayedOnce;
        }
    }
}
