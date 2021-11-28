using System;
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
                double jae = (variable * variable) / (2.0 * powSigma) * -1;
                double term = Math.Exp(jae);
                gaussianResult[i] = coefficient * term;
            }

            Console.WriteLine(string.Join(",", gaussianResult));
            return gaussianResult;
        }

        public static double[] Convolve1D(double[] signal, double[] filter)
        {
            return null;
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