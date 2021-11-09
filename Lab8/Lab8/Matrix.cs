using System;
namespace Lab8
{
    public static class Matrix
    {
        public static int DotProduct(int[] v1, int[] v2)
        {
            int sum = 0;

            for (int i = 0; i < v1.Length; i++)
            {
                sum += v1[i] * v2[i];
            }

            return sum;
        }

        public static int[,] Transpose(int[,] input)
        {
            
            int[,] transposeResult = new int[input.GetLength(1), input.GetLength(0)];

            for (int i = 0; i < input.GetLength(1); i++)
            {
                for (int j = 0; j < input.GetLength(0); j++)
                {
                    transposeResult[i, j] = input[i, j];
                }
            }

            return transposeResult;
        }
    }
}
