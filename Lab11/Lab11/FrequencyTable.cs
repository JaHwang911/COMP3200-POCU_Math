using System;
using System.Collections.Generic;

namespace Lab11
{
    public static class FrequencyTable
    {
        public static List<Tuple<Tuple<int, int>, int>> GetFrequencyTable(int[] data, int maxBinCount)
        {
            //데이터의 개수를 샌다.
            //N = 12
            //데이터의 최대값, 최소값 그리고 범위를 구한다.
            //R = 33 - 8 = 25
            //계급의 수를 정한다.
            //K = 1 + log12 / log2 = 4.58-> 4
            //계급의 폭을 구한다.
            //C = R / K = 25 / 4 = 6.25-> 7
            //각 계급의 구간-- > 7~13, 14~20, 21~27, 28~34
            List<Tuple<Tuple<int, int>, int>> result = new List<Tuple<Tuple<int, int>, int>>();
            List<int> input = new List<int>(data.Length);
            input.AddRange(data);
            input.Sort();

            int rangeStartNum = input[0];
            double classCount = 1 + Math.Log(input.Count) / Math.Log(2);
            classCount = Math.Ceiling(classCount) > maxBinCount ? Math.Truncate(classCount) : Math.Round(classCount);
            int rangeDistance = (int)Math.Ceiling((input[input.Count - 1] - input[0]) / classCount);

            if (rangeDistance < 1)
            {
                rangeDistance = 1;
            }
            else if (input[0] + rangeDistance * classCount <= input[input.Count - 1])
            {
                rangeDistance++;
            }

            classCount = Math.Ceiling(classCount);

            for (int i = 0; i < classCount; i++)
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