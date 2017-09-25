using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Space_Scavenger
{
    public abstract class GameObject : IGameObject
    {
        public SpaceScavenger MyGame { get; set; }
        public int Timer { get; set; }
        public int ExpReward { get; set; }
        public int ScoreReward { get; set; }
        public int ReloadTimer { get; set; }
        public Texture2D ChosenTexture2D { get; set; }
        public bool IsDead { get; set; }
        public Vector2 Position { get; set; }
        public float Radius { get; set; }
        public Vector2 Speed { get; set; }
        public float Rotation { get; set; }
        public int Health { get; set; }


        public bool CollidesWith(IGameObject other)
        {
            return (Position - other.Position).LengthSquared() < (Radius + other.Radius) * (Radius + other.Radius);
        }
    }
}