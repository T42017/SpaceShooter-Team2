﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Space_Scavenger
{
    public class Boost : DrawableGameComponent
    {
        private readonly SpaceScavenger _myGame;
        private KeyboardState _previousKbState;
        public int BoostRegenerationCoolDown;

        public int NrOfBoosts;


        public Boost(Game game) : base(game)
        {
            _myGame = (SpaceScavenger) game;
            NrOfBoosts = 3;
            BoostRegenerationCoolDown = 0;
        }

        public override void Update(GameTime gameTime)
        {
            var state = Keyboard.GetState();
            if (NrOfBoosts > 0)
            {
                if (BoostRegenerationCoolDown > 0)
                    BoostRegenerationCoolDown--;

                if (state.IsKeyDown(Keys.X) && _previousKbState.IsKeyDown(Keys.X) != state.IsKeyDown(Keys.X))
                {
                    _myGame.Player.Speed =
                        new Vector2((float) Math.Cos(_myGame.Player.Rotation),
                            (float) Math.Sin(_myGame.Player.Rotation)) * 20f;
                    NrOfBoosts--;
                    BoostRegenerationCoolDown = 300;
                }
                if (BoostRegenerationCoolDown <= 0)
                    if (NrOfBoosts >= 1 && NrOfBoosts < 3)
                    {
                        NrOfBoosts++;
                        BoostRegenerationCoolDown = 300;
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
            _previousKbState = state;
        }
    }
}