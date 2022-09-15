using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Chaotic_Night
{
    class MovableCharacter : Character
    {
        protected int Speed;
        protected int SP = 25;
        public MovableCharacter() : base()
        {
            CharacterPos = Vector2.Zero;
            HealthPoint = 100;
            Speed = 10;
        }
        public MovableCharacter(Vector2 Pos) : base(Pos)
        {
            CharacterPos = Pos;
            HealthPoint = 100;
            Speed = 10;
        }
        public MovableCharacter(int Health) : base(Health)
        {
            CharacterPos = Vector2.Zero;
            HealthPoint = Health;
            Speed = 10;
        }
        public MovableCharacter(Vector2 Pos,int Health) : base(Pos,Health)
        {
            CharacterPos = Pos;
            HealthPoint = Health;
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
        public void MoveUp()
        {
            CharacterPos.Y -= Speed;
            WeaponPos.Y = CharacterPos.Y + 16;
        }
        public void MoveUp(int Amount)
        {
            CharacterPos.Y -= Amount;
            WeaponPos.Y = CharacterPos.Y + 16;
        }
        public void MoveDown()
        {
            CharacterPos.Y += Speed;
            WeaponPos.Y = CharacterPos.Y + 16;
        }
        public void MoveDown(int Amount)
        {
            CharacterPos.Y += Amount;
            WeaponPos.Y = CharacterPos.Y + 16;
        }
        public void MoveLeft()
        {
            CharacterPos.X -= Speed;
            Fliped = true;
            WeaponPos.X = CharacterPos.X - 16;
        }
        public void MoveLeft(int Amount)
        {
            CharacterPos.X -= Amount;
            Fliped = true;
            WeaponPos.X = CharacterPos.X - 16;
        }
        public void MoveRight()
        {
            CharacterPos.X += Speed;
            Fliped = false;
            WeaponPos.X = CharacterPos.X - 16;
        }
        public void MoveRight(int Amount)
        {
            CharacterPos.X += Amount;
            Fliped = false;
            WeaponPos.X = CharacterPos.X - 16;
        }
        public int GetSP()
        {
            return SP;
        }
    }
}
