using System;

namespace squared
{
    class Program
    {
        static void Main(string[] args)
        {
            double value = 1 - 0.15109343936;
            double errorLimit = 0.05;
            double sum = 1;
            int count = 0;

            while (sum >= errorLimit)
            {
                sum *= value;
                ++count;
            }

            Console.WriteLine($"{count} 번");

            //for(int i = 0; i < results.Length; i++)
            //{
            //    Console.WriteLine(results[i]);
            //}
        }
    }
}
