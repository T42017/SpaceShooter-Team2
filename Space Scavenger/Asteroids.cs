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
    class Asteroid
    {
        public Vector2 position { get; set; }
        public Vector2 speed { get; set; }
        public int hpAsteroid;
        public float RotationCounter;
        public float addCounter;
        public int chosenTexture;

    }

    class AsteroidComponent : DrawableGameComponent
    {
        public Random randomTexture = new Random();
        public Random rand = new Random();
        private  List<Texture2D> meteorTexture2Ds = new List<Texture2D>();
        public  SpaceScavenger mygame;
        private Texture2D asterTexture2D1;
        private Texture2D asterTexture2D2;
        private Texture2D asterTexture2D3;
        private Texture2D asterTexture2D4;
        public float Rotation { get; set; }
        public int wantedAsteroids = 200;


        public List<Asteroid> _nrofAsteroids = new List<Asteroid>();
        public AsteroidComponent(Game game, Player player) : base(game)

        {
            mygame = (SpaceScavenger)game;
            AsteroidSpawner();
            
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            
            asterTexture2D1 = Game.Content.Load<Texture2D>("Meteor1");
            asterTexture2D2 = Game.Content.Load<Texture2D>("Meteor2");
            asterTexture2D3 = Game.Content.Load<Texture2D>("Meteor3");
            asterTexture2D4 = Game.Content.Load<Texture2D>("Meteor4");
            base.LoadContent();
        }
        public void Draw(SpriteBatch spriteBatch)
        {

            
            for (int i = 0; i < _nrofAsteroids.Count; i++)
            {
                switch (_nrofAsteroids[i].chosenTexture)
                {
                    case 1:
                        spriteBatch.Draw(asterTexture2D1, _nrofAsteroids[i].position, null, Color.White, Rotation + _nrofAsteroids[i].RotationCounter, new Vector2(asterTexture2D1.Width / 2f, asterTexture2D1.Height / 2f), 1f, SpriteEffects.None, 0f);
                        _nrofAsteroids[i].RotationCounter += _nrofAsteroids[i].addCounter;
                        break;
                    case 2:
                        spriteBatch.Draw(asterTexture2D2, _nrofAsteroids[i].position, null, Color.White, Rotation + _nrofAsteroids[i].RotationCounter, new Vector2(asterTexture2D2.Width / 2f, asterTexture2D2.Height / 2f), 1f, SpriteEffects.None, 0f);
                        _nrofAsteroids[i].RotationCounter += _nrofAsteroids[i].addCounter;
                        break;
                    case 3:
                        spriteBatch.Draw(asterTexture2D3, _nrofAsteroids[i].position, null, Color.White, Rotation + _nrofAsteroids[i].RotationCounter, new Vector2(asterTexture2D3.Width / 2f, asterTexture2D3.Height / 2f), 1f, SpriteEffects.None, 0f);
                        _nrofAsteroids[i].RotationCounter += _nrofAsteroids[i].addCounter;
                        break;
                    case 4:
                        spriteBatch.Draw(asterTexture2D4, _nrofAsteroids[i].position, null, Color.White, Rotation + _nrofAsteroids[i].RotationCounter, new Vector2(asterTexture2D4.Width / 2f, asterTexture2D4.Height / 2f), 1f, SpriteEffects.None, 0f);
                        _nrofAsteroids[i].RotationCounter += _nrofAsteroids[i].addCounter;
                        break;
                }
                
              /*  if (_nrofAsteroids[i].RotationCounter > 2000000000 || _nrofAsteroids[i].RotationCounter < -2000000000)
                {                                                   anti integer overflow system. Activate if it happens
                    _nrofAsteroids[i].RotationCounter = 0;
                }*/
            }

            // TODO: Add your drawing code here

        }
        public override void Update(GameTime gameTime)
        {


            if (_nrofAsteroids.Count < wantedAsteroids)
            {
 
                AsteroidSpawner();
            }

            foreach (var asteroid in _nrofAsteroids)
            {
                asteroid.position += asteroid.speed;
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

            base.Update(gameTime);
        }

        public void AsteroidSpawner()
        {
            var xDiff = Math.Abs(mygame.player.Position.X - 500);

        var number = (rand.Next(4));


            switch (number)
            {
                case 1:
                    for (int i = 0; i < wantedAsteroids; i++)
                    {
                        _nrofAsteroids.Add(new Asteroid()
                        {
                            
                            
                        chosenTexture = randomTexture.Next(0, 5),
                            hpAsteroid = 10,
                            addCounter = rand.Next(-677, 677) / 10000f,
                            position = new Vector2(mygame.player.Position.X + rand.Next(0, 500) + Game.Window.ClientBounds.X, mygame.player.Position.Y + Game.Window.ClientBounds.Height + rand.Next(0, 500)),
                            speed = new Vector2((float)Math.Cos(rand.Next(-5, 5)), (float)Math.Sin(rand.Next(-5, 5)))
                        });
                    }
                    break;
                case 2:
                    for (int i = 0; i < wantedAsteroids; i++)
                    {
                        _nrofAsteroids.Add(new Asteroid()
                        {
                            hpAsteroid = 10,
                        chosenTexture = randomTexture.Next(0, 5),
                            addCounter = rand.Next(-677, 677) / 10000f,
                            position = new Vector2(mygame.player.Position.X - rand.Next(0,500) - Game.Window.ClientBounds.X, mygame.player.Position.Y - Game.Window.ClientBounds.Height - rand.Next(0, 500)),
                            speed = new Vector2((float)Math.Cos(rand.Next(-5, 5)), (float)Math.Sin(rand.Next(-5, 5)))
                        });
                    }
                    break;
                case 3:
                    for (int i = 0; i < wantedAsteroids; i++)
                    {
                        _nrofAsteroids.Add(new Asteroid()
                        {
                            hpAsteroid = 10,
                            chosenTexture = randomTexture.Next(0, 5),
                            addCounter = rand.Next(-677, 677) / 10000f,
                            position = new Vector2(mygame.player.Position.X + rand.Next(0, 500) + Game.Window.ClientBounds.X, mygame.player.Position.Y - Game.Window.ClientBounds.Height - rand.Next(0, 500)),
                            speed = new Vector2((float)Math.Cos(rand.Next(-5, 5)), (float)Math.Sin(rand.Next(-5, 50)))
                        });
                    }
                    break;
                case 4:
                    for (int i = 0; i < wantedAsteroids; i++)
                    {
                        _nrofAsteroids.Add(new Asteroid()
                        {
                            hpAsteroid = 10,
                            chosenTexture = randomTexture.Next(0, 5),
                            addCounter = rand.Next(-677, 677) / 10000f,
                            position = new Vector2(mygame.player.Position.X - rand.Next(0, 500) - Game.Window.ClientBounds.X, mygame.player.Position.Y + Game.Window.ClientBounds.Height + rand.Next(0, 500)),
                            speed = new Vector2((float)Math.Cos(rand.Next(-5, 5)), (float)Math.Sin(rand.Next(5, 5)))
                        });
                    }
                    break;


            }

        }
    }
}

