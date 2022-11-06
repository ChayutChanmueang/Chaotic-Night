using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.Collisions;

namespace Chaotic_Night
{
    public class MovableCharacter : Character
    {
        protected int Speed;
        public int SP = 100;
        public MovableCharacter(Game1 game) : base(game)
        {
            CharacterPos = Vector2.Zero;
            HealthPoint = 100;
            Speed = 10;
        }
        public MovableCharacter(Game1 game, Vector2 Pos) : base(game, Pos)
        {
            CharacterPos = Pos;
            HealthPoint = 100;
            Speed = 10;
        }
        public MovableCharacter(Game1 game, int Health) : base(game ,Health)
        {
            CharacterPos = Vector2.Zero;
            HealthPoint = Health;
            Speed = 10;
        }
        public MovableCharacter(Game1 game, Vector2 Pos,int Health) : base(game, Pos,Health)
        {
            CharacterPos = Pos;
            HealthPoint = Health;
            Speed = 10;
        }
        public MovableCharacter(Game1 game, Vector2 Pos, int Health, int SkillPoint) : base(game,Pos,Health,SkillPoint)
        {
            CharacterPos = Pos;
            HealthPoint = Health;
            SP = SkillPoint;
            Speed = 10;
        }

        public void SetSpeed(int NewSpeed)
        {
            Speed = NewSpeed;
        }
        public int GetSpeed()
        {
            return Speed;
        }
        public virtual void MoveUp()
        {
            CharacterPos.Y -= Speed;
            WeaponPos.Y = CharacterPos.Y + 16;
        }
        public virtual void MoveUp(int Amount)
        {
            CharacterPos.Y -= Amount;
            WeaponPos.Y = CharacterPos.Y + 16;
        }
        public virtual void MoveDown()
        {
            CharacterPos.Y += Speed;
            WeaponPos.Y = CharacterPos.Y + 16;
        }
        public virtual void MoveDown(int Amount)
        {
            CharacterPos.Y += Amount;
            WeaponPos.Y = CharacterPos.Y + 16;
        }
        public virtual void MoveLeft()
        {
            CharacterPos.X -= Speed;
            Fliped = true;
            WeaponPos.X = CharacterPos.X - 16;
        }
        public virtual void MoveLeft(int Amount)
        {
            CharacterPos.X -= Amount;
            Fliped = true;
            WeaponPos.X = CharacterPos.X - 16;
        }
        public virtual void MoveRight()
        {
            CharacterPos.X += Speed;
            Fliped = false;
            WeaponPos.X = CharacterPos.X - 16;
        }
        public virtual void MoveRight(int Amount)
        {
            CharacterPos.X += Amount;
            Fliped = false;
            WeaponPos.X = CharacterPos.X - 16;
        }
        public override int GetSP()
        {
            return SP;
        }
        public override void AddSP(int Amount)
        {
            if ((SP + Amount) < 100)
            {
                SP += Amount;
            }
            else if ((SP + Amount) > 100)
            {
                Amount = 100 - SP;
                SP += Amount;
            }
        }
    }
}
