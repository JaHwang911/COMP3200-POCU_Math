using System;

namespace Assignment1
{
    class Program
    {
        static void Main(string[] args)
        {
            string test = BigNumberCalculator.GetOnesComplementOrNull("0b0110101011101011100000");
            Console.WriteLine(test);
        }
    }
}
