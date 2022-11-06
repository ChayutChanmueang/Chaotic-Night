using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Chaotic_Night
{
    class NextLevelButton : Button
    {
        public Screen NextScreen;
        SpriteFont font;
        public String NextScreenName;
        int Type;
        public NextLevelButton(Screen _NextScreen,SpriteFont _font,int X,int Y,String NSN) : base(_font,X,Y)
        {
            NextScreen = _NextScreen;
            font = _font;
            NextScreenName = NSN;
            Type = 1;
        }
        public override void Draw(Vector2 CamPos)
        {
            SB.Draw(ObjectTexture, ObjectPos,new Rectangle(0,Type*72,216,72), Color.White);
            SB.DrawString(font, NextScreenName, new Vector2(ObjectPos.X+30, ObjectPos.Y+18), Color.White);
            if(IsSelected==true)
            {
                SB.Draw(ObjectTexture, ObjectPos, new Rectangle(Frame*216, 144, 216, 72), Color.White);
                Type = 0;
            }
            else
            {
                Type = 1;
            }
        }
    }
}
