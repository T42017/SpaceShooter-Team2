using System.Linq;
using System.Runtime.CompilerServices;
using System.Media;
﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

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
        Random rand = new Random();
        AsteroidComponent asteroid;
        private Texture2D laserTexture;
        private Texture2D enemyTexture;
        private SoundEffect laserEffect;
        private int reloadTime = 0;
        public GameObject gameObject;
        public Player Player { get; private set; }
        public Enemy Enemy { get; private set; }
        private KeyboardState previousKbState;
        public SoundEffect sound;
        private Camera camera;
        public List<Shot> shots = new List<Shot>();
        List<Enemy> enemies = new List<Enemy>();


        public SpaceScavenger()
        {
            graphics = new GraphicsDeviceManager(this)
            {
                PreferredBackBufferHeight = Globals.ScreenHeight,
                PreferredBackBufferWidth = Globals.ScreenWidth
            };
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
            Enemy = new Enemy();
            camera = new Camera(GraphicsDevice.Viewport);
            Components.Add(Player);
            asteroid = new AsteroidComponent(this, Player, gameObject);
       //     Components.Add(asteroid);
            gameObject = (GameObject)gameObject;
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
            laserTexture = Content.Load<Texture2D>("laserBlue");
            enemyTexture = Content.Load<Texture2D>("EnemyShip");
            laserEffect = Content.Load<SoundEffect>("laserShoot");
            asteroid.asterTexture2D1 = Content.Load<Texture2D>("Meteor1");
            asteroid.asterTexture2D2 = Content.Load<Texture2D>("Meteor2");
            asteroid.asterTexture2D3 = Content.Load<Texture2D>("Meteor3");
            asteroid.asterTexture2D4 = Content.Load<Texture2D>("Meteor4");
            asteroid.MinitETexture2D1 = Content.Load<Texture2D>("llama");

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
            if (Keyboard.GetState().IsKeyDown(Keys.Space ) && previousKbState.IsKeyUp(Keys.Space))
            {
            }

                
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
            if (state.IsKeyDown(Keys.Space))
            {
                if (reloadTime < 0)
                {
                    laserEffect.Play();
                    Shot s = Player.Shoot();
                    if (s != null)
                        shots.Add(s);
                    reloadTime = 20;
                }
            }
            if (state.IsKeyDown(Keys.B))
            {
                Player.Speed = new Vector2(0,0);
            }
            if (state.IsKeyDown(Keys.S) && previousKbState.IsKeyDown(Keys.S) != state.IsKeyDown(Keys.S))
            {
                Enemy e = Enemy.enemySpawn();
                if (e != null)
                    enemies.Add(e);
            }

            asteroid.Update(gameTime);

            foreach (Shot shot in shots)
            {
                shot.Update(gameTime);
                Enemy enemy = enemies.FirstOrDefault(d => d.CollidesWith(shot));
                 Asteroid hitasteroid = asteroid._nrofAsteroids.FirstOrDefault(e => e.CollidesWith(shot));

                if (enemy != null)
                {
                    enemies.Remove(enemy);
                    shot.isDead = true;
                }
                if (hitasteroid != null)
                {
                    asteroid.miniStroid(hitasteroid.Position);
                    asteroid.miniStroid(hitasteroid.Position);
                    asteroid.miniStroid(hitasteroid.Position);
                    asteroid._nrofAsteroids.Remove(hitasteroid);

                    shot.isDead = true;
                }
            }
            foreach (Enemy e in enemies)
            {
                e.Update(gameTime, this);
            }

            shots.RemoveAll(s => s.isDead);

            Player.Update(gameTime);
            previousKbState = state;

            camera.Update(gameTime, Player);

            if (reloadTime >= 0)
            {
                reloadTime--;
            }


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




            for (int y = -10000; y < 10000; y += backgroundTexture.Width)
            {
                for (int x = -10000; x < 10000; x += backgroundTexture.Width)
                {
                    spriteBatch.Draw(backgroundTexture, new Vector2(x, y), Color.White);

                }
            }

            foreach (Asteroid mini in asteroid._MiniStroids)
            {
                spriteBatch.Draw(asteroid.MinitETexture2D1, mini.Position, Color.White);
            }
            for (int i = 0; i < asteroid._nrofAsteroids.Count; i++)
            {
                switch (asteroid._nrofAsteroids[i].chosenTexture)
                {
                    case 1:
                        spriteBatch.Draw(asteroid.asterTexture2D1, asteroid._nrofAsteroids[i].Position, null, Color.White, asteroid.Rotation + asteroid._nrofAsteroids[i].RotationCounter, new Vector2(asteroid.asterTexture2D1.Width / 2f, asteroid.asterTexture2D1.Height / 2f), 1f, SpriteEffects.None, 0f);
                        asteroid._nrofAsteroids[i].RotationCounter += asteroid._nrofAsteroids[i].addCounter;
                        break;
                    case 2:
                        spriteBatch.Draw(asteroid.asterTexture2D2, asteroid._nrofAsteroids[i].Position, null, Color.White, asteroid.Rotation + asteroid._nrofAsteroids[i].RotationCounter, new Vector2(asteroid.asterTexture2D2.Width / 2f, asteroid.asterTexture2D2.Height / 2f), 1f, SpriteEffects.None, 0f);
                        asteroid._nrofAsteroids[i].RotationCounter += asteroid._nrofAsteroids[i].addCounter;
                        break;
                    case 3:
                        spriteBatch.Draw(asteroid.asterTexture2D3, asteroid._nrofAsteroids[i].Position, null, Color.White, asteroid.Rotation + asteroid._nrofAsteroids[i].RotationCounter, new Vector2(asteroid.asterTexture2D3.Width / 2f, asteroid.asterTexture2D3.Height / 2f), 1f, SpriteEffects.None, 0f);
                        asteroid._nrofAsteroids[i].RotationCounter += asteroid._nrofAsteroids[i].addCounter;
                        break;
                    case 4:
                        spriteBatch.Draw(asteroid.asterTexture2D4, asteroid._nrofAsteroids[i].Position, null, Color.White, asteroid.Rotation + asteroid._nrofAsteroids[i].RotationCounter, new Vector2(asteroid.asterTexture2D4.Width / 2f, asteroid.asterTexture2D4.Height / 2f), 1f, SpriteEffects.None, 0f);
                        asteroid._nrofAsteroids[i].RotationCounter += asteroid._nrofAsteroids[i].addCounter;
                        break;
                }

                /*  if (_nrofAsteroids[i].RotationCounter > 2000000000 || _nrofAsteroids[i].RotationCounter < -2000000000)
                  {                                                   anti integer overflow system. Activate if it happens
                      _nrofAsteroids[i].RotationCounter = 0;
                  }*/
            }



            

            foreach (Shot s in shots)
            {
                spriteBatch.Draw(laserTexture, s.Position, null, Color.White, s.Rotation + MathHelper.PiOver2, new Vector2(laserTexture.Width / 2, laserTexture.Height/2), 1.0f, SpriteEffects.None, 0f);
            }

            foreach (Enemy e in enemies)
            {
                spriteBatch.Draw(enemyTexture, e.Position, null, Color.White, e.Rotation + MathHelper.PiOver2, new Vector2(enemyTexture.Width / 2, enemyTexture.Height / 2), 0.3f, SpriteEffects.None, 0f);
            }

            Player.Draw(spriteBatch);
            
            spriteBatch.End();
        }
    }
}
