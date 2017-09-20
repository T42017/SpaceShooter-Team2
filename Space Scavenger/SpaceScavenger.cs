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
        SpriteBatch spriteBatch, backgrSpriteBatch;
        Texture2D backgroundTexture;
        Random rand = new Random();
        AsteroidComponent asteroid;
        private UserInterface _ui;
        private StartMenu _startMenu;
        private GameOverScreen _gameOverScreen;
        private Money Money;
        public Boost boost;
        Effects effects;
        public float startY, startX;

        public PowerUp Powerup { get; private set; }
        private int soundTime = 0;
        public Exp Exp;
        private Texture2D laserTexture;
        private Texture2D enemyTexture;
        private Texture2D spaceStation;
        private Texture2D moneyTexture;
        private Texture2D enemyLaserTexture;
        private Texture2D _powerUpHealth;
        private SoundEffect laserEffect;
        private int reloadTime = 0;
        //public int boostTime = 0;
        private int shieldTime = 0;

        private Texture2D bossTexture;

        public bool multiShot { get; set; }
        private int wantedEnemies = 15;
        private int wantedPowerUps = 5;
        public int soundEffectTimer = 0;
        public float spaceStationRotation { get; set; }
        private int playerInvincibilityTimer = 100;
        private Vector2 enemyPositionExplosion = new Vector2(0,0);
        bool enemyHit = false;
        public GameObject gameObject;
        private Texture2D enemyDamage;
        public SoundEffect EnemyShootEffect, PlayerHitAsteoid, PlayerDamage, ShieldDestroyed, ShieldRegenerating, ShieldUp, HealthPickup, MeteorExplosion, ShieldDamage;
        public Player Player { get; private set; }
        public Enemy Enemy { get; private set; }
        public BossEnemy BossEnemy { get; private set; }
        public PowerUp PowerUp { get; private set; }
        private KeyboardState _previousKbState;
        public SoundEffect Sound, Agr;
        public Song BackgroundSong;
        private Camera _camera;
        public SoundEffect Assault;
        public List<Shot> Shots = new List<Shot>();
        public List<Shot> Enemyshots = new List<Shot>();
        private readonly List<Enemy> _enemies = new List<Enemy>();
        private readonly List<PowerUp> _powerups = new List<PowerUp>();
        private readonly List<BossEnemy> bosses = new List<BossEnemy>();




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
            BossEnemy = new BossEnemy();
            PowerUp = new PowerUp();
            Exp = new Exp();
            Money = new Money();
            _camera = new Camera(GraphicsDevice.Viewport);
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
            gamestate = GameState.Menu;



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
            backgrSpriteBatch = new SpriteBatch(GraphicsDevice);

            backgroundTexture = Content.Load<Texture2D>("backgroundNeon");
            laserTexture = Content.Load<Texture2D>("laserBlue");
            _powerUpHealth = Content.Load<Texture2D>("powerupRedPill");
            enemyTexture = Content.Load<Texture2D>("EnemyShipNeon");
            moneyTexture = Content.Load<Texture2D>("Money");
            laserEffect = Content.Load<SoundEffect>("laserShoot");
            enemyDamage = Content.Load<Texture2D>("burst");
            spaceStation = Content.Load<Texture2D>("spaceStation");
            EnemyShootEffect = Content.Load<SoundEffect>("enemyShoot");
            enemyLaserTexture= Content.Load<Texture2D>("laserRed");
            bossTexture = Content.Load<Texture2D>("boss");
            asteroid.asterTexture2D1 = Content.Load<Texture2D>("Meteor1Neon");
            asteroid.asterTexture2D2 = Content.Load<Texture2D>("Meteor2Neon");
            asteroid.asterTexture2D3 = Content.Load<Texture2D>("Meteor3Neon");
            asteroid.asterTexture2D4 = Content.Load<Texture2D>("Meteor4Neon");
            asteroid.MinitETexture2D1 = Content.Load<Texture2D>("tMeteorNeon");
            PlayerDamage = Content.Load<SoundEffect>("PlayerDamage");
            PlayerHitAsteoid = Content.Load<SoundEffect>("PlayerHitAsteroid");
            ShieldRegenerating = Content.Load<SoundEffect>("ShieldRegenerating");
            ShieldDestroyed = Content.Load<SoundEffect>("ShieldDestroyed");
            ShieldUp = Content.Load<SoundEffect>("ShieldUp");
            HealthPickup = Content.Load<SoundEffect>("HealthPickup");
            MeteorExplosion = Content.Load<SoundEffect>("ExplosionMeteor");
            ShieldDamage = Content.Load<SoundEffect>("ShieldDamage");
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
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            switch (gamestate)
            {
                case GameState.Menu:
                    #region Menu
                    if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                    {
                        gamestate = GameState.Playing;
                    }
                    #endregion
                    break;
                case GameState.Playing:

                    #region Playing

                    if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
                        Keyboard.GetState().IsKeyDown(Keys.Escape))
                        Exit();
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
                        if (reloadTime <= 0)
                            if (multiShot)
                            {
                                laserEffect.Play(0.2f, 0.0f, 0.0f);
                                Shot s = Player.multiShot();
                                if (s != null)
                                    Shots.Add(s);
                                Shot s2 = Player.multiShot();
                                if (s2 != null)
                                    Shots.Add(s2);
                                reloadTime = 10;
                            }
                            else
                            {
                                laserEffect.Play(0.2f, 0.0f, 0.0f);
                                Shot s = Player.Shoot();
                                if (s != null)
                                    Shots.Add(s);
                                reloadTime = 10;
                            }

                    }
                    if (state.IsKeyDown(Keys.B))
                    {
                        Player.Speed = new Vector2(0, 0);
                    }
                    if (state.IsKeyDown(Keys.L))
                    {
                        multiShot = true;
                    }

                    #region Collision

                    foreach (Enemy enemy in _enemies)
                    {
                        var xDiffPlayer = Math.Abs(enemy.Position.X - Player.Position.X);
                        var yDiffPlayer = Math.Abs(enemy.Position.Y - Player.Position.Y);
                        if (xDiffPlayer > 3000 || yDiffPlayer > 3000)
                        {
                            enemy.isDead = true;
                        }
                    }
                    foreach (PowerUp powerup in _powerups)
                    {
                        var xDiffPlayer = Math.Abs(powerup.Position.X - Player.Position.X);
                        var yDiffPlayer = Math.Abs(powerup.Position.Y - Player.Position.Y);
                        if (xDiffPlayer > 3000 || yDiffPlayer > 3000)
                        {
                            powerup.isDead = true;
                        }
                    }


                    if (_enemies.Count < wantedEnemies)
                    {
                        Enemy e = Enemy.enemySpawn(this);
                        if (e != null)
                            _enemies.Add(e);
                    }
                    if (bosses.Count < 1)
                    {
                        BossEnemy be = BossEnemy.SpawnBoss(this);
                        if (be != null)
                            bosses.Add(be);
                    }

                    if (_powerups.Count < wantedPowerUps)
                    {
                        PowerUp p = PowerUp.PowerUpSpawn(this);
                        if (p != null)
                            _powerups.Add(p);
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
                                    PlayerHitAsteoid.Play();
                                    Player.Health -= 1;
                                    shieldTime = 200;
                                }
                                else
                                {
                                    PlayerHitAsteoid.Play();
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
                    foreach (PowerUp powerup in _powerups)
                    {

                        PowerUp hitPowerup = _powerups.FirstOrDefault(e => e.CollidesWith(Player));
                        if (hitPowerup != null)
                        {
                            HealthPickup.Play();
                            Player.Health = 10;
                            hitPowerup.isDead = true;
                            Debug.WriteLine("Powerup!");
                        }
                    }
                    foreach (Shot shot in Shots)
                    {
                        shot.Update(gameTime);
                        Enemy enemy = _enemies.FirstOrDefault(d => d.CollidesWith(shot));
                        Asteroid hitasteroid = asteroid._nrofAsteroids.FirstOrDefault(e => e.CollidesWith(shot));
                        BossEnemy hitBoss = bosses.FirstOrDefault(e => e.CollidesWith(shot));

                        if (enemy != null)
                        {
                            MeteorExplosion.Play();
                            enemy.Health -= 1;
                            if (enemy.Health <= 0)
                            {
                                enemy.isDead = true;
                                Exp.currentScore += enemy.ScoreReward;
                                for (int i = 0; i < rand.Next(1, 5); i++)
                                {
                                    Money.MoneyRoid(enemy.Position + new Vector2(rand.Next(-50, 50)));
                                }

                            }
                            enemyHit = true;
                            enemyPositionExplosion = enemy.Position;
                            Debug.WriteLine(enemyPositionExplosion);
                            shot.isDead = true;
                        }
                        if (hitBoss != null)
                        {
                            hitBoss.Health -= 1;
                            if (hitBoss.Health <= 0)
                            {
                                hitBoss.isDead = true;
                                Exp.currentScore += hitBoss.ScoreReward;
                                for (int i = 0; i < rand.Next(1, 30); i++)
                                {
                                    Money.MoneyRoid(hitBoss.Position + new Vector2(rand.Next(-100, 100)));
                                }

                            }
                            enemyHit = true;
                            enemyPositionExplosion = hitBoss.Position;
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
                    foreach (Shot shot in Enemyshots)
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
                    foreach (Enemy e in _enemies)
                    {
                        e.Update(gameTime, this);
                    }
                    foreach (BossEnemy be in bosses)
                    {
                        be.Update(gameTime);
                    }


                    Shot shotHit = Enemyshots.FirstOrDefault(e => e.CollidesWith(Player));
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
                    Asteroid moneyHit = Money.Moneyroids.FirstOrDefault(m => m.CollidesWith(Player));
                    if (moneyHit != null)
                    {
                        moneyHit.isDead = true;
                        Exp.currentEXP += 50;
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


                    Shots.RemoveAll(s => s.isDead);
                    Enemyshots.RemoveAll(shot => shot.isDead);
                    _enemies.RemoveAll(enemy => enemy.isDead);
                    _powerups.RemoveAll(powerup => powerup.isDead);
                    asteroid._MiniStroids.RemoveAll(n => n.isDead);
                    asteroid._nrofAsteroids.RemoveAll(j => j.isDead);
                    Money.Moneyroids.RemoveAll(money => money.isDead);
                    Player.Update(gameTime);
                    _ui.Update(gameTime);
                    _previousKbState = state;
                    boost.Update(gameTime);
                    _camera.Update(gameTime, Player);
                    Money.Update(gameTime, this);

                    if (reloadTime >= 0)
                    {
                        reloadTime--;
                    }
                    if (soundEffectTimer > 0)
                        soundEffectTimer--;
                    //if (boostTime >= 0)
                    //    boostTime--;
                    #endregion playing
                    break;

                case GameState.Paused:
                    #region Paused
                    #endregion Paused
                    break;
                case GameState.Shopping:
                    #region Shopping
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
                    

            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, _camera.transformn);

                        backgrSpriteBatch.Begin();

                        startX = Player.Position.X % backgroundTexture.Width;
                        startY = Player.Position.Y % backgroundTexture.Height;

                        for (float y = -startY - backgroundTexture.Height; y < Globals.ScreenHeight; y += backgroundTexture.Width)
                        {
                            for (float x = -startX - backgroundTexture.Width; x < Globals.ScreenWidth; x += backgroundTexture.Width)
                            {
                                backgrSpriteBatch.Draw(backgroundTexture, new Vector2(x, y), Color.White);

                            }
                        }

                        backgrSpriteBatch.End();

                    foreach (Asteroid mini in asteroid._MiniStroids)
            {

                spriteBatch.Draw(asteroid.MinitETexture2D1, mini.Position, null, Color.White, asteroid.Rotation + mini.RotationCounter, new Vector2(asteroid.MinitETexture2D1.Width / 2f, asteroid.MinitETexture2D1.Height / 2f), 1f, SpriteEffects.None, 0f);
                mini.RotationCounter += mini.addCounter;
            }
            foreach (Asteroid money in Money.Moneyroids)
            {
                spriteBatch.Draw(moneyTexture, money.Position, null, Color.White, money.Rotation + money.RotationCounter, new Vector2(moneyTexture.Width / 2f, moneyTexture.Height / 2f), 0.7f, SpriteEffects.None, 0f);
            }

            foreach (BossEnemy boss in bosses)
            {
                spriteBatch.Draw(bossTexture, boss.Position, null, Color.White, boss.Rotation, new Vector2(bossTexture.Width / 2f, bossTexture.Height / 2f), 3f, SpriteEffects.None, 0f);
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

            }





                    foreach (Shot s in Shots)
                    {
                        spriteBatch.Draw(laserTexture, s.Position, null, Color.White, s.Rotation + MathHelper.PiOver2, new Vector2(laserTexture.Width / 2, laserTexture.Height / 2), 1.0f, SpriteEffects.None, 0f);
                    }

                    foreach (Shot s in Enemyshots)
                    {
                        spriteBatch.Draw(enemyLaserTexture, s.Position, null, Color.White, s.Rotation, new Vector2(laserTexture.Width / 2, laserTexture.Height / 2), 1.0f, SpriteEffects.None, 0f);
                    }

                    foreach (Enemy e in _enemies)
                    {
                        spriteBatch.Draw(enemyTexture, e.Position, null, Color.White, e.Rotation + MathHelper.PiOver2, new Vector2(enemyTexture.Width / 2, enemyTexture.Height / 2), 0.4f, SpriteEffects.None, 0f);
                    }

                    foreach (PowerUp p in _powerups)
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
                    break;
                    case GameState.GameOver:
                    _gameOverScreen.Draw(gameTime);
                    break;
            }

            
        }
    }
}
