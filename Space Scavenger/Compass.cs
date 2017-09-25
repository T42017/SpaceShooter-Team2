using System;
using Microsoft.Xna.Framework;

namespace Space_Scavenger
{
    public class Compass : GameObject
    {
        public int ChosenTexture;
        public int Type;


        public Compass CompassSpawn()
        {
            return new Compass
            {
                Position = new Vector2(0, 0),
                Rotation = Rotation
            };
        }


        public void Update(GameTime gametime, Game game)
        {
            MyGame = (SpaceScavenger) game;
            var targetrotation = (float) Math.Atan2(MyGame.Player.Position.X - Position.X,
                MyGame.Player.Position.Y - Position.Y);

            if (targetrotation < 360)
                Rotation += 360;
            else if (targetrotation > 360)
                Rotation -= 360;

            Rotation = -targetrotation;
        }
    }
}