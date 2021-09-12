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

        private static string ReverseBit(string inputBinary)
        {
            StringBuilder resultString = new StringBuilder(inputBinary.Length);

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

            return resultString.ToString();
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

            int insertIndex = 0;
            StringBuilder binary = new StringBuilder(64);

            while (num > 1)
            {
                int bit = num % 2;
                binary.Insert(insertIndex, bit);
                num /= 2;
            }

            binary.Insert(insertIndex, num);

            return binary.ToString();
        }

        private static int ConvertHexToDecimal(string num)
        {

            return 0;
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
            string resultString = ReverseBit(inputBinary);

            return $"0b{resultString}";
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
            string resultString = ReverseBit(inputBinary);

            int resultDecimal = ConvertBinaryToDecimal(resultString) + 1;
            string resultbinary = ConvertDecimalToBinary(resultDecimal);

            return $"0b{resultbinary}";
        }

        public static string ToBinaryOrNull(string num)
        {
            int asciiNumberOfOne = 49;
            int asciiNumberOfNine = 57;

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
            else if ((int)num[0] < asciiNumberOfOne || (int)num[0] > asciiNumberOfNine)
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
            StringBuilder convertedBinary = new StringBuilder();

            if (bIsNum)
            {
                bool bIsNegative = false;
                if (inputDecimal < 0)
                {
                    inputDecimal *= -1;
                    bIsNegative = true;
                }

                char sign = '0';
                convertedBinary.Append(ConvertDecimalToBinary(inputDecimal));
                convertedBinary.Insert(0, sign);
                string result = convertedBinary.ToString();

                if (bIsNegative)
                {
                    string reverseBinary = ReverseBit(result);
                    int resultDecimal = ConvertBinaryToDecimal(reverseBinary) + 1;
                    string resultbinary = ConvertDecimalToBinary(resultDecimal);
                    return $"0b{resultbinary}";
                }

                return $"0b{result}";
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
                    int asciiNumberOfFirstUpper = 65;
                    int asciiNumberOfLastUpper = 90;
                    int asciiNumberOfZero = 48;
                    StringBuilder resultBinary = new StringBuilder();

                    for (int i = 2; i < num.Length; i++)
                    {
                        if ((int)num[i] < asciiNumberOfZero || (int)num[i] > asciiNumberOfNine)
                        {
                            if ((int)num[i] < asciiNumberOfFirstUpper || (int)num[i] > asciiNumberOfLastUpper)
                            {
                                return null;
                            }
                        }

                        int asciiIndex = 55;

                        if ((int)num[i] <= asciiNumberOfNine)
                        {
                            asciiIndex = 48;
                        }

                        int convertedDecimal = num[i] - asciiIndex;
                        convertedBinary.Clear();
                        convertedBinary.Append(ConvertDecimalToBinary(convertedDecimal));

                        if (convertedBinary.Length < 4)
                        {
                            int loopCount = 4 - convertedBinary.Length;

                            for (int j = 0; j < loopCount; j++)
                            {
                                convertedBinary.Insert(0, '0');
                            }
                        }

                        resultBinary.Append(convertedBinary);
                    }
                    
                    resultValue = "0b" + resultBinary.ToString();
                    break;
                default:
                    resultValue = null;
                    break;
            }

            return resultValue;
        }
    }
}