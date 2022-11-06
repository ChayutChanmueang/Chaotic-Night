using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Chaotic_Night
{
    class ShopButton : Button
    {
        SpriteFont font;
        public String ItemName;
        int Type;
        protected int ItemCost = 0;
        Texture2D ItemTex;
        protected Game1 game;
        protected int Cost;
        String ContentName;
        int W;
        int H;
        protected int FramePosX;
        protected int FramePosY;
        public ShopButton(Game1 _game,SpriteFont _font, int X, int Y, String IN) : base(_font, X, Y)
        {
            font = _font;
            ItemName = IN;
            Type = 1;
            game = _game;
        }
        public override void Load(ContentManager Content, SpriteBatch _SB, string ObjTexName, int Width, int Hight)
        {
            base.Load(Content, _SB, ObjTexName, Width, Hight);
            ContentName = ObjTexName;
            W = Width;
            H = Hight;
        }
        public override void Draw(Vector2 CamPos)
        {
            if(ContentName== "button_selection_1 (1)")
            {
                SB.Draw(ObjectTexture, ObjectPos, new Rectangle(0, Type * 72, 216, 72), Color.White);
                SB.DrawString(font, ItemName, new Vector2(ObjectPos.X + 30, ObjectPos.Y + 18), Color.White);
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
            else if (ContentName == "Relic")
            {
                SB.Draw(ObjectTexture, ObjectPos, new Rectangle(FramePosX*350, FramePosY*350, 350, 350), Color.White);
                SB.DrawString(font, "Cost : " + ItemCost.ToString() + "$", new Vector2(ObjectPos.X + 175, ObjectPos.Y + 360), Color.White);
            }
        }
    }
}
