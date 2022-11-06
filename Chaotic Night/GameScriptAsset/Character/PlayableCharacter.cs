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
    public class PlayableCharacter : MovableCharacter
    {
        public Weapons HoldedWeapon;
        protected KeyboardState PlayerKeyboard;
        protected MouseState PlayerMouse;
        bool AlreadyUseSP = false;
        public PlayableCharacter(Game1 game) : base(game)
        {
        }
        public PlayableCharacter(Game1 game, Vector2 Pos) : base(game, Pos)
        {
        }
        public PlayableCharacter(Game1 game, int Health) : base(game, Health)
        {
        }
        public PlayableCharacter(Game1 game, Vector2 Pos, int Health) : base(game, Pos, Health)
        {
        }
        public PlayableCharacter(Game1 game, Vector2 Pos, int Health, int SkillPoint) : base(game, Pos, Health, SkillPoint)
        {
            CharacterPos = Pos;
            HealthPoint = Health;
            SP = SkillPoint;
            Speed = 10;
        }
        public void Load(ContentManager Content, SpriteBatch _SB,Weapons WP)
        {
            CharacterTexture = Content.Load<Texture2D>("MiyukiV5");
            CharacterWidth = 144;
            CharacterHeight = 144;
            FramePosY = 0;
            EndFrame = 4;
            Hitbox = new Rectangle((int)CharacterPos.X+36, (int)CharacterPos.Y, 72, CharacterHeight);
            HoldedWeapon = WP;
            HoldedWeapon.Load(Content, _SB);
        }
        public override void DrawCharacter(SpriteBatch SB,Vector2 CamPos)
        {
            if (Fliped)
            {
                SB.Draw(CharacterTexture, CharacterPos - CamPos, new Rectangle(144 * FramePosX, 144 * FramePosY, 144, 144), Color.White*ChaOpa, 0, Vector2.Zero,1, SpriteEffects.FlipHorizontally, 0);
                if (HoldedWeapon.Attacking==true)
                {
                    HoldedWeapon.DrawAttackAnim(WeaponRot, CamPos);
                }
            }
            else
            {
                SB.Draw(CharacterTexture, CharacterPos - CamPos, new Rectangle(144*FramePosX,144*FramePosY, 144, 144), Color.White*ChaOpa, 0, Vector2.Zero,1, SpriteEffects.None, 0);
                if(HoldedWeapon.Attacking == true)
                {
                    HoldedWeapon.DrawAttackAnim(WeaponRot, CamPos);
                }
            }
        }
        public void DoM1Action(Character Target)
        {
            HoldedWeapon.Attack(Target);
        }
        public void DoM2Action(Character Target)
        {
            HoldedWeapon.SpecialAttack(Target);
        }
        public bool GetAttacking()
        {
            return HoldedWeapon.GetAttacking();
        }
        public void UpdateWeaponFrame(float time)
        {
            HoldedWeapon.UpdateFrame(time);
        }
        public Weapons GetWeapon()
        {
            return HoldedWeapon;
        }
        public int GetHitCount()
        {
            return HoldedWeapon.GetHitCount();
        }
        public void UpdateCharacter(float time,Vector2 CamPos,List<GameObject> GameObj,List<Enemy> Enemies,Vector2 MPos)
        {
            PlayerKeyboard = Keyboard.GetState();
            PlayerMouse = Mouse.GetState();
            if (IsAlive == true)
            {
                #region Player Movement
                int HitDisX = 10;
                int HitDisY = 10;
                if (PlayerKeyboard.IsKeyDown(Keys.W))
                {
                    foreach (GameObject Wall in GameObj)
                    {
                        if (ColisionCheck(Wall.GetHitbox()) == false)
                        {
                            if (GetHitbox().Top - GetSpeed() < Wall.GetHitbox().Bottom &&
                                GetHitbox().Bottom > Wall.GetHitbox().Bottom &&
                                GetHitbox().Right > Wall.GetHitbox().Left &&
                                GetHitbox().Left < Wall.GetHitbox().Right)
                            {
                                if (GetHitbox().Top - Wall.GetHitbox().Bottom < HitDisY)
                                {
                                    HitDisY = GetHitbox().Top - Wall.GetHitbox().Bottom;
                                }
                            }
                        }
                    }
                    if (HitDisY < GetSpeed())
                    {
                        MoveUp(HitDisY);
                    }
                    else
                    {
                        MoveUp();
                    }
                }
                else if (PlayerKeyboard.IsKeyUp(Keys.W))
                {
                    HitDisY = 10;
                }
                if (PlayerKeyboard.IsKeyDown(Keys.S))
                {
                    foreach (GameObject Wall in GameObj)
                    {
                        if (ColisionCheck(Wall.GetHitbox()) == false)
                        {
                            if (GetHitbox().Bottom + GetSpeed() > Wall.GetHitbox().Top &&
                                GetHitbox().Top < Wall.GetHitbox().Top &&
                                GetHitbox().Right > Wall.GetHitbox().Left &&
                                GetHitbox().Left < Wall.GetHitbox().Right)
                            {
                                if (Wall.GetHitbox().Top - GetHitbox().Bottom < HitDisY)
                                {
                                    HitDisY = Wall.GetHitbox().Top - GetHitbox().Bottom;
                                }
                            }
                        }
                    }
                    if (HitDisY < GetSpeed())
                    {
                        MoveDown(HitDisY);
                    }
                    else
                    {
                        MoveDown();
                    }
                }
                else if (PlayerKeyboard.IsKeyUp(Keys.S))
                {
                    HitDisY = 10;
                }
                if (PlayerKeyboard.IsKeyDown(Keys.A))
                {
                    foreach (GameObject Wall in GameObj)
                    {
                        if (ColisionCheck(Wall.GetHitbox()) == false)
                        {
                            if (GetHitbox().Left - GetSpeed() < Wall.GetHitbox().Right &&
                                GetHitbox().Right > Wall.GetHitbox().Right &&
                                GetHitbox().Bottom > Wall.GetHitbox().Top &&
                                GetHitbox().Top < Wall.GetHitbox().Bottom)
                            {

                                if (GetHitbox().Left - Wall.GetHitbox().Right < HitDisX)
                                {
                                    HitDisX = GetHitbox().Left - Wall.GetHitbox().Right;
                                }
                            }
                        }
                    }
                    if (HitDisX < GetSpeed())
                    {
                        MoveLeft(HitDisX);
                    }
                    else
                    {
                        MoveLeft();
                    }
                }
                else if (PlayerKeyboard.IsKeyUp(Keys.A))
                {
                    HitDisX = 10;
                }
                if (PlayerKeyboard.IsKeyDown(Keys.D))
                {
                    foreach (GameObject Wall in GameObj)
                    {
                        if (ColisionCheck(Wall.GetHitbox()) == false)
                        {
                            if (GetHitbox().Right + GetSpeed() > Wall.GetHitbox().Left &&
                                GetHitbox().Left < Wall.GetHitbox().Left &&
                                GetHitbox().Bottom > Wall.GetHitbox().Top &&
                                GetHitbox().Top < Wall.GetHitbox().Bottom)
                            {
                                if (Wall.GetHitbox().Left - GetHitbox().Right < HitDisX)
                                {
                                    HitDisX = Wall.GetHitbox().Left - GetHitbox().Right;
                                }
                            }
                        }
                    }
                    if (HitDisX < GetSpeed())
                    {
                        MoveRight(HitDisX);
                    }
                    else
                    {
                        MoveRight();
                    }
                }
                else if (PlayerKeyboard.IsKeyUp(Keys.D))
                {
                    HitDisX = 10;
                }
                if (PlayerKeyboard.IsKeyUp(Keys.W) && PlayerKeyboard.IsKeyUp(Keys.A) && PlayerKeyboard.IsKeyUp(Keys.S) && PlayerKeyboard.IsKeyUp(Keys.D))
                {
                    if(IsHit==false)
                    {
                        if (FramePosY != 0)
                        {
                            FramePosX = 0;
                            if (HoldedWeapon.GetType() == typeof(RangeWeapons))
                            {
                                FramePosY = 10;
                                EndFrame = 6;
                            }
                            else
                            {
                                FramePosY = 0;
                                EndFrame = 4;
                            }

                        }
                    }
                }
                #endregion
                #region Player Attack
                if (PlayerMouse.LeftButton != ButtonState.Pressed && PlayerMouse.RightButton != ButtonState.Pressed && GetAttacking() == false)
                {
                    Vector2 MousePos = new Vector2(PlayerMouse.X, PlayerMouse.Y);
                    Vector2 dPos = MPos - (CharacterOrigin - CamPos);
                    WeaponRot = (float)Math.Atan2(dPos.Y, dPos.X);
                }
                else if (PlayerMouse.LeftButton == ButtonState.Pressed)
                {
                    foreach (Enemy enemy in Enemies)
                    {
                        if (GetWeapon().GetType() == typeof(RangeWeapons))
                        {
                            DoM1Action(enemy);
                            break;
                        }
                        else
                        {
                            if (enemy.GetType() != typeof(Frankensteint))
                            {
                                DoM1Action(enemy);
                            }
                            else if (enemy.GetType() == typeof(Frankensteint))
                            {
                                if (enemy.FramePosY == 1 || enemy.FramePosY == 3)
                                {
                                    DoM1Action(enemy);
                                }
                            }
                        }
                    }
                    GetWeapon().Attacking = true;

                }
                else if (PlayerMouse.RightButton == ButtonState.Pressed)
                {
                    if (GetSP() > 0 && GetSP() > HoldedWeapon.SAtkCost)
                    {
                        foreach(Enemy enemy in Enemies)
                        {
                            HoldedWeapon.SpecialAttack(enemy);
                            break;
                        }
                        AlreadyUseSP = true;
                        GetWeapon().Attacking = true;
                        SP -= HoldedWeapon.SAtkCost;
                    }
                }
                if (GetWeapon().UpdateAnim == true)
                {
                    UpdateWeaponFrame(time);
                }
                else if(HoldedWeapon.UpdateAnim == false)
                {
                    HoldedWeapon.FramePosX = 0;
                    if (HoldedWeapon.FrameEnd > 9)
                    {
                        HoldedWeapon.FrameEnd = 9;
                    }
                }
                #endregion
                foreach(PopUpDamage PUD in HoldedWeapon.Damages)
                {
                    PUD.Update(time);
                    if(PUD.CheckIfDied())
                    {
                        HoldedWeapon.Damages.Remove(PUD);
                        break;
                    }
                }
            }
            Hitbox = new Rectangle((int)CharacterPos.X + 36, (int)CharacterPos.Y, 72, CharacterHeight);
            PreHitbox = new Rectangle((int)CharacterPos.X + 16 - 1, (int)CharacterPos.Y + 8 - 1, 35, 51);
            CharacterOrigin = CharacterPos + new Vector2(CharacterWidth / 2, CharacterHeight / 2);
            HoldedWeapon.UpdateWeapon(time);
            if (HealthPoint <= 0)
            {
                IsAlive = false;
            }
            UpdateFrame(time);
        }
        public Vector2 GetWeaponHitzoneVector()
        {
            return new Vector2(HoldedWeapon.GetHitzone().X, HoldedWeapon.GetHitzone().Y);
        }
        public override void MoveUp()
        {
            CharacterPos.Y -= Speed;
            WeaponPos.Y = CharacterPos.Y + 16;

            if (IsHit == false)
            {
                if (HoldedWeapon.GetType() == typeof(RangeWeapons))
                {
                    FramePosY = 12;
                }
                else
                {
                    FramePosY = 2;
                }
                EndFrame = 6;
            }
        }
        public override void MoveUp(int Amount)
        {
            CharacterPos.Y -= Amount;
            WeaponPos.Y = CharacterPos.Y + 16;
            if (IsHit == false)
            {
                if (HoldedWeapon.GetType() == typeof(RangeWeapons))
                {
                    FramePosY = 12;
                }
                else
                {
                    FramePosY = 2;
                }
                EndFrame = 6;
            }
        }
        public override void MoveDown()
        {
            CharacterPos.Y += Speed;
            WeaponPos.Y = CharacterPos.Y + 16;
            if (IsHit == false)
            {
                if (HoldedWeapon.GetType() == typeof(RangeWeapons))
                {
                    FramePosY = 12;
                }
                else
                {
                    FramePosY = 2;
                }
                EndFrame = 6;
            }
        }
        public override void MoveDown(int Amount)
        {
            CharacterPos.Y += Amount;
            WeaponPos.Y = CharacterPos.Y + 16;
            if (IsHit == false)
            {
                if (HoldedWeapon.GetType() == typeof(RangeWeapons))
                {
                    FramePosY = 12;
                }
                else
                {
                    FramePosY = 2;
                }
                EndFrame = 6;
            }
        }
        public override void MoveLeft()
        {
            CharacterPos.X -= Speed;
            Fliped = true;
            WeaponPos.X = CharacterPos.X - 16;
            if (IsHit == false)
            {
                if (HoldedWeapon.GetType() == typeof(RangeWeapons))
                {
                    FramePosY = 12;
                }
                else
                {
                    FramePosY = 2;
                }
                EndFrame = 6;
            }
        }
        public override void MoveLeft(int Amount)
        {
            CharacterPos.X -= Amount;
            Fliped = true;
            WeaponPos.X = CharacterPos.X - 16;
            if (IsHit == false)
            {
                if (HoldedWeapon.GetType() == typeof(RangeWeapons))
                {
                    FramePosY = 12;
                }
                else
                {
                    FramePosY = 2;
                }
                EndFrame = 6;
            }
        }
        public override void MoveRight()
        {
            CharacterPos.X += Speed;
            Fliped = false;
            WeaponPos.X = CharacterPos.X - 16;
            if (IsHit == false)
            {
                if (HoldedWeapon.GetType() == typeof(RangeWeapons))
                {
                    FramePosY = 12;
                }
                else
                {
                    FramePosY = 2;
                }
                EndFrame = 6;
            }
        }
        public override void MoveRight(int Amount)
        {
            CharacterPos.X += Amount;
            Fliped = false;
            WeaponPos.X = CharacterPos.X - 16;
            if(IsHit==false)
            {
                if (HoldedWeapon.GetType() == typeof(RangeWeapons))
                {
                    FramePosY = 12;
                }
                else
                {
                    FramePosY = 2;
                }
                EndFrame = 6;
            }
        }
        public override void UpdateFrame(float time)
        {
            base.UpdateFrame(time);
            if (IsHit == true && FramePosY == 6 && FramePosX >= 3)
            {
                EndFrame = 4;
                FramePosX = 0;
                FramePosY = 0;
                IsHit = false;
            }
            if (IsDead == false && FramePosY == 9 && FramePosX >= 3)
            {
                FadeCharacter(time);
                if (ChaOpa == 0)
                {
                    IsDead = true;
                }
            }
        }
        public override void SubtraceHP(int Amount)
        {
            if(IsHit==false)
            {
                base.SubtraceHP(Amount);
                EndFrame = 4;
                FramePosY = 6;
                FramePosX = 0;
                IsHit = true;
                if (HealthPoint <= 0)
                {
                    FramePosY = 9;
                    FramePosX = 0;
                    EndFrame = 4;
                }
            }
        }
    }
}
