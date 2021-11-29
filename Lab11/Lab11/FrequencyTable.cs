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
            List<int> input = new List<int>(data.Length);
            input.AddRange(data);
            input.Sort();

            double classCount = 1 + Math.Log(input.Count) / Math.Log(2);
            double range = (input[input.Count - 1] - input[0]) / classCount;

            return null;
        }
    }
}