using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics; 
using Microsoft.Xna.Framework.Input;

namespace Space_Scavenger
{
    class Asteroid
    {
        public Vector2 position { get; set; }
        public Vector2 speed { get; set; }
        int hpAsteroid;
        public float RotationCounter;
        public float addCounter;

    }

    class AsteroidComponent : DrawableGameComponent
    {
        public Random rand = new Random();
        private Texture2D asterTexture2D;

        public float Rotation { get; set; }

        private List<Asteroid> _nrofAsteroids = new List<Asteroid>();
        public AsteroidComponent(Game game, Player player) : base(game)

        {
            for (int i = 0; i < 20; i++)
            {
                _nrofAsteroids.Add(new Asteroid()
                {
                   addCounter = rand.Next(-677,677) / 10000f,
                    position = new Vector2(player.Position.X + rand.Next(-100, 100), player.Position.Y + rand.Next(-100,100)),
                    speed = new Vector2((float)Math.Cos(rand.Next(-5,5)), (float)Math.Sin(rand.Next(-5,5)))
            });
            }
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            asterTexture2D = Game.Content.Load<Texture2D>("Meteor1");
            base.LoadContent();
        }
        public void Draw(SpriteBatch spriteBatch)
        {

            
            for (int i = 0; i < _nrofAsteroids.Count; i++)
            {
                spriteBatch.Draw(asterTexture2D, _nrofAsteroids[i].position, null, Color.White, Rotation + _nrofAsteroids[i].RotationCounter, new Vector2(asterTexture2D.Width /2, asterTexture2D.Height /2), 1f, SpriteEffects.None, 0f);
                 _nrofAsteroids[i].RotationCounter += _nrofAsteroids[i].addCounter;
            }

            // TODO: Add your drawing code here

        }
        public override void Update(GameTime gameTime)
        {
            foreach (var asteroid in _nrofAsteroids)
            {
                asteroid.position += asteroid.speed;
            }


            // TODO: Add your update logic here

            base.Update(gameTime);
        }
    }
}

