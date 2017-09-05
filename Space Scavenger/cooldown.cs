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
        Update(GameTime gameTime)
        {
            GamePadState controller = GamePad.GetState(PlayerIndex.One);

            NewMethod(gameTime);

            if (cooldowntime >= 5000 && controller.Buttons.A == ButtonState.Pressed)
            {
                UseAbility(Shield);
                cooldowntime = 0;
            }


            else (cooldowntime >= 5000 && controller.Buttons.B == ButtonState.Pressed)
                {
                UseAbility(shot);
                cooldowntime = 0;
            }
                  

                    else (cooldowntime >= 5000 && controller.Buttons.C == ButtonState.Pressed)
                    {
                UseAbility(Special);
                cooldowntime = 0;
            }
        }


        private void Cooldown(GameTime gameTime)
        {
            cooldowntime += GetTotalMilliseconds(gameTime);
        }

        private static double GetTotalMilliseconds(GameTime gameTime)
        {
            return gameTime.ElapsedGameTime.TotalMilliseconds;
        }
        
    }
        }
 

