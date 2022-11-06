using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Chaotic_Night
{
    public class PopUpDamage
    {
        int Damage;
        Vector2 Pos;
        bool IsCrit = false;
        float Lifespan = 1;
        float time = 0;
        Character Owner;
        public PopUpDamage(int Dmg,Vector2 pos, bool crit,Character owner)
        {
            Damage = Dmg;
            Pos = pos;
            IsCrit = crit;
            Owner = owner;
        }
        public void Draw(SpriteBatch SB,SpriteFont Font, Vector2 CamPos)
        {
            if (IsCrit)
            {
                SB.DrawString(Font, Damage.ToString(), Pos - CamPos, Color.Red);
            }
            else
            {
                SB.DrawString(Font, Damage.ToString(), Pos - CamPos, Color.White);
            }
        }
        public void Update(float gameTime)
        {
            Pos.Y -= 1;
            time += (float)gameTime;
        }
        public bool CheckIfDied()
        {
            if(time>Lifespan)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
