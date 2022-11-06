using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Chaotic_Night
{
    class BuyHPButton : ShopButton
    {
        public BuyHPButton(Game1 _game, SpriteFont _font, int X, int Y, String IN) : base(_game, _font, X, Y, IN)
        {
            Cost = 250;
        }
        protected override void Interaction()
        {
            base.Interaction();

            if(game.HP < 100)
            {
                if (game.Money >= Cost)
                {
                    if (game.HP <= 75)
                    {
                        game.HP += 25;
                    }
                    else
                    {
                        game.HP += (100 - game.HP);
                    }
                    game.Money -= Cost;
                }
            }
        }
    }
}
