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
        private int _rectangleWidth;
        private int _rectangleHeight;
        private int _rectangleStartX;
        private int _rectangleStartY;
        private int X;
        private int Y;


        public ShopItem(Game game) : base(game)
        {
            _myGame = (SpaceScavenger)Game;
            

        }
        

        protected override void LoadContent()
        {
            _rectangleWidth = _myGame._shop._smallPanel.Width;
            _rectangleHeight = _myGame._shop._smallPanel.Height;
            _rectangleStartX = 1120;
            _rectangleStartY = 210;
            _spriteBatch = new SpriteBatch(Game.GraphicsDevice);
            _itemPlusMaxHealth = Game.Content.Load<Texture2D>("shield_bronze");

            _myGame._shop._rectangleHover = new Rectangle(_rectangleStartX, 205, _myGame._shop._hoverTexture.Width, _myGame._shop._hoverTexture.Height);
            _rectangleItemOne = new Rectangle(_rectangleStartX, _rectangleStartY, _rectangleWidth, _rectangleHeight);
            _rectangleItemTwo = new Rectangle(_rectangleStartX + 110, _rectangleStartY, _rectangleWidth, _rectangleHeight);
            _rectangleItemThree = new Rectangle(_rectangleStartX + 110*2, _rectangleStartY, _rectangleWidth, _rectangleHeight);
            _rectangleItemFour = new Rectangle(_rectangleStartX, _rectangleStartY + 110, _rectangleWidth, _rectangleHeight);
            _rectangleItemFive = new Rectangle(_rectangleStartX + 110, _rectangleStartY + 110, _rectangleWidth, _rectangleHeight);
            _rectangleItemSix = new Rectangle(_rectangleStartX + 110*2, _rectangleStartY + 110, _rectangleWidth, _rectangleHeight);
            _rectangleItemSeven = new Rectangle(_rectangleStartX, _rectangleStartY + 110*2, _rectangleWidth, _rectangleHeight);
            _rectangleItemEight = new Rectangle(_rectangleStartX + 110, _rectangleStartY + 110*2, _rectangleWidth, _rectangleHeight);
            _rectangleItemNine = new Rectangle(_rectangleStartX + 110*2, _rectangleStartY + 110*2, _rectangleWidth, _rectangleHeight);

            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            _state = Keyboard.GetState();

            if (_myGame.gamestate == GameState.Shopping)
            {
                #region 1-3 MaxHealth++

                if (_myGame._shop._rectangleHover.Intersects(_rectangleItemOne))
                {
                    if (_myGame.Player.MaxHealth == 5)
                    {
                        ItemCost = 300;
                        ItemDescriptionString = "Increased Maxhealth" + "\r\n" + "(100%)";
                        if (_state.IsKeyDown(Keys.Space))
                        {
                            if (_myGame.Exp.CurrentExp >= ItemCost)
                            {
                                _myGame.Player.MaxHealth = 10;
                                _myGame.Player.Health = _myGame.Player.MaxHealth;
                                _myGame.Exp.CurrentExp -= 300;

                            }
                        }
                    }
                    else
                    {
                        ItemCost = 0;
                        ItemDescriptionString = "You've already bought this item";
                    }
                }

                if (_myGame._shop._rectangleHover.Intersects(_rectangleItemTwo))
                {
                    if (_myGame.Player.MaxHealth == 10)
                    {
                        ItemCost = 600;
                        ItemDescriptionString = "Increased Maxhealth" + "\r\n" + "(150%)";
                        if (_state.IsKeyDown(Keys.Space))
                        {
                            if (_myGame.Exp.CurrentExp >= ItemCost)
                            {
                                _myGame.Player.MaxHealth = 15;
                                _myGame.Player.Health = _myGame.Player.MaxHealth;
                                _myGame.Exp.CurrentExp -= 600;
                            }
                        }
                    }
                    else if (_myGame.Player.MaxHealth < 10)
                    {
                        ItemCost = 600;
                        ItemDescriptionString = "Locked!";
                    }
                    else
                    {
                        ItemCost = 0;
                        ItemDescriptionString = "You've already bought this item";
                    }
                }

                if (_myGame._shop._rectangleHover.Intersects(_rectangleItemThree))
                {
                    if (_myGame.Player.MaxHealth == 15)
                    {
                        ItemCost = 1000;
                        ItemDescriptionString = "Increased MaxHealth" + "\r\n" + "(200%)";
                        if (_state.IsKeyDown(Keys.Space))
                        {
                            if (_myGame.Exp.CurrentExp >= ItemCost)
                            {
                                _myGame.Player.MaxHealth = 20;
                                _myGame.Player.Health = _myGame.Player.MaxHealth;
                                _myGame.Exp.CurrentExp -= 1000;
                            }
                        }
                    }
                    else if (_myGame.Player.MaxHealth < 15)
                    {
                        ItemCost = 1000;
                        ItemDescriptionString = "Locked!";
                    }
                    else
                    {
                        ItemCost = 0;
                        ItemDescriptionString = "You've already bought this item";
                    }
                }

                #endregion

                #region  3-6 Shield++

                if (_myGame._shop._rectangleHover.Intersects(_rectangleItemFour))
                {
                    if (_myGame.Player.MaxShield == 5)
                    {
                        ItemCost = 300;
                        ItemDescriptionString = "Increased MaxShield" + "\r\n" + "(100%)";
                        if (_state.IsKeyDown(Keys.Space))
                        {
                            if (_myGame.Exp.CurrentExp >= ItemCost)
                            {
                                _myGame.Player.MaxShield = 10;
                                _myGame.Player.Shield = _myGame.Player.MaxShield;
                                _myGame.Exp.CurrentExp -= 300;
                            }
                        }
                    }
                    else
                    {
                        ItemCost = 0;
                        ItemDescriptionString = "You've already " + "\r\n" + "bought this item";
                    }
                }
                else if (_myGame._shop._rectangleHover.Intersects(_rectangleItemFive))
                {
                    if (_myGame.Player.MaxShield == 10)
                    {
                        ItemCost = 600;
                        ItemDescriptionString = "Increased MaxShield" + "\r\n" + "(150%)";
                        if (_state.IsKeyDown(Keys.Space))
                        {
                            if (_myGame.Exp.CurrentExp >= ItemCost)
                            {
                                _myGame.Player.MaxShield = 15;
                                _myGame.Player.Shield = _myGame.Player.MaxShield;
                                _myGame.Exp.CurrentExp -= 600;
                            }
                        }
                    }
                    else if (_myGame.Player.Shield < 10)
                    {
                        ItemCost = 600;
                        ItemDescriptionString = "Locked!";
                    }
                    else
                    {
                        ItemCost = 0;
                        ItemDescriptionString = "You've already " + "\r\n" + "bought this item";
                    }
                }

                else if (_myGame._shop._rectangleHover.Intersects(_rectangleItemSix))
                {
                    if (_myGame.Player.MaxShield == 15)
                    {
                        ItemCost = 1000;
                        ItemDescriptionString = "Increased MaxShield" + "\r\n" + "(200%)";
                        if (_state.IsKeyDown(Keys.Space))
                        {
                            if (_myGame.Exp.CurrentExp >= ItemCost)
                            {
                                _myGame.Player.MaxShield = 20;
                                _myGame.Player.Shield = _myGame.Player.MaxShield;
                                _myGame.Exp.CurrentExp -= 1000;
                            }
                        }
                    }
                    else if (_myGame.Player.MaxShield < 15)
                    {
                        ItemCost = 1000;
                        ItemDescriptionString = "Locked!";
                    }
                    else
                    {
                        ItemCost = 0;
                        ItemDescriptionString = "You've already " + "\r\n" + "bought this item";
                    }
                }


                #endregion

                #region 6-9 Weapons++
                if (_myGame._shop._rectangleHover.Intersects(_rectangleItemSeven))
                {
                    if (!_myGame.fasterLaser)
                    {
                        ItemDescriptionString = "Increased Laserspeed.";
                        if (_state.IsKeyDown(Keys.Space))
                        {
                            ItemCost = 300;
                            _myGame.fasterLaser = true;
                            _myGame.Exp.CurrentExp -= 300;
                        }
                    }
                    else
                    {
                        ItemCost = 300;
                        ItemDescriptionString = "You've already " + "\r\n" + "bought this item";
                    }
                }


                else if (_myGame._shop._rectangleHover.Intersects(_rectangleItemEight))
                 {
                    if (_myGame.fasterLaser && !_myGame.multiShot)
                    {
                        ItemDescriptionString = "DoubleShot.";
                        if (_state.IsKeyDown(Keys.Space))
                        {
                            ItemCost = 600;
                            _myGame.multiShot = true;
                            _myGame.fasterLaser = false;
                            _myGame.Exp.CurrentExp -= 600;
                        }
                    }
                    else if (!_myGame.fasterLaser && _myGame.multiShot)
                    {
                        ItemCost = 0;
                        ItemDescriptionString = "You've already " + "\r\n" + "bought this item";
                    }
                    else
                    {
                        ItemCost = 600;
                        ItemDescriptionString = "Locked!";
                    }

                }

                else if (_myGame._shop._rectangleHover.Intersects(_rectangleItemNine))
                {
                    if (_myGame.multiShot && !_myGame.fasterLaser)
                    {
                        ItemCost = 1000;
                        ItemDescriptionString = "Doubleshot + Increased Laserspeed.";
                        if (_state.IsKeyDown(Keys.Space))
                        {
                            ItemCost = 1000;
                            _myGame.fasterLaser = true;
                            _myGame.Exp.CurrentExp -= 1000;
                        }
                    }
                    else if(_myGame.fasterLaser && _myGame.multiShot)
                    {
                        ItemCost = 0;
                        ItemDescriptionString = ItemDescriptionString = "You've already " + "\r\n" + "bought this item";
                    }
                    else
                    {
                        ItemCost = 1000;
                        ItemDescriptionString = "Locked!";
                    }
                }
                #endregion
            }
            base.LoadContent();
        }

        public override void Draw(GameTime gameTime)
        {
            X = _rectangleItemOne.X + _myGame._shop._smallPanel.Width/ 2 + 10;
            Y = _rectangleItemOne.Y + _myGame._shop._smallPanel.Height / 2;
            _spriteBatch.Begin();
            //_spriteBatch.Draw(_itemPlusMaxHealth, new Vector2(X,Y), null, Color.White, 0f, Vector2.Zero, new Vector2(1, 1), SpriteEffects.None, 0f);
            //_spriteBatch.Draw(_itemPlusMaxHealth,new Vector2(X , Y), Color.White);
            _spriteBatch.End();
        }
    }
}
