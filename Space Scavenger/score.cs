using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonoGame;
using System.Collections;


namespace Space_Scavenger
{
    class score
    {
        private float score(GameTime gameTime, List<enemy> enemys)
        {
            float addScore = 0;

            foreach (enemy ship in _grid)
            {
                if (enemy.Killed)
                {
                    addScore += 1;
                }
            }
        }
  
