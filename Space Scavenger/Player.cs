using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Space_Scavenger
{
    class Player : DrawableGameComponent, IGameObject
    {
        public bool isDead { get; set; }
        public Vector2 Position { get; set; }
        public float Radius { get; set; }
        public Vector2 Speed { get; set; }
        public float Rotation { get; set; }
        public bool Accelerating { get; set; }

        public Texture2D playerTexture;

        public Player(Game game) : base(game)
        {
            Position = new Vector2(Globals.ScreenWidth / 2, Globals.ScreenHeight / 2);
        }

        protected override void LoadContent()
        {
            playerTexture = Game.Content.Load<Texture2D>("playerShip");

            base.LoadContent();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(playerTexture, Position, null, Color.White, Rotation + MathHelper.PiOver2, new Vector2(playerTexture.Width / 2, playerTexture.Height / 2), 1.0f, SpriteEffects.None, 0f);
            
        }

        public override void Update(GameTime gameTime)
        {
            Position += Speed;
            base.Update(gameTime);
        }

        

        public void Accelerate()
        {
            Speed += new Vector2((float)Math.Cos(Rotation), (float)Math.Sin(Rotation)) * 0.08f;

            if (Speed.LengthSquared() > 100)
                Speed = Vector2.Normalize(Speed) * 10;


        }
    }
}
