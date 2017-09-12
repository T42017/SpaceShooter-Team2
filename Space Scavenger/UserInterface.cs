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
        private Texture2D HealthBarLeft;
        private Texture2D HealthbarMiddle;
        private Texture2D HealthbarRight;
        private Texture2D ShieldBarLeft;
        private Texture2D ShieldBarMiddle;
        private Texture2D ShieldBarRight;
        SpriteBatch _spriteBatch;
        
        Vector2 Position;

        public UserInterface(Game game) : base(game)
        {
            Position = new Vector2(Globals.ScreenWidth / 2f, Globals.ScreenHeight / 2f);
            
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(Game.GraphicsDevice);
            ScoreFont = Game.Content.Load<SpriteFont>("ScoreFont");
            HealthFont = Game.Content.Load<SpriteFont>("HealthFont");
            HealthBarLeft = Game.Content.Load<Texture2D>("barHorizontal_red_left");
            HealthbarMiddle = Game.Content.Load<Texture2D>("barHorizontal_red_mid");
            HealthbarRight = Game.Content.Load<Texture2D>("barHorizontal_red_right");
            ShieldBarLeft = Game.Content.Load<Texture2D>("barHorizontal_blue_left");
            ShieldBarMiddle = Game.Content.Load<Texture2D>("barHorizontal_blue_mid");
            ShieldBarRight = Game.Content.Load<Texture2D>("barHorizontal_blue_right");
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            base.LoadContent();
        }

        public override void Draw(GameTime gameTime)
        {

            // TODO implement real health, shield and score value

            // Text
            _spriteBatch.Begin();
            _spriteBatch.DrawString(ScoreFont, "Health: ", new Vector2(Position.X - 940, Position.Y - 530),Color.White );
            _spriteBatch.DrawString(ScoreFont, "Shield: ", new Vector2(Position.X - 940, Position.Y - 490), Color.White);
            _spriteBatch.DrawString(ScoreFont, "Score: " +  500 /* Score */,new Vector2(Position.X + 620, Position.Y - 530), Color.White );
            _spriteBatch.DrawString(HealthFont, 100 /*Health*/ + "%", new Vector2(Position.X - 30, Position.Y - 50), Color.White );

            // Healthbar
            _spriteBatch.Draw(HealthBarLeft, new Vector2(Position.X - 800, Position.Y - 530),Color.White);

           for (int i = 0; i < 8; i++)
           {
               _spriteBatch.Draw(HealthbarMiddle, new Vector2(Position.X - 795 + i*HealthbarMiddle.Width, Position.Y - 530), Color.White);
           }

           _spriteBatch.Draw(HealthbarRight, new Vector2(Position.X - (795 - HealthbarMiddle.Width*8), Position.Y - 530), Color.White);

            // Shieldbar

           _spriteBatch.Draw(ShieldBarLeft, new Vector2(Position.X - 800, Position.Y - 490), Color.White);
            for (int i = 0; i < 8; i++)
            {
                _spriteBatch.Draw(ShieldBarMiddle, new Vector2(Position.X - 795 + i*ShieldBarMiddle.Width,Position.Y - 490), Color.White);
            }            
            _spriteBatch.Draw(ShieldBarRight, new Vector2(Position.X - (795 - ShieldBarMiddle.Width*8), Position.Y - 490), Color.White);

            _spriteBatch.End();
        }

        
    }
}
