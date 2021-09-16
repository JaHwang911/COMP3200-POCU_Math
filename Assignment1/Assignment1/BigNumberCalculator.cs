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
        public static string ReverseBit(string inputBinary)
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

        private static string ConvertBinaryToDecimal(string num)
        {
            string binaryIndex = "1";
            string result = "0";

            for (int i = num.Length - 1; i >= 0; i--)
            {
                if (num[i] == '1')
                {
                    result = StringCalculator.PlusOperatingByString(result, binaryIndex);
                }

                binaryIndex = StringCalculator.MultiplyOperatingByString(binaryIndex, "2");
            }

            return result;
        }

        public static string ConvertDecimalToBinary(string input)
        {
            if (input == "0")
            {
                return "0000";
            }

            StringBuilder binary = new StringBuilder(64);

            while (input != "1" && input != "0")
            {
                string[] bit = StringCalculator.DivideOperatingByString(input, "2");
                binary.Insert(0, bit[1]);
                input = bit[0];
            }

            binary.Insert(0, input);

            return binary.ToString();
        }

        private static string ConvertHexToDecimal(char num)
        {
            int asciiNumberOfNine = 57;
            int asciiIndex = 55;

            if (num <= asciiNumberOfNine)
            {
                asciiIndex = 48;
            }

            int result = num - asciiIndex;

            return result.ToString();
        }

        public static string GetOnesComplementOrNull(string num)
        {
            string numberType = StringCalculator.CheckInputNumberType(num);

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
            string numberType = StringCalculator.CheckInputNumberType(num);

            if (numberType != "bin")
            {
                return null;
            }

            string inputBinary = num.Substring(2);
            string resultString = ReverseBit(inputBinary);
            string resultDecimal = ConvertBinaryToDecimal(resultString);
            resultDecimal = StringCalculator.PlusOperatingByString(resultDecimal, "1");
            string resultbinary = ConvertDecimalToBinary(resultDecimal);

            return $"0b{resultbinary}";
        }

        public static string ToBinaryOrNull(string num)
        {
            string numberType = StringCalculator.CheckInputNumberType(num);

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
                        string convertedDecimal = ConvertHexToDecimal(num[i]);
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
                    string inputDecimal = num;
                    bool bIsNegative = false;

                    if (num[0] == '-')
                    {
                        inputDecimal = inputDecimal.Substring(1);
                        bIsNegative = true;
                    }

                    char sign = '0';
                    convertedBinary.Append(ConvertDecimalToBinary(inputDecimal));
                    convertedBinary.Insert(0, sign);
                    string possitiveBinary = convertedBinary.ToString();

                    if (bIsNegative)
                    {
                        string reverseBinary = ReverseBit(possitiveBinary);
                        string resultDecimal = ConvertBinaryToDecimal(reverseBinary);
                        resultDecimal = StringCalculator.PlusOperatingByString(resultDecimal, "1");
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
            string numberType = StringCalculator.CheckInputNumberType(num);

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
                        if (binary.Length > 20)
                        {
                            string converted = StringCalculator.ConvertBigBinaryToDecimal(binary);
                            resultValue = converted;
                        }
                        else
                        {
                            resultValue = ConvertBinaryToDecimal(binary);
                        }
                    }
                    else
                    {
                        string reverseBinary = ReverseBit(binary);
                        if (binary.Length > 20)
                        {
                            string converted = StringCalculator.ConvertBigBinaryToDecimal(reverseBinary);
                            string temp = StringCalculator.PlusOperatingByString(converted, "1");
                            resultValue = $"-{temp}";
                        }
                        else
                        {
                            string resultDecimal = ConvertBinaryToDecimal(reverseBinary);
                            resultDecimal = StringCalculator.PlusOperatingByString(resultDecimal, "1");
                            resultValue = $"-{resultDecimal}";
                        }
                    }
                    break;
                case "hex":
                    string hex = num.Substring(2);
                    StringBuilder convertedBinary = new StringBuilder();
                    StringBuilder tempBinary = new StringBuilder(4);

                    for (int i = hex.Length - 1; i >= 0; i--)
                    {
                        string convertedDecimal = ConvertHexToDecimal(hex[i]);
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
        public static string ToHexOrNull(string num)
        {
            return null;
        }
    }
}