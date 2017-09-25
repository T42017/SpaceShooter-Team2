using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Space_Scavenger
{
    public class GameOverScreen : DrawableGameComponent
    {
        private readonly SpaceScavenger _myGame;
        private SpriteFont _gameOverFont;
        private KeyboardState _prevKeyboardState;
        private SpriteBatch _spriteBatch;
        public Texture2D GameOverTexture2D, GameOverFilter, PressSpaceTexture2D;

        public GameOverScreen(Game game) : base(game)
        {
            _myGame = (SpaceScavenger) Game;
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(Game.GraphicsDevice);
            _gameOverFont = Game.Content.Load<SpriteFont>("ScoreFont");
            GameOverTexture2D = Game.Content.Load<Texture2D>("GameOverText");
            GameOverFilter = Game.Content.Load<Texture2D>("Transparent-filter");
            PressSpaceTexture2D = Game.Content.Load<Texture2D>("PressSpace");

            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            if (_myGame.Gamestate == GameState.GameOver)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Space) && _prevKeyboardState.IsKeyUp(Keys.Space))
                {
                    MediaPlayer.Play(_myGame.BackgroundSong);
                    _myGame.Gamestate = GameState.Menu;
                }
                _prevKeyboardState = Keyboard.GetState();
            }

            base.LoadContent();
        }

        public override void Draw(GameTime gameTime)
        {
            var textSize2 = _gameOverFont.MeasureString("Enemies Defeated: " + _myGame.DefeatedEnemies);
            var textMiddlePoint2 = new Vector2(textSize2.X / 2, textSize2.Y / 2);
            var textPosition2 = new Vector2((int) textMiddlePoint2.X - textSize2.X,
                (int) textMiddlePoint2.Y - textSize2.Y);
            var textSize = _gameOverFont.MeasureString("Your Score is: " + _myGame.Exp.CurrentScore);
            var textMiddlePoint = new Vector2(textSize.X / 2, textSize.Y / 2);
            var textPosition = new Vector2((int) textMiddlePoint.X - textSize.X, (int) textMiddlePoint.Y - textSize.Y);
            _spriteBatch.Begin();
            _spriteBatch.Draw(GameOverFilter, new Rectangle(0, 0, Globals.ScreenWidth, Globals.ScreenHeight),
                Color.Black);
            _spriteBatch.Draw(GameOverTexture2D,
                new Vector2(Globals.ScreenWidth / 2f - GameOverTexture2D.Width / 2f,
                    Globals.ScreenHeight / 2f - GameOverTexture2D.Height / 2f - 200), Color.White);
            _spriteBatch.Draw(PressSpaceTexture2D,
                new Vector2(Globals.ScreenWidth / 2f - PressSpaceTexture2D.Width / 4f, Globals.ScreenHeight / 2f + 200),
                null, Color.White, 0.05f, new Vector2(0, 0), 0.5f, SpriteEffects.None, 0f);
            _spriteBatch.DrawString(_gameOverFont, "Your Score is: " + _myGame.Exp.CurrentScore,
                new Vector2(Globals.ScreenWidth / 2f + textPosition.X, Globals.ScreenHeight / 2f - textPosition.Y + 50),
                Color.SteelBlue);
            _spriteBatch.DrawString(_gameOverFont, "Enemies Defeated: " + _myGame.DefeatedEnemies,
                new Vector2(Globals.ScreenWidth / 2f + textPosition2.X,
                    Globals.ScreenHeight / 2f - textPosition2.Y + 100), Color.SteelBlue);
            _spriteBatch.End();
        }
    }
}