using System;
using System.Collections.Generic;
using System.Drawing;
using System.Diagnostics;

namespace Assignment4
{
    public static class SignalProcessor
    {
        public static double[] GetGaussianFilter1D(double sigma)
        {
            int filterLength = Math.Ceiling(sigma * 6) % 2 != 0 ? (int)Math.Ceiling(sigma * 6) : (int)Math.Ceiling(sigma * 6) + 1;
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
            List<double> convolved = new List<double>();
            List<double> tempSignal = new List<double>(256);
            List<double> tempFilter = new List<double>(filter.Length);
            int extraElementCount = filter.Length % 2 != 0 ? filter.Length / 2 : filter.Length / 2 + 1;

            tempFilter.AddRange(filter);
            tempFilter.Reverse();

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

                convolved.Add(value);
            }

            return convolved.ToArray();
        }

        public static double[,] GetGaussianFilter2D(double sigma)
        {
            int filterLength = Math.Ceiling(sigma * 6) % 2 != 0 ? (int)Math.Ceiling(sigma * 6) : (int)Math.Ceiling(sigma * 6) + 1;
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

        private static Color convolve2D(List<List<Color>> pixels, List<List<double>> filter)
        {
            double rValue = 0;
            double gValue = 0;
            double bValue = 0;

            for (int i = 0; i < filter.Count; i++)
            {
                for (int j = 0; j < filter[i].Count; j++)
                {
                    rValue = rValue + pixels[i][j].R * filter[i][j] > 255 ? 255 : rValue + pixels[i][j].R * filter[i][j];
                    gValue = gValue + pixels[i][j].G * filter[i][j] > 255 ? 255 : gValue + pixels[i][j].G * filter[i][j];
                    bValue = bValue + pixels[i][j].B * filter[i][j] > 255 ? 255 : bValue + pixels[i][j].B * filter[i][j];
                }
            }

            Color result = Color.FromArgb((int)rValue, (int)gValue, (int)bValue);

            return result;
        }

        public static Bitmap ConvolveImage(Bitmap bitmap, double[,] filter)
        {
            Bitmap resultBitmap = new Bitmap(bitmap.Width, bitmap.Height);
            List<List<double>> tempFilter = new List<List<double>>();

            for (int i = filter.GetLength(0) - 1; i >= 0; i--)
            {
                List<double> temp = new List<double>(filter.GetLength(1));

                for (int j = 0; j < filter.GetLength(1); j++)
                {
                    temp.Add(filter[i, j]);
                }

                temp.Reverse();
                tempFilter.Add(temp);
            }

            for (int i = 0; i < resultBitmap.Height; i++)
            {
                for (int j = 0; j < resultBitmap.Width; j++)
                {
                    List<List<Color>> pixels = new List<List<Color>>();

                    int iIndex = -1;

                    for (int k = 0; k < tempFilter.Count; k++)
                    {
                        int jIndex = -1;
                        List<Color> pixel = new List<Color>();

                        for (int l = 0; l < tempFilter[k].Count; l++)
                        {
                            int x = j + jIndex;
                            int y = i + iIndex;

                            if (x < 0 || x >= resultBitmap.Width || y < 0 || y >= resultBitmap.Height)
                            {
                                pixel.Add(Color.FromArgb(0, 0, 0));
                            }
                            else
                            {
                                pixel.Add(bitmap.GetPixel(x, y)); // xy이것만 바꿔서 확인 위에서 부터 아래로
                            }

                            jIndex++;
                        }

                        pixels.Add(pixel);
                        iIndex++;
                    }

                    var value = convolve2D(pixels, tempFilter);
                    resultBitmap.SetPixel(j, i, value);
                }
            }

            return resultBitmap;
        }
    }
}