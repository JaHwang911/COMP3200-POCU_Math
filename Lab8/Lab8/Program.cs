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

            int[,] matrix = new int[4, 6]
            {
                { 4, 6, 1, 0, 9, 2 },
                { 1, -2, 4, 5, 5, 9 },
                { 2, -8, -2, 1, -5, 2 },
                { 10, -10, 7, 7, 9, 5 },
            };
            int[,] transposed = Matrix.Transpose(matrix);
            PrintMatrix(transposed);

            int[,] identityMatrix = Matrix.GetIdentityMatrix(9);
            PrintMatrix(identityMatrix);

            matrix = new int[,]
            {
                { 2, 4, 6, 7 },
                { 4, -1, 5, 6 },
                { -5, 6, 1, 1 }
            };

            int[] row = Matrix.GetRowOrNull(matrix, 1);

            matrix = new int[,]
            {
                { 2, 4, 6, 7 },
                { 4, -1, 5, 6 },
                { -5, 6, 1, 1 }
            };

            int[] col = Matrix.GetColumnOrNull(matrix, 1); // col: [ 4, -1, 6 ]
        }

        public static void PrintMatrix(int[,] matrix)
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    Console.Write($"{matrix[i, j]}, ");
                }
                Console.WriteLine();
            }

            Console.WriteLine("------------------------------");
        }
    }
}
