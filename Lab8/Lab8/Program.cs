using System;

namespace Lab8
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] v1 = new int[] { 3, 5, 1};
            int[] v2 = new int[] { -2, 4, -1};
            int dot = Matrix.DotProduct(v1, v2);
            Console.WriteLine(dot);
        }
    }
}
