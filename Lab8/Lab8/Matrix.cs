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

            for (int i = 0; i < input.GetLength(0); i++)
            {
                for (int j = 0; j < input.GetLength(1); j++)
                {
                    transposeResult[j, i] = input[i, j];
                }
            }

            return transposeResult;
        }

        public static int[,] GetIdentityMatrix(int size)
        {
            int[,] result = new int[size, size];

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (i == j)
                    {
                        result[i, j] = 1;
                    }
                    else
                    {
                        result[i, j] = 0;
                    }
                }
            }

            return result;
        }

        public static int[] GetRowOrNull(int[,] matrix, int row)
        {
            if (row > matrix.GetLength(0) - 1)
            {
                return null;
            }

            int[] result = new int[matrix.GetLength(1)];

            for (int i = 0; i < matrix.GetLength(1); i++)
            {
                result[i] = matrix[row, i];
            }

            return result;
        }

        public static int[] GetColumnOrNull(int[,] matrix, int col)
        {
            if (col > matrix.GetLength(1) - 1)
            {
                return null;
            }

            int[] result = new int[matrix.GetLength(0)];

            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                result[i] = matrix[i, col];
            }

            return result;
        }
    }
}
