using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.Collisions;

namespace Chaotic_Night.GameScriptAsset.GameObject
{
    class TileMapObj
    {
        private readonly Game1 game;
        public IShapeF Bound { get; }
        public TileMapObj(Game1 game,RectangleF rectangleF)
        {
            this.game = game;
            Bound = rectangleF;
        }
        public virtual void Update(GameTime gameTime)
        {

        }
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawRectangle((RectangleF)Bound, Color.Red, 3);
        }
        public void OnCollision(CollisionEventArgs collisionInfo)
        {

        }
    }
}
