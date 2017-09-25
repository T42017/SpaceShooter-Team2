using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Space_Scavenger
{
    public class Asteroid : GameObject
    {
        public float AddCounter;
        public int ChosenTexture;

        public int HpAsteroid;
        public float RotationCounter;
        public int Value;
    }

    internal class AsteroidComponent : GameObject
    {
        public List<Asteroid> MiniStroids = new List<Asteroid>();
        public List<Asteroid> NrofAsteroids = new List<Asteroid>();

        public Texture2D AsterTexture2D1, AsterTexture2D2, AsterTexture2D3, AsterTexture2D4;
        public int ATimer = 10;
        public Texture2D MinitETexture2D1;
        public SpaceScavenger Mygame;
        public GameObject MyObject;
        public Random Rand = new Random();
        public Random RandAsteroitField = new Random();
        public Random RandomTexture = new Random();
        public int WantedAsteroids = 100;

        public AsteroidComponent(Game game, GameObject gameObject)
        {
            Mygame = (SpaceScavenger) game;
            MyObject = gameObject;
            AsteroidSpawner();
        }

        public new bool IsDead { get; set; }
        public new float Rotation { get; set; }
        public new int Health { get; set; }

        public void Update(GameTime gameTime)
        {
            //     Debug.WriteLine(_nrofAsteroids[1].hpAsteroid);

            if (NrofAsteroids.Count < WantedAsteroids)
                AsteroidSpawner();
            foreach (var t in MiniStroids)
                t.Position += t.Speed;
            foreach (var asteroid in NrofAsteroids)
            {
                var xDiffPlayer = Math.Abs(asteroid.Position.X - Mygame.Player.Position.X);
                var yDiffPlayer = Math.Abs(asteroid.Position.Y - Mygame.Player.Position.Y);
                asteroid.Position += asteroid.Speed;
                if (xDiffPlayer > 3000 || yDiffPlayer > 3000)
                    asteroid.IsDead = true;
            }
            for (var i = 0; i < NrofAsteroids.Count; i++)
                if (NrofAsteroids[i].HpAsteroid == 0)
                    NrofAsteroids.Remove(NrofAsteroids[i]);


            // Debug.WriteLine(mygame.Window.ClientBounds.Bottom);
            // TODO: Add your update logic here
        }

        public void MiniStroid(Vector2 aspos)
        {
            MiniStroids.Add(new Asteroid
            {
                Timer = Rand.Next(100, 300),
                //vänster
                HpAsteroid = 10,
                ChosenTexture = RandomTexture.Next(4),
                AddCounter = Rand.Next(-677, 677) / 10000f,
                Position = new Vector2(aspos.X + Rand.Next(-20, 20), aspos.Y + Rand.Next(-20, 20)),
                Speed = new Vector2((float) Math.Cos(Rand.Next(-7, 7)), (float) Math.Sin(Rand.Next(-7, 7))),
                Radius = 38
            });
        }

        public void AsteroidSpawner()
        {
            var spawnside = Rand.Next(1, 5);
            //     int Spawnside = 1;
            // Debug.WriteLine(Spawnside);
            switch (spawnside)
            {
                case 1:

                    NrofAsteroids.Add(new Asteroid
                    {
                        //vänster

                        HpAsteroid = 10,
                        ScoreReward = 10,
                        ChosenTexture = RandomTexture.Next(1, 5),
                        AddCounter = Rand.Next(-677, 677) / 10000f,
                        Position = new Vector2(
                            Mygame.Player.Position.X - Mygame.Window.ClientBounds.X -
                            Rand.Next(1000, Globals.ScreenWidth * 3),
                            Mygame.Player.Position.Y - Mygame.Window.ClientBounds.Height + Rand.Next(-2400, 3600)),
                        Speed = new Vector2((float) Math.Cos(Rand.Next(-5, 5)), (float) Math.Sin(Rand.Next(-5, 5))),
                        Radius = 38
                    });

                    break;
                case 2:
                    //höger
                    NrofAsteroids.Add(new Asteroid
                    {
                        Radius = 38,
                        HpAsteroid = 10,
                        ScoreReward = 10,
                        ChosenTexture = RandomTexture.Next(1, 5),
                        AddCounter = Rand.Next(-677, 677) / 10000f,
                        Position = new Vector2(
                            Mygame.Player.Position.X + Rand.Next(Globals.ScreenWidth, Globals.ScreenWidth * 2) +
                            Mygame.Window.ClientBounds.X,
                            Mygame.Player.Position.Y + Mygame.Window.ClientBounds.Height + Rand.Next(-2400, 3600)),
                        Speed = new Vector2((float) Math.Cos(Rand.Next(-5, 5)), (float) Math.Sin(Rand.Next(-5, 5)))
                    });

                    break;
                case 3:
                    //upp
                    NrofAsteroids.Add(new Asteroid
                    {
                        Radius = 38,
                        HpAsteroid = 10,
                        ScoreReward = 10,
                        ChosenTexture = RandomTexture.Next(1, 5),
                        AddCounter = Rand.Next(-677, 677) / 10000f,
                        Position = new Vector2(
                            Mygame.Player.Position.X + Rand.Next(-Globals.ScreenWidth, Globals.ScreenWidth * 3) +
                            Mygame.Window.ClientBounds.X,
                            Mygame.Player.Position.Y - Mygame.Window.ClientBounds.Height + Rand.Next(-2400, 0)),
                        Speed = new Vector2((float) Math.Cos(Rand.Next(-5, 5)), (float) Math.Sin(Rand.Next(-5, 50)))
                    });

                    break;
                case 4:
                    //ner
                    NrofAsteroids.Add(new Asteroid
                    {
                        Radius = 38,
                        HpAsteroid = 10,
                        ScoreReward = 10,
                        ChosenTexture = RandomTexture.Next(1, 5),
                        AddCounter = Rand.Next(-677, 677) / 10000f,
                        Position = new Vector2(
                            Mygame.Player.Position.X + Rand.Next(-Globals.ScreenWidth, Globals.ScreenWidth * 3) +
                            Mygame.Window.ClientBounds.X,
                            Mygame.Player.Position.Y + Mygame.Window.ClientBounds.Y + Rand.Next(1200, 2400)),
                        Speed = new Vector2((float) Math.Cos(Rand.Next(-5, 5)), (float) Math.Sin(Rand.Next(-5, 5)))
                    });

                    break;
            }
        }
    }
}