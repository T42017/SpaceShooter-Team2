using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Space_Scavenger
{
    public class Enemy :  GameObject

    {
        private SpaceScavenger MyGame;

        int reloadTime = 0;
        Random rnd = new Random();
        private Texture2D enemyTexture;


        public Enemy enemySpawn()
        {
            return new Enemy()
            {
                Position = new Vector2(Globals.ScreenWidth, Globals.ScreenHeight / 2),
                Rotation = Rotation,
                Health = 10,
                Radius = 20
            };
        }

        public Shot EnemyShoot()
        {
            if (reloadTime <= 0)
                return new Shot()
                {
                    Position = Position - new Vector2(rnd.Next(-10,10),rnd.Next(-10,10)),
                    Rotation = Rotation,
                    Speed = Speed + 10f * new Vector2((float) Math.Cos(Rotation - MathHelper.PiOver2), (float) Math.Sin(Rotation - MathHelper.PiOver2))
                };

                return null;

        }

        public void Update(GameTime gameTime, Game game)
        {
            MyGame = (SpaceScavenger)game;
            var followDistance = 1000;
            var aimDistance = 100;
            int i = 0;
            float xDiff = 0;
            float yDiff = 0;
            
            Vector2 left = new Vector2(-1,0);
            Vector2 direction = MyGame.Player.Position - Position;
            direction.Normalize();
            Speed += direction * 0.08f;

            if (Speed.LengthSquared() > 25)
                Speed = Vector2.Normalize(Speed) * 5;

            var xDiffPlayer = Math.Abs(Position.X - MyGame.Player.Position.X);
            var yDiffPlayer = Math.Abs(Position.Y - MyGame.Player.Position.Y);

            if (xDiffPlayer < followDistance &&  yDiffPlayer < followDistance)
            {
                
                if (xDiffPlayer > aimDistance || yDiffPlayer > aimDistance)    
                {
                    Position += Speed;
                    if(reloadTime <= 0)
                    {
                        Shot s = EnemyShoot();
                        if (s != null)
                            MyGame.enemyshots.Add(s);
                        reloadTime += 20;
                    }
                    
                }
                else
                    Speed -= Speed;
                if (reloadTime <= 0)
                {
                    Shot s = EnemyShoot();
                    if (s != null)
                        MyGame.enemyshots.Add(s);
                    reloadTime += 20;
                }
            }

            


            /*foreach (Shot shot in MyGame.shots)
            {
                i++;
            }

            for (int j = 0; j < i; j++)
            {
                xDiff = Math.Abs(Position.X - MyGame.shots[j].Position.X);
                yDiff = Math.Abs(Position.Y - MyGame.shots[j].Position.Y);

                if (xDiff < 100 && yDiff < 100)
                {
                    Position += 
                }

            }*/





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

            if (reloadTime > 0)
            {
                reloadTime--;
            }
            
            
        }
    }
}
