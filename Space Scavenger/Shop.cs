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
        public Texture2D _panel;
        private SpriteFont _font;
        private Rectangle _rectangle;
        private Vector2 _position;
        private Texture2D _hoverTestTexture;
        private KeyboardState _state;
        private KeyboardState _prevoiusKbState;
        private GameState state;



        public Shop(Game game) : base(game)
        {
            _myGame = (SpaceScavenger) game;
            _position = new Vector2(1110,160);
            

        }
        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(Game.GraphicsDevice);
            _shopPanel = Game.Content.Load<Texture2D>("plate");
            _panel = Game.Content.Load<Texture2D>("metalPanel_blueCorner");
            _font = Game.Content.Load<SpriteFont>("ScoreFont");
            _hoverTestTexture = Game.Content.Load<Texture2D>("glassPanel_projection");
            _rectangle = new Rectangle(1110, 200, _hoverTestTexture.Width, _hoverTestTexture.Height);
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {

            var x = _hoverTestTexture.Width + 20;

               if ((int)gameTime.TotalGameTime.TotalMilliseconds % 20 == 0)
               {
                   _state = Keyboard.GetState();
                   if (_state.IsKeyDown(Keys.Right))
                   {

                        if(_rectangle.X < 1110 + 2*x)
                        _rectangle.X += x/2;
                        
 
                   } 
                   else if (_state.IsKeyDown(Keys.Left))
                   {
                       if(_rectangle.X > 1110)
                       _rectangle.X -= x/2;
                   }
                   //_prevoiusKbState = _state;
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
                _spriteBatch.Draw(_panel, new Vector2(1110 + (i*120), 200), null, Color.White, 0f, Vector2.Zero, new Vector2(1, 1), SpriteEffects.None, 0f);
            }
            _spriteBatch.Draw(_hoverTestTexture, _rectangle, Color.White);
            _spriteBatch.End();
        }
    }
}
