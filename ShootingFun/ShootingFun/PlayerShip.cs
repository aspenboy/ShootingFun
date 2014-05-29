
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
namespace ShootingFun
{
    public class PlayerShip : Sprite
    {
        private MouseState previousMousePosition;
        //private Texture2D shipTexture;
        //private Vector2 vector2;
        //private Rectangle playerBounds;
        private ShotManager shotManager;
        private const double TimeBetweenShotsInSeconds = 1;
        private double timeSinceLastFireInSeconds = 0;
        

        public PlayerShip(Texture2D texture, Vector2 position, Rectangle movementBounds, ShotManager shotManager)
            : base(texture, position, movementBounds, 2, 2, 14)
        {
            this.shotManager = shotManager;
            Speed = 300;
        }

        private void HandleKeyboardInput()
        {
            var keyboardState = Keyboard.GetState();
            UpdateVelocityFromKeyboard(keyboardState);
            CheckForShotFromKeyboard(keyboardState);
        }

        private void CheckForShotFromKeyboard(KeyboardState keyboardState)
        {
            if (keyboardState.IsKeyDown(Keys.Space) && CanFireShot())
            {
                shotManager.FirePlayerShot(CalculateShotPosition());
                timeSinceLastFireInSeconds = 0;
            }
        }

        private bool CanFireShot()
        {
            return (timeSinceLastFireInSeconds > TimeBetweenShotsInSeconds) && !IsDead;
        }

        private Vector2 CalculateShotPosition()
        {
            return Position + new Vector2(Width / 2, 0);
        }

        private void UpdateVelocityFromKeyboard(KeyboardState keyboardState)
        {
            var keyDictionary = new Dictionary<Keys, Vector2>
            {
                { Keys.Left, new Vector2(-1, 0)},
                { Keys.Right, new Vector2(1, 0)},
                { Keys.Up, new Vector2(0, -1)},
                { Keys.Down, new Vector2(0, 1)},
            };

            var velocity = Vector2.Zero;

            foreach (var key in keyDictionary)
            {
                if (keyboardState.IsKeyDown(key.Key))
                    velocity += key.Value;
            }

            if (velocity != Vector2.Zero)
                velocity.Normalize();

            Velocity = velocity;
        }


        private void UpdateVelocityFromMouse()
        {
            var velocity = new Vector2(Mouse.GetState().X - previousMousePosition.X, Mouse.GetState().Y - previousMousePosition.Y);
            if (velocity != Vector2.Zero)
                velocity.Normalize();

            Velocity = velocity;

            previousMousePosition = Mouse.GetState();
        }


        public override void Update(GameTime gameTime)
        {
            timeSinceLastFireInSeconds += gameTime.ElapsedGameTime.TotalSeconds;
            HandleKeyboardInput();
            //UpdateVelocityFromMouse();
            base.Update(gameTime);
        }

        public void Hit()
        {
            IsDead = true;
        }

        public bool IsDead { get; set; }
    }
}
