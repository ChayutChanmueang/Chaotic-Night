using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Chaotic_Night
{
    class MathEx
    {
        public static float Min(params float[] values)
        {
            return values.Min();
        }
        public static float Max(params float[] values)
        {
            return values.Max();
        }
    }
}
