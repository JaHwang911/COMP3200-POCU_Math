using System;
using System.Collections.Generic;

namespace Lab11
{
    public static class FrequencyTable
    {
        public static List<Tuple<Tuple<int, int>, int>> GetFrequencyTable(int[] data, int maxBinCount)
        {
            List<Tuple<Tuple<int, int>, int>> result = new List<Tuple<Tuple<int, int>, int>>();
            List<int> input = new List<int>(data.Length);
            input.AddRange(data);
            input.Sort();

            int rangeStartNum = input[0];
            int rangeDistance = (int)Math.Truncate((input[input.Count - 1] - input[0]) / (double)maxBinCount);

            if (rangeDistance < 1)
            {
                rangeDistance = 1;
            }
            else if (input[0] + rangeDistance * maxBinCount <= input[input.Count - 1])
            {
                rangeDistance++;
            }


            for (int i = 0; i < maxBinCount; i++)
            {
                Tuple<int, int> range = new Tuple<int, int>(rangeStartNum, rangeStartNum + rangeDistance);
                int count = 0;

                for (int j = 0; j < input.Count; j++)
                {
                    if (input[j] < range.Item2)
                    {
                        count++;
                    }
                }

                input.RemoveRange(0, count);
                result.Add(new Tuple<Tuple<int, int>, int>(range, count));
                rangeStartNum += rangeDistance;

                if (input.Count == 0)
                {
                    break;
                }
            }

            return result;
        }
    }
}