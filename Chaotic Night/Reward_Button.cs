using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Chaotic_Night
{
    class Reward_Button : Button
    {
        SpriteFont font;
        public String ItemName;
        int Type;
        protected Game1 game;
        int FramePosX = 0;
        int FramePosY = 0;
        public Reward_Button(Game1 _game, SpriteFont _font, int X, int Y,int FrameX,int FrameY) : base(_font, X, Y)
        {
            font = _font;
            game = _game;
            FramePosX = FrameX;
            FramePosY = FrameY;
        }
        public override void Draw(Vector2 CamPos)
        {
            SB.Draw(ObjectTexture, ObjectPos, new Rectangle(350*FramePosX, 350*FramePosY, 350, 350), Color.White);
            //SB.DrawString(font, ItemName, new Vector2(ObjectPos.X + 30, ObjectPos.Y + 18), Color.White);
        }
        protected override void Interaction()
        {
            if(FramePosY==0)
            {
                if (FramePosX == 0)
                {
                    game.PlayerRelic = new MaxHP_R();
                }
                else if (FramePosX == 1)
                {
                    game.PlayerRelic = new Damage_R();
                }
                else if (FramePosX == 2)
                {
                    game.Money += 500;
                }
                else if (FramePosX == 3)
                {
                    game.PlayerRelic = new NormalEnemyDamage_R();
                }
                else if (FramePosX == 4)
                {
                    game.PlayerRelic = new DropMoney_R();
                }
                else if (FramePosX == 5)
                {
                    game.PlayerRelic = new ManaGain_R();
                }
                else if (FramePosX == 6)
                {
                    game.PlayerRelic = new MeleeResistance_R();
                }
                else if (FramePosX == 7)
                {
                    game.PlayerRelic = new RangeResistance_R();
                }
            }
            if (FramePosY == 1)
            {
                if (FramePosX == 0)
                {
                    game.PlayerRelic = new MaxHP_R2();
                }
                else if (FramePosX == 1)
                {
                    game.PlayerRelic = new Damage_R2();
                }
                else if (FramePosX == 2)
                {
                    game.Money += 1500;
                }
                else if (FramePosX == 3)
                {
                    game.PlayerRelic = new NormalEnemyDamage_R2();
                }
                else if (FramePosX == 4)
                {
                    game.PlayerRelic = new DropMoney_R2();
                }
                else if (FramePosX == 5)
                {
                    game.PlayerRelic = new ManaGain_R2();
                }
                else if (FramePosX == 6)
                {
                    game.PlayerRelic = new MeleeResistance_R2();
                }
                else if (FramePosX == 7)
                {
                    game.PlayerRelic = new RangeResistance_R2();
                }
                }
            }
    }
}
