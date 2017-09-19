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
        private GameState gamestate;
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D backgroundTexture;
        Random rand = new Random();
        AsteroidComponent asteroid;
        private UserInterface _ui;
        private StartMenu _startMenu;
        private GameOverScreen _gameOverScreen;
        private Shop _shop;
        public Boost boost;
        Effects effects;
        public PowerUp Powerup { get; private set; }
        private int soundTime = 0;
        public Exp Exp;
        private Texture2D laserTexture;
        private Texture2D enemyTexture;
        private Texture2D spaceStation;
        private Texture2D enemyLaserTexture;
        private Texture2D _powerUpHealth;
        private SoundEffect laserEffect;
        private int reloadTime = 0;
        //public int boostTime = 0;
        private int shieldTime = 0;
        private int _shoptimer = 0;

        private int wantedEnemies = 15;
        private int wantedPowerUps = 5;
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
        public PowerUp PowerUp { get; private set; }
        private KeyboardState previousKbState;
        public SoundEffect sound, agr;
        public Song BackgroundSong;
        private Camera camera;
        public SoundEffect Assault;
        public List<Shot> shots = new List<Shot>();
        public List<Shot> enemyshots = new List<Shot>();
        List<Enemy> enemies = new List<Enemy>();
        List<PowerUp> powerups = new List<PowerUp>();
        


       
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
            PowerUp = new PowerUp();
            Exp = new Exp();
            camera = new Camera(GraphicsDevice.Viewport);
            Components.Add(Player);
            asteroid = new AsteroidComponent(this, Player, gameObject);
            //Components.Add(asteroid);
            _ui = new UserInterface(this);
            effects = new Effects(this);
            Components.Add(_ui);
           boost = new Boost(this);
            Components.Add(boost);
            Components.Add(effects);
            _startMenu = new StartMenu(this);
            Components.Add(_startMenu);
            _gameOverScreen = new GameOverScreen(this);
            Components.Add(_gameOverScreen);
            _shop = new Shop(this);
            Components.Add(_shop);
            gamestate = GameState.Playing;
            



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
            _powerUpHealth = Content.Load<Texture2D>("powerupRedPill");
            enemyTexture = Content.Load<Texture2D>("EnemyShipNeon");
            laserEffect = Content.Load<SoundEffect>("laserShoot");
            enemyDamage = Content.Load<Texture2D>("burst");
            spaceStation = Content.Load<Texture2D>("spaceStation");
            enemyShootEffect = Content.Load<SoundEffect>("enemyShoot");
            enemyLaserTexture= Content.Load<Texture2D>("laserRed");
            asteroid.asterTexture2D1 = Content.Load<Texture2D>("Meteor1Neon");
            asteroid.asterTexture2D2 = Content.Load<Texture2D>("Meteor2Neon");
            asteroid.asterTexture2D3 = Content.Load<Texture2D>("Meteor3Neon");
            asteroid.asterTexture2D4 = Content.Load<Texture2D>("Meteor4Neon");
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
            KeyboardState state = Keyboard.GetState();
           
            switch (gamestate)
            {
                case GameState.Menu:
                    #region Menu
                    if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                        Exit();

                    if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                    {
                        gamestate = GameState.Playing;
                    }
                    #endregion

                    break;
                case GameState.Playing:
                    #region Playing
                    if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                        Exit();

                    if (Keyboard.GetState().IsKeyDown(Keys.L) && _shoptimer <= 0)
                    {
                        gamestate = GameState.Shopping;
                        _shoptimer = 10;
                    }
                    else
                    {
                        _shoptimer--;
                    }
                   
                    if (state.IsKeyDown(Keys.Up))
                    {
                        Player.Accelerate();
                    }
                    if (state.IsKeyDown(Keys.Left))
                    {
                        Player.Rotation -= 0.07f;
                    }
                    else if (state.IsKeyDown(Keys.Right))
                    {
                        Player.Rotation += 0.07f;
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
                        Player.Speed = new Vector2(0, 0);
                    }
                    
                    #region Collision
                    foreach (Enemy enemy in enemies)
                    {
                        var xDiffPlayer = Math.Abs(enemy.Position.X - Player.Position.X);
                        var yDiffPlayer = Math.Abs(enemy.Position.Y - Player.Position.Y);
                        if (xDiffPlayer > 3000 || yDiffPlayer > 3000)
                        {
                            enemy.isDead = true;
                        }
                    }
                    foreach (PowerUp powerup in powerups)
                    {
                        var xDiffPlayer = Math.Abs(powerup.Position.X - Player.Position.X);
                        var yDiffPlayer = Math.Abs(powerup.Position.Y - Player.Position.Y);
                        if (xDiffPlayer > 3000 || yDiffPlayer > 3000)
                        {
                            powerup.isDead = true;
                        }
                    }


                    if (enemies.Count < wantedEnemies)
                    {
                        Enemy e = Enemy.enemySpawn(this);
                        if (e != null)
                            enemies.Add(e);
                    }

                    if (powerups.Count < wantedPowerUps)
                    {
                        PowerUp p = PowerUp.powerUpSpawn(this);
                        if (p != null)
                            powerups.Add(p);
                    }

                    asteroid.Update(gameTime);
                    foreach (var BigAsteroid in asteroid._nrofAsteroids)
                    {

                        Asteroid hitasteroid = asteroid._nrofAsteroids.FirstOrDefault(e => e.CollidesWith(Player));
                        if (hitasteroid != null)
                        {
                            if (playerInvincibilityTimer <= 0)
                            {
                                if (Player.Shield <= 0)
                                {
                                    Player.Health -= 1;
                                    shieldTime = 200;
                                }
                                else
                                {
                                    Player.Shield--;
                                    shieldTime = 200;
                                }

                                playerInvincibilityTimer = 10;
                            }
                            hitasteroid.isDead = true;
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
                    foreach (PowerUp powerup in powerups)
                    {

                        PowerUp hitPowerup = powerups.FirstOrDefault(e => e.CollidesWith(Player));
                        if (hitPowerup != null)
                        {
                            Player.Health = 10;
                            hitPowerup.isDead = true;
                            Debug.WriteLine("Powerup!");
                        }
                    }
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
                        Player.Position = new Vector2(0, 0);
                        Player.Health = Player.MaxHealth;
                        Player.Shield = Player.MaxShield;
                        Exp.currentScore = 0;
                        Exp.currentEXP = 0;
                        gamestate = GameState.GameOver;
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
                    #endregion


                    shots.RemoveAll(s => s.isDead);
                    enemyshots.RemoveAll(shot => shot.isDead);
                    enemies.RemoveAll(enemy => enemy.isDead);
                    powerups.RemoveAll(powerup => powerup.isDead);
                    asteroid._MiniStroids.RemoveAll(n => n.isDead);
                    asteroid._nrofAsteroids.RemoveAll(j => j.isDead);
                    Player.Update(gameTime);
                    previousKbState = state;
                    _ui.Update(gameTime);
                    boost.Update(gameTime);
                    camera.Update(gameTime, Player);

                    if (reloadTime >= 0)
                    {
                        reloadTime--;
                    }
                    if (soundEffectTimer > 0)
                        soundEffectTimer--;
                    
                    #endregion playing
                    break;

                case GameState.Paused:
                    #region Paused
                    #endregion Paused
                    break;
                case GameState.Shopping:
                    #region Shopping
                    if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                        Exit();

                    if (Keyboard.GetState().IsKeyDown(Keys.L) && _shoptimer <= 0)
                    {
                        gamestate = GameState.Playing;
                        _shoptimer = 10;
                    }
                    else if(_shoptimer > 0)
                    {
                        _shoptimer--;
                    }
                    Player.Speed = new Vector2(0, 0);

                    _shop.Update(gameTime);
                      
            
            
                    #endregion Shopping
                    break;
                case GameState.GameOver:
                    if (Keyboard.GetState().IsKeyDown(Keys.Enter) || Keyboard.GetState().IsKeyDown(Keys.Space))
                    {
                        gamestate = GameState.Menu;
                        
                    }
                    break;
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>s
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            switch (gamestate)
            {
                    case GameState.Menu:
                    GraphicsDevice.Clear(Color.Black);
                    _startMenu.Draw(gameTime);
                    break;
                    case GameState.Playing:
                    #region state playing
                    GraphicsDevice.Clear(Color.CornflowerBlue);
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
                        spriteBatch.Draw(laserTexture, s.Position, null, Color.White, s.Rotation + MathHelper.PiOver2, new Vector2(laserTexture.Width / 2, laserTexture.Height / 2), 1.0f, SpriteEffects.None, 0f);
                    }

                    foreach (Shot s in enemyshots)
                    {
                        spriteBatch.Draw(enemyLaserTexture, s.Position, null, Color.White, s.Rotation, new Vector2(laserTexture.Width / 2, laserTexture.Height / 2), 1.0f, SpriteEffects.None, 0f);
                    }

                    foreach (Enemy e in enemies)
                    {
                        spriteBatch.Draw(enemyTexture, e.Position, null, Color.White, e.Rotation + MathHelper.PiOver2, new Vector2(enemyTexture.Width / 2, enemyTexture.Height / 2), 0.4f, SpriteEffects.None, 0f);
                    }

                    foreach (PowerUp p in powerups)
                    {
                        spriteBatch.Draw(_powerUpHealth, p.Position, null, Color.White, p.Rotation + MathHelper.PiOver2, new Vector2(_powerUpHealth.Width / 2, _powerUpHealth.Height / 2), 2f, SpriteEffects.None, 0f);
                    }

                    spriteBatch.Draw(spaceStation, new Vector2(0, 0), null, Color.White, spaceStationRotation, new Vector2(spaceStation.Width / 2f, spaceStation.Height / 2f), 1f, SpriteEffects.None, 0f);

                    Player.Draw(spriteBatch);

                    spaceStationRotation += 0.01f;

                    if (enemyHit)
                    {
                        spriteBatch.Draw(enemyDamage, enemyPositionExplosion, null, Color.White, 1f, new Vector2(enemyDamage.Width / 2f, enemyDamage.Height / 2f), 0.5f, SpriteEffects.None, 0f);
                        enemyHit = false;
                    }


                    spriteBatch.End();

                    _ui.Draw(gameTime);
                    #endregion state playing
                    break;
                    case GameState.Paused:
                    break;
                    case GameState.Shopping:
                    #region Shopping
                    GraphicsDevice.Clear(Color.CornflowerBlue);
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
                                spriteBatch.Draw(asteroid.asterTexture2D1, asteroid._nrofAsteroids[i].Position, null,
                                    Color.White, asteroid.Rotation + asteroid._nrofAsteroids[i].RotationCounter,
                                    new Vector2(asteroid.asterTexture2D1.Width / 2f,
                                        asteroid.asterTexture2D1.Height / 2f), 1f, SpriteEffects.None, 0f);
                                asteroid._nrofAsteroids[i].RotationCounter += asteroid._nrofAsteroids[i].addCounter;
                                break;
                            case 2:
                                spriteBatch.Draw(asteroid.asterTexture2D2, asteroid._nrofAsteroids[i].Position, null,
                                    Color.White, asteroid.Rotation + asteroid._nrofAsteroids[i].RotationCounter,
                                    new Vector2(asteroid.asterTexture2D2.Width / 2f,
                                        asteroid.asterTexture2D2.Height / 2f), 1f, SpriteEffects.None, 0f);
                                asteroid._nrofAsteroids[i].RotationCounter += asteroid._nrofAsteroids[i].addCounter;
                                break;
                            case 3:
                                spriteBatch.Draw(asteroid.asterTexture2D3, asteroid._nrofAsteroids[i].Position, null,
                                    Color.White, asteroid.Rotation + asteroid._nrofAsteroids[i].RotationCounter,
                                    new Vector2(asteroid.asterTexture2D3.Width / 2f,
                                        asteroid.asterTexture2D3.Height / 2f), 1f, SpriteEffects.None, 0f);
                                asteroid._nrofAsteroids[i].RotationCounter += asteroid._nrofAsteroids[i].addCounter;
                                break;
                            case 4:
                                spriteBatch.Draw(asteroid.asterTexture2D4, asteroid._nrofAsteroids[i].Position, null,
                                    Color.White, asteroid.Rotation + asteroid._nrofAsteroids[i].RotationCounter,
                                    new Vector2(asteroid.asterTexture2D4.Width / 2f,
                                        asteroid.asterTexture2D4.Height / 2f), 1f, SpriteEffects.None, 0f);
                                asteroid._nrofAsteroids[i].RotationCounter += asteroid._nrofAsteroids[i].addCounter;
                                break;
                        }
                    }

                    foreach (Shot s in shots)
                    {
                        spriteBatch.Draw(laserTexture, s.Position, null, Color.White, s.Rotation + MathHelper.PiOver2, new Vector2(laserTexture.Width / 2, laserTexture.Height / 2), 1.0f, SpriteEffects.None, 0f);
                    }

                    foreach (Shot s in enemyshots)
                    {
                        spriteBatch.Draw(enemyLaserTexture, s.Position, null, Color.White, s.Rotation, new Vector2(laserTexture.Width / 2, laserTexture.Height / 2), 1.0f, SpriteEffects.None, 0f);
                    }

                    foreach (Enemy e in enemies)
                    {
                        spriteBatch.Draw(enemyTexture, e.Position, null, Color.White, e.Rotation + MathHelper.PiOver2, new Vector2(enemyTexture.Width / 2, enemyTexture.Height / 2), 0.4f, SpriteEffects.None, 0f);
                    }

                    foreach (PowerUp p in powerups)
                    {
                        spriteBatch.Draw(_powerUpHealth, p.Position, null, Color.White, p.Rotation + MathHelper.PiOver2, new Vector2(_powerUpHealth.Width / 2, _powerUpHealth.Height / 2), 2f, SpriteEffects.None, 0f);
                    }

                    spriteBatch.Draw(spaceStation, new Vector2(0, 0), null, Color.White, spaceStationRotation, new Vector2(spaceStation.Width / 2f, spaceStation.Height / 2f), 1f, SpriteEffects.None, 0f);

                    Player.Draw(spriteBatch);

                    spaceStationRotation += 0.01f;

                    if (enemyHit)
                    {
                        spriteBatch.Draw(enemyDamage, enemyPositionExplosion, null, Color.White, 1f, new Vector2(enemyDamage.Width / 2f, enemyDamage.Height / 2f), 0.5f, SpriteEffects.None, 0f);
                        enemyHit = false;
                    }

                    base.Draw(gameTime);
                    spriteBatch.End();

                    _ui.Draw(gameTime);
                    _shop.Draw(gameTime);
                    #endregion Shopping
                    break;
                    case GameState.GameOver:
                    #region GameOver
                    GraphicsDevice.Clear(Color.Black);
                    _gameOverScreen.Draw(gameTime);
#endregion
                    break;
            }

            
        }
    }
}
