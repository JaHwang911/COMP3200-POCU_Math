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

            //INoise noise = new ZeroNoise();
            //List<int> newSteps = StepMaker.MakeSteps(steps, noise);

            //Debug.Assert(expectedValue1.Count == newSteps.Count);

            //for (int i = 0; i < expectedValue1.Count; i++)
            //{
            //    Debug.Assert(expectedValue1[i] == newSteps[i]);
            //}

            //noise = new ConstantNoise();
            //newSteps = StepMaker.MakeSteps(steps, noise);

            //Debug.Assert(expectedValue2.Count == newSteps.Count);

            //for (int i = 0; i < expectedValue2.Count; i++)
            //{
            //    Debug.Assert(expectedValue2[i] == newSteps[i]);
            //}

            double[] newStepDistanceAmount = new double[4];
            double[] reverseDistanceAmount = new double[4];

            newStepDistanceAmount[0] = 0.2;
            newStepDistanceAmount[1] = 0.4;
            newStepDistanceAmount[2] = 0.6;
            newStepDistanceAmount[3] = 0.8;

            reverseDistanceAmount[0] = 0.2;
            reverseDistanceAmount[1] = 0.25;
            reverseDistanceAmount[2] = 0.33;
            reverseDistanceAmount[3] = 0.5;

            INoise noise = new SineNoise();
            int[] level = new int[11];
            List<int> noiseResult = new();

            level[0] = 1;
            level[1] = 2;
            level[2] = 2;
            level[3] = 2;
            level[4] = 2;
            level[5] = 1;
            level[6] = 2;
            level[7] = 2;
            level[8] = 2;
            level[9] = 2;
            level[10] = 1;

            for (int i = 0; i < 11; i++)
            {
                noiseResult.Add(noise.GetNext(level[i]));
            }

            Console.WriteLine(string.Join(",", noiseResult));

            noise = new SineNoise();
            var test = StepMaker.AddStepsRecursive(reverseDistanceAmount, 0, 112, 170, noise);

            Console.WriteLine("No prob");
        }
    }
}
