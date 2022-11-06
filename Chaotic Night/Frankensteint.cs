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
    class Frankensteint : Enemy
    {
        bool AllowMovement = true;
        Random RAND;
        Character Target;
        float JumptimeElapsed = 0;
        float JumptimeCooldownElapsed = 0;
        float JumptimeGap = 0;
        float JumpCooldown = 15;
        bool Jumping = false;
        bool Landing = false;
        bool Attackable = true;
        bool AlreadySAtk = false;
        public bool AllowToJump = false;
        public Frankensteint(Game1 game) : base(game)
        {
            RAND = new Random();
        }
        public Frankensteint(Game1 game, Vector2 Pos) : base(game, Pos)
        {
            RAND = new Random();
        }
        public Frankensteint(Game1 game, int Health) : base(game, Health)
        {
            RAND = new Random();
        }
        public Frankensteint(Game1 game, Vector2 Pos, int Health) : base(game, Pos, Health)
        {
            RAND = new Random();
        }
        public override void Load(ContentManager Content, SpriteBatch _SB)
        {
            CharacterTexture = Content.Load<Texture2D>("Frankensteint");
            WTex = Content.Load<Texture2D>("Frankensteint_White");
            WTex = Content.Load<Texture2D>("Frankensteint_White");
            CharacterWidth = 216;
            CharacterHeight = 216;
            AttackRange = new Rectangle((int)CharacterPos.X - 108, (int)CharacterPos.Y - 108, 432, 432);
            Hitbox = new Rectangle((int)CharacterPos.X, (int)CharacterPos.Y, CharacterWidth, CharacterHeight);
            EnemyWeapon = new AI_Melee_Wep(this);
            EnemyWeapon.Load(Content, _SB);
            Speed = 1;
            FramePosY = 1;
            FramePosX = 0;
            EndFrame = 6;
            Cooldown = 2;
            IsHit = false;
            HealthPoint = 1000;
            PF = new PathFinder();
            BaseMoneyDrop = 2500;
            ExpDrop = 100;
            //Destination = CharacterPos;
        }
        public override void Attack(Character Character)
        {
            if (AllowAttack == true && Jumping == false && Landing == false)
            {
                EnemyWeapon.Attack(Character);
                AllowAttack = false;
                TotalCooldown = 0;
                FramePosY = 3;
                FramePosX = 0;
                EndFrame = 10;
            }
        }
        public void SAtkLand(Character Character)
        {
            if(FramePosX>=2&&FramePosY==7&&AlreadySAtk==false)
            {
                if(Character.GetHitbox().Intersects(AttackRange))
                {
                    if(AlreadySAtk==false)
                    {
                        Character.SubtraceHP(CalculateDamage(50, 2, 5));
                        AlreadySAtk = true;
                    }
                }
            }
        }
        public void SAtkJump()
        {
            if(JumptimeElapsed<=5)
            {
                CharacterPos = new Vector2(Target.CharacterPos.X, Target.CharacterPos.Y + CharacterHeight / 8);
            }
            FramePosY = 10;
            FramePosX = 0;
            EndFrame = 1;
        }
        public override void SpecialAttack(Character character)
        {
            if(AllowToJump==true)
            {
                AllowToJump = false;
                AllowAttack = false;
                AllowMovement = false;
                Attackable = false;
                Target = character;
                FramePosY = 6;
                //FramePosX = 0;
                EndFrame = 9;
            }
        }
        public override void MoveTo(Vector2 Destination)
        {
            if(AllowMovement==true)
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
                                FramePosY = 1;
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
                                FramePosY = 1;
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
                                FramePosY = 1;
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
                                FramePosY = 1;
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
        }
        protected int CalculateDamage(int BaseDamage,int OffsetMin,int OffsetMax)
        {
            return((int)(RAND.Next(BaseDamage - OffsetMin, BaseDamage + OffsetMax)));
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
                    if (FlickerTime > 5)
                    {
                        FlickerTime = 0;
                        IsHit = false;
                    }
                }
            }
            if (IsDead == false && FramePosY == 9 && FramePosX >= 3)
            {
                if(FramePosX>=7)
                {
                    FadeCharacter(time);
                    if (ChaOpa == 0)
                    {
                        IsDead = true;
                    }
                }
            }
            if(FramePosY==6&&FramePosX>=8)
            {
                Jumping = true;
            }
            if(FramePosY==7&&FramePosX>=8)
            {
                FramePosY = 1;
                FramePosX = 0;
                AllowMovement = true;
                AllowAttack = true;
                Landing = false;
            }
        }
        public override void UpdateCharacter(float time)
        {
            base.UpdateCharacter(time);
            if(Jumping == true)
            {
                SAtkJump();
                if (JumptimeElapsed >= 5)
                {
                    if(JumptimeGap>=2.5f)
                    {
                        FramePosY = 7;
                        FramePosX = 0;
                        EndFrame = 10;
                        Jumping = false;
                        Landing = true;
                        JumptimeElapsed = 0;
                        JumptimeGap = 0;
                    }
                    else
                    {
                        JumptimeGap += time;
                    }
                }
                else
                {
                    JumptimeElapsed += time;
                }
            }
            if(Landing == true)
            {
                SAtkLand(Target);
            }
            
            if (AllowToJump==false && JumptimeCooldownElapsed < JumpCooldown)
            {
                JumptimeCooldownElapsed += time;
                if(Attackable==false)
                {
                    Attackable = true;
                }
            }
            else
            {
                AllowToJump = true;
                JumptimeCooldownElapsed = 0;
                AlreadySAtk = false;
            }
        }
        public override void SubtraceHP(int Amount)
        {
            if(FramePosY==1||FramePosY==3)
            {
                HealthPoint -= Amount;
                /*EndFrame = 6;
                FramePosY = 1;
                FramePosX = 0;*/
                IsHit = true;
                if (HealthPoint <= 0)
                {
                    FramePosY = 9;
                    FramePosX = 0;
                    EndFrame = 10;
                    IsAlive = false;
                }
            }
        }

    }
}
