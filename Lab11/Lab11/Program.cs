using System;

namespace Lab11
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] test = new int[6] { 5, 5, 6, 6, 6, 8};
            FrequencyTable.GetFrequencyTable(test, 4);
        }
    }
}
