using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Assignment3
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> expectedValue1 = new List<int> { 100, 102, 112, 114, 116, 118, 120, 123, 125, 127, 130, 132, 135, 137, 139, 141, 143, 146, 148, 150, 153, 155, 158, 160, 162, 165, 167, 170 };
            List<int> expectedValue2 = new List<int> { 100, 102, 112, 115, 117, 120, 122, 124, 127, 129, 132, 134, 136, 139, 141, 143, 145, 147, 150, 152, 155, 157, 159, 162, 164, 166, 168, 170 };
            List<int> expectedValue3 = new List<int> { 100, 102, 112, 115, 116, 117, 117, 123, 122, 124, 128, 132, 138, 139, 143, 146, 151, 151, 161, 170 };

            int[] steps = new int[] { 100, 102, 112, 170 };

            INoise noise = new ZeroNoise();
            List<int> newSteps = StepMaker.MakeSteps(steps, noise);

            Debug.Assert(expectedValue1.Count == newSteps.Count);

            for (int i = 0; i < expectedValue1.Count; i++)
            {
                Debug.Assert(expectedValue1[i] == newSteps[i]);
            }

            noise = new ConstantNoise();
            newSteps = StepMaker.MakeSteps(steps, noise);

            Debug.Assert(expectedValue2.Count == newSteps.Count);

            for (int i = 0; i < expectedValue2.Count; i++)
            {
                Debug.Assert(expectedValue2[i] == newSteps[i]);
            }

            noise = new SineNoise();
            newSteps = StepMaker.MakeSteps(steps, noise);

            Debug.Assert(expectedValue3.Count == newSteps.Count);

            for (int i = 0; i < expectedValue3.Count; i++)
            {
                Debug.Assert(expectedValue3[i] == newSteps[i]);
            }

            steps = new int[] { 100, 360 };
            var expectedValue4 = new List<int> { 100, 107, 117, 117, 121, 124, 127, 130, 132, 135, 139, 142, 142, 149, 152, 155, 159, 157, 159, 161, 163, 165, 171, 180, 188, 197, 207, 212, 221, 221, 224, 227, 229, 232, 234, 237, 240, 243, 243, 249, 252, 254, 258, 256, 258, 260, 261, 263, 269, 278, 286, 295, 305, 312, 322, 322, 325, 329, 331, 334, 336, 339, 343, 346, 346, 352, 355, 358, 362, 360 };
            newSteps = StepMaker.MakeSteps(steps, new CosineNoise());

            Debug.Assert(expectedValue4.Count == newSteps.Count);

            for (int i = 0; i < expectedValue4.Count; i++)
            {
                Debug.Assert(expectedValue4[i] == newSteps[i]);
            }

            var temp = new List<int> { -11, -5, 0, 4, 9, 15, 19, 23, 28, 32, 37, 34, 31, 28, 25, 22, 19, 16, 13, 10, 7, 4, 1, 0, -3, -6, -9, -12, -15, -18, -21, -24, -27, -30, -33, -36, -27, -18, -9, 0, 8, 17, 26, 35, 44, 53, 62, 71, 80, 89, 98, 107, 116, 125, 134, 143, 152, 161, 170, 179, 188 };
            var temp1 = new int[] { -11, 15, 37, -36, 188 };
            newSteps = StepMaker.MakeSteps(temp1, new ZeroNoise());
            Debug.Assert(temp.Count == newSteps.Count);
            for (int i = 0; i < temp.Count; i++)
            {
                Debug.Assert(temp[i] == newSteps[i]);
            }

            Console.WriteLine("No prob");
        }
    }
}