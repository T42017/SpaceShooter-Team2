using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Space_Scavenger
{
    public class GameOverScreen : DrawableGameComponent
    {
        private SpriteBatch _spriteBatch;
        private readonly SpaceScavenger _myGame;
        private SpriteFont _gameOverFont;


        public GameOverScreen(Game game) : base(game)
        {
            _myGame = (SpaceScavenger)Game;

        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(Game.GraphicsDevice);
            _gameOverFont = Game.Content.Load<SpriteFont>("ScoreFont");
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            base.LoadContent();
        }

        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            _spriteBatch.Begin();
            _spriteBatch.DrawString(_gameOverFont, "You Died!", new Vector2(Globals.ScreenWidth / 2f, Globals.ScreenHeight / 2f), Color.Red);
            _spriteBatch.End();
        }
    }
}
