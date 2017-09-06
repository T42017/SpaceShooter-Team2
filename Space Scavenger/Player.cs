using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Space_Scavenger
{
    public class Player : DrawableGameComponent, IGameObject
    {
        public bool isDead { get; set; }
        public Vector2 Position { get; set; }
        public float Radius { get; set; }
        public Vector2 Speed { get; set; }
        public float Rotation { get; set; }
        public bool Accelerating { get; set; }
        public int Health { get; set; }

        private Texture2D playerTexture;
        private Texture2D healthTexture; 

        public Player(Game game) : base(game)
        {
            Position = new Vector2(Globals.ScreenWidth / 2, Globals.ScreenHeight / 2);
            Health = 10;
        }

        protected override void LoadContent()
        {
            playerTexture = Game.Content.Load<Texture2D>("playerShip");
            healthTexture = Game.Content.Load<Texture2D>("powerupRed");
            base.LoadContent();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(playerTexture, Position, null, Color.White, Rotation + MathHelper.PiOver2, new Vector2(playerTexture.Width / 2, playerTexture.Height / 2), 0.5f, SpriteEffects.None, 0f);


            for (int i = 0; i < Health; i++)
            {
                spriteBatch.Draw(healthTexture, new Vector2(5 + i*35, 10), Color.White);
                DrawOrder = 76;
            }
            
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
