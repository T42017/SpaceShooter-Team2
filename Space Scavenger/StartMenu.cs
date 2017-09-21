using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Space_Scavenger
{
    public class StartMenu : DrawableGameComponent
    {
        private SpriteBatch _spriteBatch;
        private readonly SpaceScavenger _myGame;
        private SpriteFont _menufont;


        public StartMenu(Game game) : base(game)
        {
            _myGame = (SpaceScavenger) Game;
            
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(Game.GraphicsDevice);
            _menufont = Game.Content.Load<SpriteFont>("ScoreFont");
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            base.LoadContent();
        }

        public override void Draw(GameTime gameTime)
        {
            Vector2 textSize = _menufont.MeasureString("Pres Enter to start!");
            Vector2 TextMiddlePoint = new Vector2(textSize.X / 2, textSize.Y / 2);
            Vector2 textPosition = new Vector2((int)TextMiddlePoint.X - textSize.X, (int)TextMiddlePoint.Y - textSize.Y);
            _spriteBatch.Begin();
            _spriteBatch.DrawString(_menufont, "Press Enter to start!", new Vector2(textPosition.X + (Globals.ScreenWidth / 2f), textPosition.Y +(Globals.ScreenHeight / 2f)), Color.Red);
            _spriteBatch.End();
        }

    }
}
