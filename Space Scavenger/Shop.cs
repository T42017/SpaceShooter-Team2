using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Space_Scavenger
{
    public class Shop : DrawableGameComponent
    {
        private readonly SpaceScavenger _myGame;
        private SpriteFont _itemDescFont, _shopHeadlineFont, _shopMoneyFont;
        private SpriteBatch _spriteBatch;
        private KeyboardState _state, _prevKeyboardState;
        public Texture2D HoverTexture, SmallPanel, ShopPanel;
        public Rectangle RectangleHover;


        public Shop(Game game) : base(game)
        {
            _myGame = (SpaceScavenger) game;
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(Game.GraphicsDevice);
            ShopPanel = Game.Content.Load<Texture2D>("panel");
            SmallPanel = Game.Content.Load<Texture2D>("blue_button10");
            _shopHeadlineFont = Game.Content.Load<SpriteFont>("ShopHeadLine");
            HoverTexture = Game.Content.Load<Texture2D>("glassPanel_projection");
            _shopMoneyFont = Game.Content.Load<SpriteFont>("ScoreFont");
            _itemDescFont = Game.Content.Load<SpriteFont>("ItemDescFont");


            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            var x = HoverTexture.Width + 10;

            _state = Keyboard.GetState();
            if (_state.IsKeyDown(Keys.Right) && _prevKeyboardState.IsKeyUp(Keys.Right))
            {
                if (RectangleHover.X < 1120 + 2 * x)
                    RectangleHover.X += x;
            }
            else if (_state.IsKeyDown(Keys.Left) && _prevKeyboardState.IsKeyUp(Keys.Left))
            {
                if (RectangleHover.X > 1120)
                    RectangleHover.X -= x;
            }

            if (_state.IsKeyDown(Keys.Down) && _prevKeyboardState.IsKeyUp(Keys.Down))
            {
                if (RectangleHover.Y < 205 + 2 * x)
                    RectangleHover.Y += x;
            }
            else if (_state.IsKeyDown(Keys.Up) && _prevKeyboardState.IsKeyUp(Keys.Up))
            {
                if (RectangleHover.Y > 205)
                    RectangleHover.Y -= x;
            }

            _prevKeyboardState = Keyboard.GetState();


            base.LoadContent();
        }

        public override void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin();


            _spriteBatch.Draw(ShopPanel, new Vector2(1100, 150), null, Color.White, 0f, Vector2.Zero,
                new Vector2(0.6f, 0.6f), SpriteEffects.None, 0f);
            _spriteBatch.DrawString(_shopHeadlineFont, "SHOP", new Vector2(1130, 160), Color.White);
            _spriteBatch.DrawString(_shopMoneyFont, "$" + _myGame.Exp.CurrentExp, new Vector2(1300, 625), Color.Green);
            _spriteBatch.DrawString(_itemDescFont, "" + _myGame.ShopItem.ItemDescriptionString, new Vector2(1130, 530),
                Color.Black);
            _spriteBatch.DrawString(_itemDescFont, "Cost: " + _myGame.ShopItem.ItemCost + "$", new Vector2(1130, 630),
                Color.Black);


            for (var i = 0; i < 3; i++)
            {
                _spriteBatch.Draw(SmallPanel, new Vector2(1120 + i * 110, 210), null, Color.White, 0f, Vector2.Zero,
                    new Vector2(2, 2), SpriteEffects.None, 0f);

                for (var k = 0; k < 2; k++)
                {
                    _spriteBatch.Draw(SmallPanel, new Vector2(1120 + i * 110, 320), null, Color.White, 0f, Vector2.Zero,
                        new Vector2(2, 2), SpriteEffects.None, 0f);
                    for (var l = 0; l < 2; l++)
                    for (var m = 0; m < 1; m++)
                        _spriteBatch.Draw(SmallPanel, new Vector2(1120 + i * 110, 430), null, Color.White, 0f,
                            Vector2.Zero, new Vector2(2, 2), SpriteEffects.None, 0f);
                }
            }

            _spriteBatch.Draw(HoverTexture, RectangleHover, Color.White);
            _spriteBatch.DrawString(_shopMoneyFont, "Press E to close the shop",
                new Vector2(Globals.ScreenWidth / 2f - 300, Globals.ScreenHeight / 2f - 300), Color.White);

            _spriteBatch.End();
        }
    }
}