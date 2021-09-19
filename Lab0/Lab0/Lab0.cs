using System;
using System.Collections.Generic;
using System.Text;

namespace Lab0
{
    public static class Lab0
    {
        public static bool TryGetRepeatingDecimal(int numerator, int denominator, out string stringDecimal)
        {
            bool bResult = false;
            stringDecimal = "";

            if (numerator <= 0 || denominator <= 0 || denominator >= 100)
            {
                stringDecimal = "0";
                return bResult;
            }

            decimal sum = (decimal)numerator / (decimal)denominator;
            stringDecimal = sum.ToString();

            if (stringDecimal.Length <= 10)
            {
                return false;
            }

            //Dictionary<char, int> cycleValue = new Dictionary<char, int>(10);
            List<char> cycleValue = new List<char>(14);
            int cycleCount = 0;

            for (int i = 2; i < stringDecimal.Length; i++)
            {
                if (i == 15 || cycleCount == 0)
                {
                    return false;
                }
                else if (cycleValue.Contains(stringDecimal[i]))
                {
                    cycleCount++;
                }
                else
                {
                    cycleValue.Add(stringDecimal[i]);
                }
            }

            return bResult;
        }

        public static bool TryGetIrreducibleFraction(string stringDecimal, out string fraction)
        {
            fraction = "";

            return false;
        }
    }
}
