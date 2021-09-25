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

            MaxBit = $"0{maxBit}";
            MinBit = $"1{minBit}";
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

        public static string ConvertTwoComplement(string inputBinary)
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
            string absoultValueX = x[0] == '1' ? ConvertTwoComplement(x) : x;
            string absoultValueY = y[0] == '1' ? ConvertTwoComplement(y) : y;
            bool bExpectedNegative = false;
            EComparison comparison = StringCalculator.BinaryComparison(absoultValueX, absoultValueY);

            if (operatingMode == EOperatingMode.Substract)
            {
                string temp = ConvertTwoComplement(subtrahend.ToString());
                subtrahend.Clear();
                subtrahend.Append(temp);
            }

            if (comparison == EComparison.Bigger && minuend[0] == '1')
            {
                bExpectedNegative = true;
            }
            else if (comparison == EComparison.Smaller && subtrahend[0] == '1')
            {
                bExpectedNegative = true;
            }
            else if (minuend[0] == '1' && subtrahend[0] == '1')
            {
                bExpectedNegative = true;
            }

            if (minuend.Length > BitCount)
            {
                int difference = minuend.Length - BitCount;
                minuend.Remove(0, difference);
            }
            else if (minuend.Length < BitCount)
            {
                int difference = BitCount - minuend.Length;

                for (int i = 0; i < difference; i++)
                {
                    minuend.Insert(0, minuend[0]);
                }
            }

            if (subtrahend.Length > BitCount)
            {
                int difference = subtrahend.Length - BitCount;
                subtrahend.Remove(0, difference);
            }
            else if (subtrahend.Length < BitCount)
            {
                int difference = BitCount - subtrahend.Length;

                for (int i = 0; i < difference; i++)
                {
                    subtrahend.Insert(0, subtrahend[0]);
                }
            }

            int remainder = 0;
            int asciiNumberOfZero = 48;

            for (int i = minuend.Length - 1; i >= 0; i--)
            {
                int bigNum = minuend[i] - asciiNumberOfZero;
                int smallNum = subtrahend[i] - asciiNumberOfZero;
                int sum = bigNum + smallNum + remainder;

                remainder = sum > 1 ? 1 : 0;
                sum = sum % 2 == 1 ? 1 : 0;

                result.Insert(0, sum);
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
            string result = "0";
            string binaryIndex = "1";
            string input = num;
            bool bIsNegative = false;

            if (num[0] == '1')
            {
                bIsNegative = true;
                input = ConvertTwoComplement(input);
            }

            for (int i = input.Length - 1; i >= 0; i--)
            {
                if (input[i] == '1')
                {
                    result = StringCalculator.OperatePlus(result, binaryIndex);
                }

                binaryIndex = StringCalculator.MultiplyOperating(binaryIndex, "2");
            }

            if (bIsNegative)
            {
                result = '-' + result;
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
                return "0";
            }

            string result = "";
            StringBuilder binary = new StringBuilder(256);

            switch (numberType)
            {
                case ENumberType.Binary:
                    result = input.Substring(2);
                    break;
                case ENumberType.Decimal:
                    string tempDecimal = input;
                    bool bIsNegative = false;

                    if (input[0] == '-')
                    {
                        bIsNegative = true;
                        tempDecimal = input.Substring(1);
                    }

                    while (tempDecimal != "1" && tempDecimal != "0")
                    {
                        string[] bit = StringCalculator.DivideOperating(tempDecimal, "2");
                        binary.Insert(0, bit[1]);
                        tempDecimal = bit[0];
                    }

                    binary.Insert(0, tempDecimal);
                    int hasOneBit = binary.ToString().IndexOf('1', 1);

                    if (binary.Length % 4 == 0 && hasOneBit < 0)
                    {
                        result = binary.ToString();
                        break;
                    }

                    char signBit = '0';
                    binary.Insert(0, signBit);
                    
                    if (bIsNegative)
                    {
                        result = ConvertTwoComplement(binary.ToString());
                    }
                    else
                    {
                        result = binary.ToString();
                    }
                    break;
                case ENumberType.Hex:
                    //string[] hexIndex = { "1010", "1011", "1100", "1101", "1110", "1111" };
                    string tempHex = input.Substring(2);
                    string digitDecimal = ConvertHexToDecimal(tempHex[0]);
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
                    else if(digitBinary.Length > 4)
                    {
                        digitBinary.Remove(0, digitBinary.Length - 4);
                    }

                    binary.Append(digitBinary);

                    for (int i = 1; i < tempHex.Length; i++)
                    {
                        digitBinary.Clear();
                        digitDecimal = ConvertHexToDecimal(tempHex[i]);
                        digitBinary.Append(ConvertToBinary(digitDecimal, ENumberType.Decimal));

                        if (digitBinary.Length < 4)
                        {
                            int looCount = 4 - digitBinary.Length;
                            for (int j = 0; j < looCount; j++)
                            {
                                digitBinary.Insert(0, '0');
                            }
                        }
                        else if (digitBinary.Length > 4)
                        {
                            digitBinary.Remove(0, digitBinary.Length - 4);
                        }

                        binary.Append(digitBinary);
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
        
        public static string ConvertBinaryToHex(string num)
        {
            int[] binaryIndex = { 1, 8, 4, 2 };
            int digitDecimal = 0;
            StringBuilder result = new StringBuilder(256);
            StringBuilder input = new StringBuilder(num);

            while (input.Length % 4 != 0)
            {
                input.Insert(0, input[0]);
            }

            input.Insert(0, '0');

            for (int i = 1; i < input.Length; i++)
            {
                int index = i % 4;

                if (input[i] == '1')
                {
                    digitDecimal += binaryIndex[index];
                }

                if (index == 0)
                {
                    if (digitDecimal < 10)
                    {
                        result.Append(digitDecimal);
                    }
                    else
                    {
                        int asciiIndexA = 55;
                        int sum = digitDecimal + asciiIndexA;
                        result.Append((char)sum);
                    }

                    digitDecimal = 0;
                }
            }

            return result.ToString();
        }

        public static string GetOnesComplementOrNull(string num)
        {
            ENumberType numberType = StringCalculator.CheckInputNumberType(num);

            if (numberType != ENumberType.Binary)
            {
                return null;
            }

            StringBuilder result = new StringBuilder(256);
            string inputBinary = num.Substring(2);

            foreach (var bit in inputBinary)
            {
                switch (bit)
                {
                    case '0':
                        result.Append('1');
                        break;
                    case '1':
                        result.Append('0');
                        break;
                    default:
                        Debug.Assert(false, "Wrong input binary");
                        break;
                }
            }

            return $"0b{result}";
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

            string resultString = ConvertTwoComplement(inputBinary);

            if (resultString.Length > inputBinary.Length)
            {
                resultString = resultString.Substring(resultString.Length - inputBinary.Length);
            }
            else if (resultString.Length < inputBinary.Length)
            {
                StringBuilder temp = new StringBuilder(resultString);

                for (int i = 0; i < inputBinary.Length - resultString.Length; i++)
                {
                    temp.Insert(0, 0);
                }

                resultString = temp.ToString();
            }

            return $"0b{resultString}";
        }

        public static string ToBinaryOrNull(string num)
        {
            string result = ConvertToBinary(num);

            if (result == null)
            {
                return null;
            }

            return $"0b{result}";
        }

        public static string ToDecimalOrNull(string num)
        {
            ENumberType numberType = StringCalculator.CheckInputNumberType(num);

            if (numberType == ENumberType.NaN)
            {
                return null;
            }
            else if (numberType == ENumberType.Decimal)
            {
                return num;
            }

            string binary = "";

            if (numberType == ENumberType.Hex)
            {
                binary = ConvertToBinary(num, numberType);
            }
            else if (numberType == ENumberType.Binary)
            {
                binary = num.Substring(2);
            }

            string result = ConvertBinaryToDecimal(binary);

            return result;
        }

        public static string ToHexOrNull(string num)
        {
            ENumberType numberType = StringCalculator.CheckInputNumberType(num);

            if (numberType == ENumberType.NaN)
            {
                return null;
            }
            else if (numberType == ENumberType.Hex)
            {
                return num;
            }

            string binary = "";

            if (numberType == ENumberType.Decimal)
            {
                binary = ConvertToBinary(num, numberType);
            }
            else if (numberType == ENumberType.Binary)
            {
                binary = num.Substring(2);
            }

            if (binary.Length == 4 && binary[0] == '1' && num[0] != '-')
            {
                binary = "0000" + binary;
            }
            string result = ConvertBinaryToHex(binary);

            return $"0x{result}";
        }

        public string AddOrNull(string num1, string num2, out bool bOverflow)
        {
            bOverflow = false;
            ENumberType input1NumberType = StringCalculator.CheckInputNumberType(num1);
            ENumberType input2NumberType = StringCalculator.CheckInputNumberType(num2);

            if (input1NumberType == ENumberType.NaN || input2NumberType == ENumberType.NaN)
            {
                return null;
            }

            string input1 = ConvertToBinary(num1, input1NumberType);
            string input2 = ConvertToBinary(num2, input2NumberType);

            int hasOneBit = input1.Substring(1).IndexOf('1');

            if (hasOneBit < 0 && num1[0] != '-')
            {
                input1 = input1.Insert(0, "0");

                if (StringCalculator.BinaryComparison(MaxBit, input1) == EComparison.Smaller)
                {
                    return null;
                }
            }
            else if (StringCalculator.BinaryComparison(MinBit, input1) == EComparison.Bigger)
            {
                return null;
            }

            hasOneBit = input2.Substring(1).IndexOf('1');

            if (hasOneBit < 0 && num2[0] != '-')
            {
                input2 = input2.Insert(0, "0");

                if (StringCalculator.BinaryComparison(MaxBit, input2) == EComparison.Smaller)
                {
                    return null;
                }
            }
            else if (StringCalculator.BinaryComparison(MinBit, input2) == EComparison.Bigger)
            {
                return null;
            }

            string result = OperateByBinary(input1, input2, EOperatingMode.Add, out bOverflow);

            if (OutputType == EMode.Decimal)
            {
                result = ConvertBinaryToDecimal(result.Substring(2));
            }

            return result;
        }

        public string SubtractOrNull(string num1, string num2, out bool bOverflow)
        {
            bOverflow = false;
            ENumberType input1NumberType = StringCalculator.CheckInputNumberType(num1);
            ENumberType input2NumberType = StringCalculator.CheckInputNumberType(num2);

            if (input1NumberType == ENumberType.NaN || input2NumberType == ENumberType.NaN)
            {
                return null;
            }

            string input1 = ConvertToBinary(num1, input1NumberType);
            string input2 = ConvertToBinary(num2, input2NumberType);

            int hasOneBit = input1.Substring(1).IndexOf('1');

            if (hasOneBit < 0)
            {
                input1 = input1.Insert(0, "0");
            }

            hasOneBit = input2.Substring(1).IndexOf('1');

            if (hasOneBit < 0)
            {
                input2 = input2.Insert(0, "0");
            }

            if (input1[0] == '1' && StringCalculator.BinaryComparison(MinBit, input1) == EComparison.Bigger)
            {
                return null;
            }
            else if (input1[0] != '-' && StringCalculator.BinaryComparison(MaxBit, input1) == EComparison.Smaller)
            {
                return null;
            }
            else if (input2[0] == '1' && StringCalculator.BinaryComparison(MinBit, input2) == EComparison.Bigger)
            {
                return null;
            }
            else if (input2[0] != '-' && StringCalculator.BinaryComparison(MaxBit, input2) == EComparison.Smaller)
            {
                return null;
            }

            string result = OperateByBinary(input1, input2, EOperatingMode.Substract, out bOverflow);

            if (OutputType == EMode.Decimal)
            {
                result = ConvertBinaryToDecimal(result.Substring(2));
            }

            return result;
        }
    }
}