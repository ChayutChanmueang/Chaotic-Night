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
        public EventHandler ScreenEvent;
        public bool IsAlreadyLoaded = false;

        //Screen Fading
        protected float ScreenOpa = 1;
        protected Texture2D ScreenHider;
        Color[] CData = new Color[1920 * 1080];
        protected float TimeProg = 0;
        protected bool FadingIsDone= false;

        //GameState
        protected bool IsPaused = false;
        public Screen(Game1 game,EventHandler SEvent)
        {
            ScreenEvent = SEvent;
            ScreenHider = new Texture2D(game.GraphicsDevice, 1920, 1080);
            for(int i=0;i<CData.Length;i++)
            {
                CData[i] = Color.Black;
            }
            ScreenHider.SetData(CData);
        }
        public virtual void Load()
        {

        }
        public virtual void Reload()
        {

        }
        public virtual void Update(GameTime gameTime)
        {

        }
        public virtual void Draw(SpriteBatch _spriteBatch)
        {

        }
        public virtual void ScreenFadeIn(GameTime time)
        {
            TimeProg += (float)time.ElapsedGameTime.TotalSeconds;
            if (ScreenOpa > 0)
            {
                if (TimeProg > 0.07)
                {
                    ScreenOpa -= (float)0.1;
                    TimeProg = 0;
                }
                IsPaused = true;
            }
            else
            {
                ScreenOpa = 0;
                IsPaused = false;
                FadingIsDone = true;
            }

        }
        public virtual void ScreenFadeOut(GameTime time)
        {
            TimeProg += (float)time.ElapsedGameTime.TotalSeconds;
            if (ScreenOpa < 1)
            {
                if (TimeProg > 0.07)
                {
                    ScreenOpa += (float)0.1;
                    TimeProg = 0;
                }
                IsPaused = true;
            }
            else
            {
                ScreenOpa = 1;
                IsPaused = true;
            }
        }
    }
}
