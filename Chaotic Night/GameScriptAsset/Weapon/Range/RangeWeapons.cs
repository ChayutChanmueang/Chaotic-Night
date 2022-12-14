using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Chaotic_Night
{
    public class RangeWeapons : Weapons
    {
        Texture2D BulletTex;
        public RangeWeapons(Character OwningCharacter) : base(OwningCharacter)
        {
            Owner = OwningCharacter;
            Fliped = true;
            FrameEnd = 5;
            TexOrigin = new Vector2(-16, 32);
            UpdateOrigin(Owner);
            UpdateAnim = true;
            FramePosX = 1;
            Bullets = new List<Bullet>();
            SAtkCost = 50;
        }
        public override void Attack(Character Target)
        {
            if (Attacking == false)
            {
                HitCount++;
                UpdateAnim = true;
                CalculateDamage();
                Bullets.Add(new PlayerBullet(Owner.GetOrigin(), Owner.CharacterTexture, Owner.WeaponRot,Damage));
            }
        }
        public override void SpecialAttack(Character Target)
        {
            if (Attacking == false)
            {
                HitCount++;
                UpdateAnim = true;
                CalculateDamage(75);
                Bullets.Add(new RangeUlti(Owner.GetOrigin(), Owner.CharacterTexture, Owner.WeaponRot, Damage));
            }
        }
        public override void Load(ContentManager Content, SpriteBatch SB)
        {
            _SB = SB;
            WeaponTexture = Content.Load<Texture2D>("gun-Sheet");
            BulletTex = Content.Load<Texture2D>("Arm");
            UpdateHitboxTransformMatrix(Rot);
            UpdateHitzone();
        }
        public override void DrawAttackAnim(float Rot, Vector2 CamPos)
        {
            //base.DrawAttackAnim(Rot, CamPos);
            foreach (Bullet i in Bullets)
            {
                i.Draw(_SB, CamPos);
            }
        }
        public override List<Bullet> GetBullets()
        {
            return Bullets;
        }
        public override void UpdateWeapon(float time)
        {
            base.UpdateWeapon(time);
            foreach (Bullet i in Bullets)
            {
                i.Update(time);
            }
        }
    }
}
