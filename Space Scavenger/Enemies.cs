using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Space_Scavenger
{
    class Enemies : DrawableGameComponent, IGameObject

    {
        private SpaceScavenger MyGame;

        
        private Texture2D enemyTexture;

        public Enemies(Game game) : base(game)
        {
            Position = new Vector2(Globals.ScreenWidth, Globals.ScreenHeight / 2);
            Health = 10;
            MyGame = (SpaceScavenger) game;
            
        }

        protected override void LoadContent()
        {
            enemyTexture = Game.Content.Load<Texture2D>("EnemyShip");
            base.LoadContent();

        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(enemyTexture, Position, null, Color.White, Rotation + MathHelper.PiOver2, new Vector2(enemyTexture.Width / 2, enemyTexture.Height / 2), 1.0f, SpriteEffects.None, 0f);

        }

        public override void Update(GameTime gameTime)
        {
            var followDistance = 1000;
            Vector2 direction = MyGame.Player.Position - Position;
            direction.Normalize();
            Speed += direction * 0.08f;

            if (Speed.LengthSquared() > 25)
                Speed = Vector2.Normalize(Speed) * 5;

            var xDiff = Math.Abs(Position.X - MyGame.Player.Position.X);
            var yDiff = Math.Abs(Position.Y - MyGame.Player.Position.Y);

            if (xDiff < followDistance &&  yDiff < followDistance)
                if (xDiff > 400 || yDiff > 400)
                    Position += Speed;
                else
                    Speed -= Speed;


            //if (xDiff > followDistance && Position.Y - MyGame.Player.Position.Y > followDistance)
            //        if (Position.X - MyGame.Player.Position.X < 200 || Position.Y - MyGame.Player.Position.Y < 200)
            //            Position += Speed;

            float targetrotation = (float)Math.Atan2(Position.X - MyGame.Player.Position.X, Position.Y - MyGame.Player.Position.Y);
            
            if (targetrotation < 360)
            {
                Rotation += 360; 
            }
            else if (targetrotation > 360)
            {
                Rotation -= 360;
            }

            Rotation = -targetrotation;
            base.Update(gameTime);
            
        }

        public float targetrotation { get; set; }
        public Vector2 playerPosition { get; set; }
        public bool isDead { get; set; }
        public Vector2 Position { get; set; }
        public float Radius { get; set; }
        public Vector2 Speed { get; set; }
        public float Rotation { get; set; }
        public int Health { get; set; }
    }
}
