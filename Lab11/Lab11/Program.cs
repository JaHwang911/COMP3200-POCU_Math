using System;

namespace Lab11
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] test = new int[6] { 4, 5, 6, 7, 6, 8 };
            FrequencyTable.GetFrequencyTable(test, 4);
        }
    }
}
