using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Space_Scavenger
{
    class Camera
    {
        public Matrix transformn;
        private Viewport view;
        private Vector2 centre;


        public Camera(Viewport newView)
        {
            view = newView;
        }

        public void Update(GameTime gameTIme, Player player)
        {
            centre = new Vector2(player.Position.X  - 640, player.Position.Y  - 360);
            transformn = Matrix.CreateScale(new Vector3(1, 1, 0)) * 
                                                    Matrix.CreateTranslation(new Vector3(-centre.X, -centre.Y, 0));
        }
    }
}
