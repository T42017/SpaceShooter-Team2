using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Space_Scavenger
{
    public class Money : GameObject
    {
        private readonly Random _rnd = new Random();
        public List<Asteroid> Moneyroids = new List<Asteroid>();


        public void Update(GameTime gametime, Game game)
        {
            MyGame = (SpaceScavenger) game;
            foreach (var m in Moneyroids)
            {
                var direction = MyGame.Player.Position - m.Position;
                direction.Normalize();
                m.Position += direction * 8f;
            }
        }

        public void MoneyRoid(Vector2 aspos)
        {
            Moneyroids.Add(new Asteroid
            {
                Timer = 1000,
                HpAsteroid = 1,
                Value = 50,
                AddCounter = _rnd.Next(-677, 677) / 10000f,
                Position = new Vector2(aspos.X + _rnd.Next(-20, 20), aspos.Y + _rnd.Next(-20, 20)),
                Speed = new Vector2((float) Math.Cos(_rnd.Next(-7, 7)), (float) Math.Sin(_rnd.Next(-7, 7))),
                Radius = 20
            });
        }
    }
}