using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Chaotic_Night
{
    class RangeUlti : Bullet
    {
        public RangeUlti(Vector2 SpawnPos, Texture2D Tex, float Rot, int Dmg) : base(SpawnPos, Tex, Rot, Dmg)
        {
            if (Rot > -0.785 && Rot < 0.785) //-45 - 45
            {
                Pos = new Vector2(SpawnPos.X, SpawnPos.Y - 72);
            }
            else if (Rot > -1.57 && Rot < -0.785)
            {
                Pos = new Vector2(SpawnPos.X - 72, SpawnPos.Y);
            }
            else if ((Rot > -3.14 && Rot < -1.57) || (Rot > 1.57 && Rot < 3.14))
            {
                Pos = new Vector2(SpawnPos.X, SpawnPos.Y + 72);
            }
            else if (Rot > 0.785 && Rot < 1.57)
            {
                Pos = new Vector2(SpawnPos.X + 72, SpawnPos.Y);
            }
            Velocity = new Vector2((float)Math.Cos(Rot), (float)Math.Sin(Rot)) * Speed;
            Hitbox = new Rectangle((int)Pos.X, (int)Pos.Y, 144, 144);
            BulletTex = Tex;
            Rotation = Rot;
            Damage = Dmg;
        }
        public override void Draw(SpriteBatch SB, Vector2 CamPos)
        {
            //SB.Draw(BulletTex, Pos-CamPos, Color.White);
            SB.Draw(BulletTex, Pos - CamPos, new Rectangle(144, 2016, 144, 144), Color.White, Rotation, Vector2.Zero, 1, SpriteEffects.None, 0);
        }
        public override void Update(float time)
        {
            Pos += Velocity;
            Hitbox = new Rectangle((int)Pos.X, (int)Pos.Y, 144, 144);
        }
    }
}
