using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Space_Scavenger
{
    public class Boost : DrawableGameComponent
    {
        public int BoostTime = 0;
        public int NrOfBoosts = 0;
        private readonly SpaceScavenger _myGame;
        private KeyboardState previousKbState;


        public Boost(Game game) : base(game)
        {
            _myGame = (SpaceScavenger) game;
        }

        public override void Update(GameTime gameTime)
        {
            KeyboardState state = Keyboard.GetState();
            if (state.IsKeyDown(Keys.X) && previousKbState.IsKeyDown(Keys.X) != state.IsKeyDown(Keys.X))
            {
                if (BoostTime <= 0)
                {
                    _myGame.Player.Speed = new Vector2((float) Math.Cos(_myGame.Player.Rotation), (float) Math.Sin(_myGame.Player.Rotation)) *
                                   20f;
                    BoostTime = 600;
                }

                if (BoostTime >= 0)
                { BoostTime--;}
            }

            previousKbState = state;
        }
    }
}
