using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Chaotic_Night
{
    class AI_Range_Wep : RangeWeapons
    {
        public AI_Range_Wep(Character OwningCharacter) : base(OwningCharacter)
        {
            Owner = OwningCharacter;
            Fliped = true;
            FrameEnd = 5;
            TexOrigin = new Vector2(-16, 32);
            UpdateOrigin(Owner);
            UpdateAnim = true;
            FramePosX = 1;
            Bullets = new List<Bullet>();
            BaseDamage = 5;
        }
        public override void Load(ContentManager Content, SpriteBatch SB)
        {
            _SB = SB;
            WeaponTexture = Content.Load<Texture2D>("gun-Sheet");
            UpdateHitboxTransformMatrix(Rot);
            UpdateHitzone();
        }
        public override void Attack(Character Target)
        {
            if (Attacking == false)
            {
                HitCount++;
                UpdateAnim = true;
                CalculateDamage();
                Vector2 dPos =  Target.CharacterPos - Owner.CharacterPos;
                float Rot = (float)Math.Atan2(dPos.Y, dPos.X);
                Bullets.Add(new Imp_FireBall(Owner.GetOrigin(), Owner.CharacterTexture, Rot, Damage));
            }
        }
        public override void DrawAttackAnim(float Rot, Vector2 CamPos)
        {
            foreach (Bullet i in Bullets)
            {
                i.Draw(_SB, CamPos);
            }
        }
    }
}
