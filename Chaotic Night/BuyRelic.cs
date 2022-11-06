using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Chaotic_Night
{
    class BuyRelic : ShopButton
    {
        public BuyRelic(Game1 _game, SpriteFont _font, int X, int Y, String IN,int FrameX,int FrameY) : base(_game,_font, X, Y,IN)
        {
            FramePosX = FrameX;
            FramePosY = FrameY;
            if(FramePosX==2)
            {
                FramePosX = 1;
            }
            if (FramePosY == 0)
            {
                if (FramePosX == 0)
                {
                    ItemCost = 300;
                }
                else if (FramePosX == 1)
                {
                    ItemCost = 300;
                }
                else if (FramePosX == 3)
                {
                    ItemCost = 200;
                }
                else if (FramePosX == 4)
                {
                    ItemCost = 350;
                }
                else if (FramePosX == 5)
                {
                    ItemCost = 250;
                }
                else if (FramePosX == 6)
                {
                    ItemCost = 300;
                }
                else if (FramePosX == 7)
                {
                    ItemCost = 300;
                }
            }
            if (FramePosY == 1)
            {
                if (FramePosX == 0)
                {
                    ItemCost = 600;
                }
                else if (FramePosX == 1)
                {
                    ItemCost = 600;
                }
                else if (FramePosX == 3)
                {
                    ItemCost = 400;
                }
                else if (FramePosX == 4)
                {
                    ItemCost = 700;
                }
                else if (FramePosX == 5)
                {
                    ItemCost = 500;
                }
                else if (FramePosX == 6)
                {
                    ItemCost = 600;
                }
                else if (FramePosX == 7)
                {
                    ItemCost = 600;
                }
            }
        }
        protected override void Interaction()
        {
            if(game.Money>=ItemCost)
            {
                if (FramePosY == 0)
                {
                    if (FramePosX == 0)
                    {
                        game.PlayerRelic = new MaxHP_R();
                    }
                    else if (FramePosX == 1)
                    {
                        game.PlayerRelic = new Damage_R();
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
                game.Money -= ItemCost;
            }
        }
    }
}
