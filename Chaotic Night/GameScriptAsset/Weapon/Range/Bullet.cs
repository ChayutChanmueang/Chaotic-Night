using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Chaotic_Night
{
    class Bullet
    {
        Texture2D BulletTex;
        Vector2 Pos;
        Vector2 Velocity;
        public Rectangle Hitbox;
        float Rotation;
        public Bullet(Vector2 SpawnPos,Texture2D Tex,float Rot)
        {
            Pos = SpawnPos;
            Velocity = new Vector2((float)Math.Cos(Rot), (float)Math.Sin(Rot)) * 5f;
            Hitbox = new Rectangle((int)Pos.X, (int)Pos.Y, 16, 8);
            BulletTex = Tex;
            Rotation = Rot;
        }
        public void Draw(SpriteBatch SB,Vector2 CamPos)
        {
            //SB.Draw(BulletTex, Pos-CamPos, Color.White);
            SB.Draw(BulletTex, Pos - CamPos, null, Color.White,Rotation, Vector2.Zero, 1, SpriteEffects.None, 0);
        }
        public void Update()
        {
            Pos += Velocity;
            Hitbox = new Rectangle((int)Pos.X, (int)Pos.Y, 16, 8);
        }
    }
}
