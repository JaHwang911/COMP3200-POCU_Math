using System.Collections.Generic;

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

        public static int[] MultiplyMatrixVectorOrNull(int[,] matrix, int[] vector)
        {
            if (matrix.GetLength(0) != vector.Length && matrix.GetLength(1) != vector.Length)
            {
                return null;
            }
            else if (matrix.GetLength(0) != vector.Length && matrix.GetLength(1) == vector.Length)
            {
                matrix = Transpose(matrix);
            }

            int[] result = new int[matrix.GetLength(1)];

            for (int i = 0; i < matrix.GetLength(1); i++)
            {
                int tempSum = 0;

                for (int j = 0; j < matrix.GetLength(0); j++)
                {
                    tempSum += matrix[j, i] * vector[j];
                }

                result[i] = tempSum;
            }

            return result;
        }

        public static int[] MultiplyVectorMatrixOrNull(int[] vector, int[,] matrix)
        {
            return MultiplyMatrixVectorOrNull(matrix, vector);
        }

        public static int[,] MultiplyOrNull(int[,] multiplicand, int[,] multiplier)
        {
            if (multiplicand.GetLength(1) != multiplier.GetLength(0) && multiplicand.GetLength(1) != multiplier.GetLength(1))
            {
                return null;
            }
            else if (multiplicand.GetLength(1) != multiplier.GetLength(0) && multiplicand.GetLength(1) == multiplier.GetLength(1))
            {
                multiplier = Transpose(multiplier);
            }

            int[,] result = new int[multiplicand.GetLength(0), multiplier.GetLength(1)];

            for (int i = 0; i < multiplicand.GetLength(0); i++)
            {
                List<int> tempMultiplicand = new List<int>();

                for (int j = 0; j < multiplicand.GetLength(1); j++)
                {
                    tempMultiplicand.Add(multiplicand[i, j]);
                }

                int[] tempComponent = MultiplyMatrixVectorOrNull(multiplier, tempMultiplicand.ToArray());

                for (int j = 0; j < tempComponent.Length; j++)
                {
                    result[i, j] = tempComponent[j];
                }
            }

            return result;
        }
    }
}