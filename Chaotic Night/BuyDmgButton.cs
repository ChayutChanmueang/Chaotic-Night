using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Chaotic_Night
{
    class BuyDmgButton : ShopButton
    {
        public BuyDmgButton(Game1 _game,SpriteFont _font, int X, int Y, String IN) : base(_game,_font, X, Y,IN)
        {
            Cost = 750;
        }
        protected override void Interaction()
        {
            base.Interaction();

            if(game.Money >= Cost)
            {
                game.WepDamageMuti += 0.2f;
                game.Money -= Cost;
            }
        }
    }
}
