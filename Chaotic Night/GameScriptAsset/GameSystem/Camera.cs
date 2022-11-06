using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Chaotic_Night
{
    class Camera
    {
        Vector2 CamPos;
        public Camera()
        {
            CamPos = Vector2.Zero;
        }
        public void GoTo(Vector2 WTargetPos)
        {
            Vector2 STargetPos = WTargetPos - new Vector2(1920/2,1080/2);

            CamPos = Vector2.Lerp(CamPos, STargetPos, 0.3f);
            //CamPos = new Vector2(WTargetPos.X - (1920 / 2), WTargetPos.Y - (1080 / 2));
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
