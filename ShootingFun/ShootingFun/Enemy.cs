using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShootingFun
{
    class Enemy : Sprite
    {
        public Enemy(Texture2D texture, Vector2 position, Rectangle bounds) : base(texture, position, bounds)
        {
            Speed = 200;
        }
    }
}
