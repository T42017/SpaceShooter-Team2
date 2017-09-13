using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Space_Scavenger
{
    class Pause
    {
        protected override void Update(GameTime gameTime)
        {
            KeyboardState currentKeyboardState = Keyboard.GetState();

            if (currentKeyboardState.IsKeyDown(Keys.Escape))
                this.Exit();

            else if (currentGameState == GameStates.NotPlaying)
            {
                if (currentKeyboardState.IsKeyDown(Keys.Space))
                    currentGameState = GameStates.Playing;
            }

            else if (currentGameState == GameStates.Playing)
            {
                if (currentKeyboardState.IsKeyDown(Keys.P))
                    currentGameState = GameStates.Paused;
            }

            else if (currentGameState == GameStates.Paused)
            {
                if (currentKeyboardState.IsKeyDown(Keys.P))
                    currentGameState = GameStates.Playing;
            }

            base.Update(gameTime);
        }
    }
}
