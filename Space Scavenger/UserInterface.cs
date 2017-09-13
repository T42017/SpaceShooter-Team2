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
        
        private SpriteFont _scoreFont;
        private SpriteFont _healthFont;
        private SpriteBatch _spriteBatch;

        private Texture2D _healthBarLeft;
        private Texture2D _healthbarMiddle;
        private Texture2D _healthbarRight;
        private Texture2D _shieldBarLeft;
        private Texture2D _shieldBarMiddle;
        private Texture2D _shieldBarRight;
        private Texture2D _boosticon;
        private readonly SpaceScavenger _myGame;
        private Vector2 _position;

        public UserInterface(Game game) : base(game)
        {
            _position = new Vector2(Globals.ScreenWidth / 2f, Globals.ScreenHeight / 2f);
            _myGame = (SpaceScavenger) game;
            
            
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(Game.GraphicsDevice);
            
            
            _scoreFont = Game.Content.Load<SpriteFont>("ScoreFont");
            _healthFont = Game.Content.Load<SpriteFont>("HealthFont");
            _healthBarLeft = Game.Content.Load<Texture2D>("barHorizontal_red_left");
            _healthbarMiddle = Game.Content.Load<Texture2D>("barHorizontal_red_mid");
            _healthbarRight = Game.Content.Load<Texture2D>("barHorizontal_red_right");
            _shieldBarLeft = Game.Content.Load<Texture2D>("barHorizontal_blue_left");
            _shieldBarMiddle = Game.Content.Load<Texture2D>("barHorizontal_blue_mid");
            _shieldBarRight = Game.Content.Load<Texture2D>("barHorizontal_blue_right");
            _boosticon = Game.Content.Load<Texture2D>("powerupBlue_bolt");

            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            base.LoadContent();
        }

        public override void Draw(GameTime gameTime)
        {


            // TODO implement real health, shield and score value
            _spriteBatch.Begin();
            //Font
            #region DrawFonts


            _spriteBatch.DrawString(_scoreFont, "Health: ", new Vector2(_position.X - 940, _position.Y - 530),Color.White );
            _spriteBatch.DrawString(_scoreFont, "Shield: ", new Vector2(_position.X - 940, _position.Y - 490), Color.White);
            _spriteBatch.DrawString(_scoreFont, "score: " + _myGame.Exp.currentScore,new Vector2(_position.X + 620, _position.Y - 530), Color.White );
            _spriteBatch.DrawString(_healthFont, _myGame.Player.Health * 10 + "%", new Vector2(_position.X - 30, _position.Y - 50), Color.White );
            _spriteBatch.DrawString(_scoreFont, "Boost: ", new Vector2(_position.X - 940, _position.Y - 450), Color.White );
            #endregion
            
            
            // Healthbar
            #region DrawHealthBars



            if (_myGame.Player.Health >= 1)
            {
                _spriteBatch.Draw(_healthBarLeft, new Vector2(_position.X - 800, _position.Y - 530), Color.White);

                if (_myGame.Player.Health >= 2)
                {
                    _spriteBatch.Draw(_healthbarMiddle, new Vector2(_position.X - 795, _position.Y - 530), Color.White);
                }

                if (_myGame.Player.Health >= 3)
                {
                    _spriteBatch.Draw(_healthbarMiddle, new Vector2(_position.X - 795 + _healthbarMiddle.Width, _position.Y - 530), Color.White);
                }

               if (_myGame.Player.Health >= 4)
               {
                   _spriteBatch.Draw(_healthbarMiddle, new Vector2(_position.X - 795 + _healthbarMiddle.Width*2, _position.Y - 530), Color.White);
               }

               if (_myGame.Player.Health >= 5)
               {
                   _spriteBatch.Draw(_healthbarMiddle, new Vector2(_position.X - 795 + _healthbarMiddle.Width*3, _position.Y - 530), Color.White);
               }

               if (_myGame.Player.Health >= 6)
               {
                    _spriteBatch.Draw(_healthbarMiddle, new Vector2(_position.X - 795 + _healthbarMiddle.Width*4, _position.Y - 530), Color.White);
               }

               if (_myGame.Player.Health >= 7)
               {
                   _spriteBatch.Draw(_healthbarMiddle, new Vector2(_position.X - 795 + _healthbarMiddle.Width*5, _position.Y - 530), Color.White);
               }

               if (_myGame.Player.Health >= 8)
               {
                   _spriteBatch.Draw(_healthbarMiddle, new Vector2(_position.X - 795 + _healthbarMiddle.Width*6, _position.Y - 530), Color.White);
               }

               if (_myGame.Player.Health >= 9)
               {
                   _spriteBatch.Draw(_healthbarMiddle, new Vector2(_position.X - 795 + _healthbarMiddle.Width*7, _position.Y - 530), Color.White);
               }

               if (_myGame.Player.Health >= 10)
               {
                    _spriteBatch.Draw(_healthbarRight, new Vector2(_position.X - 795 + _healthbarMiddle.Width*8, _position.Y - 530), Color.White);
               }
            }


            #endregion


            // Shieldbar

            #region DrawShieldBar

           
          
            if (_myGame.Player.Shield >= 1)
            {
             _spriteBatch.Draw(_shieldBarLeft, new Vector2(_position.X - 800, _position.Y - 490), Color.White);
         
             if (_myGame.Player.Shield >= 2)
             {
                 _spriteBatch.Draw(_shieldBarMiddle, new Vector2(_position.X - 795, _position.Y - 490), Color.White);
             }
         
             if (_myGame.Player.Shield >= 3)
             {
                 _spriteBatch.Draw(_shieldBarMiddle, new Vector2(_position.X - 795 + _shieldBarMiddle.Width, _position.Y - 490), Color.White);
             }
         
             if (_myGame.Player.Shield >= 4)
             {
                 _spriteBatch.Draw(_shieldBarMiddle, new Vector2(_position.X - 795 + _shieldBarMiddle.Width * 2, _position.Y - 490), Color.White);
             }
         
             if (_myGame.Player.Shield >= 5)
             {
                 _spriteBatch.Draw(_shieldBarMiddle, new Vector2(_position.X - 795 + _shieldBarMiddle.Width * 3, _position.Y - 490), Color.White);
             }
         
             if (_myGame.Player.Shield >= 6)
             {
                 _spriteBatch.Draw(_shieldBarMiddle, new Vector2(_position.X - 795 + _shieldBarMiddle.Width * 4, _position.Y - 490), Color.White);
             }
         
             if (_myGame.Player.Shield >= 7)
             {
                 _spriteBatch.Draw(_shieldBarMiddle, new Vector2(_position.X - 795 + _shieldBarMiddle.Width * 5, _position.Y - 490), Color.White);
             }
         
             if (_myGame.Player.Shield >= 8)
             {
                 _spriteBatch.Draw(_shieldBarMiddle, new Vector2(_position.X - 795 + _shieldBarMiddle.Width * 6, _position.Y - 490), Color.White);
             }
         
             if (_myGame.Player.Shield >= 9)
             {
                 _spriteBatch.Draw(_shieldBarMiddle, new Vector2(_position.X - 795 + _shieldBarMiddle.Width * 7, _position.Y - 490), Color.White);
             }
         
             if (_myGame.Player.Shield >= 10)
             {
                 _spriteBatch.Draw(_shieldBarRight, new Vector2(_position.X - 795 + _shieldBarMiddle.Width * 8, _position.Y - 490), Color.White);
             }
         }


            #endregion

            // Boost

            #region Boost


            if (_myGame.boost.BoostTime <= 0)
            {
                _spriteBatch.Draw(_boosticon, new Vector2(_position.X - 800, _position.Y - 455), Color.White);
            }
           
            
            #endregion





            _spriteBatch.End();
        }

        
    }
}
