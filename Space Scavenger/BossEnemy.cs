using System;
using Microsoft.Xna.Framework;

namespace Space_Scavenger
{
    public class BossEnemy : GameObject
    {
        readonly Random _rnd = new Random();



        public void Update(GameTime gametime)
        {
            Position += Speed;
        }

        public BossEnemy SpawnBoss(Game game)
        {
            MyGame = (SpaceScavenger) game;

            int spawnside = _rnd.Next(1, 2);
            switch (spawnside)
            {
                case 1:

                    return new BossEnemy
                    {
                        //vänster
                        Radius = 150,
                        Health = 30,
                        ExpReward = 100,
                        ScoreReward = 1000,
                        Position = new Vector2(MyGame.Player.Position.X - MyGame.Window.ClientBounds.X - _rnd.Next(1000, Globals.ScreenWidth * 3), 
                         MyGame.Player.Position.Y - MyGame.Window.ClientBounds.Height + _rnd.Next(-2400, 3600)),
                        Rotation = Rotation,
                        Speed = new Vector2((float)Math.Cos(_rnd.Next(-5, 5)), (float)Math.Sin(_rnd.Next(-5, 5))),
                    };
                case 2:
                    //höger
                    return new BossEnemy()
                    {
                        Radius = 150,
                        Health = 3,
                        ExpReward = 100,
                        ScoreReward = 100,
                        Rotation = Rotation,
                        Position = new Vector2(MyGame.Player.Position.X + _rnd.Next(Globals.ScreenWidth, Globals.ScreenWidth * 2) + MyGame.Window.ClientBounds.X, MyGame.Player.Position.Y + MyGame.Window.ClientBounds.Height + _rnd.Next(-2400, 3600)),
                        Speed = new Vector2((float)Math.Cos(_rnd.Next(-5, 5)), (float)Math.Sin(_rnd.Next(-5, 5)))
                    };
                case 3:
                    //upp
                    return new BossEnemy()
                    {
                        Radius = 150,
                        Health = 3,
                        ExpReward = 100,
                        ScoreReward = 100,
                        Rotation = Rotation,
                        Position = new Vector2(
                            MyGame.Player.Position.X + _rnd.Next(-Globals.ScreenWidth, Globals.ScreenWidth * 3) +
                            MyGame.Window.ClientBounds.X,
                            MyGame.Player.Position.Y - MyGame.Window.ClientBounds.Height + _rnd.Next(-2400, 0)),
                        Speed = new Vector2((float)Math.Cos(_rnd.Next(-5, 5)), (float)Math.Sin(_rnd.Next(-5, 5)))
                    };
                case 4:
                    //ner
                    return new BossEnemy()
                    {
                        Radius = 150,
                        Health = 3,
                        ExpReward = 100,
                        ScoreReward = 100,
                        Rotation = Rotation,
                        Position = new Vector2(
                            MyGame.Player.Position.X + _rnd.Next(-Globals.ScreenWidth, Globals.ScreenWidth * 3) +
                            MyGame.Window.ClientBounds.X,
                            MyGame.Player.Position.Y + MyGame.Window.ClientBounds.Y + _rnd.Next(1200, 2400)),
                        Speed = new Vector2((float)Math.Cos(_rnd.Next(-5, 5)), (float)Math.Sin(_rnd.Next(-5, 5)))
                    };
            }

            return null;

        }


    }
}
