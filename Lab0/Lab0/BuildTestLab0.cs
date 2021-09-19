using System;
using System.Text;
using System.Collections.Generic;

namespace Lab0
{
    public static class BuildTestLab0
    {
        private const int MAX_SUCCESS = 15;
        private static int countSuccess;
        private static string stringDecimal = "";
        private static string irreducibleFraction = "";
        private static int buildNumber;

        public static void BuildTest()
        {
            ++countSuccess;

            Console.WriteLine($" ----------------------------------------");
            Console.WriteLine($" 실습 0");
            Console.WriteLine($" 빌드 {buildNumber} - 성공");
            Console.WriteLine($" ----------------------------------------");

            bool bPassB0 = B0Test();
            bool bPassB1 = B1Test();
            bool bPassB2 = B2Test();
            bool bPassB3 = B3Test();
            bool bPassB4 = B4Test();
            bool bPassB5 = B5Test();

            bool bPassC0 = C0Test();
            bool bPassC1 = C1Test();
            bool bPassC2 = C2Test();
            bool bPassC3 = C3Test();
            bool bPassC4 = C4Test();
            bool bPassC5 = C5Test();
            bool bPassC6 = C6Test();
            bool bPassC7 = C7Test();

            if (countSuccess != MAX_SUCCESS)
            {
                Console.WriteLine(" 실패한 테스트");
                Console.WriteLine($" ----------------------------------------");

            }
            if (!bPassB1)
            {
                Console.WriteLine(" B1_NumeratorOutOfRange");
            }
            if (!bPassB2)
            {
                Console.WriteLine(" B2_DenominatorOutOfRange");
            }
            if (!bPassB3)
            {
                Console.WriteLine(" B3_NumeratorIsZero");
            }
            if (!bPassB4)
            {
                Console.WriteLine(" B4_NumeratorIsOne");
            }
            if (!bPassB5)
            {
                Console.WriteLine(" B5_NumeratorIsMaxValue");
            }

            if (!bPassC1)
            {
                Console.WriteLine(" C1_EmptyStringDecimal");
            }
            if (!bPassC2)
            {
                Console.WriteLine(" C2_ZeroStringDecimal");
            }
            if (!bPassC3)
            {
                Console.WriteLine(" C3_InvalidStringDecimal");
            }
            if (!bPassC4)
            {
                Console.WriteLine(" C4_FiniteStringDecimal");
            }
            if (!bPassC5)
            {
                Console.WriteLine(" C5_OneRepetendStringDecimal");
            }
            if (!bPassC6)
            {
                Console.WriteLine(" C6_MultipleRepetendStringDecimal");
            }
            if (!bPassC7)
            {
                Console.WriteLine(" C7_MixedRepetingStringDecimal");
            }


            Console.WriteLine($" ----------------------------------------");
            Console.WriteLine($" 테스트 {countSuccess} / {MAX_SUCCESS} 통과");
            Console.WriteLine($" ----------------------------------------");
            Console.WriteLine($"");

        }
        #region B_Test
        private static bool B0Test()
        {
            // 함수가 존재하는 지 검사

            Lab0.TryGetRepeatingDecimal(1, 1, out stringDecimal);
            ++countSuccess;

            return true;
        }
        private static bool B1Test()
        {
            // 유효한 분자범위가 아닐 때
            bool bPass = true;

            bool b1 = Lab0.TryGetRepeatingDecimal(-1, 1, out stringDecimal);

            if (b1 == false && stringDecimal == "")
            {
                ++countSuccess;
            }
            else
            {
                return false;
            }

            return bPass;
        }
        private static bool B2Test()
        {
            // 유효한 분모범위가 아닐 때
            bool bPass = true;

            try
            {
                string stringDecimal1;
                string stringDecimal2;
                string stringDecimal3;
                bool bResult1 = Lab0.TryGetRepeatingDecimal(1, 0, out stringDecimal1);
                bool bResult2 = Lab0.TryGetRepeatingDecimal(1, int.MinValue, out stringDecimal2);
                bool bResult3 = Lab0.TryGetRepeatingDecimal(1, int.MaxValue, out stringDecimal3);

                if (bResult1 == false && stringDecimal1 == ""
                    && bResult2 == false && stringDecimal2 == ""
                    && bResult3 == false && stringDecimal3 == "")
                {
                    ++countSuccess;
                }
                else
                {
                    return false;
                }
            }
            catch (DivideByZeroException)
            {
                return false;
            }

            return bPass;
        }
        private static bool B3Test()
        {
            // 분자가 0 일 때
            bool bPass = true;

            string tempStringDecimal;
            string compareStringDecimal;

            for (int i = 1; i <= 100; ++i)
            {
                bool bResult1 = Lab0.TryGetRepeatingDecimal(0, i, out tempStringDecimal);
                bool bResult2 = testTryGetRepeatingDecimal(0, i, out compareStringDecimal);

                if (bResult1 != bResult2 || tempStringDecimal != compareStringDecimal)
                {
                    return false;
                }
            }

            ++countSuccess;

            return bPass;
        }
        private static bool B4Test()
        {
            // 분자가 1 일 때
            bool bPass = true;

            string tempStringDecimal;
            string compareStringDecimal;

            for (int i = 1; i <= 100; ++i)
            {
                bool bResult1 = Lab0.TryGetRepeatingDecimal(1, i, out tempStringDecimal);
                bool bResult2 = testTryGetRepeatingDecimal(1, i, out compareStringDecimal);

                if (bResult1 != bResult2 || tempStringDecimal != compareStringDecimal)
                {
                    return false;
                }
            }

            if (bPass)
            {
                ++countSuccess;
            }

            return bPass;
        }
        private static bool B5Test()
        {
            // 분자가 최대값 일 때
            bool bPass = true;

            string tempStringDecimal;
            string compareStringDecimal;

            for (int i = 1; i <= 100; ++i)
            {
                bool bResult1 = Lab0.TryGetRepeatingDecimal(int.MaxValue, i, out tempStringDecimal);
                bool bResult2 = testTryGetRepeatingDecimal(int.MaxValue, i, out compareStringDecimal);

                if (bResult1 != bResult2 || tempStringDecimal != compareStringDecimal)
                {
                    return false;
                }
            }

            if (bPass)
            {
                ++countSuccess;
            }

            return bPass;
        }
        #endregion
        #region C_Test
        private static bool C0Test()
        {
            // 함수가 존재하는지 검사
            Lab0.TryGetIrreducibleFraction("0.1", out irreducibleFraction);
            ++countSuccess;

            return true;
        }
        private static bool C1Test()
        {
            // 문자열 소수가 비어있을 때
            bool bPass = true;

            try
            {
                bool bResult = Lab0.TryGetIrreducibleFraction("", out irreducibleFraction);

                if (bResult != false || irreducibleFraction != "")
                {
                    bPass = false;
                }

                if (bPass)
                {
                    ++countSuccess;
                }
            }
            catch (FormatException)
            {
                return false;
            }

            return bPass;
        }
        private static bool C2Test()
        {
            // 문자열 소수가 0일 때
            bool bPass = true;

            try
            {
                bool bResult = Lab0.TryGetIrreducibleFraction("0", out irreducibleFraction);

                if (bResult != false || irreducibleFraction != "")
                {
                    bPass = false;
                }

                if (bPass)
                {
                    ++countSuccess;
                }
            }
            catch (FormatException)
            {
                return false;
            }

            return bPass;
        }
        private static bool C3Test()
        {
            // 문자열 소수가 유효하지 않은 값일 때
            stringDecimal = "0.0.*0588235294117647*";

            bool bPass = true;

            try
            {
                bool bResult = Lab0.TryGetIrreducibleFraction(stringDecimal, out irreducibleFraction);

                if (bResult != false || irreducibleFraction != "")
                {
                    bPass = false;
                }

                if (bPass)
                {
                    ++countSuccess;
                }
            }
            catch (Exception)
            {
                return false;
            }

            return bPass;
        }
        private static bool C4Test()
        {
            // 문자열 소수가 유한소수일 때
            bool bPass = true;

            string resultFraction;
            string compareFraction;

            int num = 1;

            var twoPowerN = new List<int>(5);
            var fivePowerN = new List<int>(5);
            var divisor = new HashSet<string>(25);

            twoPowerN.Add(num);
            fivePowerN.Add(num);

            for (int n = 0; n < 5; ++n)
            {
                num *= 2;
                twoPowerN.Add(num);
            }

            num = 1;

            for (int n = 0; n < 5; ++n)
            {
                num *= 5;
                fivePowerN.Add(num);
            }

            foreach (var tempNum2 in twoPowerN)
            {
                foreach (var tempNum5 in fivePowerN)
                {
                    divisor.Add((tempNum2 * tempNum5).ToString().PadLeft(6, '0').TrimEnd('0').Insert(1, "."));
                }
            }

            divisor.Remove("1.");
            divisor.Add("0.000000001");

            foreach (var temp in divisor)
            {
                bool bResult = Lab0.TryGetIrreducibleFraction(temp, out resultFraction);
                bool bCompare = testTryGetIrreducibleFraction(temp, out compareFraction);

                if (bResult != bCompare || resultFraction != compareFraction)
                {
                    bPass = false;
                }
            }

            if (bPass)
            {
                ++countSuccess;
            }

            return bPass;
        }
        private static bool C5Test()
        {
            // 문자열 소수가 순환마디 길이가 1인 순순환소수일 때
            bool bPass = true;

            string[] testStringDecimals = { "0.*1*", "0.*2*", "0.*3*", "0.*4*", "0.*5*", "0.*6*", "0.*7*", "0.*8*", "0.*9*" };

            string resultFraction;
            string compareFraction;

            for (int i = 0; i < testStringDecimals.Length; ++i)
            {
                bool bResult = Lab0.TryGetIrreducibleFraction(testStringDecimals[i], out resultFraction);
                bool bCompare = testTryGetIrreducibleFraction(testStringDecimals[i], out compareFraction);

                if (bResult != bCompare || resultFraction != compareFraction)
                {
                    bPass = false;
                }
            }

            if (bPass)
            {
                ++countSuccess;
            }

            return bPass;
        }
        private static bool C6Test()
        {
            // 문자열 소수가 순환마디 길이가 2 이상인 순순환소수일 때
            bool bPass = true;

            string[] testStringDecimals = { "0.*142857*", "0.*09*", "0.*32*", "0.*523*", "0.*15*", "0.*1234*", "0.*12345*", "0.*1234567*" };

            string resultFraction;
            string compareFraction;

            for (int i = 0; i < testStringDecimals.Length; ++i)
            {
                bool bResult = Lab0.TryGetIrreducibleFraction(testStringDecimals[i], out resultFraction);
                bool bCompare = testTryGetIrreducibleFraction(testStringDecimals[i], out compareFraction);

                if (bResult != bCompare || resultFraction != compareFraction)
                {
                    bPass = false;
                }
            }

            if (bPass)
            {
                ++countSuccess;
            }

            return bPass;
        }
        private static bool C7Test()
        {
            // 문자열 소수가 혼순환소수일 때
            bool bPass = true;

            string[] testStringDecimals = { "0.08*3*", "0.041*6*", "0.03*571428*", "0.02*27*", "0.01*923076*", "0.017*857142*", "0.01*190476*", "0.011*36*" };

            string resultFraction;
            string compareFraction;

            for (int i = 0; i < testStringDecimals.Length; ++i)
            {
                bool bResult = Lab0.TryGetIrreducibleFraction(testStringDecimals[i], out resultFraction);
                bool bCompare = testTryGetIrreducibleFraction(testStringDecimals[i], out compareFraction);

                if (bResult != bCompare || resultFraction != compareFraction)
                {
                    bPass = false;
                }
            }

            if (bPass)
            {
                ++countSuccess;
            }

            return bPass;
        }
        #endregion
        #region testFunction
        private static bool testTryGetRepeatingDecimal(int numerator, int denominator, out string stringDecimal)
        {
            stringDecimal = "";

            if (numerator < 0 || denominator <= 0 || denominator > 100)
            {
                return false;
            }

            const int LIMIT_CAP = 100;

            var sb = new StringBuilder(LIMIT_CAP);
            var quotients = new List<int>(LIMIT_CAP);
            var remainders = new List<int>(LIMIT_CAP);

            int tempQuotient = numerator / denominator;
            int tempRemainder = numerator % denominator;

            sb.Append(tempQuotient);
            quotients.Add(tempQuotient);
            remainders.Add(tempRemainder);

            if (remainders[0] == 0)
            {
                stringDecimal = sb.ToString();

                return false;
            }

            int countRemainder = 1;

            while (tempRemainder != 0)
            {
                tempQuotient = 10 * tempRemainder / denominator;
                tempRemainder = 10 * tempRemainder % denominator;

                quotients.Add(tempQuotient);
                remainders.Add(tempRemainder);

                if (countRemainder++ == (LIMIT_CAP - 1))
                {
                    break;
                }
            }

            if (remainders[remainders.Count - 1] == 0)
            {
                sb.Append('.');
                for (int i = 1; i < quotients.Count; ++i)
                {
                    sb.Append(quotients[i]);
                }
                stringDecimal = sb.ToString();

                return false;
            }

            int indexOfFirstRepetend = -1;
            int indexOfLastRepetend = -1;

            for (int i = 0; i < remainders.Count - 1; ++i)
            {
                for (int j = i + 1; j < remainders.Count; ++j)
                {
                    if (remainders[i] == remainders[j])
                    {
                        indexOfFirstRepetend = i + 1;
                        indexOfLastRepetend = j + 1;

                        break;
                    }
                }
                if (indexOfFirstRepetend != -1)
                {
                    break;
                }
            }

            if (quotients[indexOfFirstRepetend] == 0)
            {
                indexOfFirstRepetend += 1;
                indexOfLastRepetend += 1;
            }

            quotients.RemoveRange(indexOfLastRepetend, quotients.Count - indexOfLastRepetend);

            sb.Append('.');
            for (int i = 1; i < indexOfFirstRepetend; ++i)
            {
                sb.Append(quotients[i]);
            }
            sb.Append('*');
            for (int i = indexOfFirstRepetend; i < quotients.Count; ++i)
            {
                sb.Append(quotients[i]);
            }
            sb.Append('*');

            stringDecimal = sb.ToString();

            return true;
        }
        private static bool testTryGetIrreducibleFraction(string stringDecimal, out string irreducibleFraction)
        {
            irreducibleFraction = "";

            if (String.IsNullOrEmpty(stringDecimal) || stringDecimal == "0")
            {
                return false;
            }

            stringDecimal = stringDecimal.Replace(".", "");
            int startIndexOfRepetend = stringDecimal.IndexOf('*');
            stringDecimal = stringDecimal.Replace("*", "");
            int lastIndexOfRepetend = stringDecimal.Length - 1;

            if (stringDecimal.Length > 10)
            {
                return false;
            }

            int denominator = 1;
            int numerator = int.Parse(stringDecimal.TrimStart('0'));

            for (int i = 0; i < lastIndexOfRepetend; ++i)
            {
                denominator *= 10;
            }

            if (startIndexOfRepetend != -1)
            {
                int tempDenomRemoved = 1;
                int tempNumerRemoved = 1;

                for (int i = 0; i < startIndexOfRepetend - 1; ++i)
                {
                    tempDenomRemoved *= 10;
                }

                for (int i = 0; i < lastIndexOfRepetend - startIndexOfRepetend + 1; ++i)
                {
                    tempNumerRemoved *= 10;
                }

                numerator = numerator - numerator / tempNumerRemoved;
                denominator -= tempDenomRemoved;
            }

            var numeratorDivisors = new HashSet<int>();
            var denominatorDivisors = new HashSet<int>();

            for (int i = 1; i <= numerator; ++i)
            {
                if (numerator % i == 0)
                {
                    numeratorDivisors.Add(i);
                }
            }
            for (int i = 1; i <= denominator; ++i)
            {
                if (denominator % i == 0)
                {
                    denominatorDivisors.Add(i);
                }
            }

            var commonDivisors = new HashSet<int>();

            foreach (var divisor in numeratorDivisors)
            {
                if (denominatorDivisors.Contains(divisor))
                {
                    commonDivisors.Add(divisor);
                }
            }

            int tempDivisor = 1;

            foreach (var divisor in commonDivisors)
            {
                tempDivisor = tempDivisor > divisor ? tempDivisor : divisor;
            }

            numerator /= tempDivisor;
            denominator /= tempDivisor;

            irreducibleFraction = $"{numerator} / {denominator}";

            return true;
        }
        #endregion
    }
}
