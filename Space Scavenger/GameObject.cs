﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Space_Scavenger
{
    public abstract class GameObject : IGameObject
    {
        public bool isDead { get; set; }
        public Vector2 Position { get; set; }
        public float Radius { get; set; }
        public Vector2 Speed { get; set; }
        public float Rotation { get; set; }
        public int Health { get; set; }
        public SpaceScavenger MyGame { get; set; }
        public int Timer { get; set; }
        public int ExpReward { get; set; }
        public int ScoreReward { get; set; }


        public bool CollidesWith(IGameObject other)
        {
            return (this.Position - other.Position).LengthSquared() < (Radius + other.Radius) * (Radius + other.Radius);
        }

    }
}
