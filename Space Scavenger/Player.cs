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
        public bool IsDead { get; set; }
        public Vector2 Position { get; set; }
        public float Radius { get; set; }
        public Vector2 Speed { get; set; }
        public float Rotation { get; set; }
        public bool Accelerating { get; set; }
        public int Health { get; set; }
        public int Shield { get; set; }
        public int MaxHealth { get; set; }
        public int MaxShield { get; set; }
        public int lastShot = 1;


        private Texture2D playerTexture;
        private Texture2D healthTexture; 

        public Player(Game game) : base(game)
        {
            Position = new Vector2(0,0);
            Health = 5;
            Shield = 5;
            Radius = 12;
            MaxHealth = 10;
            MaxShield = Shield;

        }

        protected override void LoadContent()
        {
            playerTexture = Game.Content.Load<Texture2D>("playerShipNeon");
            healthTexture = Game.Content.Load<Texture2D>("powerupRed");
            base.LoadContent();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(playerTexture, Position, null, Color.White, Rotation + MathHelper.PiOver2, new Vector2(playerTexture.Width / 2, playerTexture.Height / 2), 0.5f, SpriteEffects.None, 0f);

        }

        public override void Update(GameTime gameTime)
        {
            Position += Speed;
            if (Speed.LengthSquared() > 30)
                Speed = Speed * 0.98f;

            if (Speed.LengthSquared() <= 30 && !Accelerating)
            {
                Speed = Speed * 0.99f;
            }
            Accelerating = false;
            base.Update(gameTime);
        }

        

        public void Accelerate()
        {
            Speed += new Vector2((float)Math.Cos(Rotation), (float)Math.Sin(Rotation)) * 0.15f;
            Accelerating = true;
        }

        public Shot Shoot()
        {

            return new Shot()
            {
                Position = Position,
                Rotation = Rotation,
                Timer = 200,
                Speed = 15f * new Vector2((float) Math.Cos(Rotation), (float) Math.Sin(Rotation))
            };

        }
        public Shot multiShot()
        {
            float radius = 10;

            if (lastShot == 1)
            {
                lastShot = 0;
                return new Shot()
                {
                    Position = Position + (Vector2.One*radius).Rotate(-MathHelper.PiOver2 + Rotation),
                    Rotation = Rotation,
                    Timer = 200,
                    Speed = 15f * new Vector2((float) Math.Cos(Rotation), (float) Math.Sin(Rotation))
                };
            }
            else
            {
                lastShot = 1;
                return new Shot()
                {
                    Position = Position + (Vector2.One * radius).Rotate(MathHelper.PiOver2 + Rotation),
                    Rotation = Rotation,
                    Timer = 200,
                    Speed = 15f * new Vector2((float)Math.Cos(Rotation), (float)Math.Sin(Rotation))
                };
            }
            
        }
    }
}
