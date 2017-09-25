using System;
using Microsoft.Xna.Framework;

namespace Space_Scavenger
{
    public class BombEnemy : GameObject

    {
        private readonly Random _rnd = new Random();
        private SpaceScavenger _myGame;

        public BombEnemy BombEnemySpawn(Game game)
        {
            _myGame = (SpaceScavenger) game;

            _myGame.Gamestate = GameState.Playing;
            var spawnside = _rnd.Next(1, 5);
            switch (spawnside)
            {
                case 1:

                    return new BombEnemy
                    {
                        //vänster
                        Radius = 20,
                        Health = 3,
                        ScoreReward = 100,
                        Rotation = _myGame.EnemyRotation,
                        Position = new Vector2(
                            _myGame.Player.Position.X - _myGame.Window.ClientBounds.X -
                            _rnd.Next(1000, Globals.ScreenWidth * 3),
                            _myGame.Player.Position.Y - _myGame.Window.ClientBounds.Height + _rnd.Next(-2400, 3600))
                    };
                case 2:
                    //höger
                    return new BombEnemy
                    {
                        Radius = 20,
                        Health = 3,
                        ScoreReward = 100,
                        Rotation = _myGame.EnemyRotation,
                        Position = new Vector2(
                            _myGame.Player.Position.X + _rnd.Next(Globals.ScreenWidth, Globals.ScreenWidth * 2) +
                            _myGame.Window.ClientBounds.X,
                            _myGame.Player.Position.Y + _myGame.Window.ClientBounds.Height + _rnd.Next(-2400, 3600))
                    };
                case 3:
                    //upp
                    return new BombEnemy
                    {
                        Radius = 20,
                        Health = 3,
                        ScoreReward = 100,
                        Rotation = _myGame.EnemyRotation,
                        Position = new Vector2(
                            _myGame.Player.Position.X + _rnd.Next(-Globals.ScreenWidth, Globals.ScreenWidth * 3) +
                            _myGame.Window.ClientBounds.X,
                            _myGame.Player.Position.Y - _myGame.Window.ClientBounds.Height + _rnd.Next(-2400, 0))
                    };
                case 4:
                    //ner
                    return new BombEnemy
                    {
                        Radius = 20,
                        Health = 3,
                        ScoreReward = 100,
                        Rotation = _myGame.EnemyRotation,
                        Position = new Vector2(
                            _myGame.Player.Position.X + _rnd.Next(-Globals.ScreenWidth, Globals.ScreenWidth * 3) +
                            _myGame.Window.ClientBounds.X,
                            _myGame.Player.Position.Y + _myGame.Window.ClientBounds.Y + _rnd.Next(1200, 2400))
                    };
            }
            return null;
        }

        public void Update(GameTime gameTime, Game game)
        {
            _myGame = (SpaceScavenger) game;
            var followDistance = 1500;

            var direction = _myGame.Player.Position - Position;
            direction.Normalize();
            Speed += direction * 0.2f;

            if (Speed.LengthSquared() > 25)
                Speed = Vector2.Normalize(Speed) * 5;

            var xDiffPlayer = Math.Abs(Position.X - _myGame.Player.Position.X);
            var yDiffPlayer = Math.Abs(Position.Y - _myGame.Player.Position.Y);

            if (xDiffPlayer < followDistance && yDiffPlayer < followDistance)
                Position += Speed;
        }
    }
}