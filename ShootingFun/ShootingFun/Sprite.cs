using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShootingFun
{
    public class Sprite
    {
        private readonly Texture2D texture;
        private Vector2 position;
        protected Vector2 Velocity { get; set; }
        protected float Speed { get; set; }
        private readonly Rectangle movementBounds;


        public Sprite(Texture2D texture, Vector2 position, Rectangle movementBounds)
        {
            this.texture = texture;
            this.position = position;
        }


        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.White);
        }


        public virtual void Update(KeyboardState keyboardState, GameTime gameTime)
        {
            //UpdateVelocity(keyboardState);

            position += (Velocity * (float)gameTime.ElapsedGameTime.TotalSeconds);
        }


    }
}
