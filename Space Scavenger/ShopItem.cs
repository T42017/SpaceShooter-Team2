using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Space_Scavenger
{
    public class ShopItem : DrawableGameComponent
    {
        private SpriteBatch _spriteBatch;
        private SpaceScavenger _myGame;
        public string ItemDescriptionString { get; private set; }
        private KeyboardState _state;
        public int ItemCost { get; private set; }
        private Texture2D _itemPlusMaxHealth;
        
        public ShopItem(Game game) : base(game)
        {
            _myGame = (SpaceScavenger)Game;
        }


        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(Game.GraphicsDevice);
            _itemPlusMaxHealth = Game.Content.Load<Texture2D>("shield_bronze");
            
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            _state = Keyboard.GetState();
           
            #region shopItem1 MaxHealth++

            if (_myGame.gamestate == GameState.Shopping)
            {
                if (_myGame._shop._rectangleHover.Intersects(_myGame._shop._rectangleItem1))
                {
                    ItemCost = 100;
                    ItemDescriptionString = "Increase Maxhealth" + "\r\n" + "to 150%";
                    if (_state.IsKeyDown(Keys.Space))
                    {
                        if (_myGame.Exp.currentEXP >= ItemCost)
                        {
                            _myGame.Player.MaxHealth = 15;
                            _myGame.Player.Health = _myGame.Player.MaxHealth;
                            _myGame.Exp.currentEXP -= 100;
                            _myGame._shop._rectangleItem1.Width = 0;
                            _myGame._shop._rectangleItem1.Height = 0;

                        }
                    }
                }
                else
                {
                    ItemDescriptionString = "";
                    ItemCost = 0;
                }
                if (_myGame._shop._rectangleItem1.Width == 0 && _myGame._shop._rectangleItem1.Height == 0)
                {
                    ItemDescriptionString = "You've already bought" + "\r\n" + " this item";
                }
            }

            #endregion
            base.LoadContent();
        }

        public override void Draw(GameTime gameTime)
        {
           
        }
    }
}
