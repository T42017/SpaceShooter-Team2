using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
// ReSharper disable PossibleLossOfFraction

namespace Space_Scavenger
{
    public class UserInterface : DrawableGameComponent
    {
        private readonly SpaceScavenger _myGame;
        private Texture2D _boosticon;
        private Texture2D _bossCompassT;
        private Texture2D _compassT;
        private Texture2D _healthBarLeft;
        private Texture2D _healthbarMiddle;
        private Texture2D _healthbarRight;
        private SpriteFont _healthFont;
        private Vector2 _position;
        private Texture2D _shieldBarLeft;
        private Texture2D _shieldBarMiddle;
        private Texture2D _shieldBarRight;
        private SpriteBatch _spriteBatch;


        public UserInterface(Game game) : base(game)
        {
            _position = new Vector2(Globals.ScreenWidth / 2f, Globals.ScreenHeight / 2f);
            _myGame = (SpaceScavenger) game;
        }

        public SpriteFont ScoreFont { get; private set; }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(Game.GraphicsDevice);

            ScoreFont = Game.Content.Load<SpriteFont>("ScoreFont");
            _healthFont = Game.Content.Load<SpriteFont>("HealthFont");
            _healthBarLeft = Game.Content.Load<Texture2D>("barHorizontal_purple_left");
            _healthbarMiddle = Game.Content.Load<Texture2D>("barHorizontal_purple_mid");
            _healthbarRight = Game.Content.Load<Texture2D>("barHorizontal_purple_right");
            _shieldBarLeft = Game.Content.Load<Texture2D>("barHorizontal_blue_left");
            _shieldBarMiddle = Game.Content.Load<Texture2D>("barHorizontal_blue_mid");
            _shieldBarRight = Game.Content.Load<Texture2D>("barHorizontal_blue_right");
            _boosticon = Game.Content.Load<Texture2D>("powerupBlue_bolt");
            _compassT = Game.Content.Load<Texture2D>("Arrow");
            _bossCompassT = Game.Content.Load<Texture2D>("bossCompassT");

            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            base.LoadContent();
        }

        public override void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin();

            //Font

            #region DrawFonts

            // Health
            _spriteBatch.DrawString(ScoreFont, "Health: ", new Vector2(_position.X - 940, _position.Y - 530),
                new Color(255, 0, 226));
            _spriteBatch.DrawString(_healthFont, _myGame.Player.Health * 10 + "%",
                new Vector2(_position.X - 810 + _healthbarMiddle.Width * _myGame.Player.MaxHealth, _position.Y - 530),
                Color.White);
            // BossHealth
            if (_myGame.Bosses.Count > 0)
            {
                _spriteBatch.DrawString(ScoreFont, "BossHealth: ", new Vector2(Globals.ScreenWidth / 2 - 170, 9),
                    new Color(255, 0, 226));
                _spriteBatch.DrawString(_healthFont, _myGame.Bosses[0].Health + "%",
                    new Vector2(Globals.ScreenWidth / 2 + 50, 10), Color.White);
            }
            //Shield
            _spriteBatch.DrawString(ScoreFont, "Shield: ", new Vector2(_position.X - 940, _position.Y - 490),
                Color.SkyBlue);
            _spriteBatch.DrawString(_healthFont, _myGame.Player.Shield * 10 + "%",
                new Vector2(_position.X - 810 + _healthbarMiddle.Width * _myGame.Player.MaxShield, _position.Y - 490),
                Color.White);
            //Score and Currency
            _spriteBatch.DrawString(ScoreFont, "score: " + _myGame.Exp.CurrentScore,
                new Vector2(_position.X + 620, _position.Y - 530), Color.White);
            _spriteBatch.DrawString(ScoreFont, "$: " + _myGame.Exp.CurrentExp,
                new Vector2(_position.X + 620, _position.Y - 495), Color.Green);
            // Boost
            _spriteBatch.DrawString(ScoreFont, "Boost: ", new Vector2(_position.X - 940, _position.Y - 450),
                Color.White);
            // Shop

            #endregion


            // Healthbar

            #region DrawHealthBar

            if (_myGame.Player.Health >= 1)
            {
                _spriteBatch.Draw(_healthBarLeft, new Vector2(_position.X - 800, _position.Y - 530), Color.White);

                for (var i = 0; i < _myGame.Player.MaxHealth - 2; i++)
                    _spriteBatch.Draw(_healthbarMiddle,
                        new Vector2(_position.X - 795 + i * _healthbarMiddle.Width, _position.Y - 530), Color.White);


                if (_myGame.Player.Health >= _myGame.Player.MaxHealth)
                    _spriteBatch.Draw(_healthbarRight,
                        new Vector2(_position.X - 795 + _healthbarMiddle.Width * (_myGame.Player.MaxHealth - 2),
                            _position.Y - 530), Color.White);
            }

            #endregion


            // Shieldbar

            #region DrawShieldBar

            if (_myGame.Player.Shield >= 1)
            {
                _spriteBatch.Draw(_shieldBarLeft, new Vector2(_position.X - 800, _position.Y - 490), Color.White);

                for (var i = 0; i < _myGame.Player.Shield - 2; i++)
                    _spriteBatch.Draw(_shieldBarMiddle,
                        new Vector2(_position.X - 795 + i * _shieldBarMiddle.Width, _position.Y - 490), Color.White);

                if (_myGame.Player.Shield >= _myGame.Player.MaxShield)
                    _spriteBatch.Draw(_shieldBarRight,
                        new Vector2(_position.X - 795 + _shieldBarMiddle.Width * (_myGame.Player.MaxShield - 2),
                            _position.Y - 490), Color.White);
            }

            #endregion


            // Boost

            #region Boost

            for (var i = 0; i < _myGame.Boost.NrOfBoosts; i++)
                _spriteBatch.Draw(_boosticon,
                    new Vector2(_position.X - 800 + i * (_boosticon.Width + 20), _position.Y - 455), Color.White);

            //if (_myGame.boost.BoostTime <= 0)
            //{
            //    _spriteBatch.Draw(_boosticon, new Vector2(_position.X - 800, _position.Y - 455), Color.White);
            //}

            #endregion


            /* _spriteBatch.Draw(CompassTexture, new Vector2(500, 500), null, Color.White, 0f, new Vector2(CompassTexture.Width / 2f, CompassTexture.Height / 2f), 1f, SpriteEffects.None, 0f);*/


            _spriteBatch.Draw(_compassT, new Vector2(Globals.ScreenWidth / 2, Globals.ScreenHeight / 2), null,
                Color.White, _myGame.Compass.Rotation, new Vector2(_compassT.Width / 2f, _compassT.Height / 2f), 1f,
                SpriteEffects.None, 0f);
            if (_myGame.Bosses.Count > 0)
                _spriteBatch.Draw(_bossCompassT, new Vector2(Globals.ScreenWidth / 2, Globals.ScreenHeight / 2), null,
                    Color.White, _myGame.Bosscompass.Rotation,
                    new Vector2(_bossCompassT.Width / 2f, _bossCompassT.Height / 2f), 1f, SpriteEffects.None, 0f);


            _spriteBatch.End();
        }
    }
}