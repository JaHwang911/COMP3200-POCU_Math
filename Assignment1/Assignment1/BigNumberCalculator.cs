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

        private static string CheckInputNumberType(string input)
        {
            if (String.IsNullOrWhiteSpace(input))
            {
                return "NaN";
            }

            bool bResult = true;
            int asciiNumberOfZero = 48;
            int asciiNumberOfOne = 49;
            int asciiNumberOfNine = 57;
            int asciiNumberOfFirstUpper = 65;
            int asciiNumberOfLastUpper = 90;

            switch (input[0])
            {
                case '0':
                    if (input.Length <= 2)
                    {
                        bResult = false;
                    }
                    else if (input[1] != 'b' && input[1] != 'x')
                    {
                        bResult = false;
                    }
                    break;
                case '-':
                    if (input.Length < 2 || input[1] == '0')
                    {
                        bResult = false;
                    }
                    else if (input[1] < asciiNumberOfOne || input[1] > asciiNumberOfNine)
                    {
                        bResult = false;
                    }
                    break;
                default:
                    if (input[0] < asciiNumberOfOne || input[0] > asciiNumberOfNine)
                    {
                        bResult = false;
                    }
                    break;
            }

            if (!bResult)
            {
                return "NaN";
            }

            string inputType = "";
            string profix = input.Substring(0, 2);

            switch (profix)
            {
                case "0b":
                    for (int i = 2; i < input.Length; i++)
                    {
                        if (input[i] != '0' && input[i] != '1')
                        {
                            bResult = false;
                            break;
                        }
                    }

                    inputType = bResult ? "bin" : "NaN";
                    break;
                case "0x":
                    for (int i = 2; i < input.Length; i++)
                    {
                        if (input[i] < asciiNumberOfZero || input[i] > asciiNumberOfNine)
                        {
                            if (input[i] < asciiNumberOfFirstUpper || input[i] > asciiNumberOfLastUpper)
                            {
                                bResult = false;
                                break;
                            }
                        }
                    }

                    inputType = bResult ? "hex" : "NaN";
                    break;
                default:
                    for (int i = 1; i < input.Length; i++)
                    {
                        if (input[i] < asciiNumberOfZero || input[i] > asciiNumberOfNine)
                        {
                            bResult = false;
                            break;
                        }
                    }

                    inputType = bResult ? "dec" : "NaN";
                    break;
            }

            return inputType;
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
            ulong test = 1;
            int resultInt = 0;

            for (int i = num.Length - 1; i >= 0; i--)
            {
                if (num[i] == '1')
                {
                    resultInt += index;
                }

                index *= 2;
                test *= 2;
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

        private static int ConvertHexToDecimal(char num)
        {
            int asciiNumberOfNine = 57;
            int asciiIndex = 55;

            if (num <= asciiNumberOfNine)
            {
                asciiIndex = 48;
            }

            return num - asciiIndex;
        }

        private static string ConvertHexToBinary(string num)
        {
            return null;
        }

        public static string GetOnesComplementOrNull(string num)
        {
            string numberType = CheckInputNumberType(num);

            if (numberType != "bin")
            {
                return null;
            }

            string inputBinary = num.Substring(2);
            string resultString = ReverseBit(inputBinary);

            return $"0b{resultString}";
        }

        public static string GetTwosComplementOrNull(string num)
        {
            string numberType = CheckInputNumberType(num);

            if (numberType != "bin")
            {
                return null;
            }

            string inputBinary = num.Substring(2);
            string resultString = ReverseBit(inputBinary);
            int resultDecimal = ConvertBinaryToDecimal(resultString) + 1;
            string resultbinary = ConvertDecimalToBinary(resultDecimal);

            return $"0b{resultbinary}";
        }

        public static string ToBinaryOrNull(string num)
        {
            string numberType = CheckInputNumberType(num);

            if (numberType == "NaN")
            {
                return null;
            }

            string resultValue = "";
            StringBuilder convertedBinary = new StringBuilder();

            switch (numberType)
            {
                case "bin":
                    resultValue = num;
                    break;
                case "hex":
                    StringBuilder resultBinary = new StringBuilder();
                    for (int i = 2; i < num.Length; i++)
                    {
                        int convertedDecimal = ConvertHexToDecimal(num[i]);
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
                case "dec":
                    int inputDecimal = int.Parse(num);
                    bool bIsNegative = false;

                    if (inputDecimal < 0)
                    {
                        inputDecimal *= -1;
                        bIsNegative = true;
                    }

                    char sign = '0';
                    convertedBinary.Append(ConvertDecimalToBinary(inputDecimal));
                    convertedBinary.Insert(0, sign);
                    string possitiveBinary = convertedBinary.ToString();

                    if (bIsNegative)
                    {
                        string reverseBinary = ReverseBit(possitiveBinary);
                        int resultDecimal = ConvertBinaryToDecimal(reverseBinary) + 1;
                        string negativebinary = ConvertDecimalToBinary(resultDecimal);
                        resultValue = $"0b{negativebinary}";
                    }
                    else
                    {
                        resultValue = $"0b{possitiveBinary}";
                    }
                    break;
                default:
                    Debug.Assert(false, "Wrong CheckInputDataType");
                    break;
            }

            return resultValue;
        }

        public static string ToDecimalOrNull(string num)
        {
            string numberType = CheckInputNumberType(num);

            if (numberType == "NaN")
            {
                return null;
            }

            string resultValue = "";

            switch(numberType)
            {
                case "bin":
                    string binary = num.Substring(2);
                    if (binary[0] == '0')
                    {
                        int convertedDecimal = ConvertBinaryToDecimal(binary);
                        resultValue = convertedDecimal.ToString();
                    }
                    else
                    {
                        string reverseBinary = ReverseBit(binary);
                        int tempDecimal = ConvertBinaryToDecimal(reverseBinary) + 1;
                        resultValue = $"-{tempDecimal.ToString()}";
                    }
                    break;
                case "hex":
                    string hex = num.Substring(2);
                    StringBuilder convertedBinary = new StringBuilder();
                    StringBuilder tempBinary = new StringBuilder();

                    for (int i = hex.Length - 1; i >= 0; i--)
                    {
                        int convertedDecimal = ConvertHexToDecimal(hex[i]);
                        tempBinary.Clear();
                        tempBinary.Append(ConvertDecimalToBinary(convertedDecimal));

                        if (tempBinary.Length < 4)
                        {
                            int loopCount = 4 - tempBinary.Length;

                            for (int j = 0; j < loopCount; j++)
                            {
                                tempBinary.Insert(0, '0');
                            }
                        }
                        convertedBinary.Insert(0, tempBinary.ToString());
                    }
                    convertedBinary.Insert(0, "0b");
                    resultValue = ToDecimalOrNull(convertedBinary.ToString());
                    break;
                case "dec":
                    resultValue = num;
                    break;
                default:
                    Debug.Assert(false, "Wrong CheckInputNumberType function");
                    break;
            }

            return resultValue;
        }
    }
}