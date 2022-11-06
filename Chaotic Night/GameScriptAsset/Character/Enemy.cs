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
    public class Enemy : MovableCharacter
    {
        public Weapons EnemyWeapon;
        protected Rectangle AttackRange;
        protected int LHitDisX = 12;
        protected int RHitDisX = 12;
        protected int UHitDisY = 12;
        protected int DHitDisY = 12;
        protected PathFinder PF;
        Path path;
        protected Texture2D WTex;
        protected float TotalCooldown;
        protected float Cooldown;
        protected bool AllowAttack = true;
        protected bool FlickerCha = false;
        protected bool IsFlickering = false;
        protected int FlickerTime = 0;
        //protected Vector2 Destination;
        public Enemy(Game1 game) : base(game)
        {
        }
        public Enemy(Game1 game, Vector2 Pos) : base(game, Pos)
        {
        }
        public Enemy(Game1 game, int Health) : base(game, Health)
        {
        }
        public Enemy(Game1 game, Vector2 Pos, int Health) : base(game, Pos, Health)
        {
            HealthPoint = Health;
        }
        public override void Load(ContentManager Content, SpriteBatch _SB)
        {
            CharacterTexture = Content.Load<Texture2D>("Big-Imp");
            WTex = Content.Load<Texture2D>("Big-Imp_White");
            CharacterWidth = 216;
            CharacterHeight = 216;
            AttackRange = new Rectangle((int)CharacterPos.X - 108, (int)CharacterPos.Y - 108, 432, 432);
            Hitbox = new Rectangle((int)CharacterPos.X, (int)CharacterPos.Y, CharacterWidth, CharacterHeight);
            EnemyWeapon = new AI_Melee_Wep(this);
            EnemyWeapon.Load(Content, _SB);
            Speed = 2;
            FramePosY = 1;
            FramePosX = 0;
            Cooldown=1;
            IsHit = false;
            PF = new PathFinder();
            BaseMoneyDrop = 250;
            ExpDrop = 25;
            HealthPoint = 500;
            //Destination = CharacterPos;
        }
        public override void DrawCharacter(SpriteBatch SB,Vector2 CamPos)
        {
            if(FlickerCha==false)
            {
                if (Fliped)
                {
                    SB.Draw(CharacterTexture, CharacterPos - CamPos, new Rectangle(216 * FramePosX, 216 * FramePosY, CharacterWidth, CharacterHeight), Color.White * ChaOpa, 0, Vector2.Zero, 1, SpriteEffects.FlipHorizontally, 0);
                    EnemyWeapon.DrawAttackAnim(0, CamPos);
                }
                else
                {
                    SB.Draw(CharacterTexture, CharacterPos - CamPos, new Rectangle(216 * FramePosX, 216 * FramePosY, CharacterWidth, CharacterHeight), Color.White * ChaOpa, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
                    EnemyWeapon.DrawAttackAnim((float)3.14, CamPos);
                }
            }
            else if(FlickerCha==true)
            {
                if (Fliped)
                {
                    SB.Draw(WTex, CharacterPos - CamPos, new Rectangle(216 * FramePosX, 216 * FramePosY, CharacterWidth, CharacterHeight), Color.White * ChaOpa, 0, Vector2.Zero, 1, SpriteEffects.FlipHorizontally, 0);
                    EnemyWeapon.DrawAttackAnim(0, CamPos);
                }
                else
                {
                    SB.Draw(WTex, CharacterPos - CamPos, new Rectangle(216 * FramePosX, 216 * FramePosY, CharacterWidth, CharacterHeight), Color.White * ChaOpa, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
                    EnemyWeapon.DrawAttackAnim((float)3.14, CamPos);
                }
            }
        }
        public virtual void Attack(Character Character)
        {
            if(AllowAttack)
            {
                EnemyWeapon.Attack(Character);
                AllowAttack = false;
                TotalCooldown = 0;
                FramePosY = 7;
            }
        }
        public virtual void SpecialAttack(Character character)
        {

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
            //EnemyWeapon.UpdateFrame(time);
        }
        public virtual void MoveTo(Vector2 Destination)
        {
            if(IsAlive==true)
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
        public void CalculateHitDis(GameObject BlockingObj)
        {
            if (Hitbox.Left - Speed < BlockingObj.GetHitbox().Right &&
                Hitbox.Right > BlockingObj.GetHitbox().Right &&
                Hitbox.Bottom > BlockingObj.GetHitbox().Top &&
                Hitbox.Top < BlockingObj.GetHitbox().Bottom)
            {
                if (Hitbox.Left - BlockingObj.GetHitbox().Right < LHitDisX)
                {
                    LHitDisX =  Math.Abs(Hitbox.Left - BlockingObj.GetHitbox().Right);
                    if (Hitbox.Intersects(BlockingObj.GetHitbox()))
                    {
                        MoveRight(1);
                        MoveLeft(-1);
                    }
                    /*if (LHitDisX < -1)
                    {
                        LHitDisX = 0;
                    }*/
                }
                
            }
            if (Hitbox.Right + Speed > BlockingObj.GetHitbox().Left &&
                Hitbox.Left < BlockingObj.GetHitbox().Left &&
                Hitbox.Bottom > BlockingObj.GetHitbox().Top &&
                Hitbox.Top < BlockingObj.GetHitbox().Bottom)
            {
                if (BlockingObj.GetHitbox().Left - Hitbox.Right < RHitDisX)
                {
                    RHitDisX = Math.Abs(BlockingObj.GetHitbox().Left - Hitbox.Right);
                    if (Hitbox.Intersects(BlockingObj.GetHitbox()))
                    {
                        MoveLeft(1);
                        MoveRight(-1);
                    }
                    /*if (RHitDisX < -1)
                    {
                        RHitDisX = 0;
                    }*/
                }
            }
            if (Hitbox.Top - Speed < BlockingObj.GetHitbox().Bottom &&
                Hitbox.Bottom > BlockingObj.GetHitbox().Bottom &&
                Hitbox.Right > BlockingObj.GetHitbox().Left &&
                Hitbox.Left < BlockingObj.GetHitbox().Right)
            {
                if (BlockingObj.GetHitbox().Top - Hitbox.Bottom < UHitDisY)
                {
                    UHitDisY = Hitbox.Top - BlockingObj.GetHitbox().Bottom;
                    /*if (UHitDisY < -1)
                    {
                        UHitDisY = 0;
                    }*/
                }
            }
            if (Hitbox.Bottom + Speed > BlockingObj.GetHitbox().Top &&
                 Hitbox.Top < BlockingObj.GetHitbox().Top &&
                 Hitbox.Right > BlockingObj.GetHitbox().Left &&
                 Hitbox.Left < BlockingObj.GetHitbox().Right)
            {
                if (BlockingObj.GetHitbox().Top - Hitbox.Bottom < DHitDisY)
                {
                    DHitDisY = BlockingObj.GetHitbox().Top - Hitbox.Bottom;
                    /*if (DHitDisY < -1)
                    {
                        DHitDisY = 0;
                    }*/
                }
            }
        }
        /*public void CalculatePath(SquareGrid grid, Character Target)
        {
            if (PathNodes == null || (PathNodes.Count == 0 && CharacterPos.X == Destination.X && CharacterPos.Y == Destination.Y))
            {
                PathNodes = FindPath(grid, grid.GetSlotsFromPixel(Target.GetPos(), Vector2.Zero));
            }
        }*/
        public override void UpdateCharacter(float time)
        {
            Hitbox = new Rectangle((int)CharacterPos.X, (int)CharacterPos.Y, CharacterWidth, CharacterHeight);
            CharacterOrigin = CharacterPos + new Vector2(108, 108);
            AttackRange = new Rectangle((int)CharacterPos.X - 108, (int)CharacterPos.Y - 108, 432, 432);
            EnemyWeapon.UpdateWeapon(time);
            UpdateFrame(time);

            if(AllowAttack==false)
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
            /*if(IsHit==true)
            {
                
            }*/
        }
        public override void SubtraceHP(int Amount)
        {
            base.SubtraceHP(Amount);
            /*EndFrame = 3;
            FramePosY = 9;
            FramePosX = 0;*/
            IsHit = true;
            if(HealthPoint<=0)
            {
                FramePosY = 11;
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
                if(IsHit==true)
                {
                    if(FlickerCha==false)
                    {
                        FlickerCha = true;
                        FlickerTime += 1;
                    }
                    else if (FlickerCha == true)
                    {
                        FlickerCha = false;
                        FlickerTime += 1;
                    }
                    if(FlickerTime>5)
                    {
                        FlickerTime = 0;
                        IsHit = false;
                    }
                }
            }
            /*if(IsHit==true && FramePosY==9 && FramePosX>=2)
            {
                EndFrame = 6;
                FramePosY = 1;
                FramePosX = 0;
                IsHit = false;
            }*/
            if (IsDead==false && FramePosY == 11 && FramePosX >=3)
            {
                FadeCharacter(time);
                if(ChaOpa==0)
                {
                    IsDead = true;
                }
            }
        }

        public virtual Path FindPath(Vector2 Destination,Grid grid)
        {
            return PF.FindPath(new GridPosition((int)CharacterPos.X, (int)CharacterPos.Y), new GridPosition((int)CharacterPos.X, (int)CharacterPos.Y), grid);
        }
    }
}
