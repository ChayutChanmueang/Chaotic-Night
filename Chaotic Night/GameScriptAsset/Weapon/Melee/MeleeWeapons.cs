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
        }
        public override void Attack(Character Target)
        {
            if (Attacking == false)
            {
                Attacking = true;
                AllowCombo = true;
                if (CheckHit(Target))
                {
                    Target.SubtraceHP(Damage);
                    HitCount++;
                }
            }
        }
        public override void SpecialAttack(Character Target)
        {
            if (Attacking == false)
            {
                Attacking = true;
                AllowCombo = true;
                if (CheckHit(Target))
                {
                    Target.SubtraceHP(Damage*2);
                    HitCount++;
                }
            }
        }
    }
}
