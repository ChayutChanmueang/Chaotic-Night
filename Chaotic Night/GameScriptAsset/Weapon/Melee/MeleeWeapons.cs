using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Chaotic_Night
{
    class MeleeWeapons : Weapons
    {
        public bool AllowCombo = false;
        public MeleeWeapons(Character OwningCharacter) : base(OwningCharacter)
        {
            Owner = OwningCharacter;
            FrameEnd = 7;
        }
        public override void Attack(Character Target)
        {
            if (Attacking == false)
            {
                AllowCombo = true;
                if (CheckHit(Target))
                {
                    Target.SubtraceHP(Damage);
                    HitCount++;
                    UpdateAnim = true;
                }
                else
                {
                    UpdateAnim = true;
                    Attacking = false;
                }
            }
        }
        public override void SpecialAttack(Character Target)
        {
            if (Attacking == false)
            {
                AllowCombo = true;
                if (CheckHit(Target))
                {
                    Target.SubtraceHP(Damage*2);
                    HitCount++;
                    UpdateAnim = true;
                }
                else
                {
                    UpdateAnim = true;
                    Attacking = false;
                }
            }
        }
    }
}
