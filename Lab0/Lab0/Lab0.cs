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

            List<int> quotient = new List<int>();
            List<int> remainder = new List<int>();
            int loopCount = 1;
            int startIndex = 0;
            int endIndex = 0;

            quotient.Add(numerator / denominator);
            remainder.Add(numerator % denominator);

            while (loopCount <= 100)
            {
                int remainderLastIndex = remainder.Count - 1;
                int tempNumerator = remainder[remainderLastIndex] * 10;
                quotient.Add(tempNumerator / denominator);
                int tempRemainder = tempNumerator % denominator;
                int hasSameRemainder = remainder.IndexOf(tempRemainder);

                if (tempRemainder == 0)
                {
                    bResult = false;
                    break;
                }
                else if (hasSameRemainder >= 0)
                {
                    startIndex = hasSameRemainder + 1;
                    endIndex = loopCount;
                    bResult = true;
                    break;
                }

                remainder.Add(tempRemainder);
                loopCount++;
            }

            if (!bResult)
            {
                double sum = (double)numerator / (double)denominator;
                stringDecimal = sum.ToString();
            }
            else
            {
                StringBuilder tempResult = new StringBuilder(100);

                for (int i = 0; i < quotient.Count; i++)
                {
                    if (i == startIndex)
                    {
                        tempResult.Append("*");
                    }

                    tempResult.Append(quotient[i]);

                    if (i == endIndex)
                    {
                        tempResult.Append("*");
                    }
                }

                tempResult.Insert(1, ".");
                stringDecimal = tempResult.ToString();
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
