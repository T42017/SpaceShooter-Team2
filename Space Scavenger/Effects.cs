using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Space_Scavenger
{
    internal class Effects : DrawableGameComponent
    {
        private Texture2D _enemyDamage;
        private SpriteBatch _explosionBatch;
        private Vector2 _position;

        public Effects(Game game) : base(game)
        {
            _position = new Vector2(Globals.ScreenWidth / 2f, Globals.ScreenHeight / 2f);
        }

        protected override void LoadContent()
        {
            _explosionBatch = new SpriteBatch(Game.GraphicsDevice);
            _enemyDamage = Game.Content.Load<Texture2D>("burst");
            base.LoadContent();
        }

        public void Draw(GameTime gameTime, Vector2 enemyposition)
        {
            _explosionBatch.Begin();
            _explosionBatch.Draw(_enemyDamage,
                new Vector2(_position.X + enemyposition.X, _position.Y + enemyposition.Y));
            Debug.WriteLine("Draw!");
            _explosionBatch.End();
        }
    }
}