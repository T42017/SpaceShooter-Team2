using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics; 
using Microsoft.Xna.Framework.Input;

namespace Space_Scavenger
{
    class Asteroid : GameObject
    {

        public int hpAsteroid;
        public float RotationCounter;
        public float addCounter;
        public int chosenTexture;
    }

    class AsteroidComponent : GameObject
    {
        public Random randomTexture = new Random();
        public Random rand = new Random();
        private  List<Texture2D> meteorTexture2Ds = new List<Texture2D>();
        public  SpaceScavenger mygame;
        public GameObject myObject;
        public Texture2D asterTexture2D1;
        public Texture2D asterTexture2D2;
        public Texture2D asterTexture2D3;
        public Texture2D asterTexture2D4;
        public bool isDead { get; set; }
        public float Rotation { get; set; }
        public int Health { get; set; }
        public int wantedAsteroids = 200;


        public List<Asteroid> _nrofAsteroids = new List<Asteroid>();

        public AsteroidComponent(Game game, Player Player, GameObject gameObject) 

        {
            mygame = (SpaceScavenger)game;
            myObject = (GameObject)gameObject;
            AsteroidSpawner();
        }

        public  void Update(GameTime gameTime)
        {
       //     Debug.WriteLine(_nrofAsteroids[1].hpAsteroid);
            
            if (_nrofAsteroids.Count < wantedAsteroids)
            {

                AsteroidSpawner();
            }

            foreach (var asteroid in _nrofAsteroids)
            {
                asteroid.Position += asteroid.Speed;
            }
            for (int i = 0; i < _nrofAsteroids.Count; i++)
            {
                if (_nrofAsteroids[i].hpAsteroid == 0)
                {
                    _nrofAsteroids.Remove(_nrofAsteroids[i]);
                }
            }

            // Debug.WriteLine(mygame.Window.ClientBounds.Bottom);
            // TODO: Add your update logic here
        }

        public void AsteroidSpawner()
        {
            var xDiff = Math.Abs(mygame.Player.Position.X - 500);

       int Spawnside = (rand.Next(0, 5));
        //    int Spawnside = 4;

            switch (Spawnside)
            {
                case 1:

                        _nrofAsteroids.Add(new Asteroid()
                        {

                          //vänster
                            hpAsteroid = 10,
                            chosenTexture = randomTexture.Next(0, 5),
                            addCounter = rand.Next(-677, 677) / 10000f,
                            Position = new Vector2(mygame.Player.Position.X + rand.Next(-1500, -1200) + mygame.Window.ClientBounds.X, mygame.Player.Position.Y + mygame.Window.ClientBounds.Height + rand.Next(-1000, 1000)),
                            Speed = new Vector2((float)Math.Cos(rand.Next(-5, 5)), (float)Math.Sin(rand.Next(-5, 5))),
                            Radius = 38
                        });
                    
                    break;
                case 2:
                    //höger
                        _nrofAsteroids.Add(new Asteroid()
                        {
                            Radius = 38,
                            hpAsteroid = 10,
                            chosenTexture = randomTexture.Next(0, 5),
                            addCounter = rand.Next(-677, 677) / 10000f,
                            Position = new Vector2(mygame.Player.Position.X + rand.Next(1200,1500) + mygame.Window.ClientBounds.X, mygame.Player.Position.Y - mygame.Window.ClientBounds.Height - rand.Next(-1500, -1000)),
                            Speed = new Vector2((float)Math.Cos(rand.Next(-5, 5)), (float)Math.Sin(rand.Next(-5, 5)))
                        });
                    
                    break;
                case 3:
                    //upp
                        _nrofAsteroids.Add(new Asteroid()
                        {
                            Radius = 38,
                            hpAsteroid = 10,
                            chosenTexture = randomTexture.Next(0, 5),
                            addCounter = rand.Next(-677, 677) / 10000f,
                            Position = new Vector2(mygame.Player.Position.X + rand.Next(-1000, 1000) + mygame.Window.ClientBounds.X, mygame.Player.Position.Y - (mygame.Window.ClientBounds.Height + rand.Next(-750, -500))),
                            Speed = new Vector2((float)Math.Cos(rand.Next(-5, 5)), (float)Math.Sin(rand.Next(-5, 50)))
                        });
                    
                    break;
                case 4:
                    //ner
                        _nrofAsteroids.Add(new Asteroid()
                        {
                            Radius = 38,
                            hpAsteroid = 10,
                            chosenTexture = randomTexture.Next(0, 5),
                            addCounter = rand.Next(-677, 677) / 10000f,
                            Position = new Vector2(mygame.Player.Position.X + rand.Next(-1000, 1000) - mygame.Window.ClientBounds.X, mygame.Player.Position.Y + mygame.Window.ClientBounds.Y + rand.Next(500, 750)),
                            Speed = new Vector2((float)Math.Cos(rand.Next(-5, 5)), (float)Math.Sin(rand.Next(-5, 5)))
                        });
                    
                    break;


            }

        }
    }
}

