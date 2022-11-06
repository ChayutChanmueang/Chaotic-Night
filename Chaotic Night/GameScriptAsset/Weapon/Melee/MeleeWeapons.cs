using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Chaotic_Night
{
    public class MeleeWeapons : Weapons
    {
        public bool AllowCombo = false;
        bool Attacked = false;
        public int ComboNum = 1;
        float ComboGapMax = 0.2f;
        float CurComboGap = 0;
        Random RAND;
        public MeleeWeapons(Character OwningCharacter) : base(OwningCharacter)
        {
            Owner = OwningCharacter;
            FrameEnd = 9;
            CooldownLimit = (float)0.5;
            RAND = new Random();
            CooldownLimit = 0.45f;
            TexOrigin = new Vector2(108, 108);
            Bullets = new List<Bullet>();
        }
        public override void Load(ContentManager Content, SpriteBatch SB)
        {
            _SB = SB;
            WeaponTexture = Content.Load<Texture2D>("All-Combo");
            UpdateHitboxTransformMatrix(Rot);
            UpdateHitzone();
        }
        public override void DrawAttackAnim(float Rot, Vector2 CamPos)
        {
            this.Rot = Rot;
            if (Fliped == true)
            {
                _SB.Draw(WeaponTexture, (Origin) - CamPos, new Rectangle(FramePosX * 216, FramePosY * 216, 216, 216), Color.White, Rot, new Vector2(88, 108), new Vector2(1,-1), SpriteEffects.FlipVertically, (float)0.5);
            }
            else
            {
                _SB.Draw(WeaponTexture, Origin - CamPos, new Rectangle(FramePosX * 216, FramePosY * 216, 216, 216), Color.White, Rot, new Vector2(88,108), 1.0f, SpriteEffects.None, (float)0.5);
            }
            if (Rot > -1.04 && Rot < 0)
            {
                offsetX = 108;
                offsetY = -108;
                HitZone = new Rectangle((int)HitzonePos.X, (int)HitzonePos.Y, 64, 128);
            }
            else if (Rot > -2.08 && Rot < -1.04)
            {
                offsetX = 108;
                offsetY = -108;
                HitZone = new Rectangle((int)HitzonePos.X, (int)HitzonePos.Y, 64, 128);
            }
            else if (Rot > -3.14 && Rot < -2.08)
            {
                offsetX = -108;
                offsetY = -108;
                HitZone = new Rectangle((int)HitzonePos.X, (int)HitzonePos.Y, 64, 128);
            }
            else if (Rot < 1.04 && Rot > 0)
            {
                offsetX = 108;
                offsetY = 108;
                HitZone = new Rectangle((int)HitzonePos.X, (int)HitzonePos.Y, 64, 96);
            }
            else if (Rot < 2.08 && Rot > 1.04)
            {
                offsetX = 108;
                offsetY = 108;
                HitZone = new Rectangle((int)HitzonePos.X, (int)HitzonePos.Y, 64, 64);
            }
            else if (Rot < 3.14 && Rot > 2.08)
            {
                offsetX = -108;
                offsetY = 108;
                HitZone = new Rectangle((int)HitzonePos.X, (int)HitzonePos.Y, 64, 96);
            }
        }
        public override void Attack(Character Target)
        {
            if (Attacking == false)
            {
                if(AllowCombo==false)
                {
                    FramePosY = 0;
                    ComboNum = 1;
                    Attacked = true;
                    if (CheckHit(Target) && Target.IsAlive == true)
                    {
                        CalculateDamage();
                        if(Target.GetType()!=typeof(Frankensteint)&& Target.GetType() != typeof(Frankensteint))
                        {
                            Target.SubtraceHP(Damage);
                        }
                        else
                        {
                            Target.SubtraceHP((int)(Damage + Owner._game.PlayerRelic.NormalEnemyDamageMultiRate));
                        }
                        HitCount++;
                        UpdateAnim = true;
                        if (Target.HealthPoint >= 0 && Target.IsAlive == true)
                        {
                            Damages.Add(new PopUpDamage(Damage, Target.CharacterPos, false, Target));
                        }
                        else if (Target.HealthPoint <= 0)
                        {
                            Owner._game.Money += (int)(RAND.Next(Target.BaseMoneyDrop - 100, Target.BaseMoneyDrop + 250) * Owner._game.PlayerRelic.MoneyDropRate);
                            Owner._game.LVProgess += Target.ExpDrop;
                            Target.IsAlive = false;
                        }
                        int TempSP = (int)(RAND.Next(0, 5) * Owner._game.SPGainRate * Owner._game.PlayerRelic.ManaGainRate);
                        if (Owner.GetSP() < 100)
                        {
                            if (Owner.GetSP() + TempSP < 100)
                            {
                                Owner.AddSP(TempSP);
                            }
                            else
                            {
                                Owner.AddSP((Owner.GetSP() + TempSP) - 100);
                            }
                        }
                    }
                    else
                    {
                        UpdateAnim = true;
                        Attacking = false;
                    }
                }
                else if(AllowCombo==true)
                {
                    FramePosY = 3;
                    ComboNum = 2;
                    Attacked = true;
                    if (CheckHit(Target) && Target.IsAlive == true)
                    {
                        CalculateDamage();
                        if (Target.GetType() != typeof(Frankensteint) && Target.GetType() != typeof(Frankensteint))
                        {
                            Target.SubtraceHP(Damage);
                        }
                        else
                        {
                            Target.SubtraceHP((int)(Damage + Owner._game.PlayerRelic.NormalEnemyDamageMultiRate));
                        }
                        HitCount++;
                        UpdateAnim = true;
                        if (Target.HealthPoint >= 0 && Target.IsAlive == true)
                        {
                            Damages.Add(new PopUpDamage(Damage, Target.CharacterPos, false, Target));
                        }
                        else if (Target.HealthPoint <= 0)
                        {
                            Owner._game.Money += (int)(RAND.Next(Target.BaseMoneyDrop - 100, Target.BaseMoneyDrop + 250) * Owner._game.PlayerRelic.MoneyDropRate);
                            Owner._game.LVProgess += Target.ExpDrop;
                            Target.IsAlive = false;
                        }
                        int TempSP = (int)(RAND.Next(0, 5) * Owner._game.SPGainRate * Owner._game.PlayerRelic.ManaGainRate);
                        if (Owner.GetSP()<100)
                        {
                            if(Owner.GetSP()+TempSP<100)
                            {
                                Owner.AddSP(TempSP);
                            }
                            else
                            {
                                Owner.AddSP((Owner.GetSP() + TempSP) - 100);
                            }
                        }
                    }
                    else
                    {
                        UpdateAnim = true;
                        Attacking = false;
                    }
                }
            }
        }
        public override void SpecialAttack(Character Target)
        {
            if (Attacking == false && UpdateAnim == false)
            {
                /*AllowCombo = true;
                if (CheckHit(Target)&&Target.IsAlive==true)
                {
                    CalculateDamage();
                    Target.SubtraceHP(Damage*2);
                    HitCount++;
                    UpdateAnim = true;
                    if(Target.HealthPoint>=0)
                    {
                        Damages.Add(new PopUpDamage(Damage * 2, Target.CharacterPos, true, Target));
                    }
                    else if (Target.HealthPoint <= 0)
                    {
                        Owner._game.Money += RAND.Next(250, 300);
                        Owner._game.LVProgess += 15;
                        Target.IsAlive = false;
                    }
                }
                else
                {
                    UpdateAnim = true;
                    Attacking = false;
                }*/

                FramePosY = 7;
                FramePosX = 0;
                UpdateAnim = true;
                Attacking = true;
            }
        }
        public override void UpdateFrame(float time)
        {
            TotalElapsed += time;
            if (TotalElapsed > TimePerFrame && ComboNum == 1)
            {
                FramePosX = (FramePosX + 1) % 9;
                TotalElapsed -= TimePerFrame;
            }
            if (TotalElapsed > TimePerFrame && ComboNum == 2)
            {
                FramePosX = (FramePosX + 1) % 6;
                TotalElapsed -= TimePerFrame;
            }
            if (FramePosX >= 9 && ComboNum == 1)
            {
                FramePosX = 0;
                Attacking = false;
                UpdateAnim = false;
                Attacked = false;
            }
            else if (FramePosX >= 6 && ComboNum == 2)
            {
                FramePosX = 0;
                Attacking = false;
                UpdateAnim = false;
                Attacked = false;
            }
            if (TotalElapsed > TimePerFrame && FramePosY==7)
            {
                FramePosX = (FramePosX + 1) % 7;
                TotalElapsed -= TimePerFrame;
            }
            if(FramePosX >= 7 && FramePosY == 7)
            {
                CalculateDamage(BaseSAtkDamage);
                Bullets.Add(new MeleeUlti(Owner.GetOrigin(), WeaponTexture, Owner.WeaponRot, Damage));
                FramePosX = 0;
                Attacking = false;
                UpdateAnim = false;
                Attacked = false;
            }

        }
        public override void UpdateWeapon(float time)
        {
            if(ComboNum==1)
            {
                CooldownLimit = 0.45f;
            }
            if(ComboNum==2)
            {
                CooldownLimit = 0.30f;
            }
            if (Attacking == true)
            {
                if (TotalCooldown < CooldownLimit)
                {
                    TotalCooldown += time;
                }
                else
                {
                    Attacking = false;
                    UpdateAnim = false;
                    TotalCooldown -= CooldownLimit;
                    FramePosX = 0;
                    if(ComboNum==1)
                    {
                        AllowCombo = true;
                    }
                    else if(ComboNum==2)
                    {
                        AllowCombo = false;
                    }
                }
                if(AllowCombo==true&&Attacked==false)
                {
                    if (CurComboGap < ComboGapMax)
                    {
                        CurComboGap += time;
                    }
                    else
                    {
                        AllowCombo = false;
                        ComboNum = 1;
                    }
                }
            }
            foreach (Bullet i in Bullets)
            {
                i.Update(time);
            }
            UpdateHitboxTransformMatrix(Rot);
            UpdateHitzone();
            UpdateOrigin(Owner);
        }
    }
}
