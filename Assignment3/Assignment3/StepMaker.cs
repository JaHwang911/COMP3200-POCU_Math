using System;
using System.Collections.Generic;

namespace Assignment3
{
    public static class StepMaker
    {
        public static List<int> MakeSteps(int[] steps)
        {
            List<int> resultSteps = new List<int>();
            double[] AddRate = new double[5];
            int index = 0;

            resultSteps.AddRange(steps);
            AddRate[0] = 0.2;
            AddRate[1] = 0.25;
            AddRate[2] = 0.33;
            AddRate[3] = 0.5;
            AddRate[4] = 1;
            
            while (index < resultSteps.Count)
            {
                if ((resultSteps[index + 1] - resultSteps[index]) > 10)
                {
                    int newStepDistance = (int)((resultSteps[index + 1] - resultSteps[index]) * 0.2);
                    var newSteps = AddStepsRecursive(newStepDistance, 0, resultSteps[index], resultSteps[index + 1]);
                    resultSteps.InsertRange(index + 1, newSteps);
                }
                else
                {
                    index++;
                }
            }

            return resultSteps;
        }

        private static List<int> AddStepsRecursive(int newStepDistance, int recursiveLevel, int min, int max)
        {
            List<int> steps = new List<int>();
            int newStepNumber = max - newStepDistance;

            if (newStepNumber == min)
            {
                return steps;
            }

            recursiveLevel++;
            steps = AddStepsRecursive(newStepDistance, recursiveLevel, min, newStepNumber);
            steps.Add(newStepNumber);

            return steps;
        }
    }
}