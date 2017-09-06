using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Space_Scavenger
{
    public class Shot : GameObject
    {
        public Shot()
        {
            Radius = 16;
        }

        public void Update(GameTime gameTime)
        {
            Position += Speed;
        }
    }
}
