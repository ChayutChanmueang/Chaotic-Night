using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.Collisions;

namespace Chaotic_Night
{
    public class Character
    {
        public Texture2D CharacterTexture;
        public int CharacterHeight;
        public int CharacterWidth;
        public Vector2 CharacterPos;
        protected Vector2 CharacterOrigin;
        public int HealthPoint;
        protected Rectangle Hitbox;
        protected Rectangle PreHitbox;
        protected bool IsHit;
        public bool Fliped;
        protected float WeaponOffset = 20;
        public Vector2 WeaponPos;
        public bool IsAlive = true;
        public bool IsDead = false;
        public float WeaponRot;
        public Game1 _game;
        public float ChaOpa = 1;
        public float TimeProg = 0;
        public bool FadingIsDone = false;
        public int BaseMoneyDrop=0;
        public int ExpDrop = 0;

        //Anim
        public int FramePosX = 0;
        public int FramePosY = 0;
        protected float TimePerFrame = (float)1 / 5;
        protected int FrameEnd;
        protected float TotalElapsed;
        public int EndFrame = 4;
        public Character (Game1 game)
        {
            CharacterPos = Vector2.Zero;
            CharacterOrigin = CharacterPos + new Vector2(CharacterWidth/2, CharacterHeight/2);
            HealthPoint = 100;
            _game = game;
        }
        public Character(Game1 game,Vector2 Pos)
        {
            CharacterPos = Pos;
            CharacterOrigin = CharacterPos + new Vector2(CharacterWidth / 2, CharacterHeight / 2);
            HealthPoint = 100;
            _game = game;
        }
        public Character(Game1 game, Vector2 Pos,int Health)
        {
            CharacterPos = Pos;
            CharacterOrigin = CharacterPos + new Vector2(CharacterWidth / 2, CharacterHeight / 2);
            HealthPoint = Health;
            _game = game;
        }
        public Character(Game1 game, Vector2 Pos, int Health, int SkillPoint)
        {
            CharacterPos = Pos;
            HealthPoint = Health;
        }
        public Character(Game1 game, int Health)
        {
            CharacterPos = Vector2.Zero;
            CharacterOrigin = CharacterPos + new Vector2(32, 32);
            HealthPoint = Health;
            _game = game;
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

            if (HealthPoint <= 0)
            {
                IsAlive = false;
            }
        }
        public void AddHP(int Amount)
        {
            if((HealthPoint + Amount) < _game.MaxHP)
            {
                HealthPoint += Amount;
            }
            else if ((HealthPoint + Amount) > _game.MaxHP)
            {
                Amount = _game.MaxHP - HealthPoint;
                HealthPoint += Amount;
            }

            if (HealthPoint <= 0)
            {
                IsAlive = false;
            }
        }
        public virtual void SubtraceHP(int Amount)
        {
            HealthPoint -= Amount;
            
            if (HealthPoint <= 0)
            {
                IsAlive = false;
            }
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
            CharacterOrigin = CharacterPos + new Vector2(CharacterWidth / 2, CharacterHeight / 2);
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
        public virtual void UpdateFrame(float time)
        {
            TotalElapsed += time;
            if (TotalElapsed > TimePerFrame)
            {
                FramePosX = (FramePosX + 1) % EndFrame;
                TotalElapsed -= TimePerFrame;
            }
        }
        public void FadeCharacter(float time)
        {
            TimeProg += time;
            if (ChaOpa > 0)
            {
                if (TimeProg > 0.05)
                {
                    ChaOpa -= (float)0.3;
                    TimeProg = 0;
                }
            }
            else
            {
                ChaOpa = 0;
            }
        }
        public virtual void AddSP(int Amount)
        {

        }
        public virtual int GetSP()
        {
            return 0;
        }
    }
}
