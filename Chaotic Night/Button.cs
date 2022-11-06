using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Chaotic_Night
{
    class Button : InteractableObject
    {
        SpriteFont font;
        protected float TimePerFrame = (float)1 / 5;
        protected int FrameEnd;
        float TotalElapsed;
        protected int EndFrame = 4;
        protected int Frame = 0;
        public Button(SpriteFont _font,int X,int Y) : base(X,Y)
        {
            font = _font;
        }
        public virtual void UpdateFrame(float time)
        {
            TotalElapsed += time;
            if (TotalElapsed > TimePerFrame)
            {
                Frame = (Frame + 1) % EndFrame;
                TotalElapsed -= TimePerFrame;
            }
        }
    }
}
