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

        private Sprite titleScreen;
        private Sprite background;
        private Sprite pauseScreen;
        private Sprite gameOverScreen;
        private PlayerShip playerShip;
        private SpriteFont gameFont;
        private EnemyManager enemyManager;
        private ShotManager shotManager;
        private CollisionManager collisionManager;
        private ExplosionManager explosionManager;
        private SoundManager soundManager;
        private StatusManager statusManager;

        private GameState gameState;

        private KeyboardState currentKeyboardState;
        private KeyboardState previousKeyboardState;

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
            gameState = new TitleScreenState(this);
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            titleScreen = new Sprite(Content.Load<Texture2D>("start"), Vector2.Zero, graphics.GraphicsDevice.Viewport.Bounds, 2, 2, 8);
            background = new Sprite(Content.Load<Texture2D>("background"), Vector2.Zero, graphics.GraphicsDevice.Viewport.Bounds);
            pauseScreen = new Sprite(Content.Load<Texture2D>("paused"), Vector2.Zero, graphics.GraphicsDevice.Viewport.Bounds);
            gameOverScreen = new Sprite(Content.Load<Texture2D>("gameover"), Vector2.Zero, graphics.GraphicsDevice.Viewport.Bounds);

            soundManager = new SoundManager(Content);

            var shipTexture = Content.Load<Texture2D>("ship1");
            var xPositionOfShip = (graphics.GraphicsDevice.Viewport.Width / 2) - (shipTexture.Width / 2);
            var yPositionOfShip = graphics.GraphicsDevice.Viewport.Height - shipTexture.Height - 10;
            var playerBounds = new Rectangle(0, graphics.GraphicsDevice.Viewport.Height - 200, graphics.GraphicsDevice.Viewport.Width, 200);
            shotManager = new ShotManager(Content.Load<Texture2D>("shot"), graphics.GraphicsDevice.Viewport.Bounds, soundManager);
            playerShip = new PlayerShip(shipTexture, new Vector2(xPositionOfShip, yPositionOfShip), playerBounds, shotManager);
            enemyManager = new EnemyManager(Content.Load<Texture2D>("enemy"), graphics.GraphicsDevice.Viewport.Bounds, shotManager);
            explosionManager = new ExplosionManager(Content.Load<Texture2D>("explosion"), graphics.GraphicsDevice.Viewport.Bounds, soundManager);
            collisionManager = new CollisionManager(playerShip, shotManager, enemyManager, explosionManager);

            gameFont = Content.Load<SpriteFont>("GameFont");
            statusManager = new StatusManager(gameFont, graphics.GraphicsDevice.Viewport.Bounds, enemyManager, shipTexture)
            {
                Lives = 3,
                Score = 0
            };
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
            currentKeyboardState = Keyboard.GetState();
            gameState.Update(gameTime);
            previousKeyboardState = currentKeyboardState;
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

            gameState.Draw(spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }


        public class TitleScreenState : GameState
        {
            public TitleScreenState(Game1 game)
                : base(game)
            {
            }

            public override void Update(GameTime gameTime)
            {
                game.titleScreen.Update(gameTime);
                if (game.currentKeyboardState.IsKeyDown(Keys.Space) && !game.previousKeyboardState.IsKeyDown(Keys.Space))
                    game.gameState = new PlayingState(game);

            }

            public override void Draw(SpriteBatch spriteBatch)
            {
                game.titleScreen.Draw(spriteBatch);
            }
        }

        public class PlayingState : GameState
        {
            public PlayingState(Game1 game)
                : base(game)
            {
            }

            public override void Update(GameTime gameTime)
            {
                game.playerShip.Update(gameTime);
                game.enemyManager.Update(gameTime);
                game.shotManager.Update(gameTime);
                game.explosionManager.Update(gameTime);
                game.collisionManager.Update(gameTime);
                game.statusManager.UpdateScore();

                if (game.currentKeyboardState.IsKeyDown(Keys.P))
                    game.gameState = new PausedState(game);

                if (game.playerShip.IsDead)
                {
                    game.statusManager.Lives--;
                    if (game.statusManager.Lives < 1)
                        game.gameState = new GameOverState(game);
                    else
                        game.playerShip.IsDead = false;
                }
            }

            public override void Draw(SpriteBatch spriteBatch)
            {
                game.background.Draw(spriteBatch);
                if (!game.playerShip.IsDead)
                    game.playerShip.Draw(spriteBatch);
                game.statusManager.Draw(spriteBatch);
                game.enemyManager.Draw(spriteBatch);
                game.shotManager.Draw(spriteBatch);
                game.explosionManager.Draw(spriteBatch);
            }
        }

        public class PausedState : PlayingState
        {
            public PausedState(Game1 game)
                : base(game)
            {
            }

            public override void Update(GameTime gameTime)
            {
                game.pauseScreen.Update(gameTime);
                if (game.currentKeyboardState.IsKeyDown(Keys.Space) && !game.previousKeyboardState.IsKeyDown(Keys.Space))
                    game.gameState = new PlayingState(game);
            }

            public override void Draw(SpriteBatch spriteBatch)
            {
                base.Draw(spriteBatch);
                game.pauseScreen.Draw(spriteBatch);
            }

        }

        public class GameOverState : PlayingState
        {
            public GameOverState(Game1 game)
                : base(game)
            {
            }

            public override void Update(GameTime gameTime)
            {
                base.Update(gameTime);
                game.gameOverScreen.Update(gameTime);

                if (game.currentKeyboardState.IsKeyDown(Keys.Space))
                {
                    game.LoadContent();
                    game.gameState = new TitleScreenState(game);
                }
            }

            public override void Draw(SpriteBatch spriteBatch)
            {
                base.Draw(spriteBatch);
                game.gameOverScreen.Draw(spriteBatch);
            }

        }

    }

    public abstract class GameState
    {
        protected readonly Game1 game;
        public GameState(Game1 game)
        {
            this.game = game;
        }

        public abstract void Update(GameTime gameTime);
        public abstract void Draw(SpriteBatch spriteBatch);
    }


}
