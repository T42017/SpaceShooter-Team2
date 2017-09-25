using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

// ReSharper disable PossibleLossOfFraction

namespace Space_Scavenger
{
    /// <summary>
    ///     This is the main type for your game.
    /// </summary>
    public class SpaceScavenger : Game
    {
        private readonly List<Enemy> _enemies = new List<Enemy>();
        private readonly GraphicsDeviceManager _graphics;
        private readonly List<PowerUp> _powerups = new List<PowerUp>();
        private readonly Random _rand = new Random();
        private readonly int _wantedPowerUps = 5;
        public readonly List<BombEnemy> Bombships = new List<BombEnemy>();
        public readonly List<BossEnemy> Bosses = new List<BossEnemy>();
        public readonly List<Shot> BossShots = new List<Shot>();
        public readonly List<TreasureShip> TreasureShips = new List<TreasureShip>();
        private AsteroidComponent _asteroid;
        private Texture2D _backgroundTexture;
        private Texture2D _bombEnemyTexture;
        private Texture2D _bossTexture;
        private Camera _camera;
        private Effects _effects;
        private int _enemyAmountTimer = 600;
        private Texture2D _enemyDamage;
        private bool _enemyHit;
        private Texture2D _enemyLaserTexture;
        private Vector2 _enemyPositionExplosion = new Vector2(0, 0);
        private Texture2D _enemyTexture;
        private GameOverScreen _gameOverScreen;
        private string _inRangeToBuyString = "";
        private SoundEffect _laserEffect;
        private Texture2D _laserTexture;
        private Texture2D _moneyTexture;
        private int _playerInvincibilityTimer = 100;
        private int _playerShieldCooldown;
        private int _playerShieldTimer;
        private Texture2D _powerUpHealth, _shield;
        private float _reloadTime;
        private int _shieldTime;
        private Texture2D _spaceStation;
        private bool _spawnBossCompass = true;
        private bool _spawnCompass = true;
        private SpriteBatch _spriteBatch, _backgrSpriteBatch;
        private StartMenu _startMenu;
        private TreasureShip _treasureShip;
        private Texture2D _treasureShipTexture;
        private UserInterface _ui;
        private int _wantedEnemies = 5;
        private WinScreen _winScreen;
        public SoundEffect Assault;
        public Song BackgroundSong;
        public BombEnemy BombEnemy;
        public Boost Boost;
        public BossCompass Bosscompass;
        public bool BossKill;
        public Texture2D BossShotTexture;
        public Texture2D BossShotTexture2;
        public Compass Compass;
        public int DefeatedEnemies;

        public SoundEffect EnemyShootEffect,
            PlayerHitAsteoid,
            PlayerDamage,
            ShieldDestroyed,
            ShieldRegenerating,
            ShieldUp,
            HealthPickup,
            MeteorExplosion,
            ShieldDamage,
            DeathSound;

        public List<Shot> EnemyShots = new List<Shot>();
        public Exp Exp;
        public GameObject GameObject;
        public GameState Gamestate;
        public Money Money;
        public KeyboardState PreviousKbState;
        public Shop Shop;
        public List<Shot> Shots = new List<Shot>();
        public SoundEffect Sound, Agr;
        public int SoundEffectTimer;
        public float StartY, StartX;


        public SpaceScavenger()
        {
            _graphics = new GraphicsDeviceManager(this)
            {
                PreferredBackBufferHeight = Globals.ScreenHeight,
                PreferredBackBufferWidth = Globals.ScreenWidth
            };
            Content.RootDirectory = "Content";
        }

        public ShopItem ShopItem { get; private set; }
        public bool FasterLaser { get; set; }
        public bool MultiShot { get; set; }
        public float SpaceStationRotation { get; set; }
        public float EnemyRotation { get; set; }
        public Player Player { get; private set; }
        public Enemy Enemy { get; private set; }
        public BossEnemy BossEnemy { get; private set; }
        public PowerUp PowerUp { get; private set; }

        /// <summary>
        ///     Allows the game to perform any initialization it needs to before starting to run.
        ///     This is where it can query for any required services and load any non-graphic
        ///     related content.  Calling base.Initialize will enumerate through any components
        ///     and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            Exp = new Exp();
            BombEnemy = new BombEnemy();
            Player = new Player(this);
            Enemy = new Enemy();
            _treasureShip = new TreasureShip();
            BossEnemy = new BossEnemy();
            PowerUp = new PowerUp();
            Compass = new Compass();
            Bosscompass = new BossCompass();
            Exp = new Exp();
            Money = new Money();
            _camera = new Camera(GraphicsDevice.Viewport);
            Components.Add(Player);
            _asteroid = new AsteroidComponent(this, GameObject);
            //Components.Add(asteroid);
            _ui = new UserInterface(this);
            _effects = new Effects(this);
            Components.Add(_ui);
            Boost = new Boost(this);
            Components.Add(Boost);
            Components.Add(_effects);
            _startMenu = new StartMenu(this);
            Components.Add(_startMenu);
            _gameOverScreen = new GameOverScreen(this);
            _winScreen = new WinScreen(this);
            Components.Add(_winScreen);
            Components.Add(_gameOverScreen);
            Gamestate = GameState.Menu;
            Shop = new Shop(this);
            Components.Add(Shop);
            ShopItem = new ShopItem(this);
            Components.Add(ShopItem);

            FasterLaser = false;


            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        ///     LoadContent will be called once per game and is the place to load
        ///     all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _backgrSpriteBatch = new SpriteBatch(GraphicsDevice);

            _backgroundTexture = Content.Load<Texture2D>("backgroundNeon");
            _laserTexture = Content.Load<Texture2D>("laserBlue");
            _bombEnemyTexture = Content.Load<Texture2D>("ufoGreen");
            DeathSound = Content.Load<SoundEffect>("DeathSound");
            _powerUpHealth = Content.Load<Texture2D>("powerupRedPill");
            _enemyTexture = Content.Load<Texture2D>("EnemyShipNeon");
            _moneyTexture = Content.Load<Texture2D>("Money");
            _shield = Content.Load<Texture2D>("Shield");
            _treasureShipTexture = Content.Load<Texture2D>("TreasureShip");
            BossShotTexture = Content.Load<Texture2D>("BossShotNeon");
            BossShotTexture2 = Content.Load<Texture2D>("TreasureShot");
            _laserEffect = Content.Load<SoundEffect>("laserShoot");
            _enemyDamage = Content.Load<Texture2D>("burst");
            _spaceStation = Content.Load<Texture2D>("spaceStation");
            EnemyShootEffect = Content.Load<SoundEffect>("enemyShoot");
            _enemyLaserTexture = Content.Load<Texture2D>("laserRed");
            _bossTexture = Content.Load<Texture2D>("ufoBlue");
            _asteroid.AsterTexture2D1 = Content.Load<Texture2D>("Meteor1Neon");
            _asteroid.AsterTexture2D2 = Content.Load<Texture2D>("Meteor2Neon");
            _asteroid.AsterTexture2D3 = Content.Load<Texture2D>("Meteor3Neon");
            _asteroid.AsterTexture2D4 = Content.Load<Texture2D>("Meteor4Neon");
            _asteroid.MinitETexture2D1 = Content.Load<Texture2D>("tMeteorNeon");
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
        ///     UnloadContent will be called once per game and is the place to unload
        ///     game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        ///     Allows the game to run logic such as updating the world,
        ///     checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            var state = Keyboard.GetState();
            if (state.IsKeyDown(Keys.F11))
                _graphics.ToggleFullScreen();
            switch (Gamestate)
            {
                case GameState.Menu:

                    #region Menu

                    if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
                        Keyboard.GetState().IsKeyDown(Keys.Escape))
                        Exit();

                    if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                    {
                        Player.Position = Vector2.Zero;
                        Player.Speed = Vector2.Zero;
                        Money.Moneyroids.Clear();
                        Exp.CurrentScore = 0;
                        Exp.CurrentExp = 0;
                        BossKill = false;
                        Player.MaxHealth = 5;
                        Player.MaxShield = 5;
                        Player.Health = Player.MaxHealth;
                        Player.Shield = Player.MaxShield;
                        DefeatedEnemies = 0;
                        Bosses.Clear();
                        Bombships.Clear();
                        _enemies.Clear();
                        _asteroid.MiniStroids.Clear();
                        BossShots.Clear();
                        Shots.Clear();
                        EnemyShots.Clear();
                        DefeatedEnemies = 0;
                        //playerShieldCooldown = 0;
                        //shieldTime = 0;
                        //playerShieldTimer = 0;
                        _wantedEnemies = 1;
                        TreasureShips.Clear();
                        _asteroid.NrofAsteroids.Clear();
                        FasterLaser = false;
                        MultiShot = false;
                        Boost.NrOfBoosts = 3;


                        Gamestate = GameState.Playing;
                    }

                    #endregion

                    break;
                case GameState.Playing:

                    #region Playing

                    //if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
                    //    Keyboard.GetState().IsKeyDown(Keys.Escape))
                    //    Exit();
                    if (BossKill)
                        Gamestate = GameState.Winscreen;
                    if (Player.Position.X <= new Vector2(400, 0).X && Player.Position.X >= new Vector2(-400, 0).X)
                        if (Player.Position.Y <= new Vector2(0, 400).Y + 400 &&
                            Player.Position.Y >= new Vector2(0, -400).Y)
                        {
                            _inRangeToBuyString = "Press E to buy";
                            if (Keyboard.GetState().IsKeyDown(Keys.E) && PreviousKbState.IsKeyUp(Keys.E))
                                Gamestate = GameState.Shopping;
                        }
                        else
                        {
                            _inRangeToBuyString = "";
                        }
                    else
                        _inRangeToBuyString = "";

                    if (state.IsKeyDown(Keys.Up))
                        Player.Accelerate();

                    if (state.IsKeyDown(Keys.Left))
                        Player.Rotation -= 0.07f;
                    else if (state.IsKeyDown(Keys.Right))
                        Player.Rotation += 0.07f;

                    if (state.IsKeyDown(Keys.P) && PreviousKbState.IsKeyUp(Keys.P))
                        Gamestate = GameState.Paused;
                    if (state.IsKeyDown(Keys.K) && PreviousKbState.IsKeyUp(Keys.K))
                        Player.Health = 0;
                    PreviousKbState = Keyboard.GetState();
                    if (state.IsKeyDown(Keys.Space))
                        if (_reloadTime <= 0)
                            if (MultiShot)
                            {
                                _laserEffect.Play(0.2f, 0.0f, 0.0f);
                                var s = Player.MultiShot();
                                if (s != null)
                                    Shots.Add(s);
                                var s2 = Player.MultiShot();
                                if (s2 != null)
                                    Shots.Add(s2);
                                _reloadTime = 10;
                            }
                            else
                            {
                                _laserEffect.Play(0.2f, 0.0f, 0.0f);
                                var s = Player.Shoot();
                                if (s != null)
                                    Shots.Add(s);
                                _reloadTime = 10;
                            }
                    if (state.IsKeyDown(Keys.C) && _playerShieldCooldown <= 0)
                    {
                        _playerShieldTimer = 60;
                        _playerShieldCooldown = 300;
                    }
                    if (state.IsKeyDown(Keys.B))
                        Player.Speed = new Vector2(0, 0);
                    if (state.IsKeyDown(Keys.L))
                        MultiShot = true;

                    #region Playermovement

                    Player.Position += Player.Speed;
                    if (Player.Speed.LengthSquared() > 120)
                        Player.Speed = Player.Speed * 0.97f;

                    if (Player.Speed.LengthSquared() <= 120 && !Player.Accelerating)
                        Player.Speed = Player.Speed * 0.99f;
                    Player.Accelerating = false;
                    base.Update(gameTime);

                    #endregion

                    _enemyAmountTimer--;
                    if (_enemyAmountTimer <= 0)
                    {
                        _wantedEnemies++;
                        _enemyAmountTimer = 600;
                    }


                    #region Collision

                    foreach (var enemy in _enemies)
                    {
                        var xDiffPlayer = Math.Abs(enemy.Position.X - Player.Position.X);
                        var yDiffPlayer = Math.Abs(enemy.Position.Y - Player.Position.Y);
                        if (xDiffPlayer > 3000 || yDiffPlayer > 3000)
                            enemy.IsDead = true;
                    }
                    foreach (var enemy in Bombships)
                    {
                        var xDiffPlayer = Math.Abs(enemy.Position.X - Player.Position.X);
                        var yDiffPlayer = Math.Abs(enemy.Position.Y - Player.Position.Y);
                        if (xDiffPlayer > 3000 || yDiffPlayer > 3000)
                            enemy.IsDead = true;
                    }
                    foreach (var powerup in _powerups)
                    {
                        var xDiffPlayer = Math.Abs(powerup.Position.X - Player.Position.X);
                        var yDiffPlayer = Math.Abs(powerup.Position.Y - Player.Position.Y);
                        if (xDiffPlayer > 3000 || yDiffPlayer > 3000)
                            powerup.IsDead = true;
                    }

                    if (_spawnCompass)
                    {
                        _spawnCompass = false;
                        Compass = Compass.CompassSpawn();
                    }
                    if (_spawnBossCompass)
                    {
                        _spawnBossCompass = false;
                        Bosscompass = Bosscompass.BossCompassSpawn();
                    }
                    if (Bosses.Count > 0)
                        Bosscompass.Update(gameTime, this);
                    Compass.Update(gameTime, this);


                    if (_enemies.Count + Bombships.Count < _wantedEnemies)
                        switch (_rand.Next(1, 3))
                        {
                            case 1:
                                var e = Enemy.EnemySpawn(this);
                                if (e != null)
                                    _enemies.Add(e);
                                break;
                            case 2:
                                var be = BombEnemy.BombEnemySpawn(this);
                                if (be != null)
                                    Bombships.Add(be);
                                break;
                        }

                    if (Exp.CurrentEnemiesKilled > 1)
                    {
                        if (Bosses.Count < 1)
                        {
                            var be = BossEnemy.SpawnBoss(this);
                            if (be != null)
                                Bosses.Add(be);
                        }
                        Exp.CurrentEnemiesKilled = 0;
                    }
                    if (TreasureShips.Count < 1)
                        if (_rand.Next(0, 140) == 120)
                        {
                            var te = _treasureShip.SpawnTreasureShip(this);
                            if (te != null)
                                TreasureShips.Add(te);
                        }

                    if (_powerups.Count < _wantedPowerUps)
                    {
                        var p = PowerUp.PowerUpSpawn(this);
                        if (p != null)
                            _powerups.Add(p);
                    }

                    _asteroid.Update(gameTime);
                    foreach (var dummy in _asteroid.NrofAsteroids)
                    {
                        var hitasteroid = _asteroid.NrofAsteroids.FirstOrDefault(e => e.CollidesWith(Player));
                        if (hitasteroid != null)
                        {
                            PlayerHitAsteoid.Play();
                            if (_playerInvincibilityTimer <= 0 && _playerShieldTimer <= 0)
                            {
                                if (Player.Shield <= 0)
                                {
                                    Player.Health -= 1;
                                    _shieldTime = 200;
                                }
                                else
                                {
                                    Player.Shield--;
                                    _shieldTime = 200;
                                }

                                _playerInvincibilityTimer = 10;
                            }
                            hitasteroid.IsDead = true;
                            for (var k = 0; k < 10; k++)
                                _asteroid.MiniStroid(hitasteroid.Position);
                        }
                        break;
                    }
                    foreach (var currentMiniAsteroid in _asteroid.MiniStroids)
                    {
                        if (currentMiniAsteroid.Timer <= 0)
                            currentMiniAsteroid.IsDead = true;
                        currentMiniAsteroid.Timer--;
                    }
                    _asteroid.Timer--;
                    foreach (var dummy in _powerups)
                    {
                        var hitPowerup = _powerups.FirstOrDefault(p => p.CollidesWith(Player));
                        if (hitPowerup == null) continue;
                        HealthPickup.Play();
                        Player.Health += 1;
                        Player.Health = Player.MaxHealth;
                        hitPowerup.IsDead = true;
                        break;
                    }
                    foreach (var dummy in Bombships)
                    {
                        var buHit = Bombships.FirstOrDefault(p => p.CollidesWith(Player));
                        if (buHit == null) continue;
                        if (_playerInvincibilityTimer <= 0 && _playerShieldTimer <= 0)
                        {
                            if (Player.Shield <= 0)
                            {
                                Player.Health -= 3;
                                _shieldTime = 500;
                            }
                            else
                            {
                                Player.Shield -= 3;
                                _shieldTime = 500;
                            }
                            if (Player.Shield < 0)
                                Player.Shield = 0;


                            _playerInvincibilityTimer = 10;
                        }
                        buHit.IsDead = true;
                        break;
                    }
                    _powerups.RemoveAll(powerup => powerup.IsDead);
                    Bombships.RemoveAll(powerup => powerup.IsDead);
                    foreach (var bs in BossShots)
                    {
                        bs.Timer--;
                        if (bs.Timer <= 0)
                            bs.IsDead = true;
                    }
                    foreach (var shot in Shots)
                    {
                        shot.Update(gameTime);
                        var enemy = _enemies.FirstOrDefault(d => d.CollidesWith(shot));
                        var hitasteroid = _asteroid.NrofAsteroids.FirstOrDefault(e => e.CollidesWith(shot));
                        var hitBoss = Bosses.FirstOrDefault(e => e.CollidesWith(shot));
                        var treasureHit = TreasureShips.FirstOrDefault(te => te.CollidesWith(shot));
                        var bomb = Bombships.FirstOrDefault(beb => beb.CollidesWith(shot));

                        if (enemy != null)
                        {
                            enemy.Health -= 1;
                            if (enemy.Health <= 0)
                            {
                                MeteorExplosion.Play(0.5f, 0.0f, 0.0f);
                                enemy.IsDead = true;
                                DefeatedEnemies += 1;
                                Exp.CurrentScore += enemy.ScoreReward;
                                for (var i = 0; i < _rand.Next(1, 5); i++)
                                    Money.MoneyRoid(enemy.Position + new Vector2(_rand.Next(-50, 50)));
                                Exp.CurrentEnemiesKilled++;
                            }
                            _enemyHit = true;
                            _enemyPositionExplosion = enemy.Position;
                            shot.IsDead = true;
                        }
                        if (treasureHit != null)
                        {
                            treasureHit.Health -= 1;
                            for (var i = 0; i < _rand.Next(1, 4); i++)
                                Money.MoneyRoid(treasureHit.Position + new Vector2(_rand.Next(-50, 50)));
                            if (treasureHit.Health <= 0)
                            {
                                MeteorExplosion.Play(0.5f, 0.0f, 0.0f);
                                treasureHit.IsDead = true;
                                Exp.CurrentScore += treasureHit.ScoreReward;
                                Exp.CurrentEnemiesKilled++;
                            }
                            _enemyHit = true;
                            _enemyPositionExplosion = treasureHit.Position;
                            Debug.WriteLine(_enemyPositionExplosion);
                            shot.IsDead = true;
                        }
                        if (hitBoss != null)
                        {
                            hitBoss.Health -= 1;
                            if (hitBoss.Health <= 0)
                            {
                                _spawnBossCompass = true;
                                MeteorExplosion.Play(0.5f, 0.0f, 0.0f);
                                hitBoss.IsDead = true;
                                BossKill = true;
                                Exp.CurrentScore += hitBoss.ScoreReward;
                                for (var i = 0; i < _rand.Next(100, 150); i++)
                                    Money.MoneyRoid(hitBoss.Position + new Vector2(_rand.Next(-100, 100)));
                            }
                            _enemyHit = true;
                            _enemyPositionExplosion = hitBoss.Position;
                            Debug.WriteLine(_enemyPositionExplosion);
                            shot.IsDead = true;
                        }
                        if (hitasteroid != null)
                        {
                            var xDiffPlayer = Math.Abs(hitasteroid.Position.X - Player.Position.X);
                            var yDiffPlayer = Math.Abs(hitasteroid.Position.Y - Player.Position.Y);
                            if (yDiffPlayer < 1300 && xDiffPlayer < 1300)
                                MeteorExplosion.Play(0.5f, 0.0f, 0.0f);
                            _asteroid.MiniStroid(hitasteroid.Position);
                            _asteroid.MiniStroid(hitasteroid.Position);
                            _asteroid.MiniStroid(hitasteroid.Position);
                            _asteroid.NrofAsteroids.Remove(hitasteroid);
                            Exp.CurrentScore += hitasteroid.ScoreReward;
                            Debug.WriteLine(Exp.CurrentScore);
                            shot.IsDead = true;
                        }
                        shot.Timer--;
                        if (shot.Timer <= 0)
                            shot.IsDead = true;
                        if (bomb != null)
                        {
                            bomb.Health -= 1;
                            if (bomb.Health <= 0)
                            {
                                for (var i = 0; i < _rand.Next(1, 4); i++)
                                    Money.MoneyRoid(bomb.Position + new Vector2(_rand.Next(-100, 100)));
                                MeteorExplosion.Play(0.5f, 0.0f, 0.0f);
                                bomb.IsDead = true;
                                DefeatedEnemies++;
                                Exp.CurrentScore += bomb.ScoreReward;
                                Exp.CurrentEnemiesKilled++;
                            }
                            _enemyHit = true;
                            _enemyPositionExplosion = bomb.Position;
                            shot.IsDead = true;
                        }
                    }
                    foreach (var shot in EnemyShots)
                    {
                        var hitasteroid = _asteroid.NrofAsteroids.FirstOrDefault(e => e.CollidesWith(shot));

                        if (hitasteroid != null)
                        {
                            _asteroid.MiniStroid(hitasteroid.Position);
                            _asteroid.MiniStroid(hitasteroid.Position);
                            _asteroid.MiniStroid(hitasteroid.Position);
                            _asteroid.NrofAsteroids.Remove(hitasteroid);

                            shot.IsDead = true;
                        }

                        shot.Update(gameTime);
                        shot.Timer--;
                        if (shot.Timer <= 0)
                            shot.IsDead = true;
                    }
                    foreach (var e in _enemies)
                        e.Update(gameTime, this);
                    foreach (var be in Bombships)
                        be.Update(gameTime, this);

                    foreach (var be in Bosses)
                        be.Update(gameTime, this);
                    foreach (var te in TreasureShips)
                    {
                        te.Update(gameTime, this);
                        te.Timer--;
                        if (te.Timer <= 0)
                            te.IsDead = true;
                    }

                    foreach (var s in BossShots)
                        s.Update(gameTime);

                    var shotHit = EnemyShots.FirstOrDefault(e => e.CollidesWith(Player));
                    if (shotHit != null)
                    {
                        if (_playerInvincibilityTimer <= 0 && _playerShieldTimer <= 0)
                        {
                            if (Player.Shield <= 0)
                            {
                                PlayerDamage.Play(0.5f, 0.0f, 0.0f);
                                Player.Health -= 1;
                                _shieldTime = 500;
                            }
                            else
                            {
                                ShieldDamage.Play(0.5f, 0.0f, 0.0f);
                                Player.Shield--;
                                _shieldTime = 500;
                            }

                            _playerInvincibilityTimer = 10;
                        }
                        shotHit.IsDead = true;
                    }
                    var bossShot = BossShots.FirstOrDefault(be => be.CollidesWith(Player));
                    if (bossShot != null)
                    {
                        if (_playerInvincibilityTimer <= 0 && _playerShieldTimer <= 0)
                        {
                            if (Player.Shield <= 0)
                            {
                                PlayerDamage.Play(0.5f, 0.0f, 0.0f);
                                Player.Health -= 2;
                                _shieldTime = 500;
                            }
                            else
                            {
                                ShieldDamage.Play(0.5f, 0.0f, 0.0f);
                                if (Player.Shield > 4)
                                    Player.Shield -= 5;
                                else
                                    Player.Shield = 0;

                                _shieldTime = 500;
                            }

                            _playerInvincibilityTimer = 10;
                        }
                        bossShot.IsDead = true;
                    }


                    var moneyHit = Money.Moneyroids.FirstOrDefault(m => m.CollidesWith(Player));
                    if (moneyHit != null)
                    {
                        moneyHit.IsDead = true;
                        Exp.CurrentExp += 50;
                    }


                    if (Player.Health <= 0)
                    {
                        Player.Position = new Vector2(0, 0);
                        Player.Health = Player.MaxHealth;
                        Player.Shield = Player.MaxShield;
                        DeathSound.Play();
                        MediaPlayer.Stop();
                        Gamestate = GameState.GameOver;
                    }


                    if (Player.Shield < Player.MaxShield && _shieldTime <= 0)
                    {
                        Player.Shield++;
                        _shieldTime = 40;
                        if (Player.Shield == Player.MaxShield)
                            ShieldUp.Play(0.5f, 0.0f, 0.0f);
                        else
                            ShieldRegenerating.Play(0.5f, 0.0f, 0.0f);
                    }
                    if (_shieldTime >= 0)
                        _shieldTime--;
                    if (_playerInvincibilityTimer >= 0)
                        _playerInvincibilityTimer--;

                    #endregion


                    Shots.RemoveAll(s => s.IsDead);
                    TreasureShips.RemoveAll(treasure => treasure.IsDead);
                    BossShots.RemoveAll(bs => bs.IsDead);
                    EnemyShots.RemoveAll(shot => shot.IsDead);
                    _enemies.RemoveAll(enemy => enemy.IsDead);

                    _asteroid.MiniStroids.RemoveAll(n => n.IsDead);
                    _asteroid.NrofAsteroids.RemoveAll(j => j.IsDead);
                    Money.Moneyroids.RemoveAll(money => money.IsDead);
                    Bosses.RemoveAll(b => b.IsDead);
                    Player.Update(gameTime);
                    _ui.Update(gameTime);
                    PreviousKbState = state;
                    Boost.Update(gameTime);
                    _camera.Update(gameTime, Player);
                    Money.Update(gameTime, this);
                    _gameOverScreen.Update(gameTime);
                    if (_reloadTime >= 0)
                        if (FasterLaser)
                            _reloadTime -= 1.6f;
                        else
                            _reloadTime--;
                    if (_playerShieldCooldown >= 0)
                        _playerShieldCooldown--;
                    if (_playerShieldTimer >= 0)
                        _playerShieldTimer--;
                    if (SoundEffectTimer > 0)
                        SoundEffectTimer--;

                    #endregion playing

                    break;
                case GameState.Paused:

                    #region Paused

                    if (state.IsKeyDown(Keys.P) && PreviousKbState.IsKeyUp(Keys.P))
                        Gamestate = GameState.Playing;
                    PreviousKbState = Keyboard.GetState();

                    #endregion Paused

                    break;
                case GameState.Shopping:

                    #region Shopping

                    //if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                    //    Exit();


                    if (Keyboard.GetState().IsKeyDown(Keys.E) && PreviousKbState.IsKeyUp(Keys.E))
                        Gamestate = GameState.Playing;
                    Player.Speed = new Vector2(0, 0);

                    ShopItem.Update(gameTime);
                    Shop.Update(gameTime);
                    PreviousKbState = Keyboard.GetState();

                    #endregion Shopping

                    break;
                case GameState.GameOver:
                    _gameOverScreen.Update(gameTime);
                    _asteroid.NrofAsteroids.Clear();
                    _asteroid.MiniStroids.Clear();
                    _enemies.Clear();
                    Bosses.Clear();

                    _wantedEnemies = 5;
                    TreasureShips.Clear();
                    BossShots.Clear();
                    Shots.Clear();
                    break;
                case GameState.Winscreen:
                    _winScreen.Update(gameTime);
                    _asteroid.NrofAsteroids.Clear();
                    _asteroid.MiniStroids.Clear();
                    _enemies.Clear();
                    Bosses.Clear();
                    _wantedEnemies = 5;
                    TreasureShips.Clear();
                    BossShots.Clear();
                    break;
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            switch (Gamestate)
            {
                case GameState.Menu:

                    #region Menu

                    GraphicsDevice.Clear(Color.Black);
                    _startMenu.Draw(gameTime);

                    #endregion

                    break;
                case GameState.Playing:

                    #region Playing

                    GraphicsDevice.Clear(Color.CornflowerBlue);
                    base.Draw(gameTime);


                    _spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null,
                        _camera.Transformn);

                    _backgrSpriteBatch.Begin();

                    StartX = Player.Position.X % _backgroundTexture.Width;
                    StartY = Player.Position.Y % _backgroundTexture.Height;

                    for (var y = -StartY - _backgroundTexture.Height;
                        y < Globals.ScreenHeight;
                        y += _backgroundTexture.Width)
                    for (var x = -StartX - _backgroundTexture.Width;
                        x < Globals.ScreenWidth;
                        x += _backgroundTexture.Width)
                        _backgrSpriteBatch.Draw(_backgroundTexture, new Vector2(x, y), Color.White);

                    _backgrSpriteBatch.End();

                    foreach (var mini in _asteroid.MiniStroids)
                    {
                        _spriteBatch.Draw(_asteroid.MinitETexture2D1, mini.Position, null, Color.White,
                            _asteroid.Rotation + mini.RotationCounter,
                            new Vector2(_asteroid.MinitETexture2D1.Width / 2f, _asteroid.MinitETexture2D1.Height / 2f),
                            1f, SpriteEffects.None, 0f);
                        mini.RotationCounter += mini.AddCounter;
                    }
                    foreach (var money in Money.Moneyroids)
                        _spriteBatch.Draw(_moneyTexture, money.Position, null, Color.White,
                            money.Rotation + money.RotationCounter,
                            new Vector2(_moneyTexture.Width / 2f, _moneyTexture.Height / 2f), 0.7f, SpriteEffects.None,
                            0f);


                    for (var i = 0; i < _asteroid.NrofAsteroids.Count; i++)
                        switch (_asteroid.NrofAsteroids[i].ChosenTexture)
                        {
                            case 1:
                                _spriteBatch.Draw(_asteroid.AsterTexture2D1, _asteroid.NrofAsteroids[i].Position, null,
                                    Color.White, _asteroid.Rotation + _asteroid.NrofAsteroids[i].RotationCounter,
                                    new Vector2(_asteroid.AsterTexture2D1.Width / 2f,
                                        _asteroid.AsterTexture2D1.Height / 2f), 1f, SpriteEffects.None, 0f);
                                _asteroid.NrofAsteroids[i].RotationCounter += _asteroid.NrofAsteroids[i].AddCounter;
                                break;
                            case 2:
                                _spriteBatch.Draw(_asteroid.AsterTexture2D2, _asteroid.NrofAsteroids[i].Position, null,
                                    Color.White, _asteroid.Rotation + _asteroid.NrofAsteroids[i].RotationCounter,
                                    new Vector2(_asteroid.AsterTexture2D2.Width / 2f,
                                        _asteroid.AsterTexture2D2.Height / 2f), 1f, SpriteEffects.None, 0f);
                                _asteroid.NrofAsteroids[i].RotationCounter += _asteroid.NrofAsteroids[i].AddCounter;
                                break;
                            case 3:
                                _spriteBatch.Draw(_asteroid.AsterTexture2D3, _asteroid.NrofAsteroids[i].Position, null,
                                    Color.White, _asteroid.Rotation + _asteroid.NrofAsteroids[i].RotationCounter,
                                    new Vector2(_asteroid.AsterTexture2D3.Width / 2f,
                                        _asteroid.AsterTexture2D3.Height / 2f), 1f, SpriteEffects.None, 0f);
                                _asteroid.NrofAsteroids[i].RotationCounter += _asteroid.NrofAsteroids[i].AddCounter;
                                break;
                            case 4:
                                _spriteBatch.Draw(_asteroid.AsterTexture2D4, _asteroid.NrofAsteroids[i].Position, null,
                                    Color.White, _asteroid.Rotation + _asteroid.NrofAsteroids[i].RotationCounter,
                                    new Vector2(_asteroid.AsterTexture2D4.Width / 2f,
                                        _asteroid.AsterTexture2D4.Height / 2f), 1f, SpriteEffects.None, 0f);
                                _asteroid.NrofAsteroids[i].RotationCounter += _asteroid.NrofAsteroids[i].AddCounter;
                                break;
                        }

                    foreach (var s in Shots)
                        _spriteBatch.Draw(_laserTexture, s.Position, null, Color.White, s.Rotation + MathHelper.PiOver2,
                            new Vector2(_laserTexture.Width / 2f, _laserTexture.Height / 2f), 1.0f, SpriteEffects.None,
                            0f);

                    foreach (var s in EnemyShots)
                        _spriteBatch.Draw(_enemyLaserTexture, s.Position, null, Color.White, s.Rotation,
                            new Vector2(_laserTexture.Width / 2, _laserTexture.Height / 2), 1.0f, SpriteEffects.None,
                            0f);

                    foreach (var s in BossShots)
                        _spriteBatch.Draw(s.ChosenTexture2D, s.Position, null, Color.White, s.Rotation,
                            new Vector2(BossShotTexture.Width / 2, BossShotTexture.Height / 2), 1.0f,
                            SpriteEffects.None, 0f);


                    foreach (var e in _enemies)
                        _spriteBatch.Draw(_enemyTexture, e.Position, null, Color.White, e.Rotation + MathHelper.PiOver2,
                            new Vector2(_enemyTexture.Width / 2, _enemyTexture.Height / 2), 0.4f, SpriteEffects.None,
                            0f);

                    foreach (var be in Bombships)
                        _spriteBatch.Draw(_bombEnemyTexture, be.Position, null, Color.White, be.Rotation + 0.01f,
                            new Vector2(_bombEnemyTexture.Width / 2, _bombEnemyTexture.Height / 2), 0.2f,
                            SpriteEffects.None, 0f);

                    foreach (var p in _powerups)
                        _spriteBatch.Draw(_powerUpHealth, p.Position, null, Color.White,
                            p.Rotation + MathHelper.PiOver2,
                            new Vector2(_powerUpHealth.Width / 2, _powerUpHealth.Height / 2), 0.15f, SpriteEffects.None,
                            0f);

                    foreach (var boss in Bosses)
                        _spriteBatch.Draw(_bossTexture, boss.Position, null, Color.White, EnemyRotation,
                            new Vector2(_bossTexture.Width / 2f, _bossTexture.Height / 2f), 1f, SpriteEffects.None, 0f);

                    foreach (var treasureShip in TreasureShips)
                        _spriteBatch.Draw(_treasureShipTexture, treasureShip.Position, null, Color.White, EnemyRotation,
                            new Vector2(_treasureShipTexture.Width / 2f, _treasureShipTexture.Height / 2f), 1f,
                            SpriteEffects.None, 0f);

                    _spriteBatch.Draw(_spaceStation, new Vector2(0, 0), null, Color.White, SpaceStationRotation,
                        new Vector2(_spaceStation.Width / 2f, _spaceStation.Height / 2f), 2f, SpriteEffects.None, 0f);

                    if (_playerShieldTimer > 0)
                        _spriteBatch.Draw(_shield, new Vector2(Player.Position.X, Player.Position.Y), null, Color.White,
                            0f, new Vector2(_shield.Width / 2f, _shield.Height / 2f), 0.5f, SpriteEffects.None, 0f);


                    Player.Draw(_spriteBatch);

                    EnemyRotation += 0.02f;
                    SpaceStationRotation += 0.001f;

                    if (_enemyHit)
                    {
                        _spriteBatch.Draw(_enemyDamage, _enemyPositionExplosion, null, Color.White, 1f,
                            new Vector2(_enemyDamage.Width / 2f, _enemyDamage.Height / 2f), 0.5f, SpriteEffects.None,
                            0f);
                        _enemyHit = false;
                    }

                    if (_inRangeToBuyString.Length > 0)
                        _spriteBatch.DrawString(_ui.ScoreFont, _inRangeToBuyString, new Vector2(-120, -300),
                            Color.White);


                    _spriteBatch.End();

                    _ui.Draw(gameTime);

                    #endregion state playing

                    break;
                case GameState.Paused:
                    break;
                case GameState.Shopping:

                    #region Shopping

                    GraphicsDevice.Clear(Color.CornflowerBlue);
                    base.Draw(gameTime);
                    _spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null,
                        _camera.Transformn);

                    _backgrSpriteBatch.Begin();

                    StartX = Player.Position.X % _backgroundTexture.Width;
                    StartY = Player.Position.Y % _backgroundTexture.Height;

                    for (var y = -StartY - _backgroundTexture.Height;
                        y < Globals.ScreenHeight;
                        y += _backgroundTexture.Width)
                    for (var x = -StartX - _backgroundTexture.Width;
                        x < Globals.ScreenWidth;
                        x += _backgroundTexture.Width)
                        _backgrSpriteBatch.Draw(_backgroundTexture, new Vector2(x, y), Color.White);

                    _backgrSpriteBatch.End();

                    foreach (var mini in _asteroid.MiniStroids)
                        _spriteBatch.Draw(_asteroid.MinitETexture2D1, mini.Position, Color.White);
                    for (var i = 0; i < _asteroid.NrofAsteroids.Count; i++)
                        switch (_asteroid.NrofAsteroids[i].ChosenTexture)
                        {
                            case 1:
                                _spriteBatch.Draw(_asteroid.AsterTexture2D1, _asteroid.NrofAsteroids[i].Position, null,
                                    Color.White, _asteroid.Rotation + _asteroid.NrofAsteroids[i].RotationCounter,
                                    new Vector2(_asteroid.AsterTexture2D1.Width / 2f,
                                        _asteroid.AsterTexture2D1.Height / 2f), 1f, SpriteEffects.None, 0f);
                                _asteroid.NrofAsteroids[i].RotationCounter += _asteroid.NrofAsteroids[i].AddCounter;
                                break;
                            case 2:
                                _spriteBatch.Draw(_asteroid.AsterTexture2D2, _asteroid.NrofAsteroids[i].Position, null,
                                    Color.White, _asteroid.Rotation + _asteroid.NrofAsteroids[i].RotationCounter,
                                    new Vector2(_asteroid.AsterTexture2D2.Width / 2f,
                                        _asteroid.AsterTexture2D2.Height / 2f), 1f, SpriteEffects.None, 0f);
                                _asteroid.NrofAsteroids[i].RotationCounter += _asteroid.NrofAsteroids[i].AddCounter;
                                break;
                            case 3:
                                _spriteBatch.Draw(_asteroid.AsterTexture2D3, _asteroid.NrofAsteroids[i].Position, null,
                                    Color.White, _asteroid.Rotation + _asteroid.NrofAsteroids[i].RotationCounter,
                                    new Vector2(_asteroid.AsterTexture2D3.Width / 2f,
                                        _asteroid.AsterTexture2D3.Height / 2f), 1f, SpriteEffects.None, 0f);
                                _asteroid.NrofAsteroids[i].RotationCounter += _asteroid.NrofAsteroids[i].AddCounter;
                                break;
                            case 4:
                                _spriteBatch.Draw(_asteroid.AsterTexture2D4, _asteroid.NrofAsteroids[i].Position, null,
                                    Color.White, _asteroid.Rotation + _asteroid.NrofAsteroids[i].RotationCounter,
                                    new Vector2(_asteroid.AsterTexture2D4.Width / 2f,
                                        _asteroid.AsterTexture2D4.Height / 2f), 1f, SpriteEffects.None, 0f);
                                _asteroid.NrofAsteroids[i].RotationCounter += _asteroid.NrofAsteroids[i].AddCounter;
                                break;
                        }

                    foreach (var s in Shots)
                        _spriteBatch.Draw(_laserTexture, s.Position, null, Color.White, s.Rotation + MathHelper.PiOver2,
                            new Vector2(_laserTexture.Width / 2, _laserTexture.Height / 2), 1.0f, SpriteEffects.None,
                            0f);

                    foreach (var s in EnemyShots)
                        _spriteBatch.Draw(_enemyLaserTexture, s.Position, null, Color.White, s.Rotation,
                            new Vector2(_laserTexture.Width / 2, _laserTexture.Height / 2), 1.0f, SpriteEffects.None,
                            0f);

                    foreach (var e in _enemies)
                        _spriteBatch.Draw(_enemyTexture, e.Position, null, Color.White, e.Rotation + MathHelper.PiOver2,
                            new Vector2(_enemyTexture.Width / 2, _enemyTexture.Height / 2), 0.4f, SpriteEffects.None,
                            0f);

                    foreach (var p in _powerups)
                        _spriteBatch.Draw(_powerUpHealth, p.Position, null, Color.White,
                            p.Rotation + MathHelper.PiOver2,
                            new Vector2(_powerUpHealth.Width / 2, _powerUpHealth.Height / 2), 0.15f, SpriteEffects.None,
                            0f);

                    _spriteBatch.Draw(_spaceStation, new Vector2(0, 0), null, Color.White, SpaceStationRotation,
                        new Vector2(_spaceStation.Width / 2f, _spaceStation.Height / 2f), 2f, SpriteEffects.None, 0f);

                    Player.Draw(_spriteBatch);

                    SpaceStationRotation += 0.001f;

                    if (_enemyHit)
                    {
                        _spriteBatch.Draw(_enemyDamage, _enemyPositionExplosion, null, Color.White, 1f,
                            new Vector2(_enemyDamage.Width / 2f, _enemyDamage.Height / 2f), 0.5f, SpriteEffects.None,
                            0f);
                        _enemyHit = false;
                    }


                    _spriteBatch.End();

                    _ui.Draw(gameTime);
                    Shop.Draw(gameTime);
                    ShopItem.Draw(gameTime);

                    #endregion Shopping

                    break;
                case GameState.GameOver:

                    #region GameOver

                    _gameOverScreen.Draw(gameTime);

                    #endregion

                    break;
                case GameState.Winscreen:
                    _winScreen.Draw(gameTime);
                    break;
            }
        }
    }
}