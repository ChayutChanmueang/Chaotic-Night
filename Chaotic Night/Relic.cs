using System;
using System.Collections.Generic;
using System.Text;

namespace Chaotic_Night
{
    public class Relic
    {
        public float MaxHPMultiRate;
        public float BaseDamageMultiRate;
        public float NormalEnemyDamageMultiRate;
        public float MoneyDropRate;
        public float ManaGainRate;
        public float MeleeDamageResistance;
        public float RangeDamageResistance;
        public Relic()
        {
            MaxHPMultiRate=1;
            BaseDamageMultiRate=1;
            NormalEnemyDamageMultiRate=1;
            MoneyDropRate=1;
            ManaGainRate=1;
            MeleeDamageResistance=0;
            RangeDamageResistance=0;
        }
    }
}
