using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.Collisions;
using Roy_T.AStar.Grids;
using Roy_T.AStar.Primitives;
using Roy_T.AStar.Paths;

namespace Chaotic_Night
{
    public class Small_Imp : Enemy
    {
        
        public Small_Imp(Game1 game) : base(game)
        {
        }
        public Small_Imp(Game1 game, Vector2 Pos) : base(game, Pos)
        {
        }
        public Small_Imp(Game1 game, int Health) : base(game, Health)
        {
        }
        public Small_Imp(Game1 game, Vector2 Pos, int Health) : base(game, Pos, Health)
        {

        }
        public override void Load(ContentManager Content, SpriteBatch _SB)
        {
            CharacterTexture = Content.Load<Texture2D>("Small-Imp");
            WTex = Content.Load<Texture2D>("Small-Imp_Whitet");
            CharacterWidth = 144;
            CharacterHeight = 144;
            HealthPoint = 150;
            AttackRange = new Rectangle((int)CharacterPos.X - 108, (int)CharacterPos.Y - 108, 432, 432);
            Hitbox = new Rectangle((int)CharacterPos.X, (int)CharacterPos.Y, CharacterWidth, CharacterHeight);
            EnemyWeapon = new AI_Range_Wep(this);
            EnemyWeapon.Load(Content, _SB);
            Speed = 2;
            FramePosY = 1;
            FramePosX = 0;
            Cooldown = 1;
            IsHit = false;
            PF = new PathFinder();
            BaseMoneyDrop = 150;
            ExpDrop = 15;
            //Destination = CharacterPos;
        }
        public override void DrawCharacter(SpriteBatch SB, Vector2 CamPos)
        {
            if (Fliped)
            {
                SB.Draw(CharacterTexture, CharacterPos - CamPos, new Rectangle(144 * FramePosX, 144 * FramePosY, CharacterWidth, CharacterHeight), Color.White * ChaOpa, 0, Vector2.Zero, 1, SpriteEffects.FlipHorizontally, 0);
                EnemyWeapon.DrawAttackAnim(0, CamPos);
            }
            else
            {
                SB.Draw(CharacterTexture, CharacterPos - CamPos, new Rectangle(144 * FramePosX, 144 * FramePosY, CharacterWidth, CharacterHeight), Color.White * ChaOpa, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
                EnemyWeapon.DrawAttackAnim((float)3.14, CamPos);
            }
        }
        public override void Attack(Character Character)
        {
            if (AllowAttack)
            {
                EnemyWeapon.Attack(Character);
                AllowAttack = false;
                TotalCooldown = 0;
                FramePosY = 7;
            }
        }
        public override void UpdateCharacter(float time)
        {
            Hitbox = new Rectangle((int)CharacterPos.X, (int)CharacterPos.Y, CharacterWidth, CharacterHeight);
            CharacterOrigin = CharacterPos + new Vector2(CharacterWidth / 2, CharacterHeight / 2);
            AttackRange = new Rectangle((int)CharacterPos.X - 108, (int)CharacterPos.Y - 108, 432, 432);
            EnemyWeapon.UpdateWeapon(time);
            UpdateFrame(time);

            if (AllowAttack == false)
            {
                if (TotalCooldown > Cooldown)
                {
                    AllowAttack = true;
                    TotalCooldown = 0;
                }
                else
                {
                    TotalCooldown += time;
                }
            }
        }
        public override void SubtraceHP(int Amount)
        {
            base.SubtraceHP(Amount);
            /*EndFrame = 3;
            FramePosY = 11;
            FramePosX = 0;*/
            IsHit = true;
            if (HealthPoint <= 0)
            {
                FramePosY = 13;
                FramePosX = 0;
                EndFrame = 4;
            }
        }
        public override void UpdateFrame(float time)
        {
            TotalElapsed += time;
            if (TotalElapsed > TimePerFrame)
            {
                FramePosX = (FramePosX + 1) % EndFrame;
                TotalElapsed -= TimePerFrame;
                if (IsHit == true)
                {
                    if (FlickerCha == false)
                    {
                        FlickerCha = true;
                        FlickerTime += 1;
                    }
                    else if (FlickerCha == true)
                    {
                        FlickerCha = false;
                        FlickerTime += 1;
                    }
                    if (FlickerTime > 10)
                    {
                        FlickerTime = 0;
                        IsHit = false;
                    }
                }
            }
            if (IsHit == true && FramePosY == 11 && FramePosX >= 2)
            {
                EndFrame = 6;
                FramePosY = 1;
                FramePosX = 0;
                IsHit = false;
            }
            if (IsDead == false && FramePosY == 13 && FramePosX >= 3)
            {
                FadeCharacter(time);
                if (ChaOpa == 0)
                {
                    IsDead = true;
                }
            }
        }
    }
}
