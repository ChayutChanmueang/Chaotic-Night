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
    class Vampire : Enemy
    {
        protected int SAtktime = 0;
        Random RAND;
        bool AllowToFireBalls = false;
        float FireBallCooldown = 0;
        public Vampire(Game1 game) : base(game)
        {
            RAND = new Random();
        }
        public Vampire(Game1 game, Vector2 Pos) : base(game, Pos)
        {
            RAND = new Random();
        }
        public Vampire(Game1 game, int Health) : base(game, Health)
        {
            RAND = new Random();
        }
        public Vampire(Game1 game, Vector2 Pos, int Health) : base(game, Pos, Health)
        {
            RAND = new Random();
        }
        public override void Load(ContentManager Content, SpriteBatch _SB)
        {
            CharacterTexture = Content.Load<Texture2D>("Vampire");
            WTex = Content.Load<Texture2D>("Vampire_White");
            CharacterWidth = 216;
            CharacterHeight = 216;
            AttackRange = new Rectangle((int)CharacterPos.X - 108, (int)CharacterPos.Y - 108, 432, 432);
            Hitbox = new Rectangle((int)CharacterPos.X, (int)CharacterPos.Y, CharacterWidth, CharacterHeight);
            EnemyWeapon = new AI_Melee_Wep(this);
            EnemyWeapon.Load(Content, _SB);
            Speed = 3;
            FramePosY = 1;
            FramePosX = 0;
            EndFrame = 6;
            Cooldown = 2;
            IsHit = false;
            HealthPoint = 100;
            PF = new PathFinder();
            BaseMoneyDrop = 2500;
            ExpDrop = 100;
            HealthPoint = 1500;
            //Destination = CharacterPos;
        }
        public override void Attack(Character Character)
        {
            if (AllowAttack == true)
            {
                EnemyWeapon.Attack(Character);
                AllowAttack = false;
                TotalCooldown = 0;
                FramePosY = 8;
                FramePosX = 0;
                EndFrame = 8;
            }
        }
        public override void SpecialAttack(Character character)
        {
            if (AllowToFireBalls == true)
            {
                AllowAttack = false;
                AllowToFireBalls = false;
                if (SAtktime < 4)
                {
                    FramePosY = 10;
                    FramePosX = 0;
                    Vector2 dPos = character.CharacterPos - CharacterPos;
                    float Rot = (float)Math.Atan2(dPos.Y, dPos.X);
                    EnemyWeapon.GetBullets().Add(new Vampire_Fireball(CharacterOrigin, CharacterTexture, Rot, 15));
                    SAtktime += 1;
                }
                if (SAtktime >= 5)
                {
                    FramePosY = 5;
                    FramePosX = 0;
                    int DirecNum = RAND.Next(1, 4);
                    Vector2 dPos = Vector2.Zero;
                    if (DirecNum == 1)
                    {
                        dPos = character.CharacterPos - new Vector2(260, 400);
                        float Rot = (float)Math.Atan2(dPos.Y, dPos.X);
                        EnemyWeapon.GetBullets().Add(new Vampire_Bat(new Vector2(260, 400), CharacterTexture, Rot, 15));
                    }
                    if (DirecNum == 2)
                    {
                        dPos = character.CharacterPos - new Vector2(1510, 400);
                        float Rot = (float)Math.Atan2(dPos.Y, dPos.X);
                        EnemyWeapon.GetBullets().Add(new Vampire_Bat(new Vector2(1510, 400), CharacterTexture, Rot, 15));
                    }
                    if (DirecNum == 3)
                    {
                        dPos = character.CharacterPos - new Vector2(260, 1852);
                        float Rot = (float)Math.Atan2(dPos.Y, dPos.X);
                        EnemyWeapon.GetBullets().Add(new Vampire_Bat(new Vector2(260, 1852), CharacterTexture, Rot, 15));
                    }
                    if (DirecNum == 4)
                    {
                        dPos = character.CharacterPos - new Vector2(1510, 1852);
                        float Rot = (float)Math.Atan2(dPos.Y, dPos.X);
                        EnemyWeapon.GetBullets().Add(new Vampire_Bat(new Vector2(1510, 1852), CharacterTexture, Rot, 15));
                    }
                    SAtktime = 0;
                }
            }
        }
        public override void MoveTo(Vector2 Destination)
        {
            if (IsAlive == true)
            {
                /*if(CharacterPos==Destination)
            {
                Destination = PathNodes[0];
                PathNodes.RemoveAt(0);
            }*/
                if (CharacterPos.X > Destination.X - CharacterWidth / 2)
                {
                    if (IsHit == false)
                    {
                        if (AllowAttack == true)
                        {
                            FramePosY = 3;
                            EndFrame = 6;
                        }
                    }
                    if (LHitDisX < Speed)
                    {
                        if (LHitDisX > 0)
                        {
                            MoveLeft(LHitDisX);
                        }
                        /*else
                        {
                            if (CharacterPos.X > Destination.X)
                            {
                                MoveLeft(1);
                            }
                            else
                            {
                                MoveRight(1);
                            }
                        }*/
                    }
                    else
                    {
                        MoveLeft();
                    }
                    LHitDisX = 12;
                }
                if (CharacterPos.X < Destination.X - CharacterWidth / 2)
                {
                    if (IsHit == false)
                    {
                        if (AllowAttack == true)
                        {
                            FramePosY = 3;
                            EndFrame = 6;
                        }
                    }
                    if (RHitDisX < Speed)
                    {
                        if (RHitDisX > 0)
                        {
                            MoveRight(RHitDisX);

                        }
                        /*else
                        {
                            if (CharacterPos.X < Destination.X)
                            {
                                MoveRight(1);
                            }
                            else
                            {
                                MoveLeft(1);
                            }
                        }*/
                    }
                    else
                    {
                        MoveRight();
                    }
                    RHitDisX = 12;
                }
                if (CharacterPos.Y > Destination.Y - CharacterHeight / 2)
                {
                    if (IsHit == false)
                    {
                        if (AllowAttack == true)
                        {
                            FramePosY = 3;
                            EndFrame = 6;
                        }
                    }
                    if (UHitDisY < Speed)
                    {
                        if (UHitDisY > 0)
                        {
                            MoveUp(UHitDisY);
                        }
                        else
                        {

                        }
                    }
                    else
                    {
                        MoveUp();
                    }
                    UHitDisY = 12;
                }
                if (CharacterPos.Y < Destination.Y - CharacterHeight / 2)
                {
                    if (IsHit == false)
                    {
                        if (AllowAttack == true)
                        {
                            FramePosY = 3;
                            EndFrame = 6;
                        }
                    }
                    if (DHitDisY < Speed)
                    {
                        if (DHitDisY > 0)
                        {
                            MoveDown(DHitDisY);
                        }
                        else
                        {

                        }
                    }
                    else
                    {
                        MoveDown();
                    }
                    DHitDisY = 12;
                }
            }
        }
        public override void UpdateFrame(float time)
        {
            TotalElapsed += time;
            if (TotalElapsed > TimePerFrame)
            {
                FramePosX = (FramePosX + 1) % EndFrame;
                TotalElapsed -= TimePerFrame;
                if (IsHit == true && IsAlive == true)
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
                    if (FlickerTime > 5)
                    {
                        FlickerTime = 0;
                        IsHit = false;
                    }
                }
            }
            if (IsDead == false && FramePosY == 14 && FramePosX >= 3)
            {
                if (FramePosX >= 5)
                {
                    FadeCharacter(time);
                    if (ChaOpa == 0)
                    {
                        IsDead = true;
                    }
                }
            }
        }
        public override void SubtraceHP(int Amount)
        {
            HealthPoint -= Amount;
            /*EndFrame = 6;
            FramePosY = 1;
            FramePosX = 0;*/
            IsHit = true;
            if (HealthPoint <= 0)
            {
                FramePosY = 14;
                FramePosX = 0;
                EndFrame = 8;
                IsAlive = false;
            }
        }
        public override void UpdateCharacter(float time)
        {
            base.UpdateCharacter(time);
            if(FireBallCooldown<15 && AllowToFireBalls == false)
            {
                FireBallCooldown += time;
            }
            if (FireBallCooldown > 15)
            {
                AllowToFireBalls = true;
                FireBallCooldown = 0;
            }

        }
    }
}
