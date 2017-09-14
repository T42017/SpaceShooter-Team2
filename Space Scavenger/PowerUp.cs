using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Space_Scavenger
{
    public class PowerUp : GameObject
    {

        private readonly Random rnd = new Random();


        public PowerUp powerUpSpawn(Game game)
        {
            MyGame = (SpaceScavenger) game;

            int Spawnside = rnd.Next(1, 5);
            switch (Spawnside)
            {
                case 1:

                    return new PowerUp
                    {
                        //vänster
                        Radius = 20,
                        Health = 3,
                        ScoreReward = 100,
                        Position = new Vector2(
                            MyGame.Player.Position.X - MyGame.Window.ClientBounds.X -
                            rnd.Next(1000, Globals.ScreenWidth * 3),
                            MyGame.Player.Position.Y - MyGame.Window.ClientBounds.Height + rnd.Next(-2400, 3600))
                    };
                case 2:
                    //höger
                    return new PowerUp()
                    {
                        Radius = 20,
                        Health = 3,
                        ScoreReward = 100,
                        Position = new Vector2(
                            MyGame.Player.Position.X + rnd.Next(Globals.ScreenWidth, Globals.ScreenWidth * 2) +
                            MyGame.Window.ClientBounds.X,
                            MyGame.Player.Position.Y + MyGame.Window.ClientBounds.Height + rnd.Next(-2400, 3600))
                    };
                case 3:
                    //upp
                    return new PowerUp()
                    {
                        Radius = 20,
                        Health = 3,
                        ScoreReward = 100,
                        Position = new Vector2(
                            MyGame.Player.Position.X + rnd.Next(-Globals.ScreenWidth, Globals.ScreenWidth * 3) +
                            MyGame.Window.ClientBounds.X,
                            MyGame.Player.Position.Y - MyGame.Window.ClientBounds.Height + rnd.Next(-2400, 0))
                    };
                case 4:
                    //ner
                    return new PowerUp()
                    {
                        Radius = 20,
                        Health = 3,
                        ScoreReward = 100,
                        Position = new Vector2(
                            MyGame.Player.Position.X + rnd.Next(-Globals.ScreenWidth, Globals.ScreenWidth * 3) +
                            MyGame.Window.ClientBounds.X,
                            MyGame.Player.Position.Y + MyGame.Window.ClientBounds.Y + rnd.Next(1200, 2400))
                    };
            }

            return null;


        }

    }
}
