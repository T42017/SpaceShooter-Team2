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
        UserInterface ui;
        Effects effects;
        public PowerUp Powerup { get; private set; }
        private int soundTime = 0;
        public Exp Exp;
        private Texture2D laserTexture;
        private Texture2D enemyTexture;
        private Texture2D spaceStation;
        private Texture2D enemyLaserTexture;
        private SoundEffect laserEffect;
        private int reloadTime = 0;
        public int boostTime = 0;
        private int shieldTime = 0;
        private int wantedEnemies = 15;
        public int soundEffectTimer = 0;
        public float spaceStationRotation { get; set; }
        private int playerInvincibilityTimer = 100;
        private Vector2 enemyPositionExplosion = new Vector2(0,0);
        bool enemyHit = false;
        public GameObject gameObject;
        private Texture2D enemyDamage;
        public SoundEffect enemyShootEffect;
        public Player Player { get; private set; }
        public Enemy Enemy { get; private set; }
        private KeyboardState previousKbState;
        public SoundEffect sound, agr;
        public Song BackgroundSong;
        private Camera camera;
        public SoundEffect Assault;
        public List<Shot> shots = new List<Shot>();
        public List<Shot> enemyshots = new List<Shot>();
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
            Exp = new Exp();
            camera = new Camera(GraphicsDevice.Viewport);
            Components.Add(Player);
            asteroid = new AsteroidComponent(this, Player, gameObject);
            //Components.Add(asteroid);
            ui = new UserInterface(this);
            effects = new Effects(this);
            Powerup = new PowerUp(this);
            Components.Add(ui);
            Components.Add(effects);
            Components.Add(Powerup);
           
       
            
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

            backgroundTexture = Content.Load<Texture2D>("backgroundNeon");
            laserTexture = Content.Load<Texture2D>("laserBlue");
            enemyTexture = Content.Load<Texture2D>("EnemyShip");
            laserEffect = Content.Load<SoundEffect>("laserShoot");
            enemyDamage = Content.Load<Texture2D>("burst");
            spaceStation = Content.Load<Texture2D>("spaceStation");
            enemyShootEffect = Content.Load<SoundEffect>("enemyShoot");
            enemyLaserTexture= Content.Load<Texture2D>("laserRed");
            asteroid.asterTexture2D1 = Content.Load<Texture2D>("Meteor1Neon");
            asteroid.asterTexture2D2 = Content.Load<Texture2D>("Meteor2Neon");
            asteroid.asterTexture2D3 = Content.Load<Texture2D>("Meteor3");
            asteroid.asterTexture2D4 = Content.Load<Texture2D>("Meteor4");
            asteroid.MinitETexture2D1 = Content.Load<Texture2D>("tMeteor");
            //Assault = Content.Load<SoundEffect>("oblivion3");
            BackgroundSong = Content.Load<Song>("backgroundMusicNeon");
            //agr = Content.Load<SoundEffect>("AGR");
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Play(BackgroundSong);
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
            if (state.IsKeyDown(Keys.Space))
            {
                if (reloadTime < 0)
                {
                    laserEffect.Play(0.2f, 0.0f, 0.0f);
                    Shot s = Player.Shoot();
                    if (s != null)
                        shots.Add(s);
                    reloadTime = 10;
                }
                
            }
            if (state.IsKeyDown(Keys.B))
            {
                Player.Speed = new Vector2(0,0);
            }
            if (state.IsKeyDown(Keys.X) && previousKbState.IsKeyDown(Keys.X) != state.IsKeyDown(Keys.X))
            {
                if (boostTime <= 0)
                {
                    Player.Speed = new Vector2((float)Math.Cos(Player.Rotation), (float)Math.Sin(Player.Rotation)) * 20f;
                    boostTime = 600;
                }
                

                
            }

            foreach (Enemy enemy in enemies)
            {
                var xDiffPlayer = Math.Abs(enemy.Position.X - Player.Position.X);
                var yDiffPlayer = Math.Abs(enemy.Position.Y - Player.Position.Y);
                if (xDiffPlayer > 3000 || yDiffPlayer > 3000)
                {
                    enemy.isDead = true;
                }
            }


            if (enemies.Count < wantedEnemies)
            {
                Enemy e = Enemy.enemySpawn(this);
                if (e != null)
                    enemies.Add(e);
            }

  
            asteroid.Update(gameTime);
            foreach (var BigAsteroid in asteroid._nrofAsteroids)
            {
                
                Asteroid hitasteroid = asteroid._nrofAsteroids.FirstOrDefault(e => e.CollidesWith(Player));
                if (hitasteroid != null)
                {
                    Player.Health--;
                    hitasteroid.isDead = true;
                    //agr.Play();
                    for (int k = 0; k < 10; k++)
                    {
                        asteroid.miniStroid(hitasteroid.Position);
                    }
                }
                break;
            }
            foreach (var currentMiniAsteroid in asteroid._MiniStroids)
            {

                if (currentMiniAsteroid.Timer <= 0)
                {
                    currentMiniAsteroid.isDead = true;
                }
                currentMiniAsteroid.Timer--;
                
            }
            asteroid.Timer--;
            foreach (Shot shot in shots)
            {
                shot.Update(gameTime);
                Enemy enemy = enemies.FirstOrDefault(d => d.CollidesWith(shot));
                Asteroid hitasteroid = asteroid._nrofAsteroids.FirstOrDefault(e => e.CollidesWith(shot));


                if (enemy != null)
                {
                    enemy.Health -= 1;
                    if (enemy.Health <= 0)
                    {
                        enemy.isDead = true;
                        Exp.currentEXP += enemy.ExpReward;
                        Exp.currentScore += enemy.ScoreReward;
                        
                    }
                    enemyHit = true;
                    enemyPositionExplosion = enemy.Position;
                    Debug.WriteLine(enemyPositionExplosion);
                    shot.isDead = true;
                }
                if (hitasteroid != null)
                {
                    
                    asteroid.miniStroid(hitasteroid.Position);
                    asteroid.miniStroid(hitasteroid.Position);
                    asteroid.miniStroid(hitasteroid.Position);
                    asteroid._nrofAsteroids.Remove(hitasteroid);
                    Exp.currentScore += hitasteroid.ScoreReward;
                    Debug.WriteLine(Exp.currentScore);
                    shot.isDead = true;
                }
                shot.Timer--;
                if (shot.Timer <= 0)
                {
                    shot.isDead = true;
                }
            }
            foreach (Shot shot in enemyshots)
            {
                Asteroid hitasteroid = asteroid._nrofAsteroids.FirstOrDefault(e => e.CollidesWith(shot));

                if (hitasteroid != null)
                {
                    
                    asteroid.miniStroid(hitasteroid.Position);
                    asteroid.miniStroid(hitasteroid.Position);
                    asteroid.miniStroid(hitasteroid.Position);
                    asteroid._nrofAsteroids.Remove(hitasteroid);

                    shot.isDead = true;
                }

                shot.Update(gameTime);
                shot.Timer--;
                if (shot.Timer <= 0)
                {
                    shot.isDead = true;
                }
                
            }
            foreach (Enemy e in enemies)
            {
                e.Update(gameTime, this);
            }
                Shot shotHit = enemyshots.FirstOrDefault(e => e.CollidesWith(Player));
                if (shotHit != null)
                {
                    if (playerInvincibilityTimer <= 0)
                    {
                        if (Player.Shield <= 0)
                        {
                            Player.Health -= 1;
                            shieldTime = 500;
                        }
                        else
                        {
                            Player.Shield--;
                            shieldTime = 500;
                        }

                        playerInvincibilityTimer = 10;
                    }
                    shotHit.isDead = true;
                }

                if (Player.Health <= 0)
                {
                    Player.Position = new Vector2(0,0);
                    Player.Health = Player.MaxHealth;
                    Player.Shield = Player.MaxShield;
                }
            

            if (Player.Shield < 10 && shieldTime <= 0)
            {
                Player.Shield++;
                shieldTime = 40;
                Debug.WriteLine(Player.Shield + " " + shieldTime);
            }
            if (shieldTime >= 0)
            {
                shieldTime--;
            }
            if (playerInvincibilityTimer >= 0)
            {
                playerInvincibilityTimer--;
            }

            shots.RemoveAll(s => s.isDead);
            enemyshots.RemoveAll(shot => shot.isDead);
            enemies.RemoveAll(enemy => enemy.isDead);
            asteroid._MiniStroids.RemoveAll(n => n.isDead);
            asteroid._nrofAsteroids.RemoveAll(j => j.isDead);
            Player.Update(gameTime);
            previousKbState = state;
            ui.Update(gameTime);
            Powerup.Update(gameTime);

            camera.Update(gameTime, Player);

            if (reloadTime >= 0)
            {
                reloadTime--;
            }
            if (boostTime >= 0)
                boostTime--;
            if (soundEffectTimer > 0)
                soundEffectTimer--;

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

            foreach (Shot s in enemyshots)
            {
                spriteBatch.Draw(enemyLaserTexture, s.Position, null, Color.White, s.Rotation, new Vector2(laserTexture.Width / 2, laserTexture.Height / 2), 1.0f, SpriteEffects.None, 0f);
            }

            foreach (Enemy e in enemies)
            {
                spriteBatch.Draw(enemyTexture, e.Position, null, Color.White, e.Rotation + MathHelper.PiOver2, new Vector2(enemyTexture.Width / 2, enemyTexture.Height / 2), 0.3f, SpriteEffects.None, 0f);
            }

            spriteBatch.Draw(spaceStation, new Vector2(0,0), null, Color.White, spaceStationRotation, new Vector2(spaceStation.Width / 2f, spaceStation.Height / 2f), 1f, SpriteEffects.None, 0f);

            Powerup.Draw(spriteBatch);
            Player.Draw(spriteBatch);

            spaceStationRotation += 0.01f;

            if (enemyHit)
            {
                spriteBatch.Draw(enemyDamage, enemyPositionExplosion, null, Color.White, 1f, new Vector2(enemyDamage.Width / 2f, enemyDamage.Height / 2f), 0.5f, SpriteEffects.None, 0f);
                enemyHit = false;
            }

            
            spriteBatch.End();
            
            ui.Draw(gameTime);
        }
    }
}
