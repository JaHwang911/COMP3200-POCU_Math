using System;
using System.Diagnostics;
using System.Text;

namespace Assignment1
{
    public class BigNumberCalculator
    {
        public BigNumberCalculator(int bitCount, EMode mode)
        {

        }

        private static int ConvertToDecimal(string num)
        {
            int index = 1;
            int resultInt = 0;

            for (int i = num.Length - 1; i >=0; i--)
            {
                if (num[i] == '1')
                {
                    resultInt += index;
                }

                index <<= 1;
            }

            return resultInt;
        }

        public static string GetOnesComplementOrNull(string num)
        {
            if (num[0] != '0' || num[1] != 'b')
            {
                return null;
            }

            for (int i = 2; i < num.Length; i++)
            {
                if (num[i] != '0' && num[i] != '1')
                {
                    return null;
                }
            }

            string inputBinary = num.Substring(2);
            StringBuilder resultString = new StringBuilder("0b", num.Length);

            foreach(var bit in inputBinary)
            {
                switch (bit)
                {
                    case '0':
                        resultString.Append('1');
                        break;
                    case '1':
                        resultString.Append('0');
                        break;
                    default:
                        Debug.Assert(false, "Wrong input binary");
                        break;
                }
            }

            return resultString.ToString();
        }
    }
}