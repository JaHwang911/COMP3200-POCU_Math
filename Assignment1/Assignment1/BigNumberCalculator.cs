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
        private static string SizeComparison(string x, string y)
        {
            string result = "same";

            if (x.Length > y.Length)
            {
                return "bigger";
            }
            else if (x.Length < y.Length)
            {
                return "smaller";
            }

            for (int i = 0; i < x.Length; i++)
            {
                if (x[i] > y[i])
                {
                    return "bigger";
                }
                else if (x[i] < y[i])
                {
                    return "smaller";
                }
            }

            return result;
        }

        public static string MinusOperatingByString(string x, string y)
        {
            string resultComparison = SizeComparison(x, y);

            if (resultComparison == "smaller")
            {
                return null;
            }

            StringBuilder result = new StringBuilder();
            StringBuilder bigger = new StringBuilder(x);
            StringBuilder smaller = new StringBuilder(y);
            int difference = x.Length - y.Length;
            int asciiNumberOfZero = 48;

            for (int i = 0; i < difference; i++)
            {
                smaller.Insert(0, '0');
            }

            int remainder = 0;
            int sum = 0;

            for (int i = bigger.Length - 1; i >= 0; i--)
            {
                int bigNum = bigger[i] - asciiNumberOfZero;
                int smallNum = smaller[i] - asciiNumberOfZero;

                if (bigNum < smallNum)
                {
                    sum = (10 + bigNum) - smallNum - remainder;
                    remainder = 1;
                }
                else
                {
                    sum = bigNum - smallNum - remainder;
                }

                if (i == 0 && sum == 0)
                {
                    break;
                }

                result.Insert(0, sum);
            }

            return result.ToString();
        }

        private static string[] DivideOperatingByString(string denominator, string numerator)
        {
            string resultComparison = SizeComparison(denominator, numerator);

            if (resultComparison == "smaller")
            {
                return null;
            }

            string[] result = new string[2];
            string value = "1";

            while (true)
            {
                string sum = StringCalculator.MultiplyOperatingByString(numerator, value);
                resultComparison = SizeComparison(denominator, sum);

                if (resultComparison == "same")
                {
                    result[0] = value;
                    result[1] = "0";
                }
                else if (resultComparison == "smaller")
                {
                    value = MinusOperatingByString(value, "1");
                    sum = StringCalculator.MultiplyOperatingByString(numerator, value);
                    result[0] = value;
                    result[1] = MinusOperatingByString(denominator, sum);
                }

                value = StringCalculator.PlusOperatingByString(value, "1");
            }

            return result;
        }
        
        private static int ConvertBinaryToDecimal(string num)
        {
            int index = 1;
            int resultInt = 0;

            for (int i = num.Length - 1; i >= 0; i--)
            {
                if (num[i] == '1')
                {
                    resultInt += index;
                }

                index *= 2;
            }

            return resultInt;
        }

        public static string ConvertDecimalToBinary1(string input)
        {
            if (input == "0")
            {
                return "0000";
            }

            StringBuilder binary = new StringBuilder(64);

            while (input != "1" || input != "0")
            {
                string[] bit = DivideOperatingByString(input, "2");
                binary.Insert(0, bit[1]);
                input = bit[0];
            }

            //binary.Insert(0, num);

            return binary.ToString();
        }

        private static string ConvertDecimalToBinary(int num)
        {
            if (num == 0)
            {
                return "0000";
            }

            StringBuilder binary = new StringBuilder(64);

            while (num > 1)
            {
                int bit = num % 2;
                binary.Insert(0, bit);
                num /= 2;
            }

            binary.Insert(0, num);

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

        public static string GetOnesComplementOrNull(string num)
        {
            string numberType = StringCalculator.CheckInputNumberType(num);

            if (numberType != "bin")
            {
                return null;
            }

            string inputBinary = num.Substring(2);
            string resultString = StringCalculator.ReverseBit(inputBinary);

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
            string resultString = StringCalculator.ReverseBit(inputBinary);
            int resultDecimal = ConvertBinaryToDecimal(resultString) + 1;
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
                        string reverseBinary = StringCalculator.ReverseBit(possitiveBinary);
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
                            int convertedDecimal = ConvertBinaryToDecimal(binary);
                            resultValue = convertedDecimal.ToString();
                        }
                    }
                    else
                    {
                        string reverseBinary = StringCalculator.ReverseBit(binary);
                        if (binary.Length > 20)
                        {
                            string converted = StringCalculator.ConvertBigBinaryToDecimal(reverseBinary);
                            string temp = StringCalculator.PlusOperatingByString(converted, "1");
                            resultValue = $"-{temp}";
                        }
                        else
                        {
                            int tempDecimal = ConvertBinaryToDecimal(reverseBinary) + 1;
                            resultValue = $"-{tempDecimal.ToString()}";
                        }
                    }
                    break;
                case "hex":
                    string hex = num.Substring(2);
                    StringBuilder convertedBinary = new StringBuilder();
                    StringBuilder tempBinary = new StringBuilder(4);

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

        public static string ToHexOrNull(string num)
        {
            return null;
        }
    }
}