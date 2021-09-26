using System;
using System.Diagnostics;
using System.Text;

namespace Assignment1
{
    class StringCalculator
    {
        public static ENumberType CheckInputNumberType(string input)
        {
            if (String.IsNullOrWhiteSpace(input))
            {
                return ENumberType.NaN;
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
                    if (input.Length == 2)
                    {
                        bResult = false;
                    }
                    else if (input.Length == 1)
                    {
                        return ENumberType.Decimal;
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
                    else
                    {
                        return ENumberType.Decimal;
                    }
                    break;
            }

            if (!bResult)
            {
                return ENumberType.NaN;
            }

            ENumberType inputType = ENumberType.NaN;
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

                    inputType = bResult ? ENumberType.Binary : ENumberType.NaN;
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

                    inputType = bResult ? ENumberType.Hex : ENumberType.NaN;
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

                    inputType = bResult ? ENumberType.Decimal : ENumberType.NaN;
                    break;
            }

            return inputType;
        }

        public static EComparison CompareBinary(string x, string y)
        {
            EComparison resultComparison = EComparison.Same;
            char positiveSign = '0';
            bool bNegativeComparison = false;
            bool bXNotZero = x.Contains('1');
            bool bYNotZero = y.Contains('1');

            if (!bXNotZero)
            {
                return EComparison.Smaller;
            }
            else if (!bYNotZero)
            {
                return EComparison.Bigger;
            }

            string input1 = x;
            string input2 = y;


            if (input1[0] == positiveSign && input2[0] != positiveSign)
            {
                string temp = BigNumberCalculator.ConvertTwoComplement(input2);

                if (temp == input2)
                {
                    input1 = BigNumberCalculator.ConvertTwoComplement(input1);
                    bNegativeComparison = true;
                }
                else
                {
                    input2 = temp;
                }
            }
            else if (input1[0] != positiveSign && input2[0] == positiveSign)
            {
                string temp = BigNumberCalculator.ConvertTwoComplement(input1);

                if (temp == input1)
                {
                    input2 = BigNumberCalculator.ConvertTwoComplement(input2);
                    bNegativeComparison = true;
                }
                else
                {
                    input1 = temp;
                }
            }
            else if (input1[0] != positiveSign && input2[0] != positiveSign)
            {
                bNegativeComparison = true;
            }

            if (input1.Length > input2.Length)
            {
                return EComparison.Bigger;
            }
            else if (input1.Length < input2.Length)
            {
                return EComparison.Smaller;
            }

            for (int i = 0; i < input1.Length; i++)
            {
                if (input1[i] < input2[i])
                {
                    resultComparison = EComparison.Smaller;
                    break;
                }
                else if (input1[i] > input2[i])
                {
                    resultComparison = EComparison.Bigger;
                    break;
                }
            }

            if (bNegativeComparison)
            {
                resultComparison = resultComparison == EComparison.Bigger ? EComparison.Smaller : EComparison.Bigger;
            }

            return resultComparison;
        }

        public static EComparison SizeComparison(string x, string y)
        {
            EComparison result = EComparison.Same;
            bool bNegativeComparison = false;

            if (x[0] != '-' && y[0] == '-')
            {
                return EComparison.Bigger;
            }
            else if (x[0] == '-' && y[0] != '-')
            {
                return EComparison.Smaller;
            }
            else if (x[0] == '-' && y[0] == '-')
            {
                bNegativeComparison = true;
                x = x.Substring(1);
                y = y.Substring(1);
            }

            if (x.Length > y.Length)
            {
                return bNegativeComparison ? EComparison.Smaller : EComparison.Bigger;
            }
            else if (x.Length < y.Length)
            {
                return bNegativeComparison ? EComparison.Bigger : EComparison.Smaller;
            }

            for (int i = 0; i < x.Length; i++)
            {
                if (x[i] > y[i])
                {
                    result = EComparison.Bigger;
                    break;
                }
                else if (x[i] < y[i])
                {
                    result = EComparison.Smaller;
                    break;
                }
            }

            if (bNegativeComparison && result != EComparison.Same)
            {
                switch (result)
                {
                    case EComparison.Bigger:
                        result = EComparison.Smaller;
                        break;
                    case EComparison.Smaller:
                        result = EComparison.Bigger;
                        break;
                }
            }

            return result;
        }

        public static string OperatePlus(string x, string y)
        {
            StringBuilder result = new StringBuilder(256);
            string bigger = x;
            StringBuilder smaller = new StringBuilder(256);
            bool bNegativeOperating = false;

            if (x[0] == '-' && y[0] == '-')
            {
                bNegativeOperating = true;
                x = x.Substring(1);
                y = y.Substring(1);
            }

            int asciiNumberOfZero = 48;
            int difference = x.Length - y.Length;

            if (difference >= 0)
            {
                bigger = x;
                smaller.Append(y);
            }
            else
            {
                bigger = y;
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

            if (bNegativeOperating)
            {
                result.Insert(0, '-');
            }

            return result.ToString();
        }

        public static string MultiplyOperating(string x, string y)
        {
            StringBuilder result = new StringBuilder(256);
            StringBuilder bigger = new StringBuilder(256);
            StringBuilder smaller = new StringBuilder(256);
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
                StringBuilder tempResult = new StringBuilder(256);

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

                string multiplyResult = OperatePlus(result.ToString(), tempResult.ToString());
                result.Clear();
                result.Append(multiplyResult);
                multiplyCount++;
            }

            return result.ToString();
        }

        public static string[] DivideOperating(string denominator, string numerator)
        {
            EComparison resultComparison = SizeComparison(denominator, numerator);

            if (resultComparison == EComparison.Smaller)
            {
                return null;
            }

            string[] result = new string[2];
            StringBuilder tempDigit = new StringBuilder(256); // re
            StringBuilder resultQuotient = new StringBuilder(256);
            int integerNumerator = int.Parse(numerator); // numerator가 너무 크면 불가능함
            int remainder = 0;

            for (int i = 0; i < denominator.Length; i++)
            {
                tempDigit.Append(denominator[i]);
                remainder *= 10;
                int digitValue = int.Parse(tempDigit.ToString()) + remainder;

                if (digitValue < integerNumerator && i == 0)
                {
                    continue;
                }

                int digitQuotient = digitValue / integerNumerator;
                remainder = digitValue % integerNumerator;

                resultQuotient.Append(digitQuotient);
                tempDigit.Clear();
            }

            result[0] = resultQuotient.ToString();
            result[1] = remainder.ToString();

            return result;
        }
    }
}
