using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Chaotic_Night
{
    public class BulletShooter
    {
        public List<Bullet> Bullets = new List<Bullet>();
        Vector2 Pos;
        Texture2D BulletTex;
        public bool IsShooted = true;
        public float Cooldown;
        public float TotalCooldown = 0;
        float BulletRot;
        public BulletShooter(Vector2 pos,float cooldown,float Rot,Game1 game)
        {
            Pos = pos;
            Cooldown = cooldown;
            BulletRot = Rot;
            BulletTex = game.Content.Load<Texture2D>("Small-Imp");
        }
        public void Update(float time)
        {
            if(IsShooted == true)
            {
                TotalCooldown += time;
                if(TotalCooldown>=Cooldown)
                {
                    TotalCooldown = 0;
                    IsShooted = false;
                }
            }
            else if(IsShooted == false)
            {
                Shoot();
                IsShooted = true;
            }
            foreach(Bullet bullet in Bullets)
            {
                bullet.Update(time);
            }
        }
        public void Shoot()
        {
            Bullets.Add(new Imp_FireBall(new Vector2(Pos.X+24, Pos.Y), BulletTex, BulletRot, 10));
        }
        public void ClearBullet()
        {
            if (Bullets != null)
            {
                for (int i = Bullets.Count; i > 0; i--)
                {
                    Bullets.RemoveAt(i - 1);
                }
            }
        }
    }
}
