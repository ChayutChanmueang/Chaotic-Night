using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Chaotic_Night
{
    public class Shopkeeper : InteractableObject
    {
        public int FramePosX = 0;
        public int FramePosY;
        public bool IsInteracted=false;
        protected float TimePerFrame = (float)1 / 5;
        protected int FrameEnd;
        float TotalElapsed;
        protected int EndFrame = 8;
        public bool PlayAnim = true;
        public Shopkeeper(int X, int Y) : base(X, Y)
        {
            
        }
        public override void Draw(Vector2 CamPos)
        {
            SB.Draw(ObjectTexture, ObjectPos-CamPos, new Rectangle(216*FramePosX, 216*FramePosY, 216, 216), Color.White);
            Hitbox = new Rectangle((int)(ObjectPos.X - CamPos.X), (int)(ObjectPos.Y - CamPos.Y), 216, 216);
        }
        public override void Interact()
        {
            FramePosY = 1;
            EndFrame = 6;
            if(FramePosY==1&&FramePosX>=4)
            {
                if (IsSelected)
                {
                    Interaction();
                }
            }
        }
        protected override void Interaction()
        {
            IsInteracted = true;
        }
        public void UpdateFrame(float time)
        {
            if(PlayAnim==true)
            {
                TotalElapsed += time;
                if (TotalElapsed > TimePerFrame)
                {
                    FramePosX = (FramePosX + 1) % EndFrame;
                    TotalElapsed -= TimePerFrame;
                }
            }
            if(FramePosY==2&&FramePosX>=4)
            {
                FramePosX = 0;
                FramePosY = 0;
                EndFrame = 8;
            }
            if (FramePosY == 1 && FramePosX >= 4)
            {
                PlayAnim = false;
            }
        }
    }
}
