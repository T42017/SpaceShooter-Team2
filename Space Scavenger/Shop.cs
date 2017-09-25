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
    public class Shop : DrawableGameComponent
    {
        private SpriteBatch _spriteBatch;
        private SpaceScavenger _myGame;
        private Texture2D _shopPanel;
        public Texture2D _smallPanel;
        private SpriteFont _shopHeadlineFont;
        private SpriteFont _shopMoneyFont;
        public Rectangle _rectangleHover;
        public Texture2D _hoverTexture;
        private KeyboardState _state, _prevKeyboardState;
        private SpriteFont _itemDescFont;
        private string CloseShopString;
       
        
        

        public Shop(Game game) : base(game)
        {
            _myGame = (SpaceScavenger) game;
            
           
            
        }
        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(Game.GraphicsDevice);
            _shopPanel = Game.Content.Load<Texture2D>("panel");
            _smallPanel = Game.Content.Load<Texture2D>("blue_button10");
            _shopHeadlineFont = Game.Content.Load<SpriteFont>("ShopHeadLine");
            _hoverTexture = Game.Content.Load<Texture2D>("glassPanel_projection");
            _shopMoneyFont = Game.Content.Load<SpriteFont>("ScoreFont");
            _itemDescFont = Game.Content.Load<SpriteFont>("ItemDescFont");

           
            
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            var x = _hoverTexture.Width + 10;

                _state = Keyboard.GetState();
                   if (_state.IsKeyDown(Keys.Right) && _prevKeyboardState.IsKeyUp(Keys.Right))
                   {

                        if(_rectangleHover.X < 1120 + 2*x)
                        _rectangleHover.X += x;

                   } 
                   else if (_state.IsKeyDown(Keys.Left) && _prevKeyboardState.IsKeyUp(Keys.Left))
                   {
                       if(_rectangleHover.X > 1120)
                       _rectangleHover.X -= x;
                   }

                   if (_state.IsKeyDown(Keys.Down) && _prevKeyboardState.IsKeyUp(Keys.Down))
                   {
                       if (_rectangleHover.Y < 205 + 2*x)
                           _rectangleHover.Y += x;
                   }
                   else if (_state.IsKeyDown(Keys.Up) && _prevKeyboardState.IsKeyUp(Keys.Up))
                   {
                       if (_rectangleHover.Y > 205)
                           _rectangleHover.Y -= x;
                   }
               
            _prevKeyboardState = Keyboard.GetState();

           
           
            base.LoadContent();
        }

        public override void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin();

            
            _spriteBatch.Draw(_shopPanel, new Vector2(1100, 150), null, Color.White, 0f, Vector2.Zero, new Vector2(0.6f, 0.6f), SpriteEffects.None, 0f);
            _spriteBatch.DrawString(_shopHeadlineFont, "SHOP", new Vector2(1130, 160), Color.White);
            _spriteBatch.DrawString(_shopMoneyFont, "$" + _myGame.Exp.CurrentExp, new Vector2(1300, 625), Color.Green);
            _spriteBatch.DrawString(_itemDescFont, "" + _myGame._shopItem.ItemDescriptionString, new Vector2(1130, 530), Color.Black);
            _spriteBatch.DrawString(_itemDescFont, "Cost: " + _myGame._shopItem.ItemCost + "$", new Vector2(1130, 630), Color.Black);

            

            for (int i = 0; i < 3; i++)
            {
                _spriteBatch.Draw(_smallPanel, new Vector2(1120 + (i*110), 210), null, Color.White, 0f, Vector2.Zero, new Vector2(2, 2), SpriteEffects.None, 0f);

                    for (int k = 0; k < 2; k++)
                    {
                        _spriteBatch.Draw(_smallPanel, new Vector2(1120 + (i*110), 320), null, Color.White, 0f, Vector2.Zero, new Vector2(2, 2), SpriteEffects.None, 0f);
                       for (int l = 0; l < 2; l++)
                       {
                          for (int m = 0; m < 1; m++)
                          {
                                 _spriteBatch.Draw(_smallPanel, new Vector2(1120 + (i*110), 430), null, Color.White, 0f, Vector2.Zero, new Vector2(2, 2), SpriteEffects.None, 0f);
                          }
                       }
                    }
            }
            
            _spriteBatch.Draw(_hoverTexture, _rectangleHover, Color.White);
            _spriteBatch.DrawString(_shopMoneyFont,"Press E to close the shop", new Vector2(Globals.ScreenWidth / 2f - 300, Globals.ScreenHeight / 2f - 300), Color.White);

            _spriteBatch.End();
        }
    }
}
