using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Space_Scavenger
{
    public class PowerUp : DrawableGameComponent
    {
        private Texture2D _powerUpHealth;
       

        public PowerUp(Game game) : base(game)
        {
           
        }


        protected override void LoadContent()
        {
            
            _powerUpHealth = Game.Content.Load<Texture2D>("powerupRedPill");
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            


            base.Update(gameTime);
        }


        public void Draw(SpriteBatch spriteBatch)
        {
           
            spriteBatch.Draw(_powerUpHealth, new Vector2(300,300),Color.White);
           
        }
    }
}
