using System;
using System.Collections.Generic;
using System.Drawing;

namespace Assignment4
{
    public static class SignalProcessor
    {
        public static double[] GetGaussianFilter1D(double sigma)
        {
            int filterLength = (sigma * 6) % 2 != 0 ? (int)(sigma * 6): (int)(sigma * 6) + 1;
            int firstValue = (filterLength / 2 + 1) - filterLength;
            double[] gaussianFilter = new double[filterLength];

            for (int i = 0; i < filterLength; i++)
            {
                double variable = firstValue + i;
                double coefficient = 1 / (sigma * Math.Sqrt(2 * Math.PI));
                double exponent = (variable * variable) / (2.0 * Math.Pow(sigma, 2)) * -1;
                double term = Math.Exp(exponent);
                gaussianFilter[i] = coefficient * term;
            }

            return gaussianFilter;
        }

        public static double[] Convolve1D(double[] signal, double[] filter)
        {
            List<double> Convolved = new List<double>();
            List<double> tempSignal = new List<double>(256);
            List<double> tempFilter = new List<double>(filter.Length);
            int extraElementCount = filter.Length % 2 != 0 ? filter.Length / 2 : filter.Length / 2 + 1;

            Array.Reverse(filter);
            tempFilter.AddRange(filter);

            for (int i = 0; i < extraElementCount; i++)
            {
                tempSignal.Add(0);
            }

            tempSignal.AddRange(signal);

            for (int i = 0; i < extraElementCount; i++)
            {
                tempSignal.Add(0);
            }

            for (int i = tempFilter.Count - extraElementCount - 1; i < tempSignal.Count - extraElementCount; i++)
            {
                double value = 0;

                for (int j = 0; j < tempFilter.Count; j++)
                {
                    int index = i + j - extraElementCount;
                    value += tempFilter[j] * tempSignal[index];
                }

                Convolved.Add(value);
            }

            return Convolved.ToArray();
        }

        public static double[,] GetGaussianFilter2D(double sigma)
        {
            // 짝수길이 틀릴 수
            int filterLength = (sigma * 6) % 2 != 0 ? (int)(sigma * 6): (int)(sigma * 6) + 1;
            double[,] gaussianFilter = new double[filterLength, filterLength];
            int midValue = filterLength / 2;

            for (int i = 0; i < gaussianFilter.GetLength(0); i++)
            {
                for (int j = 0; j < gaussianFilter.GetLength(1); j++)
                {
                    int xValue = j - midValue;
                    int yValue = i - midValue;
                    double coefficient = 1 / (2 * Math.PI * Math.Pow(sigma, 2));
                    double exponent = (Math.Pow(xValue, 2) + Math.Pow(yValue, 2)) / (2 * Math.Pow(sigma, 2)) * -1;
                    double term = Math.Exp(exponent);
                    gaussianFilter[i, j] = coefficient * term;
                }
            }

            return gaussianFilter;
        }

        public static Bitmap ConvolveImage(Bitmap bitmap, double[,] filter)
        {
            return null;
        }
    }
}