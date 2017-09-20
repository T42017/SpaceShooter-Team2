using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Space_Scavenger
{
    class Money : GameObject
    {
        Random rnd = new Random();
        public List<Asteroid> Moneyroids = new List<Asteroid>();
        

        public void Update(GameTime gametime, Game game)
        {
            MyGame = (SpaceScavenger)game;
            for (int i = 0; Moneyroids.Count > i; i++)
            {
                var direction = MyGame.Player.Position - Moneyroids[i].Position;
                direction.Normalize();
                Moneyroids[i].Position += direction * 8f;
            }
        }

        public void MoneyRoid(Vector2 aspos)
        {

            Moneyroids.Add(new Asteroid()
            {
                Timer = 1000,
                hpAsteroid = 1,
                value = 50,
                addCounter = rnd.Next(-677, 677) / 10000f,
                Position = new Vector2(aspos.X + rnd.Next(-20, 20), aspos.Y + rnd.Next(-20, 20)),
                Speed = new Vector2((float)Math.Cos(rnd.Next(-7, 7)), (float)Math.Sin(rnd.Next(-7, 7))),
                Radius = 20
            });

        }
    }
}
