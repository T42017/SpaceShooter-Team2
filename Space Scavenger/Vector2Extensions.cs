using System;
using Microsoft.Xna.Framework;

namespace Space_Scavenger
{
    public static class Vector2Extensions
    {
        public static Vector2 Rotate(this Vector2 v, float radians)
        {
            var sin = (float) Math.Sin(radians);
            var cos = (float) Math.Cos(radians);

            var tx = v.X;
            var ty = v.Y;
            var x = cos * tx - sin * ty;
            var y = sin * tx + cos * ty;
            return new Vector2(x, y);
        }
    }
}