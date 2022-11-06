using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Chaotic_Night
{
    class GameObj_IgnoreBullets : GameObject
    {
        public GameObj_IgnoreBullets():base()
        {
        }
        public GameObj_IgnoreBullets(int X, int Y):base(X,Y)
        {
        }
        public GameObj_IgnoreBullets(Vector2 Pos):base(Pos)
        {
        }
    }
}
