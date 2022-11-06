using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Chaotic_Night
{
    class PlayableCharacter : MovableCharacter
    {
        protected Weapons HoldedWeapon;
        public PlayableCharacter () : base()
        {
        }
        public PlayableCharacter(Vector2 Pos) : base(Pos)
        {
        }
        public PlayableCharacter (int Health) : base(Health)
        {
        }
        public PlayableCharacter(Vector2 Pos, int Health) : base(Pos, Health)
        {
        }
        public override void Load(ContentManager Content, SpriteBatch _SB)
        {
            CharacterTexture = Content.Load<Texture2D>("main_character_V_1");
            CharacterWidth = 64;
            CharacterHeight = 64;
            Hitbox = new Rectangle((int)CharacterPos.X, (int)CharacterPos.Y, CharacterWidth, CharacterHeight);
            HoldedWeapon = new MeleeWeapons(this);
            HoldedWeapon.Load(Content, _SB);
        }
        public void DrawCharacter(SpriteBatch SB,float Rot,Vector2 CamPos)
        {
            if (Fliped)
            {
                SB.Draw(CharacterTexture, CharacterPos-CamPos, null, Color.White, 0, Vector2.Zero, 1, SpriteEffects.FlipHorizontally, 0);
                HoldedWeapon.DrawAttackAnim(Rot,CamPos);
            }
            else
            {
                SB.Draw(CharacterTexture, CharacterPos-CamPos, null, Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
                HoldedWeapon.DrawAttackAnim(Rot,CamPos);
            }
        }
        public void DoM1Action(Character Target)
        {
            HoldedWeapon.Attack(Target);
        }
        public void DoM2Action(Character Target)
        {
            if (SP >= 25)
            {
                SP -= 25;
            }
            HoldedWeapon.SpecialAttack(Target);
            SP -= 25;
        }
        public bool GetAttacking()
        {
            return HoldedWeapon.GetAttacking();
        }
        public void UpdateWeaponFrame(float time)
        {
            HoldedWeapon.UpdateFrame(time);
        }
        public Weapons GetWeapon()
        {
            return HoldedWeapon;
        }
        public int GetHitCount()
        {
            return HoldedWeapon.GetHitCount();
        }
        public override void UpdateCharacter(float time)
        {
            Hitbox = new Rectangle((int)CharacterPos.X+16, (int)CharacterPos.Y+8, 34, 50);
            PreHitbox = new Rectangle((int)CharacterPos.X + 16 - 1, (int)CharacterPos.Y + 8 - 1, 35, 51);
            CharacterOrigin = CharacterPos + new Vector2(CharacterTexture.Width/2, CharacterTexture.Height / 2);
            HoldedWeapon.UpdateWeapon(time);
        }
        public Vector2 GetWeaponHitzoneVector()
        {
            return new Vector2(HoldedWeapon.GetHitzone().X, HoldedWeapon.GetHitzone().Y);
        }
    }
}
