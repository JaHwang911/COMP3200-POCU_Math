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

        private static int ConvertBinaryToDecimal(string num)
        {
            int index = 1;
            int resultInt = 0;

            for (int i = num.Length - 1; i >=0; i--)
            {
                if (num[i] == '1')
                {
                    resultInt += index;
                }

                index *= 2;
            }

            return resultInt;
        }

        private static string ConvertDecimalToBinary(int num)
        {
            if (num == 0)
            {
                return "0000";
            }

            StringBuilder binary = new StringBuilder();

            while(num > 1)
            {
                int bit = num % 2;
                binary.Insert(0, bit);
                num /= 2;
            }

            binary.Insert(0, num);

            return binary.ToString();
        }

        public static string GetOnesComplementOrNull(string num)
        {
            if (num.Length <= 2)
            {
                return null;
            }
            else if (num[0] != '0' || num[1] != 'b')
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

        public static string GetTwosComplementOrNull(string num)
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
            StringBuilder resultString = new StringBuilder(num.Length);

            foreach (var bit in inputBinary)
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

            int resultDecimal = ConvertBinaryToDecimal(resultString.ToString()) + 1;
            string resultbinary = ConvertDecimalToBinary(resultDecimal);

            return $"0b{resultbinary}";
        }

        public static string ToBinaryOrNull(string num)
        {
            if (String.IsNullOrWhiteSpace(num))
            {
                return null;
            }
            else if (num[0] == '0')
            {
                if (num.Length <= 2)
                {
                    return null;
                }
                else if (num[1] != 'b' && num[1] != 'x')
                {
                    return null;
                }
            }
            else if ((int)num[0] < 49 || (int)num[0] > 57)
            {
                if (num[0] == '-')
                {
                    if (num.Length < 2)
                    {
                        return null;
                    }
                    if ((int)num[1] < 49 || (int)num[1] > 57)
                    {
                        return null;
                    }
                }
                else
                {
                    return null;
                }
            }

            int inputDecimal;
            bool bIsNum = int.TryParse(num, out inputDecimal);

            if (bIsNum)
            {
                string binary = ConvertDecimalToBinary(inputDecimal); // 자리 계산 해야함

                return binary;
            }

            string profix = num.Substring(0, 2);
            string resultValue = "";

            switch(profix)
            {
                case "0b":
                    for (int i = 2; i < num.Length; i++)
                    {
                        if (num[i] != '0' && num[i] != '1')
                        {
                            return null;
                        }
                    }

                    resultValue = num;
                    break;
                case "0x": // 대문자 소문자 구분 아스키로
                    
                    break;
                default:
                    resultValue = null;
                    break;
            }

            return resultValue;
        }
    }
}