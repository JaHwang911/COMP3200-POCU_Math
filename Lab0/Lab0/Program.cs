using System;

namespace Lab0
{
    class Program
    {
        static void Main(string[] args)
        {
            //BuildTestLab0.BuildTest();

            string stringDecimal1;
            string stringDecimal2;
            string stringDecimal3;
            string stringDecimal4;
            string stringDecimal5;

            bool bResult1 = Lab0.TryGetRepeatingDecimal(1, 6, out stringDecimal1); // False, "0"
            bool bResult2 = Lab0.TryGetRepeatingDecimal(1, 7, out stringDecimal2); // True, "0.*3*"
            bool bResult3 = Lab0.TryGetRepeatingDecimal(1, 4, out stringDecimal3); // false, "0.25"
            bool bResult4 = Lab0.TryGetRepeatingDecimal(2, 9, out stringDecimal4); // True, "0.*2*"
            bool bResult5 = Lab0.TryGetRepeatingDecimal(1, -3, out stringDecimal5); // False, ""
            //bool bResult1 = Lab0.TryGetRepeatingDecimal(0, 1, out stringDecimal1); // False, "0"
            //bool bResult2 = Lab0.TryGetRepeatingDecimal(1, 3, out stringDecimal2); // True, "0.*3*"
            //bool bResult3 = Lab0.TryGetRepeatingDecimal(1, 4, out stringDecimal3); // false, "0.25"
            //bool bResult4 = Lab0.TryGetRepeatingDecimal(2, 9, out stringDecimal4); // True, "0.*2*"
            //bool bResult5 = Lab0.TryGetRepeatingDecimal(1, -3, out stringDecimal5); // False, ""

            //Console.WriteLine($"{bResult1}, \"{stringDecimal1}\"");
            //Console.WriteLine($"{bResult2}, \"{stringDecimal2}\"");
            //Console.WriteLine($"{bResult3}, \"{stringDecimal3}\"");
            //Console.WriteLine($"{bResult4}, \"{stringDecimal4}\"");
            //Console.WriteLine($"{bResult5}, \"{stringDecimal5}\"");

            string irreducibleFraction1;
            string irreducibleFraction2;
            string irreducibleFraction3;

            bool bResult6 = Lab0.TryGetIrreducibleFraction("0.13", out irreducibleFraction1); // True, "13 / 100"
            bool bResult7 = Lab0.TryGetIrreducibleFraction("0.1*6*", out irreducibleFraction2); // True, "1 / 6"
            bool bResult8 = Lab0.TryGetIrreducibleFraction("0.25", out irreducibleFraction3); // True, "1 / 4"

            Console.WriteLine($"{bResult6}, \"{irreducibleFraction1}\"");
            Console.WriteLine($"{bResult7}, \"{irreducibleFraction2}\"");
            Console.WriteLine($"{bResult8}, \"{irreducibleFraction3}\"");
        }
    }
}
