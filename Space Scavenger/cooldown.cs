/*
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Space_Scavenger
{
    class cooldown
    {
        float cooldowntime = 0;

        public float Cooldowntime
        {
            get => cooldowntime; set => cooldowntime = value;
        }

        Update(GameTime gameTime)

        {
            GamePadState controller = GamePad.GetState(PlayerIndex.One);

            NewMethod(gameTime);

            if (cooldowntime >= 5000 && controller.Buttons.A == ButtonState.Pressed)
            {
                UseAbility(Shield);
                cooldowntime = 30;
            }


            else (cooldowntime >= 5000 && controller.Buttons.B == ButtonState.Pressed)
                {
                UseAbility(shot);
                cooldowntime = 2;
            } 
                  

                    else (cooldowntime >= 5000 && controller.Buttons.X == ButtonState.Pressed)
                    {
                UseAbility(Special);
                cooldowntime = 20;
            }
        }


        private void Cooldown(GameTime gameTime)
        {
            NewMethod1(gameTime);
        }

        private void NewMethod1(GameTime gameTime)
        {
            cooldowntime += NewMethod(gameTime);
        }

        private static double NewMethod(GameTime gameTime)
        {
            return GetTotalMilliseconds(gameTime);
        }

        private static double GetTotalMilliseconds(GameTime gameTime)
        {
            return gameTime.ElapsedGameTime.TotalMilliseconds;
        }
        
    }
        }
 

