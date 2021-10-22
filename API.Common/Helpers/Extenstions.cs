using System;
using System.Collections.Generic;
using System.Text;

namespace API.Common.Helpers
{
    public static class Extenstions
    {
        public static float SubtractFloat(float num1,  float num2)
        {
            var res = (decimal)num1 - (decimal)num2;
            return (float)res;
        }
    }
}
