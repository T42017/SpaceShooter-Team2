﻿using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Space_Scavenger
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class SpaceScavenger : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D backgroundTexture;
        
        
        public Player Player { get; private set; }
        Enemies enemies;
        private KeyboardState previousKbState;
        private Camera camera;

        public SpaceScavenger()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferHeight = Globals.ScreenHeight;
            graphics.PreferredBackBufferWidth = Globals.ScreenWidth;
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

            Player = new Player(this);
            enemies = new Enemies(this);
            camera = new Camera(GraphicsDevice.Viewport);
            Components.Add(Player);
            Components.Add(enemies);
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

            backgroundTexture = Content.Load<Texture2D>("purple");
            

        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
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
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            KeyboardState state = Keyboard.GetState();

            if (state.IsKeyDown(Keys.Up))
            {
                Player.Accelerate();
            }
            if (state.IsKeyDown(Keys.Left))
            {
                Player.Rotation -= 0.05f;
            }
            else if (state.IsKeyDown(Keys.Right))
            {
                Player.Rotation += 0.05f;
            }

            Player.Update(gameTime);
            previousKbState = state;

            camera.Update(gameTime, Player);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>s
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            base.Draw(gameTime);
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, camera.transformn);
            for (int y = 0; y < Globals.ScreenWidth; y += backgroundTexture.Width)
            {
                for (int x = 0; x < Globals.ScreenWidth; x += backgroundTexture.Width)
                {
                    spriteBatch.Draw(backgroundTexture, new Vector2(x, y), Color.White);
                }
            }

            Player.Draw(spriteBatch);
            enemies.Draw(spriteBatch);
            spriteBatch.End();
            
        }
    }
}
