using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Space_Scavenger
{
    class shot : GameObject
    {
        public Shot()
        {
            Radius = 20;
        }

        public void Update(GameTime gameTime)
        {
            Position += Speed;
            Rotation += 0.1f;

            if (Rotation > MathHelper.TwoPi)
                Rotation = 0;
        }
    }
}

   