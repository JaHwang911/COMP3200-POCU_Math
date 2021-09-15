using System;
using System.Diagnostics;
using System.Text;

namespace Assignment1
{
    class StringCalculator
    {
        public static string PlusOperatingByString(string x, string y)
        {
            StringBuilder result = new StringBuilder();
            StringBuilder bigger = new StringBuilder();
            StringBuilder smaller = new StringBuilder();
            int asciiNumberOfZero = 48;
            int difference = x.Length - y.Length;

            if (difference >= 0)
            {
                bigger.Append(x);
                smaller.Append(y);
            }
            else
            {
                bigger.Append(y);
                smaller.Append(x);
                difference *= -1;
            }

            for (int i = 0; i < difference; i++)
            {
                smaller.Insert(0, '0');
            }

            int remainder = 0;

            for (int i = bigger.Length - 1; i >= 0; i--)
            {
                int bigNum = bigger[i] - asciiNumberOfZero;
                int smallNum = smaller[i] - asciiNumberOfZero;
                int sum = bigNum + smallNum + remainder;
                int value = i == 0 ? sum : sum % 10;
                remainder = sum / 10;
                result.Insert(0, value);
            }

            return result.ToString();
        }

        public static string MultiplyOperatingByString(string x, string y)
        {
            StringBuilder result = new StringBuilder();
            StringBuilder tempResult = new StringBuilder();
            StringBuilder bigger = new StringBuilder();
            StringBuilder smaller = new StringBuilder();
            int asciiNumberOfZero = 48;

            if (x.Length - y.Length >= 0)
            {
                bigger.Append(x);
                smaller.Append(y);
            }
            else
            {
                bigger.Append(y);
                smaller.Append(x);
            }

            int multiplyCount = 0;
            result.Append('0');

            for (int i = smaller.Length - 1; i >= 0; i--)
            {
                int smallNum = smaller[i] - asciiNumberOfZero;
                int remainder = 0;

                for (int j = bigger.Length - 1; j >= 0; j--)
                {
                    int bigNum = bigger[j] - asciiNumberOfZero;
                    int sum = bigNum * smallNum + remainder;
                    int digitValue = j == 0 ? sum : sum % 10;
                    remainder = sum / 10;
                    tempResult.Insert(0, digitValue);
                }

                for (int j = multiplyCount; j > 0; j--)
                {
                    tempResult.Append('0');
                }

                string multiplyResult = PlusOperatingByString(result.ToString(), tempResult.ToString());
                result.Clear();
                tempResult.Clear();
                result.Append(multiplyResult);
                multiplyCount++;
            }

            return result.ToString();
        }

        public static string CheckInputNumberType(string input)
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

        public static string ConvertBigBinaryToDecimal(string input)
        {
            string index = "1";
            string resultDecimal = "0";

            for (int i = input.Length - 1; i >= 0; i--)
            {
                if (input[i] == '1')
                {
                    resultDecimal = PlusOperatingByString(resultDecimal, index);
                }

                index = MultiplyOperatingByString(index, "2");
            }

            return resultDecimal;
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
    }
}
