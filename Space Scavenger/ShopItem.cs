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
        private Rectangle _rectangleItemOne;
        private Rectangle _rectangleItemTwo;
        private Rectangle _rectangleItemThree;
        private Rectangle _rectangleItemFour;
        private Rectangle _rectangleItemFive;
        private Rectangle _rectangleItemSix;
        private Rectangle _rectangleItemSeven;
        private Rectangle _rectangleItemEight;
        private Rectangle _rectangleItemNine;


        public ShopItem(Game game) : base(game)
        {
            _myGame = (SpaceScavenger)Game;
        }


        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(Game.GraphicsDevice);
            _itemPlusMaxHealth = Game.Content.Load<Texture2D>("shield_bronze");

            _myGame._shop._rectangleHover = new Rectangle(1120, 205, _myGame._shop._hoverTexture.Width, _myGame._shop._hoverTexture.Height);
            _rectangleItemOne = new Rectangle(1120, 210, _myGame._shop._smallPanel.Width, _myGame._shop._smallPanel.Height);
            _rectangleItemTwo = new Rectangle(1120 + 110, 210, _myGame._shop._smallPanel.Width, _myGame._shop._smallPanel.Height);
            _rectangleItemThree = new Rectangle(1120 + 110*2, 210, _myGame._shop._smallPanel.Width, _myGame._shop._smallPanel.Height);
            _rectangleItemFour = new Rectangle(1120, 210+110*2, _myGame._shop._smallPanel.Width, _myGame._shop._smallPanel.Height);
            _rectangleItemFive = new Rectangle(1120 + 110, 210+110*2, _myGame._shop._smallPanel.Width, _myGame._shop._smallPanel.Height);
            _rectangleItemSix = new Rectangle(1120 + 110*2, 210+110*2, _myGame._shop._smallPanel.Width, _myGame._shop._smallPanel.Height);
            _rectangleItemSeven = new Rectangle(1120, 210+110*3, _myGame._shop._smallPanel.Width, _myGame._shop._smallPanel.Height);
            _rectangleItemEight = new Rectangle(1120 + 110, 210+110*3, _myGame._shop._smallPanel.Width, _myGame._shop._smallPanel.Height);
            _rectangleItemNine = new Rectangle(1120 + 110*2, 210+110*3, _myGame._shop._smallPanel.Width, _myGame._shop._smallPanel.Height);

            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            _state = Keyboard.GetState();
           
            

            if (_myGame.gamestate == GameState.Shopping)
            {
                #region shopItem1 MaxHealth++
                if (_myGame._shop._rectangleHover.Intersects(_rectangleItemOne))
                {
                   
                    ItemCost = 100;
                    ItemDescriptionString = "Increase Maxhealth" + "\r\n" + "to 150%";
                    if (_state.IsKeyDown(Keys.Space))
                    {
                        if (_rectangleItemOne.Width > 0 && _rectangleItemOne.Height > 0)
                        {
                            if (_myGame.Exp.CurrentExp >= ItemCost)
                            {
                                _myGame.Player.MaxHealth = 15;
                                _myGame.Player.Health = _myGame.Player.MaxHealth;
                                _myGame.Exp.CurrentExp -= 100;
                                _rectangleItemOne.Width = 0;
                                _rectangleItemOne.Height = 0;
                                ItemDescriptionString = "You've already bought this item";
                            }
                        }
                        else if (_rectangleItemOne.Width <= 0 && _rectangleItemOne.Height <= 0)
                        {
                            ItemDescriptionString = "You've already bought this item";
                        }
                    } 
                }
                #endregion
            }




            base.LoadContent();
        }

        public override void Draw(GameTime gameTime)
        {
           
        }
    }
}
