using System;
using System.Diagnostics;

namespace Lab8
{
    class Program
    {
        static void Main(string[] args)
        {
            int[,] matrix = new int[,]
            {
                { 2, 3 },
                { 1, 1 },
                { 3, 4 }
            };

            int[] vector = new int[] { 2, 1, 1};
            int[] expected = new int[] { 8, 11 };

            var result = Matrix.MultiplyMatrixVectorOrNull(matrix, vector);

            Debug.Assert(result.Length == expected.Length);

            for (int i = 0; i < expected.Length; i++)
            {
                Debug.Assert(expected[i] == result[i]);
            }
            
            matrix = new int[,]
            {
                { 2, 1, 3 },
                { 3, 1, 4 }
            };

            vector = new int[] { 2, 1, 1 };
            expected = new int[] { 8, 11 };

            result = Matrix.MultiplyMatrixVectorOrNull(matrix, vector);

            Debug.Assert(result.Length == expected.Length);

            for (int i = 0; i < expected.Length; i++)
            {
                Debug.Assert(expected[i] == result[i]);
            }

            matrix = new int[,]
            {
                { 2, 1 },
                { 2, 1 }
            };

            result = Matrix.MultiplyMatrixVectorOrNull(matrix, vector);
            Debug.Assert(result == null);

            matrix = new int[,]
            {
                { 2 },
                { 1}
            };

            vector = new int[] { 3, 3 };
            expected = new int[] { 9 };
            result = Matrix.MultiplyMatrixVectorOrNull(matrix, vector);
            Debug.Assert(expected.Length == result.Length);

            matrix = new int[,]
            {
                { 2 }
            };

            result = Matrix.MultiplyMatrixVectorOrNull(matrix, vector);
            Debug.Assert(result == null);

            vector = new int[] { 3 };
            expected = new int[] { 6 };
            result = Matrix.MultiplyMatrixVectorOrNull(matrix, vector);

            Debug.Assert(result.Length == expected.Length);

            for (int i = 0; i < expected.Length; i++)
            {
                Debug.Assert(expected[i] == result[i]);
            }

            matrix = new int[,]
            {
                { 2, 1, 3},
                { 4, 2, 2 }
            };
            vector = new int[] { 1 };
            result = Matrix.MultiplyMatrixVectorOrNull(matrix, vector);

            Debug.Assert(result != null);

            int[,] multiplicand = new int[,]
            {
                { 3, 1, 2},
                { 1, 1, 4},
                { 3, 3, 2}
            };

            int[,] multiplier = new int[,]
            {
                { 1, 2},
                { 2, 2},
                { 3, 1}
            };

            int[,] matrixExpected = new int[,]
            {
                { 11, 10 },
                { 15, 8 },
                { 15, 14 }
            };

            var matrixResult = Matrix.MultiplyOrNull(multiplicand, multiplier);

            for (int i = 0; i < matrixExpected.GetLength(0); i++)
            {
                for (int j = 0; j < matrixExpected.GetLength(1); j++)
                {
                    Debug.Assert(matrixExpected[i, j] == matrixResult[i, j]);
                }
            }

            multiplier = new int[,]
            {
                { 1, 2},
                { 2, 2},
                { 3, 1}
            };

            multiplier = new int[,]
            {
                { 1, 2, 3 },
                { 2, 2, 1 }
            };

            matrixResult = Matrix.MultiplyOrNull(multiplicand, multiplier);

            for (int i = 0; i < matrixExpected.GetLength(0); i++)
            {
                for (int j = 0; j < matrixExpected.GetLength(1); j++)
                {
                    Debug.Assert(matrixExpected[i, j] == matrixResult[i, j]);
                }
            }

            multiplier = new int[,]
            {
                { 1, 2},
                { 2, 2},
                { 3, 1}
            };

            multiplier = new int[,]
            {
                { 1, 2, 3, 2},
                { 2, 2, 1, 7}
            };

            matrixResult = Matrix.MultiplyOrNull(multiplicand, multiplier);
            Debug.Assert(matrixResult == null);

            Console.WriteLine("No prob");
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
