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
        private PlayerShip playerShip;
        private SpriteFont gameFont;
        private EnemyManager enemyManager;
        private ShotManager shotManager;
        private CollisionManager collisionManager;

        private int score = 0;

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
            background = new Sprite(Content.Load<Texture2D>("background"), Vector2.Zero, graphics.GraphicsDevice.Viewport.Bounds);
            
            var shipTexture = Content.Load<Texture2D>("ship1");
            var xPositionOfShip = (graphics.GraphicsDevice.Viewport.Width / 2) - (shipTexture.Width / 2);
            var yPositionOfShip = graphics.GraphicsDevice.Viewport.Height - shipTexture.Height - 10;
            var playerBounds = new Rectangle(0, graphics.GraphicsDevice.Viewport.Height - 200, graphics.GraphicsDevice.Viewport.Width, 200);
            shotManager = new ShotManager(Content.Load<Texture2D>("shot"), graphics.GraphicsDevice.Viewport.Bounds);
            playerShip = new PlayerShip(shipTexture, new Vector2(xPositionOfShip, yPositionOfShip), playerBounds, shotManager);
            
            enemyManager = new EnemyManager(Content.Load<Texture2D>("enemy"), graphics.GraphicsDevice.Viewport.Bounds, shotManager);
            collisionManager = new CollisionManager(playerShip, shotManager, enemyManager);


            gameFont = Content.Load<SpriteFont>("GameFont");
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
            //var keyboardState = Keyboard.GetState();
            playerShip.Update(gameTime);
            enemyManager.Update(gameTime);
            shotManager.Update(gameTime);

            collisionManager.Update(gameTime);

            UpdateScore();
 
            base.Update(gameTime);
        }

        private void UpdateScore()
        {
            var kills = enemyManager.GetKillCount();
            score += (kills * 1000);
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
            if (!playerShip.IsDead)
                playerShip.Draw(spriteBatch);
            enemyManager.Draw(spriteBatch);
            shotManager.Draw(spriteBatch);

            var scoreText = string.Format("Score: {0}", score);
            var scoreDimensions = gameFont.MeasureString(scoreText);

            var scoreX = graphics.GraphicsDevice.Viewport.Width - scoreDimensions.X - 5;
            var scoreY = 5;

            spriteBatch.DrawString(gameFont, scoreText, new Vector2(scoreX, scoreY), Color.White);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
