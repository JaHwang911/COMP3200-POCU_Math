using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Text;

namespace Assignment1
{
    public class BigNumberCalculator
    {
        public static int BitCount { get; private set; }
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

            for (int i = 0; i < BitCount - 1; i++)
            {
                allOneBit.Append("1");
            }

            MaxNumber = ConvertBinaryToDecimal(allOneBit.ToString());
            MinNumber = $"-{StringCalculator.PlusOperating(MaxNumber, "1")}";

            return result;
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

        public static string OperateByBinary(string x, string y, EOperatingMode operatingMode, out bool bOverflow)
        {
            bOverflow = false;
            StringBuilder result = new StringBuilder(256);
            StringBuilder minuend = new StringBuilder(x);
            StringBuilder subtrahend = new StringBuilder(y);
            string absoultValueX = x[0] == '-' ? x.Substring(1) : x;
            string absoultValueY = y[0] == '-' ? y.Substring(1) : y;
            char signBit = '0';
            bool bExpectedNegative = false;
            EComparison comparison = StringCalculator.SizeComparison(absoultValueX, absoultValueY);

            if (operatingMode == EOperatingMode.Substract)
            {
                if (subtrahend[0] != '-')
                {
                    subtrahend.Insert(0, '-');
                }
                else if (subtrahend[0] == '-')
                {
                    subtrahend.Remove(0, 1);
                }
            }

            if (comparison == EComparison.Smaller && subtrahend[0] == '-')
            {
                signBit = '1';
                bExpectedNegative = true;
            }
            else if (comparison == EComparison.Bigger && minuend[0] == '-')
            {
                signBit = '1';
                bExpectedNegative = true;
            }
            else if (minuend[0] == '-' && subtrahend[0] == '-')
            {
                signBit = '1';
                bExpectedNegative = true;
            }

            x = ToBinaryOrNull(minuend.ToString());
            y = ToBinaryOrNull(subtrahend.ToString());
            minuend.Clear();
            subtrahend.Clear();
            minuend.Append(x);
            subtrahend.Append(y);

            int inputXBitCount = minuend.Length - 2;
            int inputYBitCount = subtrahend.Length - 2;

            if (inputXBitCount > BitCount)
            {
                minuend.Remove(2, inputXBitCount - BitCount);
            }

            if (inputYBitCount > BitCount)
            {
                subtrahend.Remove(2, inputXBitCount - BitCount);
            }

            int difference = minuend.Length - subtrahend.Length;
            
            if (difference < 0)
            {
                string temp = minuend.ToString();
                minuend.Clear();
                minuend.Append(subtrahend.ToString());
                subtrahend.Clear();
                subtrahend.Append(temp);
                difference *= -1;
            }

            for (int i = 0; i < difference; i++)
            {
                subtrahend.Insert(2, subtrahend[2]);
            }

            int remainder = 0;
            int sum = 0;
            int asciiNumberOfZero = 48;

            for (int i = minuend.Length - 1; i >= 2; i--)
            {
                int bigNum = minuend[i] - asciiNumberOfZero;
                int smallNum = subtrahend[i] - asciiNumberOfZero;
                sum = bigNum + smallNum + remainder;

                remainder = sum > 1 ? 1 : 0;
                sum = sum % 2 == 1 ? 1 : 0;

                result.Insert(0, sum);
            }

            if (result.Length < BitCount)
            {
                int loopCount = BitCount - result.Length;
                for (int i = 0; i < loopCount; i++)
                {
                    result.Insert(0, signBit);
                }
            }
            else if (result.Length > BitCount)
            {
                result.Remove(2, result.Length - BitCount);
            }

            if (bExpectedNegative && result[0] == '0')
            {
                bOverflow = true;
            }
            else if (!bExpectedNegative && result[0] == '1')
            {
                bOverflow = true;
            }

            result.Insert(0, "0b");

            return result.ToString();
        }

        public static string ConvertBinaryToDecimal(string num)
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

        public static string ConvertDecimalToBinary(string input)
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

        public static string ConvertHexToDecimal(char num)
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
            if (inputBinary == "0")
            {
                return "0b0";
            }
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
            List<char> converted = new List<char>(256);

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
                    else if (num == "0")
                    {
                        return "0b0";
                    }

                    string tempCovnerted = ConvertDecimalToBinary(inputDecimal);
                    int hasOneBit = tempCovnerted.IndexOf('1', 1);

                    if (hasOneBit == -1 && bIsNegative)
                    {
                        return $"0b{tempCovnerted}";
                    }
                    char sign = '0';
                    convertedBinary.Append(sign + tempCovnerted);
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

            switch (numberType)
            {
                case ENumberType.Binary:
                    string binary = num.Substring(2);

                    if (binary[0] == '0')
                    {
                        resultValue = ConvertBinaryToDecimal(binary);
                    }
                    else
                    {
                        string reverseBinary = ReverseBit(binary);
                        string resultDecimal = ConvertBinaryToDecimal(reverseBinary);
                        resultDecimal = StringCalculator.PlusOperating(resultDecimal, "1");
                        resultValue = $"-{resultDecimal}";
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
                        else if (i == 0)
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
                    if (num == "0")
                    {
                        return "0x0";
                    }
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
            string input1 = num1;
            string input2 = num2;
            ENumberType numberType = StringCalculator.CheckInputNumberType(num1);

            if (numberType == ENumberType.NaN)
            {
                return null;
            }
            else if (numberType != ENumberType.Decimal)
            {
                input1 = ToDecimalOrNull(num1);
            }

            numberType = StringCalculator.CheckInputNumberType(num2);

            if (numberType == ENumberType.NaN)
            {
                return null;
            }
            else if (numberType != ENumberType.Decimal)
            {
                input2 = ToDecimalOrNull(num2);
            }

            if (StringCalculator.SizeComparison(MaxNumber, input1) == EComparison.Smaller)
            {
                return null;
            }
            else if (StringCalculator.SizeComparison(MinNumber, input1) == EComparison.Bigger)
            {
                return null;
            }
            else if (StringCalculator.SizeComparison(MaxNumber, input2) == EComparison.Smaller)
            {
                return null;
            }
            else if (StringCalculator.SizeComparison(MinNumber, input2) == EComparison.Bigger)
            {
                return null;
            }

            string result = OperateByBinary(input1, input2, EOperatingMode.Add, out bOverflow);

            if (OutputType == EMode.Decimal)
            {
                result = ToDecimalOrNull(result);
            }

            return result;
        }

        public string SubtractOrNull(string num1, string num2, out bool bOverflow)
        {
            bOverflow = false;
            string input1 = num1;
            string input2 = num2;
            ENumberType numberType = StringCalculator.CheckInputNumberType(num1);

            if (numberType == ENumberType.NaN)
            {
                return null;
            }
            else if (numberType != ENumberType.Decimal)
            {
                input1 = ToDecimalOrNull(num1);
            }

            numberType = StringCalculator.CheckInputNumberType(num2);

            if (numberType == ENumberType.NaN)
            {
                return null;
            }
            else if (numberType != ENumberType.Decimal)
            {
                input2 = ToDecimalOrNull(num2);
            }

            if (StringCalculator.SizeComparison(MaxNumber, input1) == EComparison.Smaller)
            {
                return null;
            }
            else if (StringCalculator.SizeComparison(MinNumber, input1) == EComparison.Bigger)
            {
                return null;
            }
            else if (StringCalculator.SizeComparison(MaxNumber, input2) == EComparison.Smaller)
            {
                return null;
            }
            else if (StringCalculator.SizeComparison(MinNumber, input2) == EComparison.Bigger)
            {
                return null;
            }

            string result = OperateByBinary(input1, input2, EOperatingMode.Substract, out bOverflow);

            if (OutputType == EMode.Decimal)
            {
                result = ToDecimalOrNull(result);
            }

            return result;
        }
    }
}