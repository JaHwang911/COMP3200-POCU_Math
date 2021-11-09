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
                    resultSteps.InsertRange(index + 1, AddStepsRecursive(newStepDistanceAmout, 0, resultSteps[index], resultSteps[index + 1], noise));
                }
                else
                {
                    index++;
                }
            }

            return resultSteps;
        }

        public static List<int> AddStepsRecursive(double[] newStepDistanceAmount, int level, double min, double max, INoise noise)
        {
            List<int> steps = new ();

            steps.Add((int)min);

            for (int i = 0; i < newStepDistanceAmount.Length; i++)
            {
                double newStepDistance = (max - min) * newStepDistanceAmount[i];
                int resultNoise = noise.GetNext(level);
                min = min + (int)newStepDistance + resultNoise;
                steps.Add((int)min);
            }

            steps.Add((int)max);

            for (int i = 0; i < steps.Count; i++)
            {
                if (i + 1 < steps.Count && steps[i + 1] - steps[i] > 10)
                {
                    steps.InsertRange(i + 1, AddStepsRecursive(newStepDistanceAmount, level + 1, steps[i], steps[i + 1], noise));
                }
            }

            steps.RemoveAt(0);
            steps.RemoveAt(steps.Count - 1);

            return steps;
        }
    }
}