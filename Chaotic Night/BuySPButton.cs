using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Chaotic_Night
{
    class BuySPButton : ShopButton
    {
        public BuySPButton(Game1 _game, SpriteFont _font, int X, int Y, String IN) : base(_game, _font, X, Y, IN)
        {
            Cost = 250;
        }
        protected override void Interaction()
        {
            base.Interaction();

            if (game.SP < 100)
            {
                if (game.Money >= Cost)
                {
                    if (game.SP <= 75)
                    {
                        game.SP += 25;
                    }
                    else
                    {
                        game.SP += (100 - game.HP);
                    }
                    game.Money -= Cost;
                }
            }
        }
    }
}
