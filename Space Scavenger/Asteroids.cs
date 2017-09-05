using System;
using System.Collections.Generic;
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
        public float speed { get; set; }
        int hpAsteroid;

    }

    class AsteroidComponent : DrawableGameComponent
    {
        public Random rand = new Random();
        private Texture2D asterTexture2D;
        private List<Asteroid> _nrofAsteroids = new List<Asteroid>();
        SpriteBatch _spriteBatch;
        public AsteroidComponent(Game game) : base(game)

        {
            for (int i = 0; i < 20; i++)
            {
                _nrofAsteroids.Add(new Asteroid()
                {
                    position = new Vector2(rand.Next(-300, 300 ), rand.Next(-350, 300 )),
                    speed = 0.1f + rand.Next(20) / 10f
                });
            }
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            asterTexture2D = Game.Content.Load<Texture2D>("Tempass");
        }

        public override void Update(GameTime gameTime)
        {
            foreach (var asteroid in _nrofAsteroids)
            {
                asteroid.position += new Vector2(asteroid.speed, 0);
            }


            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {

            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin();
            foreach (var asteroid in _nrofAsteroids)
            {
                _spriteBatch.Draw(asterTexture2D, asteroid.position, Color.White);
            }

            // TODO: Add your drawing code here
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}

