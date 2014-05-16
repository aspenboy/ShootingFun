
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
        

        public PlayerShip(Texture2D texture, Vector2 position, Rectangle movementBounds)
            : base(texture, position, movementBounds)
        {
            Speed = 300;
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

            //foreach (var keypress in keyboardState.GetPressedKeys())
            //{
            //    velocity += keyDictionary[keypress];
            //}

            Velocity = velocity * Speed;
        }


        private void UpdateVelocityFromMouse()
        {
            var velocity = new Vector2(Mouse.GetState().X - previousMousePosition.X, Mouse.GetState().Y - previousMousePosition.Y);
            if (velocity != Vector2.Zero)
                velocity.Normalize();

            Velocity = velocity * Speed;

            previousMousePosition = Mouse.GetState();
        }


        public override void Update(KeyboardState keyboardState, GameTime gameTime)
        {
            UpdateVelocityFromKeyboard(keyboardState);
            //UpdateVelocityFromMouse();
            base.Update(keyboardState, gameTime);
        }
    }
}
