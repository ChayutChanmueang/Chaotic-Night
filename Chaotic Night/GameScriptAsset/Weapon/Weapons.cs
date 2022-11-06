using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Chaotic_Night
{
    public class Weapons
    {
        public Texture2D WeaponTexture;
        protected Rectangle HitZone;
        protected Vector2 Origin;
        protected List<Bullet> Bullets;
        public List<PopUpDamage> Damages = new List<PopUpDamage>();
        Random RAND;
        //protected Matrix HitZoneTransform;
        protected Vector2 HitzonePos;
        protected int HitCount;
        public int FramePosX = 0;
        public int FramePosY = 0;
        protected int offsetX;
        protected int offsetY;
        protected float Rot;
        protected int BaseDamage=25;
        protected int BaseSAtkDamage = 50;
        public int SAtkCost = 25;
        public int Damage;
        public float CooldownLimit=3;
        protected float TotalCooldown;
        public SpriteBatch _SB;
        protected float TimePerFrame = (float)1/15;
        public int FrameEnd;
        protected Vector2 TexOrigin;
        protected float TotalElapsed;
        public Character Owner;
        public bool Attacking = false;
        public bool Fliped = false;
        public bool UpdateAnim = false;
        public bool SAtkCharging = false;
        public Weapons(Character OwningCharacter)
        {
            Owner = OwningCharacter;
            offsetX = 32;
            offsetY = 32;
            TexOrigin = new Vector2(16, 32);
            UpdateOrigin(Owner);
            RAND = new Random();
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
        public virtual void DrawAttackAnim(float Rot,Vector2 CamPos)
        {
            this.Rot = Rot;
            if(Fliped==true)
            {
                _SB.Draw(WeaponTexture, (Origin) -CamPos, new Rectangle(FramePosX*64, FramePosY* 64, 64, 64), Color.White, Rot, Vector2.Zero, 1.0f, SpriteEffects.FlipHorizontally, (float)0.5);
            }
            else
            {
                _SB.Draw(WeaponTexture, (Origin)-CamPos, new Rectangle(FramePosX * 64, FramePosY* 64, 64, 64), Color.White, Rot, Vector2.Zero, 1.0f, SpriteEffects.None, (float)0.5);
            }
            if(Rot > -1.04 && Rot < 0)
            {
                offsetX = 32;
                offsetY = -32;
                HitZone = new Rectangle((int)HitzonePos.X, (int)HitzonePos.Y, 64, 96);
            }
            else if (Rot > -2.08 && Rot < -1.04)
            {
                offsetX = 0;
                offsetY = -32;
                HitZone = new Rectangle((int)HitzonePos.X, (int)HitzonePos.Y, 64, 64);
            }
            else if (Rot > -3.14 && Rot < -2.08)
            {
                offsetX = -32;
                offsetY = -32;
                HitZone = new Rectangle((int)HitzonePos.X, (int)HitzonePos.Y, 64, 96);
            }
            else if (Rot < 1.04 && Rot > 0)
            {
                offsetX = 32;
                offsetY = 32;
                HitZone = new Rectangle((int)HitzonePos.X, (int)HitzonePos.Y, 64, 96);
            }
            else if (Rot < 2.08 && Rot > 1.04)
            {
                offsetX = 0;
                offsetY = 32;
                HitZone = new Rectangle((int)HitzonePos.X, (int)HitzonePos.Y, 64, 64);
            }
            else if (Rot < 3.14 && Rot > 2.08)
            {
                offsetX = -32;
                offsetY = 32;
                HitZone = new Rectangle((int)HitzonePos.X, (int)HitzonePos.Y, 64, 96);
            }
        }
        public virtual void Load(ContentManager Content, SpriteBatch SB)
        {
            _SB = SB;
            WeaponTexture = Content.Load<Texture2D>("Combo-1_2_25_3");
            UpdateHitboxTransformMatrix(Rot);
            UpdateHitzone();
        }
        public virtual void UpdateFrame (float time)
        {
            TotalElapsed += time;
            if (TotalElapsed > TimePerFrame)
            {
                FramePosX = (FramePosX + 1) % 8;
                TotalElapsed -= TimePerFrame;
            }
            if (FramePosX >= FrameEnd)
            {
                FramePosX = 0;
                Attacking = false;
                UpdateAnim = false;
            }

        }
        public virtual void UpdateWeapon(float time)
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
                    UpdateAnim = false;
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
        public virtual void UpdateHitboxTransformMatrix(float Rot)
        {
            /*Vector2 Ori = new Vector2(32, 32);
            Vector2 Pos = new Vector2(Owner.GetPos().X + offsetX, Owner.GetPos().Y+offsetY );
            HitZoneTransform = Matrix.CreateTranslation(new Vector3(-Ori,0)) *
                               Matrix.CreateScale((float)0.25) * Matrix.CreateRotationZ(Rot) *
                               Matrix.CreateTranslation(new Vector3(Pos,0));*/

            /*if(Rot>=-2.335&&Rot<=-0.785)
            {
                HitzonePos = new Vector2(Owner.WeaponPos.X + (Owner.CharacterWidth / 2), Owner.WeaponPos.Y- (Owner.CharacterHeight / 2));
            }
            if (Rot > -0.785 && Rot <= 0) //45 Deg
            {
                HitzonePos = new Vector2(Owner.WeaponPos.X + Owner.CharacterWidth, Owner.WeaponPos.Y - (Owner.CharacterHeight / 2));
            }
            if (Rot >= -3.14 && Rot < -2.335)
            {
                HitzonePos = new Vector2(Owner.WeaponPos.X, Owner.WeaponPos.Y - (Owner.CharacterHeight / 2));
            }
            if (Rot > 0 && Rot <= 0.785)
            {
                HitzonePos = new Vector2(Owner.WeaponPos.X + Owner.CharacterWidth, Owner.WeaponPos.Y + (Owner.CharacterHeight / 2));
                if(Owner.GetType() == typeof(PlayableCharacter))
                {
                    HitzonePos = new Vector2(Owner.WeaponPos.X + Owner.CharacterWidth, Owner.WeaponPos.Y + 30);
                }
            }
            if (Rot > 0.785 && Rot <= 2.355)
            {
                HitzonePos = new Vector2(Owner.WeaponPos.X + (Owner.CharacterWidth / 2), Owner.WeaponPos.Y + (Owner.CharacterHeight / 2));
            }
            if (Rot > 2.355 && Rot <= 3.14)
            {
                HitzonePos = new Vector2(Owner.WeaponPos.X , Owner.WeaponPos.Y + (Owner.CharacterHeight / 2));
                if (Owner.GetType() == typeof(PlayableCharacter))
                {
                    HitzonePos = new Vector2(Owner.WeaponPos.X, Owner.WeaponPos.Y + 30);
                }
            }*/

            if (Rot > -1.04 && Rot < 0) //60
            {
                HitzonePos = new Vector2(Owner.WeaponPos.X+Owner.CharacterWidth/1.25f, Owner.WeaponPos.Y);
            }
            else if (Rot > -2.08 && Rot < -1.04) //120
            {
                HitzonePos = new Vector2(Owner.WeaponPos.X + Owner.CharacterWidth/2.5f, Owner.WeaponPos.Y);
            }
            else if (Rot > -3.14 && Rot < -2.08) //180
            {
                HitzonePos = new Vector2(Owner.WeaponPos.X, Owner.WeaponPos.Y);
            }
            else if (Rot < 1.04 && Rot > 0) //-60
            {
                HitzonePos = new Vector2(Owner.WeaponPos.X + Owner.CharacterWidth/1.25f, Owner.WeaponPos.Y+Owner.CharacterHeight/2);
            }
            else if (Rot < 2.08 && Rot > 1.04) //-120
            {
                HitzonePos = new Vector2(Owner.WeaponPos.X + Owner.CharacterWidth / 2.25f, Owner.WeaponPos.Y + Owner.CharacterHeight/2);
            }
            else if (Rot < 3.14 && Rot > 2.08) //-180
            {
                HitzonePos = new Vector2(Owner.WeaponPos.X, Owner.WeaponPos.Y + Owner.CharacterHeight/2);
            }

        }
        protected virtual void UpdateHitzone()
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
            if(Owner.GetType()==typeof(PlayableCharacter))
            {
                if (Rot > -1.04 && Rot < 0)
                {
                    HitZone = new Rectangle((int)HitzonePos.X, (int)HitzonePos.Y, 64, 96);
                }
                else if (Rot > -2.08 && Rot < -1.04)
                {
                    HitZone = new Rectangle((int)HitzonePos.X, (int)HitzonePos.Y, 64, 96);
                }
                else if (Rot > -3.14 && Rot < -2.08)
                {
                    HitZone = new Rectangle((int)HitzonePos.X, (int)HitzonePos.Y, 64, 96);
                }
                else if (Rot < 1.04 && Rot > 0)
                {
                    HitZone = new Rectangle((int)HitzonePos.X, (int)HitzonePos.Y, 64, 96);
                }
                else if (Rot < 2.08 && Rot > 1.04)
                {
                    HitZone = new Rectangle((int)HitzonePos.X, (int)HitzonePos.Y, 64, 96);
                }
                else if (Rot < 3.14 && Rot > 2.08)
                {
                    HitZone = new Rectangle((int)HitzonePos.X, (int)HitzonePos.Y, 64, 96);
                }
            }
        }
        public virtual bool CheckHit(Character Target)
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
            Origin = Owner.GetOrigin();
        }
        public virtual List<Bullet> GetBullets()
        {
            return Bullets;
        }
        protected void CalculateDamage()
        {
            Damage = RAND.Next(BaseDamage - 2, BaseDamage + 5)-(int)Owner._game.PlayerRelic.MeleeDamageResistance;
            if(Owner.GetType()==typeof(PlayableCharacter))
            {
                Damage = (int)(RAND.Next(BaseDamage - 2, BaseDamage + 5) * Owner._game.LVDamageMuti);
            }
        }
        protected void CalculateDamage(int _BaseDamage)
        {
            Damage = RAND.Next(_BaseDamage - 2, _BaseDamage + 5);
            if (Owner.GetType() == typeof(PlayableCharacter))
            {
                Damage = (int)(RAND.Next(_BaseDamage - 2, _BaseDamage + 5) * Owner._game.LVDamageMuti);
            }
        }
        public void ClearBullet()
        {
            if(Bullets!=null)
            {
                for (int i = Bullets.Count; i > 0; i--)
                {
                    Bullets.RemoveAt(i - 1);
                }
            }
        }
    }
}
