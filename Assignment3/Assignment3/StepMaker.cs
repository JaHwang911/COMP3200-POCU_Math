using System;
using System.Collections.Generic;

namespace Assignment3
{
    public static class StepMaker
    {
        public static List<int> MakeSteps(int[] steps, INoise noise)
        {
            List<int> resultSteps = new List<int>();
            double[] newStepDistanceAmout = new double[5];
            //double[] reverseDistanceAmout = new double[5];
            int index = 0;
            
            resultSteps.AddRange(steps);

            newStepDistanceAmout[0] = 0.8;
            newStepDistanceAmout[1] = 0.75;
            newStepDistanceAmout[2] = 0.67;
            newStepDistanceAmout[3] = 0.5;

            //reverseDistanceAmout[0] = 0.2;
            //reverseDistanceAmout[1] = 0.25;
            //reverseDistanceAmout[2] = 0.33;
            //reverseDistanceAmout[3] = 0.5;

            while (index < resultSteps.Count)
            {
                if (index + 1 < resultSteps.Count && (resultSteps[index + 1] - resultSteps[index]) > 10)
                {
                    resultSteps.InsertRange(index + 1, AddStepsRecursive(newStepDistanceAmout, 0, resultSteps[index], resultSteps[index + 1], noise));
                }
                else
                {
                    index++;
                }
            }

            return resultSteps;
        }

        public static List<int> AddStepsRecursive(double[] newStepDistanceAmout, int recursiveLevel, int min, double max, INoise noise)
        {
            List<int> steps = new List<int>();

            if (recursiveLevel == newStepDistanceAmout.Length - 1)
            {
                return steps;
            }

            double newStepNumber = (max - min) * newStepDistanceAmout[recursiveLevel];

            max = min + newStepNumber;
            int noiseStep = noise.GetNext(recursiveLevel); // 위치 중요
            recursiveLevel++;
            steps = AddStepsRecursive(newStepDistanceAmout, recursiveLevel, min, max, noise);
            steps.Add((int)max + noiseStep);

            return steps;
        }

        public static List<int> ver2AddStepsRecursive(double[] newStepDistanceAmout, int recursiveLevel, int min, double max, INoise noise)
        {
            List<int> steps = new List<int>();

            if (recursiveLevel == newStepDistanceAmout.Length - 1)
            {
                return steps;
            }

            double newStepNumber = (max - min) * newStepDistanceAmout[recursiveLevel];

            max = min + newStepNumber;
            steps = AddStepsRecursive(newStepDistanceAmout, recursiveLevel + 1, min, max, noise);
            int noiseStep = noise.GetNext(recursiveLevel); // 위치 중요
            steps.Add((int)max + noiseStep);

            return steps;
        }
    }
}