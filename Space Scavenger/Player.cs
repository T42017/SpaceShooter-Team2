using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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
        public int Shield { get; set; }
        public int MaxHealth { get; set; }

        private Texture2D playerTexture;
        private Texture2D healthTexture; 

        public Player(Game game) : base(game)
        {
            Position = new Vector2(Globals.ScreenWidth / 2, Globals.ScreenHeight / 2);
            Health = 15;
            Shield = 10;
            Radius = 12;
            MaxHealth = Health;

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


            /*for (int i = 0; i < Health; i++)
            {
                spriteBatch.Draw(healthTexture, new Vector2(5 + i*35, 10), Color.White);
                DrawOrder = 76;
            }*/
            
        }

        public override void Update(GameTime gameTime)
        {
            Position += Speed;
            if (Speed.LengthSquared() > 25)
                Speed = Speed * 0.99f;


            base.Update(gameTime);
        }

        

        public void Accelerate()
        {
            Speed += new Vector2((float)Math.Cos(Rotation), (float)Math.Sin(Rotation)) * 0.08f;

        }

        public Shot Shoot()
        {
            return new Shot()
            {
                Position = Position,
                Rotation = Rotation,
                Timer = 200,
                Speed = 15f * new Vector2((float)Math.Cos(Rotation), (float)Math.Sin(Rotation))
            };

            
        }
    }
}
