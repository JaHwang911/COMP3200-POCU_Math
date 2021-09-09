namespace Assignment1
{
    public class BigNumberCalculator
    {
        public BigNumberCalculator(int bitCount, EMode mode)
        {

        }

        public static string GetOnesComplementOrNull(string num)
        {
            if (num[0] != '0' || num[1] != 'b')
            {
                return null;
            }

            string tempBinary = num.Substring(2);
            int inpuBinary = int.Parse(tempBinary);

            return null;
        }
    }
}