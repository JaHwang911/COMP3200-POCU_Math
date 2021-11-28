using System;

namespace squared
{
    class Program
    {
        static void Main(string[] args)
        {
            double value = 1 - 0.11954765751;
            double errorLimit = 0.04;
            double currentValue = 1;
            int count = 0;

            while (currentValue >= errorLimit)
            {
                currentValue *= value;
                ++count;
            }

            Console.WriteLine($"{count} 번");

            int[] input = new int[8]
            {
                1473,
                1315,
                967,
                869,
                1501,
                1421,
                1545,
                1481
            };
            double sum = 0;
            double sqrtSum = 0;
            double avg = 0;
            double sqrtAvg = 0;

            for (int i = 0; i < input.Length; i++)
            {
                sum += input[i];
                sqrtSum += input[i] * input[i];
            }

            avg = sum / input.Length;
            sqrtAvg = sqrtSum / input.Length;

            double variance = sqrtAvg - (avg * avg);
            Console.WriteLine($"분산 : {variance}");
        }
    }
}
