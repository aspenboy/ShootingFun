﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShootingFun
{
    public class Shot : Sprite
    {
        public Shot(Texture2D texture, Vector2 position, Rectangle movementBounds) : base(texture, position, movementBounds, 2, 2, 14)
        {
            Speed = 400;
        }
    }
}
