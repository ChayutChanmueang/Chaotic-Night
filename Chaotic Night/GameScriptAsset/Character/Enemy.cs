using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Chaotic_Night
{
    class Enemy : MovableCharacter
    {
        Weapons EnemyWeapon;
        Rectangle AttackRange;
        int OldDMG;
        public Enemy() : base()
        {
        }
        public Enemy(Vector2 Pos) : base(Pos)
        {
        }
        public Enemy(int Health) : base(Health)
        {
        }
        public Enemy(Vector2 Pos, int Health) : base(Pos, Health)
        {
        }
        public override void Load(ContentManager Content, SpriteBatch _SB)
        {
            CharacterTexture = Content.Load<Texture2D>("main_character_V_1");
            CharacterWidth = 64;
            CharacterHeight = 64;
            AttackRange = new Rectangle((int)CharacterPos.X - 29, (int)CharacterPos.Y - 29, 90, 90);
            Hitbox = new Rectangle((int)CharacterPos.X, (int)CharacterPos.Y, CharacterWidth, CharacterHeight);
            EnemyWeapon = new MeleeWeapons(this);
            EnemyWeapon.Load(Content, _SB);
            Speed = 2;
        }
        public override void DrawCharacter(SpriteBatch SB,Vector2 CamPos)
        {
            if (Fliped)
            {
                SB.Draw(CharacterTexture, CharacterPos-CamPos, new Rectangle(0,0,CharacterWidth,CharacterHeight), Color.White, 0, Vector2.Zero, 1, SpriteEffects.FlipHorizontally, 0);
                EnemyWeapon.DrawAttackAnim(0,CamPos);
            }
            else
            {
                SB.Draw(CharacterTexture, CharacterPos-CamPos, new Rectangle(0, 0,CharacterWidth,CharacterHeight), Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
                EnemyWeapon.DrawAttackAnim((float)3.14,CamPos);
            }
        }
        public void Attack(Character Character)
        {
            EnemyWeapon.Attack(Character);
        }
        public bool GetAttacking()
        {
            return EnemyWeapon.GetAttacking();
        }
        public Rectangle GetAttackRange()
        {
            return AttackRange;
        }
        public void UpdateWeaponFrame(float time)
        {
            EnemyWeapon.UpdateFrame(time);
        }
        public void MoveTo(Vector2 Destination)
        {
            if(CharacterPos.X>Destination.X)
            {
                MoveLeft();
            }
            if (CharacterPos.X < Destination.X)
            {
                MoveRight();
            }
            if (CharacterPos.Y > Destination.Y)
            {
                MoveUp();
            }
            if (CharacterPos.Y < Destination.Y)
            {
                MoveDown();
            }
        }
        public override void UpdateCharacter(float time)
        {
            Hitbox = new Rectangle((int)CharacterPos.X, (int)CharacterPos.Y, CharacterWidth, CharacterHeight);
            CharacterOrigin = CharacterPos + new Vector2(32, 32);
            AttackRange = new Rectangle((int)CharacterPos.X - 29, (int)CharacterPos.Y - 29, 90, 90);
            EnemyWeapon.UpdateWeapon(time);
        }
        public void DisplayPopUpDamage(SpriteBatch SB,SpriteFont Font)
        {
            SB.DrawString(Font, OldDMG.ToString(), new Vector2(CharacterPos.X, CharacterPos.Y - 40), Color.White);
        }
        public override void SubtraceHP(int Amount)
        {
            base.SubtraceHP(Amount);
            Amount = OldDMG;
        }

        public virtual Path FindPath(Vector2 Destination,Grid grid)
        {
            return PF.FindPath(new GridPosition((int)CharacterPos.X, (int)CharacterPos.Y), new GridPosition((int)CharacterPos.X, (int)CharacterPos.Y), grid);
        }
    }
}
