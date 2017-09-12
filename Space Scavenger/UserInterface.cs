using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Space_Scavenger
{
    public class UserInterface : DrawableGameComponent
    {
        
        private SpriteFont ScoreFont;
        private SpriteFont HealthFont;
        public SpriteBatch spriteBatch;
        private Vector2 Position;

        public UserInterface(Game game) : base(game)
        {
            Position = new Vector2(Globals.ScreenWidth / 2, Globals.ScreenHeight / 2);
        }

        protected override void LoadContent()
        {
            ScoreFont = Game.Content.Load<SpriteFont>("ScoreFont");
            HealthFont = Game.Content.Load<SpriteFont>("HealthFont");
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            base.LoadContent();
        }

        public override void Draw(GameTime gameTime)
        {
           
            spriteBatch.DrawString(ScoreFont, "Score: " + 500, new Vector2(Position.X,Position.Y),Color.White );
            spriteBatch.DrawString(HealthFont, 100 + "%", new Vector2(Position.X, Position.Y), Color.White );
            
        }

        
    }
}
