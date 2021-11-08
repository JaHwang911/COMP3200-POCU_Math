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

            double[] newStepDistanceAmout = new double[5];
            double[] reverseDistanceAmout = new double[4];

            newStepDistanceAmout[0] = 0.8;
            newStepDistanceAmout[1] = 0.75;
            newStepDistanceAmout[2] = 0.67;
            newStepDistanceAmout[3] = 0.5;

            reverseDistanceAmout[0] = 0.2;
            reverseDistanceAmout[1] = 0.25;
            reverseDistanceAmout[2] = 0.33;
            reverseDistanceAmout[3] = 0.5;

            INoise noise = new SineNoise();
            //List<int> newSteps = StepMaker.AddStepsRecursive(newStepDistanceAmout, 0, 112, 170, noise);

            var test = StepMaker.ver2AddStepsRecursive(reverseDistanceAmout, 0, 112, 170, noise);

            Console.WriteLine("No prob");
        }
    }
}
