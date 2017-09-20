using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Space_Scavenger
{
    class Shop : DrawableGameComponent
    {
        private SpriteBatch _spriteBatch;
        private SpaceScavenger _myGame;
        private Texture2D _shopPanel;
        private Texture2D _smallPanel;
        private SpriteFont _font;
        public Rectangle _rectangleHover;
        public Rectangle _rectangleItem1;
        private Rectangle _rectangleItem2;
        private Rectangle _rectangleItem3;
        private Vector2 _position;
        private Texture2D _hoverTexture;
        private KeyboardState _state;
        
        


        public Shop(Game game) : base(game)
        {
            _myGame = (SpaceScavenger) game;
            _position = new Vector2(1110,160);
           
            
        }
        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(Game.GraphicsDevice);
            _shopPanel = Game.Content.Load<Texture2D>("panel");
            _smallPanel = Game.Content.Load<Texture2D>("metalPanel_blueCorner");
            _font = Game.Content.Load<SpriteFont>("ScoreFont");
            _hoverTexture = Game.Content.Load<Texture2D>("glassPanel_projection");
           
            _rectangleHover = new Rectangle(1120, 210, _hoverTexture.Width, _hoverTexture.Height);
            _rectangleItem1 = new Rectangle(1120, 210, _smallPanel.Width, _smallPanel.Height);
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            var x = _hoverTexture.Width + 10;

               if ((int)gameTime.TotalGameTime.TotalMilliseconds % 20 == 0)
               {
                   _state = Keyboard.GetState();
                   if (_state.IsKeyDown(Keys.Right))
                   {

                        if(_rectangleHover.X < 1120 + 2*x)
                        _rectangleHover.X += x/2;
                        
 
                   } 
                   else if (_state.IsKeyDown(Keys.Left))
                   {
                       if(_rectangleHover.X > 1120)
                       _rectangleHover.X -= x/2;
                   }
                  
               }
               
           
            base.LoadContent();
        }

        public override void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin();
            _spriteBatch.Draw(_shopPanel, new Vector2(1100,150),null,Color.White,0f, Vector2.Zero, new Vector2(0.6f, 0.6f), SpriteEffects.None, 0f);
            _spriteBatch.DrawString(_font,"SHOP", new Vector2(1110,160), Color.Red);
          for (int i = 0; i < 3; i++)
          {
              _spriteBatch.Draw(_smallPanel, new Vector2(1120 + (i*110), 210), null, Color.White, 0f, Vector2.Zero, new Vector2(1, 1), SpriteEffects.None, 0f);
              
          }
           // _spriteBatch.Draw(null, _rectangleItem1, Color.White);
            _spriteBatch.Draw(_hoverTexture, _rectangleHover, Color.White);
            _spriteBatch.End();
        }
    }
}
