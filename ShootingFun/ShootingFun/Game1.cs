using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace ShootingFun
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        private Sprite background;
        private Sprite playerShip;
        private SpriteFont gameFont;

        private int score;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            background = new Sprite(Content.Load<Texture2D>("background"), Vector2.Zero);
            
            var shipTexture = Content.Load<Texture2D>("ship");
            var xPosiotionOfShip = graphics.GraphicsDevice.Viewport.Height - shipTexture.Height - 10;
            var yPositionOfShip = (graphics.GraphicsDevice.Viewport.Width / 2) - (shipTexture.Width / 2);

            playerShip = new Sprite(shipTexture, new Vector2(xPosiotionOfShip, yPositionOfShip));

            gameFont = Content.Load<SpriteFont>("GameFont");

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            background.Draw(spriteBatch);
            playerShip.Draw(spriteBatch);

            var scoreText = string.Format("Score: {0}", score);
            var scoreDimensions = gameFont.MeasureString(scoreText);

            var scoreX = graphics.GraphicsDevice.Viewport.Width - scoreDimensions.X - 5;
            var scoreY = 5;

            spriteBatch.DrawString(gameFont, scoreText, new Vector2(scoreX, scoreY), Color.White);

            //spriteBatch.Draw(background, Vector2.Zero, Color.White);
            //var xPosiotionOfShip = graphics.GraphicsDevice.Viewport.Height - playerShip.Height - 10;
            //var yPositionOfShip = (graphics.GraphicsDevice.Viewport.Width / 2) - (playerShip.Width / 2);
            //spriteBatch.Draw(playerShip, new Vector2(xPosiotionOfShip, yPositionOfShip), Color.White);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
