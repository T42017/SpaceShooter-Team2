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
        public int BoostTime;
        public int NrOfBoosts;
        public int BoostRegenerationCoolDown;
        private readonly SpaceScavenger _myGame;
        private KeyboardState previousKbState;


        public Boost(Game game) : base(game)
        {
            _myGame = (SpaceScavenger) game;
            NrOfBoosts = 3;
            BoostTime = 200;
            BoostRegenerationCoolDown = 0;
        }
        //_myGame.Player.Speed =  new Vector2((float) Math.Cos(_myGame.Player.Rotation), (float) Math.Sin(_myGame.Player.Rotation)) * 20f;
        public override void Update(GameTime gameTime)
        {
            KeyboardState state = Keyboard.GetState();

            if (NrOfBoosts > 0)
            {
                if (BoostRegenerationCoolDown > 0)
                {
                    BoostRegenerationCoolDown--;
                }
                
                if (state.IsKeyDown(Keys.X) && previousKbState.IsKeyDown(Keys.X) != state.IsKeyDown(Keys.X))
                {
                    _myGame.Player.Speed =
                        new Vector2((float) Math.Cos(_myGame.Player.Rotation), (float) Math.Sin(_myGame.Player.Rotation)) *
                        20f;
                    NrOfBoosts--;
                   BoostRegenerationCoolDown = 300;

                }
                if (BoostRegenerationCoolDown <= 0)
                {
                    if (NrOfBoosts >= 1 && NrOfBoosts < 3)
                    {
                        NrOfBoosts++;
                        BoostRegenerationCoolDown = 300;
                    }
                }
               
                           
            }
            else if (NrOfBoosts == 0)
            {
                if (BoostRegenerationCoolDown > 0)
                {
                    BoostRegenerationCoolDown--;
                }
                else if (BoostRegenerationCoolDown <= 0)
                {
                    NrOfBoosts++;
                    BoostRegenerationCoolDown = 300;
                }
            }


            // if (BoostRegenerationCoolDown > 0)
            // {
            //     BoostRegenerationCoolDown--;
            // }
            // if (BoostRegenerationCoolDown <= 0)
            // {
            //     NrOfBoosts++;
            // }


            previousKbState = state;
        }
    }
}
