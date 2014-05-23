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
        
        private readonly Rectangle movementBounds;


        public Sprite(Texture2D texture, Vector2 position, Rectangle movementBounds)
        {
            this.texture = texture;
            this.position = position;
            this.movementBounds = movementBounds;
        }


        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.White);
        }


        public virtual void Update(KeyboardState keyboardState, GameTime gameTime)
        {
            var newPosition = position + (Velocity * (float)gameTime.ElapsedGameTime.TotalSeconds);

            if (Blocked(newPosition))
                return;

            position = newPosition;
            //position += (Velocity * (float)gameTime.ElapsedGameTime.TotalSeconds);
        }

        private bool Blocked(Vector2 pos)
        {
            var boundingBox = CreateBoundingBoxFromPosition(pos);
            return !movementBounds.Contains(boundingBox);
        }

        private Rectangle CreateBoundingBoxFromPosition(Vector2 pos)
        {
            return new Rectangle((int)pos.X, (int)pos.Y, (int)Width, (int)Height);
        }


        protected float Speed { get; set; }

        public Vector2 Position
        {
            get { return position; }
        }

        public float Width { get { return texture.Width; } }
        public float Height { get { return texture.Height; } }

    }
}
