using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Space_Scavenger
{
    public class StartMenu : DrawableGameComponent
    {
        private Texture2D _background;
        private bool _flashingTextState = true;
        private int _flashingTextTimer;
        private Texture2D _insertCoinTexture;
        private Texture2D _menuText;
        private MovingMenu _movingMenu;
        private SpriteBatch _spriteBatch;
        public float StartY, StartX;


        public StartMenu(Game game) : base(game)
        {
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(Game.GraphicsDevice);
            _background = Game.Content.Load<Texture2D>("StartBackgroundNeon");
            _menuText = Game.Content.Load<Texture2D>("MenuText");
            _insertCoinTexture = Game.Content.Load<Texture2D>("InsertCoinText");
            _movingMenu = new MovingMenu();

            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            _movingMenu.Update(gameTime);

            if (_flashingTextTimer > 0)
            {
                _flashingTextTimer--;
            }
            else if (_flashingTextTimer <= 0)
            {
                if (_flashingTextState)
                    _flashingTextState = false;
                else
                    _flashingTextState = true;
                _flashingTextTimer = 60;
            }
            base.LoadContent();
        }

        public override void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin();

            StartX = _movingMenu.Position.X % _background.Width;
            StartY = _movingMenu.Position.Y % _background.Height;

            for (var y = -StartY - _background.Height; y < Globals.ScreenHeight; y += _background.Width)
            for (var x = -StartX - _background.Width; x < Globals.ScreenWidth; x += _background.Width)
                _spriteBatch.Draw(_background, new Vector2(x, y), Color.White);
            _spriteBatch.Draw(_menuText,
                new Vector2(Globals.ScreenWidth / 2 - _menuText.Width / 2,
                    Globals.ScreenHeight / 2 - 300 - _menuText.Height / 2), Color.White);

            if (_flashingTextState)
                _spriteBatch.Draw(_insertCoinTexture,
                    new Vector2(Globals.ScreenWidth / 2 - _insertCoinTexture.Width / 2,
                        Globals.ScreenHeight / 2 + 300 - _insertCoinTexture.Height / 2), Color.White);
            _spriteBatch.End();
        }
    }
}