using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Roy_T.AStar.Grids;
using Roy_T.AStar.Primitives;
using Roy_T.AStar.Paths;

namespace Chaotic_Night
{
    class Enemy : MovableCharacter
    {
        public Weapons EnemyWeapon;
        Rectangle AttackRange;
        int OldDMG;
        int LHitDisX = 12;
        int RHitDisX = 12;
        int UHitDisY = 12;
        int DHitDisY = 12;
        PathFinder PF;
        Path path;
        float TotalCooldown;
        bool AllowAttack = true;
        //protected Vector2 Destination;
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
            PF = new PathFinder();
            //Destination = CharacterPos;
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
            if(AllowAttack)
            {
                EnemyWeapon.Attack(Character);
                AllowAttack = false;
                TotalCooldown = 0;
            }
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
            /*if(CharacterPos==Destination)
            {
                Destination = PathNodes[0];
                PathNodes.RemoveAt(0);
            }*/
            if (CharacterPos.X>Destination.X)
            {
                if (LHitDisX < Speed)
                {
                    if(LHitDisX>0)
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
            if (CharacterPos.X < Destination.X)
            {
                if (RHitDisX < Speed)
                {
                    if(RHitDisX>0)
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
            if (CharacterPos.Y > Destination.Y)
            {
                if (UHitDisY<Speed)
                {
                    if(UHitDisY>0)
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
            if (CharacterPos.Y < Destination.Y)
            {
                if (DHitDisY<Speed)
                {
                    if(DHitDisY>0)
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
            CharacterOrigin = CharacterPos + new Vector2(32, 32);
            AttackRange = new Rectangle((int)CharacterPos.X - 29, (int)CharacterPos.Y - 29, 90, 90);
            EnemyWeapon.UpdateWeapon(time);
            
            if (TotalCooldown > 1)
            {
                AllowAttack = true;
            }
            else
            {
                TotalCooldown += time;
            }
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
