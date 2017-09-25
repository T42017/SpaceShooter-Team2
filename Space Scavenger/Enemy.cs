using System;
using Microsoft.Xna.Framework;

namespace Space_Scavenger
{
    public class Enemy : GameObject

    {
        private readonly Random _rnd = new Random();
        private SpaceScavenger _myGame;
        private int _reloadTime;


        public Enemy EnemySpawn(Game game)
        {
            _myGame = (SpaceScavenger) game;

            _myGame.Gamestate = GameState.Playing;
            var spawnside = _rnd.Next(1, 5);
            switch (spawnside)
            {
                case 1:

                    return new Enemy
                    {
                        //vänster
                        Radius = 20,
                        Health = 3,
                        ExpReward = 100,
                        ScoreReward = 100,
                        Rotation = Rotation,
                        Position = new Vector2(
                            _myGame.Player.Position.X - _myGame.Window.ClientBounds.X -
                            _rnd.Next(1000, Globals.ScreenWidth * 3),
                            _myGame.Player.Position.Y - _myGame.Window.ClientBounds.Height + _rnd.Next(-2400, 3600))
                    };
                case 2:
                    //höger
                    return new Enemy
                    {
                        Radius = 20,
                        Health = 3,
                        ExpReward = 100,
                        ScoreReward = 100,
                        Rotation = Rotation,
                        Position = new Vector2(
                            _myGame.Player.Position.X + _rnd.Next(Globals.ScreenWidth, Globals.ScreenWidth * 2) +
                            _myGame.Window.ClientBounds.X,
                            _myGame.Player.Position.Y + _myGame.Window.ClientBounds.Height + _rnd.Next(-2400, 3600))
                    };
                case 3:
                    //upp
                    return new Enemy
                    {
                        Radius = 20,
                        Health = 3,
                        ExpReward = 100,
                        ScoreReward = 100,
                        Rotation = Rotation,
                        Position = new Vector2(
                            _myGame.Player.Position.X + _rnd.Next(-Globals.ScreenWidth, Globals.ScreenWidth * 3) +
                            _myGame.Window.ClientBounds.X,
                            _myGame.Player.Position.Y - _myGame.Window.ClientBounds.Height + _rnd.Next(-2400, 0))
                    };
                case 4:
                    //ner
                    return new Enemy
                    {
                        Radius = 20,
                        Health = 3,
                        ExpReward = 100,
                        ScoreReward = 100,
                        Rotation = Rotation,
                        Position = new Vector2(
                            _myGame.Player.Position.X + _rnd.Next(-Globals.ScreenWidth, Globals.ScreenWidth * 3) +
                            _myGame.Window.ClientBounds.X,
                            _myGame.Player.Position.Y + _myGame.Window.ClientBounds.Y + _rnd.Next(1200, 2400))
                    };
            }


            return null;
        }

        public Shot EnemyShoot()
        {
            if (_reloadTime <= 0)
                return new Shot
                {
                    Timer = 200,
                    Position = Position - new Vector2(_rnd.Next(-10, 10), _rnd.Next(-10, 10)),
                    Rotation = Rotation,
                    Speed = 15f * new Vector2((float) Math.Cos(Rotation - MathHelper.PiOver2),
                                (float) Math.Sin(Rotation - MathHelper.PiOver2))
                };

            return null;
        }

        public void Update(GameTime gameTime, Game game)
        {
            _myGame = (SpaceScavenger) game;
            var followDistance = 1000;
            var aimDistance = 100;

            var direction = _myGame.Player.Position - Position;
            direction.Normalize();
            Speed += direction * 0.2f;

            if (Speed.LengthSquared() > 25)
                Speed = Vector2.Normalize(Speed) * 5;

            var xDiffPlayer = Math.Abs(Position.X - _myGame.Player.Position.X);
            var yDiffPlayer = Math.Abs(Position.Y - _myGame.Player.Position.Y);


            var targetrotation = (float) Math.Atan2(Position.X - _myGame.Player.Position.X,
                Position.Y - _myGame.Player.Position.Y);

            if (targetrotation < 360)
                Rotation += 360;
            else if (targetrotation > 360)
                Rotation -= 360;

            Rotation = -targetrotation;

            if (_reloadTime > 0)
                _reloadTime--;


            if (xDiffPlayer < followDistance && yDiffPlayer < followDistance)
            {
                if (xDiffPlayer > aimDistance || yDiffPlayer > aimDistance)
                    Position += Speed;
                else
                    Speed -= Speed;
                if (xDiffPlayer < 300 || yDiffPlayer < 300)
                    if (_reloadTime <= 0)
                    {
                        var s = EnemyShoot();
                        if (s != null)
                        {
                            _myGame.EnemyShots.Add(s);
                            if (_myGame.SoundEffectTimer <= 0)
                            {
                                _myGame.EnemyShootEffect.Play(0.8f, 0.0f, 0.0f);
                                _myGame.SoundEffectTimer = 15;
                            }
                            _reloadTime += 60;
                        }
                    }
            }
        }
    }
}