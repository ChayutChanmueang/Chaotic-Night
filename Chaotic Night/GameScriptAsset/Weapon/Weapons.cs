using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Chaotic_Night
{
    class Weapons
    {
        public Texture2D WeaponTexture;
        protected Rectangle HitZone;
        protected Vector2 Origin;
        //protected Matrix HitZoneTransform;
        protected Vector2 HitzonePos;
        protected int HitCount;
        protected int FramePosX = 0;
        protected int FramePosY = 0;
        protected int offsetX;
        protected int offsetY;
        private float Rot;
        public int Damage=25;
        public int CooldownLimit=3;
        private float TotalCooldown;
        public SpriteBatch _SB;
        protected float TimePerFrame = (float)1/20;
        float TotalElapsed;
        public Character Owner;
        public bool Attacking = false;
        public bool Fliped = false;
        public Weapons(Character OwningCharacter)
        {
            Owner = OwningCharacter;
            offsetX = 32;
            offsetY = 32;
            UpdateOrigin(Owner);
        }
        public virtual void Attack()
        {
            if(Attacking==false)
            {
                Attacking = true;
            }
        }
        public virtual void Attack(Character Target)
        {
            if (Attacking == false)
            {
                Attacking = true;
            }
        }
        public virtual void SpecialAttack()
        {
            if (Attacking == false)
            {
                Attacking = true;
            }
        }
        public virtual void SpecialAttack(Character Target)
        {
            if (Attacking == false)
            {
                Attacking = true;
            }
        }
        public void DrawAttackAnim(float Rot,Vector2 CamPos)
        {
            this.Rot = Rot;
            if(Rot<=-1.57||Rot>=1.57)
            {
                _SB.Draw(WeaponTexture, Origin-CamPos, new Rectangle(FramePosX*64, FramePosY* 64, 64, 64), Color.White, Rot, new Vector2(16, 32), 1.0f, SpriteEffects.None, (float)0.5);
            }
            else
            {
                _SB.Draw(WeaponTexture, Origin-CamPos, new Rectangle(FramePosX * 64, FramePosY* 64, 64, 64), Color.White, Rot, new Vector2(16, 32), 1.0f, SpriteEffects.None, (float)0.5);
            }
            if(Rot > -1.04 && Rot < 0)
            {
                offsetX = 32;
                offsetY = -32;
            }
            else if (Rot > -2.08 && Rot < -1.04)
            {
                offsetX = 0;
                offsetY = -32;
            }
            else if (Rot > -3.14 && Rot < -2.08)
            {
                offsetX = -32;
                offsetY = -32;
            }
            else if (Rot < 1.04 && Rot > 0)
            {
                offsetX = 32;
                offsetY = 32;
            }
            else if (Rot < 2.08 && Rot > 1.04)
            {
                offsetX = 0;
                offsetY = 32;
            }
            else if (Rot < 3.14 && Rot > 2.08)
            {
                offsetX = -32;
                offsetY = 32;
            }
        }
        public void Load(ContentManager Content, SpriteBatch SB)
        {
            _SB = SB;
            WeaponTexture = Content.Load<Texture2D>("sword_1-Sheet");
            UpdateHitboxTransformMatrix(Rot);
            UpdateHitzone();
        }
        public void UpdateFrame (float time)
        {
            TotalElapsed += time;
            if(TotalElapsed>TimePerFrame)
            {
                FramePosX = (FramePosX + 1) % 8;
                TotalElapsed -= TimePerFrame;
            }
            if(FramePosX>=7)
            {
                FramePosX = 0;
                Attacking = false;
            }
        }
        public void UpdateWeapon(float time)
        {
            if(Attacking==true)
            {
                if (TotalCooldown < CooldownLimit)
                {
                    TotalCooldown += time;
                }
                else
                {
                    Attacking = false;
                    TotalCooldown -= CooldownLimit;
                }
            }
            UpdateHitboxTransformMatrix(Rot);
            UpdateHitzone();
            UpdateOrigin(Owner);
        }
        public void SetFrame (int Frame)
        {
        }
        public int GetFrame()
        {
            return FramePosX;
        }
        public bool GetAttacking()
        {
            return Attacking;
        }
        public void UpdateHitboxTransformMatrix(float Rot)
        {
            /*Vector2 Ori = new Vector2(32, 32);
            Vector2 Pos = new Vector2(Owner.GetPos().X + offsetX, Owner.GetPos().Y+offsetY );
            HitZoneTransform = Matrix.CreateTranslation(new Vector3(-Ori,0)) *
                               Matrix.CreateScale((float)0.25) * Matrix.CreateRotationZ(Rot) *
                               Matrix.CreateTranslation(new Vector3(Pos,0));*/

            if(Rot>=-2.335&&Rot<=-0.785)
            {
                HitzonePos = new Vector2(Owner.WeaponPos.X + 16, Owner.WeaponPos.Y-32);
            }
            if (Rot > -0.785 && Rot <= 0)
            {
                HitzonePos = new Vector2(Owner.WeaponPos.X + 32, Owner.WeaponPos.Y - 32);
            }
            if (Rot >= -3.14 && Rot < -2.335)
            {
                HitzonePos = new Vector2(Owner.WeaponPos.X, Owner.WeaponPos.Y - 32);
            }
            if (Rot > 0 && Rot <= 0.785)
            {
                HitzonePos = new Vector2(Owner.WeaponPos.X + 32, Owner.WeaponPos.Y + 16);
            }
            if (Rot > 0.785 && Rot <= 2.355)
            {
                HitzonePos = new Vector2(Owner.WeaponPos.X +16 , Owner.WeaponPos.Y + 16);
            }
            if (Rot > 2.355 && Rot <= 3.14)
            {
                HitzonePos = new Vector2(Owner.WeaponPos.X , Owner.WeaponPos.Y + 16);
            }

        }
        protected void UpdateHitzone()
        {
            /*Vector2 TopLeft = Vector2.Transform(Vector2.Zero, HitZoneTransform);
            Vector2 TopRight = Vector2.Transform(new Vector2(Origin.X, 0), HitZoneTransform);
            Vector2 BottomLeft = Vector2.Transform(new Vector2(0, Origin.Y),HitZoneTransform);
            Vector2 BottomRight = Vector2.Transform(Origin, HitZoneTransform);


            Vector2 min = new Vector2(MathEx.Min(TopLeft.X, TopRight.X, BottomLeft.X, BottomRight.X),
                                      MathEx.Min(TopLeft.Y, TopRight.Y, BottomLeft.Y, BottomRight.Y));
            Vector2 max = new Vector2(MathEx.Max(TopLeft.X, TopRight.X, BottomLeft.X, BottomRight.X),
                                      MathEx.Max(TopLeft.Y, TopRight.Y, BottomLeft.Y, BottomRight.Y));

            if(MathEx.Min(TopLeft.X, TopRight.X, BottomLeft.X, BottomRight.X)> Origin.X + 64)
            {
                min.X = Origin.X + 64;
            }
            if (MathEx.Min(TopLeft.X, TopRight.X, BottomLeft.X, BottomRight.X) < Origin.X - 64)
            {
                min.X = Origin.X - 64;
            }
            if (MathEx.Min(TopLeft.Y, TopRight.Y, BottomLeft.Y, BottomRight.Y) > Origin.Y + 64)
            {
                min.Y = Origin.Y + 64;
            }
            if (MathEx.Min(TopLeft.Y, TopRight.Y, BottomLeft.Y, BottomRight.Y) < Origin.Y - 64)
            {
                min.Y = Origin.Y - 64;
            }

            HitZone = new Rectangle((int)min.X, (int)min.Y, 64, 64);*/

            HitZone = new Rectangle((int)HitzonePos.X, (int)HitzonePos.Y, 64, 64);
        }
        protected bool CheckHit(Character Target)
        {
            return HitZone.Intersects(Target.GetHitbox());
        }
        public Rectangle GetHitzone()
        {
            return HitZone;
        }
        public int GetHitCount()
        {
            return HitCount;
        }
        public int GetHitboxX()
        {
            return Owner.GetHitbox().X;
        }
        public int GetHitboxY()
        {
            return Owner.GetHitbox().Y;
        }
        public void UpdateOrigin(Character Owner)
        {
            Origin = Owner.GetPos() + new Vector2(32, 32);
        }
    }
}
