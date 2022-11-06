using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Chaotic_Night
{
    class AI_Melee_Wep : Weapons
    {
        public bool AllowCombo = false;
        bool Attacked = false;
        Random RAND;
        public AI_Melee_Wep(Character OwningCharacter) : base(OwningCharacter)
        {
            Owner = OwningCharacter;
            FrameEnd = 6;
            RAND = new Random();
            CooldownLimit = 0.30f;
            TexOrigin = new Vector2(108, 108);
            BaseDamage = 15;
            Bullets = new List<Bullet>();
        }
        public override void Load(ContentManager Content, SpriteBatch SB)
        {
            _SB = SB;
            WeaponTexture = Content.Load<Texture2D>("All-Combo");
            UpdateHitboxTransformMatrix(Rot);
            UpdateHitzone();
        }
        public override void DrawAttackAnim(float Rot, Vector2 CamPos)
        {
            this.Rot = Rot;
            foreach (Bullet i in Bullets)
            {
                i.Draw(_SB, CamPos);
            }
        }
        public override void Attack(Character Target)
        {
            if (Attacking == false)
            {
                Attacked = true;
                if (CheckHit(Target))
                {
                    CalculateDamage();
                    Target.SubtraceHP(Damage);
                    HitCount++;
                    UpdateAnim = true;
                    /*if (Target.HealthPoint >= 0)
                    {
                        Damages.Add(new PopUpDamage(Damage, Target.CharacterPos, false, Target));
                    }*/
                }
                else
                {
                    UpdateAnim = true;
                    Attacking = false;
                }
            }
        }
        public override void UpdateFrame(float time)
        {
            TotalElapsed += time;
            if (TotalElapsed > TimePerFrame)
            {
                FramePosX = (FramePosX + 1) % 6;
                TotalElapsed -= TimePerFrame;
            }
            if(FramePosX>=6)
            {
                Attacking = false;
                UpdateAnim = false;
            }
        }
        public override void UpdateWeapon(float time)
        {
            if (Attacking == true)
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
            foreach (Bullet i in Bullets)
            {
                i.Update(time);
            }
            UpdateHitboxTransformMatrix(Rot);
            UpdateHitzone();
            UpdateOrigin(Owner);
        }
        public override void UpdateHitboxTransformMatrix(float Rot)
        {
            if(Rot > -1.57 && Rot < 1.57)
            {
                HitzonePos = new Vector2(Owner.CharacterPos.X,Owner.CharacterPos.Y);
            }
            if ((Rot > -3.14 && Rot < -1.57) || (Rot > 1.57 && Rot < 3.14))
            {
                HitzonePos = new Vector2(Owner.CharacterPos.X- Owner.CharacterWidth / 2, Owner.CharacterPos.Y);
            }
        }
        protected override void UpdateHitzone()
        {
            HitZone = new Rectangle((int)HitzonePos.X, (int)HitzonePos.Y, 280, 216);
        }
    }
}
