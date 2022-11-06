using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Chaotic_Night
{
    class Exit_Button : Button
    {
        SpriteFont font;
        int Type;
        protected Game1 game;
        public Exit_Button(Game1 _game, SpriteFont _font, int X, int Y) : base(_font, X, Y)
        {
            font = _font;
            Type = 1;
            game = _game;
        }
        public override void Draw(Vector2 CamPos)
        {
            SB.Draw(ObjectTexture, ObjectPos, new Rectangle(0, Type * 72, 216, 72), Color.White);
            SB.DrawString(font, "Exit", new Vector2(ObjectPos.X + 30, ObjectPos.Y + 18), Color.White);
            if (IsSelected == true)
            {
                SB.Draw(ObjectTexture, ObjectPos, new Rectangle(Frame * 216, 144, 216, 72), Color.White);
                Type = 0;
            }
            else
            {
                Type = 1;
            }
        }
    }
}
