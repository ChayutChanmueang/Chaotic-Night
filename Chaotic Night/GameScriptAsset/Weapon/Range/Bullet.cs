using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Chaotic_Night
{
    public class Bullet
    {
        public Texture2D BulletTex;
        public Vector2 Pos;
        public Vector2 Velocity;
        public Rectangle Hitbox;
        public float Rotation;
        public int Damage;
        protected int FramePosX;
        protected int FramePosY;
        protected float TimePerFrame = (float)1 / 5;
        protected int FrameEnd;
        protected float TotalElapsed;
        public int EndFrame = 6;
        protected float Speed = 15;
        public Bullet(Vector2 SpawnPos,Texture2D Tex,float Rot,int Dmg)
        {
            Pos = SpawnPos;
            Velocity = new Vector2((float)Math.Cos(Rot), (float)Math.Sin(Rot)) * Speed;
            Hitbox = new Rectangle((int)Pos.X, (int)Pos.Y, 16, 8);
            BulletTex = Tex;
            Rotation = Rot;
            Damage = Dmg;
        }
        public virtual void Draw(SpriteBatch SB,Vector2 CamPos)
        {
            //SB.Draw(BulletTex, Pos-CamPos, Color.White);
            SB.Draw(BulletTex, Pos - CamPos, null, Color.White,Rotation, Vector2.Zero, 1, SpriteEffects.None, 0);
        }
        public virtual void Update(float time)
        {
            Pos += Velocity;
            Hitbox = new Rectangle((int)Pos.X, (int)Pos.Y, 16, 8);
        }
        public virtual void UpdateFrame(float time)
        {
            TotalElapsed += time;
            if (TotalElapsed > TimePerFrame)
            {
                FramePosX = (FramePosX + 1) % EndFrame;
                TotalElapsed -= TimePerFrame;
            }
        }
    }
}
