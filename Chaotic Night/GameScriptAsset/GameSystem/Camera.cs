using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Chaotic_Night
{
    public class Camera
    {
        public Vector2 CamPos;
        int ScreenW;
        int ScreenH;
        public Camera(int ScreenW,int ScreenH)
        {
            CamPos = Vector2.Zero;
            this.ScreenW = ScreenW;
            this.ScreenH = ScreenH;
        }
        public void GoTo(Vector2 WTargetPos)
        {
            Vector2 CamTargetPos = WTargetPos - new Vector2(ScreenW/2,ScreenH/2);

            CamPos = Vector2.Lerp(CamPos, CamTargetPos, 0.3f);
        }
        public void FollowCharacter(Character Target)
        {
            GoTo(Target.GetPos());
        }
        public Vector2 GetCamPos()
        {
            return CamPos;
        }
    }
}
