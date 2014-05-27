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
        public Vector2 Velocity { get; set; }

        private readonly Rectangle movementBounds;
        private readonly int rows;
        private readonly int columns;
        private readonly double framesPerSecond;
        private int totalFrames;
        private double timeSinceLastFrame;
        private int currentFrame;
        protected bool animationPlayedOnce;


        public Sprite(Texture2D texture, Vector2 position, Rectangle movementBounds)
            : this(texture, position, movementBounds, 1, 1, 1)
        {
        }

        public Sprite(Texture2D texture, Vector2 position, Rectangle movementBounds, int rows, int columns, double framesPerSecond)
        {
            this.texture = texture;
            this.position = position;
            this.movementBounds = movementBounds;
            this.rows = rows;
            this.columns = columns;
            this.framesPerSecond = framesPerSecond;
            totalFrames = rows * columns;
        }


        public void Draw(SpriteBatch spriteBatch)
        {
            var imageWidth = texture.Width / columns;
            var imageHeight = texture.Height / rows;

            var currentRow = currentFrame / columns;
            var currentColumn = currentFrame % columns;

            var sourceRectangle = new Rectangle(imageWidth * currentColumn, imageHeight * currentRow, imageWidth, imageHeight);

            var destinationRectangle = new Rectangle((int)position.X, (int)position.Y, imageWidth, imageHeight);
            spriteBatch.Draw(texture, destinationRectangle, sourceRectangle, Color.White);
        }

        public virtual void Update(GameTime gameTime)
        {
            UpdateAnimation(gameTime);
            var newPosition = position + ((Velocity * (float)gameTime.ElapsedGameTime.TotalSeconds) * Speed);
            if (Blocked(newPosition))
                return;

            position = newPosition;
            //position += (Velocity * (float)gameTime.ElapsedGameTime.TotalSeconds);
        }

        private void UpdateAnimation(GameTime gameTime)
        {
            timeSinceLastFrame += gameTime.ElapsedGameTime.TotalSeconds;
            if (timeSinceLastFrame > SecondsBetweenFrames())
            {
                currentFrame++;
                timeSinceLastFrame = 0;
            }

            if (currentFrame == totalFrames)
            {
                currentFrame = 0;
                animationPlayedOnce = true;
            }
        }

        private double SecondsBetweenFrames()
        {
            return 1 / framesPerSecond;
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
            set
            {
                this.position = value;
            }
            get 
            { 
                return position; 
            }
        }

        public float Width { get { return texture.Width / columns; } }
        public float Height { get { return texture.Height / rows; } }

        public Rectangle BoundingBox
        {
            get { return CreateBoundingBoxFromPosition(position); }
        }

    }
}
