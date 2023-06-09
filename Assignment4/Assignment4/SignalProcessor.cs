﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.IO;

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

        public static Bitmap ConvolveImage(Bitmap bitmap, double[,] filter)
        {
            int filterMidValue = filter.GetLength(0) / 2;
            Bitmap resultBitmap = new Bitmap(bitmap.Width, bitmap.Height);
            List<List<double>> tempFilter = new List<List<double>>();

            for (int i = filter.GetLength(0) - 1; i >= 0; i--)
            {
                List<double> temp = new List<double>(filter.GetLength(1));

                for (int j = filter.GetLength(1) - 1; j >= 0; j--)
                {
                    temp.Add(filter[i, j]);
                }

                tempFilter.Add(temp);
            }

            for (int i = 0; i < resultBitmap.Height; i++)
            {
                for (int j = 0; j < resultBitmap.Width; j++)
                {
                    int indexY = filterMidValue * -1;
                    double rValue = 0.0;
                    double gValue = 0.0;
                    double bValue = 0.0;

                    for (int k = 0; k < tempFilter.Count; k++)
                    {
                        int indexX = filterMidValue * -1;

                        for (int l = 0; l < tempFilter[k].Count; l++)
                        {
                            int x = j + indexX + l;
                            int y = i + indexY + k;

                            if (0 <= x && x < resultBitmap.Width && 0 <= y && y < resultBitmap.Height)
                            {
                                Color pixel = bitmap.GetPixel(x, y);
                                rValue = rValue + pixel.R * tempFilter[k][l] > 255 ? 255 : rValue + pixel.R * tempFilter[k][l];
                                gValue = gValue + pixel.G * tempFilter[k][l] > 255 ? 255 : gValue + pixel.G * tempFilter[k][l];
                                bValue = bValue + pixel.B * tempFilter[k][l] > 255 ? 255 : bValue + pixel.B * tempFilter[k][l];
                            }
                        }
                    }

                    Color value = Color.FromArgb((byte)rValue, (byte)gValue, (byte)bValue);
                    resultBitmap.SetPixel(j, i, value);
                }
            }

            return resultBitmap;
        }

        public static Bitmap ConvolveGrayScale(Bitmap bitmap)
        {
            Bitmap resultBitmap = new Bitmap(bitmap.Width, bitmap.Height);
            double[] grayScaleVector = new double[] { 0.2126, 0.7152, 0.0722 };

            for (int i = 0; i < bitmap.Height; i++)
            {
                for (int j = 0; j < bitmap.Width; j++)
                {
                    Color pixel = bitmap.GetPixel(j, i);

                    double grayValue = pixel.R * grayScaleVector[0] + pixel.G * grayScaleVector[1] + pixel.B * grayScaleVector[2];
                    Color colorValue = Color.FromArgb((int)grayValue, (int)grayValue, (int)grayValue);
                    resultBitmap.SetPixel(j, i, colorValue);
                }
            }

            return resultBitmap;
        }
    }
}