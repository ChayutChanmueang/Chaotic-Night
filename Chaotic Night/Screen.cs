using System;
using System.Collections.Generic;
using Roy_T.AStar.Grids;
using Roy_T.AStar.Primitives;
using Roy_T.AStar.Paths;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Chaotic_Night
{
    public class Screen
    {
        protected EventHandler ScreenEvent;
        public Screen(EventHandler SEvent)
        {
            ScreenEvent = SEvent;
        }
        public virtual void Update(GameTime gameTime)
        {

        }
        public virtual void Draw(SpriteBatch _spriteBatch)
        {

        }
    }
}
