using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Chaotic_Night
{
    class Character
    {
        public Texture2D CharacterTexture;
        protected int CharacterHeight;
        protected int CharacterWidth;
        public Vector2 CharacterPos;
        protected Vector2 CharacterOrigin;
        public int HealthPoint;
        protected Rectangle Hitbox;
        protected Rectangle PreHitbox;
        protected bool IsHit;
        public bool Fliped;
        protected float WeaponOffset = 20;
        public Vector2 WeaponPos;
        public Character ()
        {
            CharacterPos = Vector2.Zero;
            CharacterOrigin = CharacterPos + new Vector2(32, 32);
            HealthPoint = 100;
        }
        public Character(Vector2 Pos)
        {
            CharacterPos = Pos;
            CharacterOrigin = CharacterPos + new Vector2(32, 32);
            HealthPoint = 100;
        }
        public Character(Vector2 Pos,int Health)
        {
            CharacterPos = Pos;
            CharacterOrigin = CharacterPos + new Vector2(32, 32);
            HealthPoint = Health;
        }
        public Character(int Health)
        {
            CharacterPos = Vector2.Zero;
            CharacterOrigin = CharacterPos + new Vector2(32, 32);
            HealthPoint = Health;
        }
        public virtual void Load(ContentManager Content, SpriteBatch _SB)
        {
        }
        public void SetPos(Vector2 NewPos)
        {
            CharacterPos = NewPos;
        }
        public Vector2 GetPos()
        {
            return CharacterPos;
        }
        public void SetHP(int NewHP)
        {
            HealthPoint = NewHP;
        }
        public void AddHP(int Amount)
        {
            HealthPoint += Amount;
        }
        public virtual void SubtraceHP(int Amount)
        {
            HealthPoint -= Amount;
        }
        public int GetHP()
        {
            return HealthPoint;
        }
        public Texture2D GetTex()
        {
            return CharacterTexture;
        }
        public virtual void DrawCharacter(SpriteBatch SB,Vector2 CamPos)
        {
            if (Fliped)
            {
                SB.Draw(CharacterTexture, CharacterPos-CamPos, null, Color.White, 0, Vector2.Zero, 1, SpriteEffects.FlipHorizontally, 0);
            }
            else
            {
                SB.Draw(CharacterTexture, CharacterPos-CamPos, null, Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
            }
        }
        public virtual void UpdateCharacter(float time)
        {
            Hitbox = new Rectangle((int)CharacterPos.X, (int)CharacterPos.Y, CharacterWidth, CharacterHeight);
            PreHitbox = new Rectangle((int)CharacterPos.X-1, (int)CharacterPos.Y-1, CharacterWidth+1, CharacterHeight+1);
            CharacterOrigin = CharacterPos + new Vector2(CharacterTexture.Width / 2, CharacterTexture.Height / 2);
        }
        public bool ColisionCheck(Rectangle IntersectedHitbox)
        {
            return Hitbox.Intersects(IntersectedHitbox);
        }
        public bool PreColisionCheck(Rectangle IntersectedHitbox)
        {
            return PreHitbox.Intersects(IntersectedHitbox);
        }
        public Rectangle GetHitbox()
        {
            return Hitbox;
        }
        public Rectangle GetPreHitbox()
        {
            return PreHitbox;
        }
        public void SetHit(bool NewHit)
        {
            IsHit = NewHit;
        }
        public bool GetHit()
        {
            return IsHit;
        }
        public Vector2 GetOrigin()
        {
            return CharacterOrigin;
        }
    }
}
