using System;
using System.Collections.Generic;
using System.Drawing;

namespace Assignment4
{
    public static class SignalProcessor
    {
        public static double[] GetGaussianFilter1D(double sigma)
        {
            double powSigma = sigma * sigma;
            int filterLength = (sigma * 6) % 2 != 0 ? (int)(sigma * 6): (int)(sigma * 6) + 1;
            int firstValue = (filterLength / 2 + 1) - filterLength;
            double[] gaussianResult = new double[filterLength];

            for (int i = 0; i < filterLength; i++)
            {
                double variable = firstValue + i;
                double coefficient = 1 / (sigma * Math.Sqrt(2 * Math.PI));
                double pow = (variable * variable) / (2.0 * powSigma) * -1;
                double term = Math.Exp(pow);
                gaussianResult[i] = coefficient * term;
            }

            return gaussianResult;
        }

        public static double[] Convolve1D(double[] signal, double[] filter)
        {
            List<double> Convolved = new List<double>();
            List<double> tempSignal = new List<double>(256);
            List<double> tempFilter = new List<double>(filter.Length);
            int extraElementCount = filter.Length % 2 != 0 ? filter.Length / 2 : filter.Length / 2 + 1;

            Array.Reverse(filter);
            tempSignal.AddRange(signal);
            tempFilter.AddRange(filter);

            for (int i = 0; i < extraElementCount; i++)
            {
                tempSignal.Insert(0, 0);
            }

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
            return null;
        }

        //public static Bitmap ConvolveImage(Bitmap bitmap, double[,] filter)
        //{
        //    return null;
        //}
    }
}