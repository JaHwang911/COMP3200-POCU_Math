using System;
using System.Collections.Generic;

namespace Assignment3
{
    public static class StepMaker
    {
        public static List<int> MakeSteps(int[] steps)
        {
            List<int> resultSteps = new List<int>();
            double[] newStepDistanceAmout = new double[5];
            int index = 0;

            resultSteps.AddRange(steps);
            newStepDistanceAmout[0] = 0.8;
            newStepDistanceAmout[1] = 0.75;
            newStepDistanceAmout[2] = 0.67;
            newStepDistanceAmout[3] = 0.5;
            
            while (index < resultSteps.Count)
            {
                if (index + 1 < resultSteps.Count && (resultSteps[index + 1] - resultSteps[index]) > 10)
                {
                    resultSteps.InsertRange(index + 1, AddStepsRecursive(newStepDistanceAmout, 0, resultSteps[index], resultSteps[index + 1]));
                }
                else
                {
                    index++;
                }
            }

            return resultSteps;
        }

        private static List<int> AddStepsRecursive(double[] newStepDistanceAmout, int recursiveLevel, int min, double max)
        {
            List<int> steps = new List<int>();

            if (recursiveLevel == newStepDistanceAmout.Length - 1)
            {
                return steps;
            }

            double newStepNumber = (max - min) * newStepDistanceAmout[recursiveLevel];

            max = min + newStepNumber;
            recursiveLevel++;
            steps = AddStepsRecursive(newStepDistanceAmout, recursiveLevel, min, max);
            steps.Add((int)max);

            return steps;
        }
    }
}