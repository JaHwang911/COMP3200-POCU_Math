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
        public string MaxBit { get; private set; }
        public string MinBit { get; private set; }
        public static int AsciiNumberOfZero = 48;

        public BigNumberCalculator(int bitCount, EMode mode)
        {
            BitCount = bitCount;
            OutputType = mode;
            GetMaxAndMinDecimal();
            GetMaxAndMinBinary();
        }

        public void GetMaxAndMinBinary()
        {
            StringBuilder maxBit = new StringBuilder(BitCount);
            StringBuilder minBit = new StringBuilder(BitCount);

            for (int i = 0; i < BitCount - 1; i++)
            {
                maxBit.Append('1');
                minBit.Append('0');
            }

            MaxNumber = $"0{maxBit}";
            MinNumber = $"1{minBit}";
        }

        public void GetMaxAndMinDecimal()
        {
            StringBuilder allOneBit = new StringBuilder();

            for (int i = 0; i < BitCount - 1; i++)
            {
                allOneBit.Append("1");
            }

            MaxNumber = ConvertBinaryToDecimal(allOneBit.ToString());
            MinNumber = $"-{StringCalculator.OperatePlus(MaxNumber, "1")}";
        }

        public static string ReverseBit(string inputBinary) // re Change name to TwoComplement
        {
            StringBuilder resultString = new StringBuilder(inputBinary.Length);
            StringBuilder tempString = new StringBuilder(256);
            StringBuilder decimalOneBinary = new StringBuilder(256);
            decimalOneBinary.Append('1');

            foreach (var bit in inputBinary)
            {
                switch (bit)
                {
                    case '0':
                        tempString.Append('1');
                        break;
                    case '1':
                        tempString.Append('0');
                        break;
                    default:
                        Debug.Assert(false, "Wrong input binary");
                        break;
                }

                decimalOneBinary.Insert(0, '0');
            }

            decimalOneBinary.Remove(0, 1);
            int remainder = 0;
            
            for (int i = tempString.Length - 1; i >= 0; i--)
            {
                int bigNum = tempString[i] - AsciiNumberOfZero;
                int smallNum = decimalOneBinary[i] - AsciiNumberOfZero;
                int sum = bigNum + smallNum + remainder;

                remainder = sum > 1 ? 1 : 0;
                sum = sum % 2 == 1 ? 1 : 0;

                resultString.Insert(0, sum);
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
                    result = StringCalculator.OperatePlus(result, binaryIndex);
                }

                binaryIndex = StringCalculator.MultiplyOperating(binaryIndex, "2");
            }

            return result;
        }

        public static string ConvertToBinary(string input, ENumberType numberType = ENumberType.NaN)
        {
            if (numberType == ENumberType.NaN)
            {
                numberType = StringCalculator.CheckInputNumberType(input);

                if (numberType == ENumberType.NaN)
                {
                    return null;
                }
            }

            if (input == "0")
            {
                return "0000";
            }

            string result = "";
            StringBuilder binary = new StringBuilder(256);
            string tempValue = input;

            switch (numberType)
            {
                case ENumberType.Binary:
                    result = input;
                    break;
                case ENumberType.Decimal:
                    bool bIsNegative = false;
                    if (input[0] == '-')
                    {
                        bIsNegative = true;
                        tempValue = input.Substring(1);
                    }

                    while (tempValue != "1" && tempValue != "0")
                    {
                        string[] bit = StringCalculator.DivideOperating(tempValue, "2");
                        binary.Insert(0, bit[1]);
                        tempValue = bit[0];
                    }

                    char signBit = '0';
                    binary.Insert(0, $"{signBit}{tempValue}");
                    
                    if (bIsNegative)
                    {
                        result = ReverseBit(binary.ToString());
                    }
                    else
                    {
                        result = binary.ToString();
                    }
                    break;
                case ENumberType.Hex:
                    string digitDecimal = ConvertHexToDecimal(tempValue[0]);
                    StringBuilder digitBinary = new StringBuilder(4);
                    digitBinary.Append(ConvertToBinary(digitDecimal, ENumberType.Decimal));

                    if (digitBinary.Length < 4)
                    {
                        int looCount = 4 - digitBinary.Length;
                        for (int j = 0; j < looCount; j++)
                        {
                            digitBinary.Insert(0, digitBinary[0]);
                        }
                    }

                    binary.Append(digitBinary);

                    for (int i = 1; i < tempValue.Length; i++)
                    {
                        digitDecimal = ConvertHexToDecimal(tempValue[i]);
                        digitBinary.Append(ConvertToBinary(digitDecimal, ENumberType.Decimal));

                        if (digitBinary.Length < 4)
                        {
                            int looCount = 4 - digitBinary.Length;
                            for (int j = 0; j < looCount; j++)
                            {
                                digitBinary.Insert(0, '0');
                            }
                        }

                        binary.Insert(0, digitBinary);
                    }

                    result = binary.ToString();
                    break;
                default:
                    Debug.Assert(false, "Problem of CheckInputNumberType");
                    break;
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
            resultDecimal = StringCalculator.OperatePlus(resultDecimal, "1");
            string resultBinary = ConvertDecimalToBinary(resultDecimal);

            if (resultBinary.Length > inputBinary.Length)
            {
                resultBinary = resultBinary.Substring(resultBinary.Length - inputBinary.Length);
            }
            else if (resultBinary.Length < inputBinary.Length)
            {
                StringBuilder temp = new StringBuilder(resultBinary);

                for (int i = 0; i < inputBinary.Length - resultBinary.Length; i++)
                {
                    temp.Insert(0, 0);
                }

                resultBinary = temp.ToString();
            }

            return $"0b{resultBinary}";
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
                        resultDecimal = StringCalculator.OperatePlus(resultDecimal, "1");
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
                        resultDecimal = StringCalculator.OperatePlus(resultDecimal, "1");
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

                    string tempConverted = ConvertDecimalToBinary(inputDecimal);
                    int hasOneBit = tempConverted.IndexOf('1', 1);

                    if (tempConverted.Length % 4 == 0 && hasOneBit == -1 && bIsNegative)
                    {
                        resultValue = ToHexOrNull($"0b{tempConverted}");
                        break;
                    }

                    char sign = '0';
                    convertedBinary.Append(sign + tempConverted);
                    string possitiveBinary = convertedBinary.ToString();
                    convertedBinary.Clear();

                    if (bIsNegative)
                    {
                        string reverseBinary = ReverseBit(possitiveBinary);
                        string resultDecimal = ConvertBinaryToDecimal(reverseBinary);
                        resultDecimal = StringCalculator.OperatePlus(resultDecimal, "1");
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