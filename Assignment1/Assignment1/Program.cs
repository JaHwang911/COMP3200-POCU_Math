using System;
using System.Diagnostics;

namespace Assignment1
{
    class Program
    {
        static void Main(string[] args)
        {
            Debug.Assert(BigNumberCalculator.GetOnesComplementOrNull("as89fdf0") == null);
            Debug.Assert(BigNumberCalculator.GetOnesComplementOrNull("0xFAKEHEX") == null);
            Debug.Assert(BigNumberCalculator.GetOnesComplementOrNull("0bFAKEBINARY") == null);
            Debug.Assert(BigNumberCalculator.GetOnesComplementOrNull("FAKEDECIMAL") == null);

            Debug.Assert(BigNumberCalculator.GetOnesComplementOrNull("0x") == null);
            Debug.Assert(BigNumberCalculator.GetOnesComplementOrNull("0b") == null);
            Debug.Assert(BigNumberCalculator.GetOnesComplementOrNull("    ") == null);
            Debug.Assert(BigNumberCalculator.GetOnesComplementOrNull("") == null);
            Debug.Assert(BigNumberCalculator.GetOnesComplementOrNull("-") == null);

            Debug.Assert(BigNumberCalculator.GetOnesComplementOrNull("-10") == null);
            Debug.Assert(BigNumberCalculator.GetOnesComplementOrNull("0xFC34") == null);

            Debug.Assert(BigNumberCalculator.GetOnesComplementOrNull("0b0000111010110") == "0b1111000101001");
            Debug.Assert(BigNumberCalculator.GetOnesComplementOrNull("0b1000") == "0b0111");
            Debug.Assert(BigNumberCalculator.GetOnesComplementOrNull("0b0110101011101011100000") == "0b1001010100010100011111");

            Debug.Assert(BigNumberCalculator.GetTwosComplementOrNull("0b0000111010110") == "0b1111000101010");
            Debug.Assert(BigNumberCalculator.GetTwosComplementOrNull("0b1000") == "0b1000");
            // Debug.Assert(BigNumberCalculator.ToBinaryOrNull("123") == "0b01111011");

            Debug.Assert(BigNumberCalculator.ToBinaryOrNull("-0") == null);
            Debug.Assert(BigNumberCalculator.ToBinaryOrNull("-01") == null);
            Debug.Assert(BigNumberCalculator.ToBinaryOrNull("-") == null);
            Debug.Assert(BigNumberCalculator.ToBinaryOrNull("0101") == null);
            Debug.Assert(BigNumberCalculator.ToBinaryOrNull("0023") == null);
            Debug.Assert(BigNumberCalculator.ToBinaryOrNull("--11") == null);
            Debug.Assert(BigNumberCalculator.ToBinaryOrNull("00000000") == null);
            Debug.Assert(BigNumberCalculator.ToBinaryOrNull("+11") == null);
            Debug.Assert(BigNumberCalculator.ToBinaryOrNull("0b0b") == null);
            Debug.Assert(BigNumberCalculator.ToBinaryOrNull("0b0x") == null);
            Debug.Assert(BigNumberCalculator.ToBinaryOrNull("0xx0b") == null);
            Debug.Assert(BigNumberCalculator.ToBinaryOrNull("    ") == null);
            Debug.Assert(BigNumberCalculator.ToBinaryOrNull("") == null);
            Debug.Assert(BigNumberCalculator.ToBinaryOrNull("  24aA1  ") == null);
            Debug.Assert(BigNumberCalculator.ToBinaryOrNull(" 123 3VXCa  ") == null);
            Debug.Assert(BigNumberCalculator.ToBinaryOrNull("0bAA") == null);
            Debug.Assert(BigNumberCalculator.ToBinaryOrNull("0b") == null);
            Debug.Assert(BigNumberCalculator.ToBinaryOrNull("0x") == null);
            Debug.Assert(BigNumberCalculator.ToBinaryOrNull("KJDSLF:N(&#") == null);
            Debug.Assert(BigNumberCalculator.ToBinaryOrNull("#$@#$@#$") == null);
            Debug.Assert(BigNumberCalculator.ToBinaryOrNull("SER#$V@$V") == null);

            Console.WriteLine("No prob");
        }
    }
}
