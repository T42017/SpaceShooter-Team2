using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
// ReSharper disable PossibleLossOfFraction

namespace Space_Scavenger
{
    public class Player : DrawableGameComponent, IGameObject
    {
        private Texture2D _playerTexture;
        public int LastShot = 1;

        public Player(Game game) : base(game)
        {
            Position = new Vector2(0, 0);
            MaxHealth = 5;
            MaxShield = 5;
            Radius = 12;
            Health = MaxHealth;
            Shield = MaxShield;
        }

        public bool Accelerating { get; set; }
        public int Shield { get; set; }
        public int MaxHealth { get; set; }
        public int MaxShield { get; set; }
        public bool IsDead { get; set; }
        public Vector2 Position { get; set; }
        public float Radius { get; set; }
        public Vector2 Speed { get; set; }
        public float Rotation { get; set; }
        public int Health { get; set; }

        protected override void LoadContent()
        {
            _playerTexture = Game.Content.Load<Texture2D>("playerShipNeon");
            base.LoadContent();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_playerTexture, Position, null, Color.White, Rotation + MathHelper.PiOver2,
                new Vector2(_playerTexture.Width / 2, _playerTexture.Height / 2), 0.5f, SpriteEffects.None, 0f);
        }


        public void Accelerate()
        {
            Speed += new Vector2((float) Math.Cos(Rotation), (float) Math.Sin(Rotation)) * 0.30f;
            Accelerating = true;
        }

        public Shot Shoot()
        {
            return new Shot
            {
                Position = Position,
                Rotation = Rotation,
                Timer = 200,
                Speed = 20f * new Vector2((float) Math.Cos(Rotation), (float) Math.Sin(Rotation))
            };
        }

        public Shot MultiShot()
        {
            float radius = 10;

            if (LastShot == 1)
            {
                LastShot = 0;
                return new Shot
                {
                    Position = Position + (Vector2.One * radius).Rotate(-MathHelper.PiOver2 + Rotation),
                    Rotation = Rotation,
                    Timer = 200,
                    Speed = 20f * new Vector2((float) Math.Cos(Rotation), (float) Math.Sin(Rotation))
                };
            }
            LastShot = 1;
            return new Shot
            {
                Position = Position + (Vector2.One * radius).Rotate(MathHelper.PiOver2 + Rotation),
                Rotation = Rotation,
                Timer = 200,
                Speed = 20f * new Vector2((float) Math.Cos(Rotation), (float) Math.Sin(Rotation))
            };
        }
    }
}