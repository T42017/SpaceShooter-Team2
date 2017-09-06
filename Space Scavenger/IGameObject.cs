using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Space_Scavenger
{
    public interface IGameObject
    {
        bool isDead { get; set; }
        Vector2 Position { get; set; }
        float Radius { get; set; }
        Vector2 Speed { get; set; }
        float Rotation { get; set; }
        int Health { get; set; }
    }
}
