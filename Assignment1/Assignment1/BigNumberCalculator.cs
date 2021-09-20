﻿using System;
using System.Diagnostics;
using System.Text;

namespace Assignment1
{
    public class BigNumberCalculator
    {
        public int BitCount { get; private set; }
        public EMode OutputType { get; private set; }
        public string MaxNumber { get; private set; }
        public string MinNumber { get; private set; }

        public BigNumberCalculator(int bitCount, EMode mode)
        {
            BitCount = bitCount;
            OutputType = mode;
            GetMaxAndMinByBitCount();
        }

        public string[] GetMaxAndMinByBitCount()
        {
            string[] result = new string[2];
            StringBuilder allOneBit = new StringBuilder();

            for (int i =0; i < BitCount - 1; i++)
            {
                allOneBit.Append("1");
            }

            MaxNumber = ConvertBinaryToDecimal(allOneBit.ToString());
            MinNumber = $"-{StringCalculator.PlusOperating(MaxNumber, "1")}";

            return result;
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

        private static string ConvertBinaryToDecimal(string num)
        {
            string binaryIndex = "1";
            string result = "0";

            for (int i = num.Length - 1; i >= 0; i--)
            {
                if (num[i] == '1')
                {
                    result = StringCalculator.PlusOperating(result, binaryIndex);
                }

                binaryIndex = StringCalculator.MultiplyOperating(binaryIndex, "2");
            }

            return result;
        }

        private static string ConvertDecimalToBinary(string input)
        {
            if (input == "0")
            {
                return "0000";
            }

            StringBuilder binary = new StringBuilder(256);

            while (input != "1" && input != "0")
            {
                string[] bit = StringCalculator.DivideOperating(input, "2");
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
            ENumberType numberType = StringCalculator.CheckInputNumberType(num);

            if (numberType != ENumberType.Binary)
            {
                return null;
            }

            string inputBinary = num.Substring(2);
            string resultString = ReverseBit(inputBinary);

            return $"0b{resultString}";
        }

        public static string GetTwosComplementOrNull(string num)
        {
            ENumberType numberType = StringCalculator.CheckInputNumberType(num);

            if (numberType != ENumberType.Binary)
            {
                return null;
            }

            string inputBinary = num.Substring(2);
            string resultString = ReverseBit(inputBinary);
            string resultDecimal = ConvertBinaryToDecimal(resultString);
            resultDecimal = StringCalculator.PlusOperating(resultDecimal, "1");
            string resultbinary = ConvertDecimalToBinary(resultDecimal);

            return $"0b{resultbinary}";
        }

        public static string ToBinaryOrNull(string num)
        {
            ENumberType numberType = StringCalculator.CheckInputNumberType(num);

            if (numberType == ENumberType.NaN)
            {
                return null;
            }

            string resultValue = "";
            StringBuilder convertedBinary = new StringBuilder();

            switch (numberType)
            {
                case ENumberType.Binary:
                    resultValue = num;
                    break;
                case ENumberType.Hex:
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

                    resultValue = $"0b{resultBinary}";
                    break;
                case ENumberType.Decimal:
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
                        resultDecimal = StringCalculator.PlusOperating(resultDecimal, "1");
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
            ENumberType numberType = StringCalculator.CheckInputNumberType(num);

            if (numberType == ENumberType.NaN)
            {
                return null;
            }

            string resultValue = "";

            switch(numberType)
            {
                case ENumberType.Binary:
                    string binary = num.Substring(2);

                    if (binary[0] == '0')
                    {
                        if (binary.Length > 20)
                        {
                            string converted = ConvertBinaryToDecimal(binary);
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
                            string converted = ConvertBinaryToDecimal(reverseBinary);
                            string temp = StringCalculator.PlusOperating(converted, "1");
                            resultValue = $"-{temp}";
                        }
                        else
                        {
                            string resultDecimal = ConvertBinaryToDecimal(reverseBinary);
                            resultDecimal = StringCalculator.PlusOperating(resultDecimal, "1");
                            resultValue = $"-{resultDecimal}";
                        }
                    }
                    break;
                case ENumberType.Hex:
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
                case ENumberType.Decimal:
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
            ENumberType numberType = StringCalculator.CheckInputNumberType(num);

            if (numberType == ENumberType.NaN)
            {
                return null;
            }

            string resultValue = "";

            switch (numberType)
            {
                case ENumberType.Binary:
                    string binary = num.Substring(2);
                    StringBuilder result = new StringBuilder(256);
                    StringBuilder convertedHex = new StringBuilder(4);

                    for (int i = binary.Length - 1; i >= 0; i--)
                    {
                        convertedHex.Insert(0, binary[i]);

                        if (convertedHex.Length == 4)
                        {
                            string convertedDecimal = ConvertBinaryToDecimal(convertedHex.ToString());
                            int tempDecimal = int.Parse(convertedDecimal);

                            if (tempDecimal >= 10)
                            {
                                tempDecimal += 55;
                                result.Insert(0, (char)tempDecimal);
                            }
                            else
                            {
                                result.Insert(0, convertedDecimal);
                            }

                            convertedHex.Clear();
                        }
                        else if(i == 0)
                        {
                            int loopCount = 4 - convertedHex.Length;

                            for (int j = 0; j < loopCount; j++)
                            {
                                convertedHex.Insert(0, binary[i]);
                            }
                            string convertedDecimal = ConvertBinaryToDecimal(convertedHex.ToString());
                            int tempDecimal = int.Parse(convertedDecimal);

                            if (tempDecimal >= 10)
                            {
                                tempDecimal += 55;
                                result.Insert(0, (char)tempDecimal);
                            }
                            else
                            {
                                result.Insert(0, convertedDecimal);
                            }
                        }
                    }

                    resultValue = $"0x{result}";
                    break;
                case ENumberType.Hex:
                    resultValue = num;
                    break;
                case ENumberType.Decimal:
                    string inputDecimal = num;
                    bool bIsNegative = false;
                    StringBuilder convertedBinary = new StringBuilder(256);
                    if (inputDecimal[0] == '-')
                    {
                        inputDecimal = inputDecimal.Substring(1);
                        bIsNegative = true;
                    }

                    char sign = '0';
                    convertedBinary.Append(ConvertDecimalToBinary(inputDecimal));
                    convertedBinary.Insert(0, sign);
                    string possitiveBinary = convertedBinary.ToString();
                    convertedBinary.Clear();

                    if (bIsNegative)
                    {
                        string reverseBinary = ReverseBit(possitiveBinary);
                        string resultDecimal = ConvertBinaryToDecimal(reverseBinary);
                        resultDecimal = StringCalculator.PlusOperating(resultDecimal, "1");
                        string negativebinary = ConvertDecimalToBinary(resultDecimal);
                        convertedBinary.Append($"0b{negativebinary}");
                    }
                    else
                    {
                        convertedBinary.Append($"0b{possitiveBinary}");
                    }
                    resultValue = ToHexOrNull(convertedBinary.ToString());
                    break;
                default:
                    Debug.Assert(false, "Wrong CheckInputNumberType function");
                    break;
            }

            return resultValue;
        }

        public string AddOrNull(string num1, string num2, out bool bOverflow)
        {
            bOverflow = false;

            if (StringCalculator.SizeComparison(MaxNumber, num1) == EComparison.Smaller)
            {
                return null;
            }
            else if (StringCalculator.SizeComparison(MinNumber, num1) == EComparison.Bigger)
            {
                return null;
            }
            else if (StringCalculator.SizeComparison(MaxNumber, num2) == EComparison.Smaller)
            {
                return null;
            }
            else if (StringCalculator.SizeComparison(MinNumber, num2) == EComparison.Bigger)
            {
                return null;
            }

            string input1 = ToDecimalOrNull(num1);
            string input2 = ToDecimalOrNull(num2);
            string result = StringCalculator.PlusOperating(input1, input2);
            EComparison comparison = EComparison.Same;

            if (result[0] == '-')
            {
                comparison = StringCalculator.SizeComparison(MinNumber, result);

                if (comparison == EComparison.Bigger)
                {
                    bOverflow = true;
                    result = StringCalculator.MinusOperating(result, MinNumber);
                    result = StringCalculator.PlusOperating(result, "1");
                    result = StringCalculator.PlusOperating(MaxNumber, result);
                }
            }
            else
            {
                comparison = StringCalculator.SizeComparison(MaxNumber, result);

                if (comparison == EComparison.Smaller)
                {
                    bOverflow = true;
                    result = StringCalculator.MinusOperating(result, MaxNumber);
                    result = StringCalculator.MinusOperating(result, "1");
                    result = StringCalculator.PlusOperating(MinNumber, result);
                }
            }

            if (OutputType == EMode.Binary) // BitCount 만큼 제한 비트 수 제한
            {
                result = ToBinaryOrNull(result);
            }

            return result;
        }
    }
}