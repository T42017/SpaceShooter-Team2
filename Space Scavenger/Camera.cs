using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Space_Scavenger
{
    public class Camera
    {
        private Vector2 _centre;
        private Viewport _view;
        public Matrix Transformn;


        public Camera(Viewport newView)
        {
            _view = newView;
        }

        public void Update(GameTime gameTIme, Player player)
        {
            _centre = new Vector2(player.Position.X - Globals.ScreenWidth / 2,
                player.Position.Y - Globals.ScreenHeight / 2);
            Transformn = Matrix.CreateScale(new Vector3(1, 1, 0)) *
                         Matrix.CreateTranslation(new Vector3(-_centre.X, -_centre.Y, 0));
        }
    }
}