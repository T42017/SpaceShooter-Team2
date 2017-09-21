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
            _rectangleItemFour = new Rectangle(1120, 210+110, _myGame._shop._smallPanel.Width, _myGame._shop._smallPanel.Height);
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
                #region shopItems 1-3 MaxHealth++
                
                if (_myGame._shop._rectangleHover.Intersects(_rectangleItemOne))
                {
                    if (_myGame.Player.MaxHealth == 10)
                    {
                        ItemCost = 100;
                        ItemDescriptionString = "Increase Maxhealth" + "\r\n" + "to 150%";
                        if (_state.IsKeyDown(Keys.Space))
                        {
                            if (_myGame.Exp.CurrentExp >= ItemCost)
                            {
                                _myGame.Player.MaxHealth = 15;
                                _myGame.Player.Health = _myGame.Player.MaxHealth;
                                _myGame.Exp.CurrentExp -= 100;
                                
                                
                                
                            }
                        }
                    }
                    else
                    {
                        ItemCost = 100;
                        ItemDescriptionString = "You've already bought this item";
                    }
                }


                if (_myGame._shop._rectangleHover.Intersects(_rectangleItemTwo))
                {
                    if (_myGame.Player.MaxHealth == 15 || _myGame.Player.MaxHealth == 10)
                    {
                        ItemCost = 200;
                        ItemDescriptionString = "Increase Maxhealth" + "\r\n" + "to 200%";
                        if (_state.IsKeyDown(Keys.Space))
                        {
                            if (_myGame.Exp.CurrentExp >= ItemCost)
                            {
                                _myGame.Player.MaxHealth = 20;
                                _myGame.Player.Health = _myGame.Player.MaxHealth;
                                _myGame.Exp.CurrentExp -= 200;



                            }
                        }
                    }
                    else
                    {
                        ItemCost = 200;
                        ItemDescriptionString = "You've already bought this item";
                    }
                }

                if (_myGame._shop._rectangleHover.Intersects(_rectangleItemThree))
                {
                    if (_myGame.Player.MaxHealth == 15 || _myGame.Player.MaxHealth == 10 || _myGame.Player.MaxHealth == 20)
                    {
                        ItemCost = 300;
                        ItemDescriptionString = "Increase Maxhealth" + "\r\n" + "to 250%";
                        if (_state.IsKeyDown(Keys.Space))
                        {
                            if (_myGame.Exp.CurrentExp >= ItemCost)
                            {
                                _myGame.Player.MaxHealth = 25;
                                _myGame.Player.Health = _myGame.Player.MaxHealth;
                                _myGame.Exp.CurrentExp -= 300;
                            }
                        }
                    }
                    else
                    {
                        ItemCost = 300;
                        ItemDescriptionString = "You've already bought this item";
                    }
                }
                #endregion

                #region shopItems BetterWeapons

                if (_myGame._shop._rectangleHover.Intersects(_rectangleItemFour))
                {
                    ItemDescriptionString = "rectangle4";
                    //if (_myGame.Player.MaxHealth == 10)
                    //{
                    //    ItemCost = 100;
                    //    ItemDescriptionString = "Increase Maxhealth" + "\r\n" + "to 150%";
                    //    if (_state.IsKeyDown(Keys.Space))
                    //    {
                    //        if (_myGame.Exp.CurrentExp >= ItemCost)
                    //        {
                    //            _myGame.Player.MaxHealth = 15;
                    //            _myGame.Player.Health = _myGame.Player.MaxHealth;
                    //            _myGame.Exp.CurrentExp -= 100;
                    //        }
                    //    }
                    //}
                    //else
                    //{
                    //    ItemCost = 100;
                    //    ItemDescriptionString = "You've already bought this item";
                    //}
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
