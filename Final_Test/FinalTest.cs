using System;
using System.IO;

namespace final
{
    public static class Test
    {
        public static void StartLab9(string filePath)
        {
            if (!File.Exists(filePath))
            {
                Console.WriteLine("파일 또는 파일의 경로를 확인 해주세요");
                return;
            }

            try
            {
                short correctionFlags = 0;

                Random random = new Random();

                string[] questions = File.ReadAllText(filePath).Split('\\');

                #region 1번

                int randomValue = random.Next(2, 37);

                int redCount = 38 - randomValue;
                int blackCount = 38 - redCount - random.Next(1, 37 - redCount);
                int greenCount = 38 - redCount - blackCount;

                Console.WriteLine(questions[0], redCount, blackCount);

                decimal rightAnswer = (int)(((redCount * 2 + greenCount * 2 + blackCount) / 38.0m + 0.0005m) * 1000) / 1000.0m;

                if (decimal.Parse(Console.ReadLine()) == rightAnswer)
                {
                    correctionFlags = 1;
                }

                #endregion
                Console.WriteLine();
                #region 2번

                randomValue = random.Next(4, 40);

                int tempNumberCount = randomValue / 2;
                int oddNumberCount = (randomValue & 1) == 0 ? tempNumberCount : ++tempNumberCount;

                Console.WriteLine(questions[1], randomValue);

                int primeNumberCount = 1;
                for (int i = 5; i <= randomValue; ++i, ++i)
                {
                    bool bPrimeNumber = true;
                    for (int j = 2; j * j <= i; ++j)
                    {
                        if (i % j == 0)
                        {
                            bPrimeNumber = false;
                        }
                    }

                    if (bPrimeNumber)
                    {
                        ++primeNumberCount;
                    }
                }

                rightAnswer = (int)((primeNumberCount / (decimal)oddNumberCount + 0.00005m) * 10000) / 100.0m;
                if (decimal.Parse(Console.ReadLine()) == rightAnswer)
                {
                    correctionFlags |= 1 << 1;
                }

                #endregion
                Console.WriteLine();
                #region 3번

                int totalPeopleCount = random.Next(9, 16);
                int groupCount = random.Next(1, 4);

                Console.WriteLine(questions[2], totalPeopleCount, groupCount);

                int permutation = getPermutation(totalPeopleCount, groupCount * 3);
                int groupFactorial = getPermutation(groupCount, groupCount);
                int setFactorial = getPermutation(3, 3);

                rightAnswer = permutation / (decimal)(Math.Pow(groupFactorial, 3) * setFactorial);
                if (decimal.Parse(Console.ReadLine()) == rightAnswer)
                {
                    correctionFlags |= 1 << 2;
                }

                #endregion
                Console.WriteLine();
                #region 4번

                int robotCount = random.Next(2, 10);
                int dollCount = random.Next(2, 10);

                Console.WriteLine(questions[3], robotCount, dollCount);

                rightAnswer = getPermutation(robotCount, robotCount) * getPermutation(dollCount, dollCount) * 2;
                if (decimal.Parse(Console.ReadLine()) == rightAnswer)
                {
                    correctionFlags |= 1 << 3;
                }

                #endregion
                Console.WriteLine();
                #region 5번

                int bitCount = random.Next(4, 13);
                Console.WriteLine(questions[4], bitCount);

                rightAnswer = (1 << (bitCount - 1)) + (1 << bitCount - 2) - (1 << bitCount - 3);
                if (decimal.Parse(Console.ReadLine()) == rightAnswer)
                {
                    correctionFlags |= 1 << 4;
                }

                #endregion
                Console.WriteLine();
                #region 6번

                int chineseBookCount = random.Next(2, 14);
                int englishBookCount = random.Next(2, 14);
                int japaneseBookCount = random.Next(2, 14);

                Console.WriteLine(questions[5], chineseBookCount, englishBookCount, japaneseBookCount);

                rightAnswer = chineseBookCount * englishBookCount + chineseBookCount * japaneseBookCount + englishBookCount * japaneseBookCount;
                if (decimal.Parse(Console.ReadLine()) == rightAnswer)
                {
                    correctionFlags |= 1 << 5;
                }

                #endregion
                Console.WriteLine();
                #region 7번

                int experimentCount = random.Next(1, 7);

                Console.WriteLine(questions[6], experimentCount);

                rightAnswer = (int)((getPermutation(6, experimentCount) / (decimal)Math.Pow(6, experimentCount) + 0.000005m) * 100000) / 1000.0m;
                if (decimal.Parse(Console.ReadLine()) == rightAnswer)
                {
                    correctionFlags |= 1 << 6;
                }

                #endregion
                Console.WriteLine();
                #region 8번

                int busRouteCount = random.Next(1, 10);
                int texiRouteCount = random.Next(1, 10);

                Console.WriteLine(questions[7], busRouteCount, texiRouteCount);

                rightAnswer = busRouteCount + texiRouteCount + 2;
                if (decimal.Parse(Console.ReadLine()) == rightAnswer)
                {
                    correctionFlags |= 1 << 7;
                }

                #endregion
                Console.WriteLine();
                #region 9번

                int shirtCount = random.Next(3, 14);
                int pantsCount = random.Next(3, 14);
                int capCount = random.Next(3, 14);

                Console.WriteLine(questions[8], shirtCount, pantsCount, capCount);

                rightAnswer = shirtCount * pantsCount * capCount + shirtCount * pantsCount;
                if (decimal.Parse(Console.ReadLine()) == rightAnswer)
                {
                    correctionFlags |= 1 << 8;
                }

                #endregion
                Console.WriteLine();
                #region 10번

                decimal[] probabilities = new decimal[] { 0.1m, 0.2m, 0.3m, 0.4m, 0.6m, 0.7m, 0.8m, 0.9m };

                decimal firstProbability = 0.5m;
                decimal secondProbability = probabilities[random.Next(0, probabilities.Length)];
                decimal thirdProbability = probabilities[random.Next(0, probabilities.Length)];

                Console.WriteLine(questions[9], firstProbability, secondProbability, thirdProbability);

                decimal firstCoinResult = firstProbability * firstProbability * firstProbability;
                decimal secondCoinResult = secondProbability * (1 - secondProbability) * (1 - secondProbability);
                decimal thirdCoinResult = thirdProbability * (1 - thirdProbability) * (1 - thirdProbability);

                rightAnswer = (int)((firstCoinResult + secondCoinResult + thirdCoinResult + 0.00005m) * 10000) / 100.0m;

                rightAnswer = shirtCount * pantsCount * capCount + shirtCount + pantsCount;
                if (decimal.Parse(Console.ReadLine()) == rightAnswer)
                {
                    correctionFlags |= 1 << 9;
                }

                #endregion
                Console.WriteLine();

                printResult(correctionFlags);
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e);
            }
        }

        public static void StartLab10(string filePath)
        {
            if (!File.Exists(filePath))
            {
                Console.WriteLine("파일이나 파일의 경로를 확인 해주세요.");
                return;
            }

            try
            {
                short correctionFlags = 0;

                Random random = new Random();

                string[] questions = File.ReadAllText(filePath).Split('\\');

                int[] confidences = new int[] { 90, 95, 99 };
                decimal[] confidenceScores = new decimal[] { 1.645m, 1.96m, 2.575m };

                #region 1번

                int sampleCount = random.Next(300, 550);
                int phoneOwnerCount = random.Next(50, 250);

                int confidenceIndex = random.Next(1, 3);
                int confidence = confidences[confidenceIndex];
                decimal confidenceScore = confidenceScores[confidenceIndex];

                decimal sampleRatio = phoneOwnerCount / (decimal)sampleCount;
                Console.WriteLine(questions[0], sampleCount, phoneOwnerCount, confidence);

                decimal rightAnswer = Math.Round(2 * confidenceScore * (decimal)Math.Sqrt((double)(sampleRatio * (1 - sampleRatio)) / sampleCount), 3);
                if (decimal.Parse(Console.ReadLine()) == rightAnswer)
                {
                    correctionFlags = 1;
                }

                #endregion
                Console.WriteLine();
                #region 2번

                Console.WriteLine(questions[1]);

                rightAnswer = 9.0m;
                if (decimal.Parse(Console.ReadLine()) == rightAnswer)
                {
                    correctionFlags |= 1 << 1;
                }

                #endregion
                Console.WriteLine();
                #region 3번

                decimal meanWeight = random.Next(50, 300);

                decimal variance = (meanWeight * meanWeight + random.Next(200, 2000)) - meanWeight * meanWeight;
                decimal sigma = (int)Math.Sqrt((double)variance);

                decimal variableWeight = meanWeight + random.Next(0, (int)sigma) - random.Next(0, (int)sigma);

                Console.WriteLine(questions[2], meanWeight, variance, variableWeight);

                rightAnswer = Math.Round((variableWeight - meanWeight) / sigma, 2);
                if (decimal.Parse(Console.ReadLine()) == rightAnswer)
                {
                    correctionFlags |= 1 << 2;
                }

                #endregion
                Console.WriteLine();
                #region 4번

                decimal[] scores = new decimal[8];

                for (int i = 0; i < scores.Length; ++i)
                {
                    scores[i] = random.Next(800, 1500);
                }

                Console.WriteLine(questions[3], scores[0], scores[1], scores[2], scores[3], scores[4], scores[5], scores[6], scores[7]);

                decimal mean = 0;
                decimal meanOfVariableSquared = 0;

                foreach (var score in scores)
                {
                    mean += score;
                    meanOfVariableSquared += score * score;
                }
                mean /= 8.0m;
                meanOfVariableSquared /= 8.0m;

                rightAnswer = Math.Round(meanOfVariableSquared - mean * mean, 2);
                if (decimal.Parse(Console.ReadLine()) == rightAnswer)
                {
                    correctionFlags |= 1 << 3;
                }

                #endregion
                Console.WriteLine();
                #region 5번

                decimal complainCount = random.Next(1, 300);
                decimal totalCoffeeCount = random.Next(400, 700);
                decimal targetValue = random.Next(1, 6) / 100.0m;

                decimal complainProbability = complainCount / totalCoffeeCount;
                Console.WriteLine(questions[4], complainCount, totalCoffeeCount, targetValue * 100);

                targetValue = (decimal)Math.Log((double)targetValue);
                decimal denominator = (decimal)Math.Log((double)(1 - complainProbability));

                rightAnswer = Math.Ceiling(targetValue / denominator);
                if (decimal.Parse(Console.ReadLine()) == rightAnswer)
                {
                    correctionFlags |= 1 << 4;
                }

                #endregion
                Console.WriteLine();
                #region 6번

                decimal faultyCount = random.Next(1, 7);
                decimal faultyProbability = faultyCount / 7;
                decimal normalProbability = 1 - faultyProbability;
                Console.WriteLine(questions[5], faultyCount);

                decimal zeroFaultProbability = normalProbability * normalProbability * normalProbability * normalProbability * normalProbability * normalProbability * normalProbability;
                decimal oneFaultProbability = faultyCount * normalProbability * normalProbability * normalProbability * normalProbability * normalProbability * normalProbability;
                decimal twoFaultProbability = 21 * faultyProbability * faultyProbability * normalProbability * normalProbability * normalProbability * normalProbability * normalProbability;

                rightAnswer = Math.Round((zeroFaultProbability + oneFaultProbability + twoFaultProbability) * 100, 2);
                if (decimal.Parse(Console.ReadLine()) == rightAnswer)
                {
                    correctionFlags |= 1 << 6;
                }

                #endregion
                Console.WriteLine();
                #region 7번

                sampleCount = random.Next(80, 200);
                mean = random.Next(20, 30);
                decimal mediumValue = mean + random.Next(20, 30) - random.Next(20, 30);
                sigma = random.Next(10, 25);

                confidenceIndex = random.Next(1, 3);
                confidence = confidences[confidenceIndex];
                confidenceScore = confidenceScores[confidenceIndex];

                Console.WriteLine(questions[6], sampleCount, mean, mediumValue, sigma, confidence);

                rightAnswer = Math.Round(mean - confidenceScore * (sigma / (decimal)Math.Sqrt(sampleCount)), 3);
                if (decimal.Parse(Console.ReadLine()) == rightAnswer)
                {
                    correctionFlags |= 1 << 6;
                }

                #endregion
                Console.WriteLine();
                #region 8번

                Console.WriteLine(questions[7]);

                rightAnswer = 44.71m;
                if (decimal.Parse(Console.ReadLine()) == rightAnswer)
                {
                    correctionFlags |= 1 << 7;
                }

                #endregion
                Console.WriteLine();
                #region 9번

                Console.WriteLine(questions[8]);

                if (decimal.Parse(Console.ReadLine()) == 0)
                {
                    correctionFlags |= 1 << 8;
                }

                #endregion
                Console.WriteLine();
                #region 10번

                decimal trialCount = random.Next(10, 30);
                decimal factor = random.Next(3, 20);
                decimal factorCount = (int)(35 / factor);

                decimal probability = factorCount / 35;
                Console.WriteLine(questions[9], trialCount, factor);

                rightAnswer = Math.Round(trialCount * probability, 2);
                if (decimal.Parse(Console.ReadLine()) == rightAnswer)
                {
                    correctionFlags |= 1 << 9;
                }
                #endregion
                Console.WriteLine();

                printResult(correctionFlags);
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e);
            }
        }

        private static int getPermutation(int total, int choosingCount)
        {
            int result = 1;

            if (choosingCount == 0)
            {
                return result;
            }

            for (int i = total; i > total - choosingCount; --i)
            {
                result *= i;
            }

            return result;
        }

        private static int getCombination(int total, int choosingCount)
        {
            int denominator = 1;

            if (choosingCount == 0 || choosingCount == 1)
            {
                return 1;
            }

            if (choosingCount > total - choosingCount)
            {
                choosingCount = total - choosingCount;
            }

            for (int i = choosingCount; i >= 2; --i)
            {
                denominator *= i;
            }

            return getPermutation(total, choosingCount) / denominator;
        }

        private static void printResult(int correctionFlags)
        {
            for (int i = 0; i < 10; ++i)
            {
                string correction;
                if ((correctionFlags & (1 << i)) != 0)
                {
                    correction = "정답";
                }
                else
                {
                    correction = "오답";
                }

                Console.WriteLine($"{i + 1}번 : {correction}");
            }
        }
    }
}