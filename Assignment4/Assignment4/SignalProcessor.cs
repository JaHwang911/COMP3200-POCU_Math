﻿using System;
using System.Collections.Generic;
using System.Drawing;

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

        public static Bitmap ConvolveImage(Bitmap bitmap, Bitmap result, double[,] filter)
        {
            Rectangle cloneRect = new Rectangle(0, 0, 250, 250);
            System.Drawing.Imaging.PixelFormat format = bitmap.PixelFormat;
            Bitmap cloneBitmap = bitmap.Clone(cloneRect, format);
            int imgWidth = cloneBitmap.Width;
            int imgHeight = cloneBitmap.Height;
            int paddingValue = filter.GetLength(0) / 2;
            var test1 = cloneBitmap.GetPixel(100, 100);

            for (int i = 0; i < imgHeight; i++)
            {
                for (int j = 0; j < imgWidth; j++)
                {
                    double rValue = 0;
                    double gValue = 0;
                    double bValue = 0;
                    int kIndex = -1;

                    for (int k = 0; k < filter.GetLength(0); k++)
                    {
                        int lIndex = -1;

                        for (int l = 0; l < filter.GetLength(1); l++)
                        {
                            int x = i + kIndex;
                            int y = j + lIndex;

                            if (x < 0 || x >= imgHeight || y < 0 || y >= imgWidth)
                            {
                                break;
                            }
                            else
                            {
                                Color value = bitmap.GetPixel(x, y);
                                rValue += filter[k, l] * value.R;
                                gValue += filter[k, l] * value.G;
                                bValue += filter[k, l] * value.B;
                            }
                            
                            lIndex++;
                        }
                    }

                    Color filteredValue = Color.FromArgb((int)rValue, (int)gValue, (int)bValue);
                    cloneBitmap.SetPixel(i, j, filteredValue);
                }
            }

            var test = cloneBitmap.GetPixel(100, 100);
            var expected = result.GetPixel(100, 100);
            return null;
        }
    }
}