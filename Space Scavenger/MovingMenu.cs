using Microsoft.Xna.Framework;

namespace Space_Scavenger
{
    internal class MovingMenu : GameObject
    {
        public void Update(GameTime gametime)
        {
            Speed = new Vector2(1, -1);
            Position += 2f * Speed;
        }
    }
}