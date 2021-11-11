using System.Collections.Generic;

namespace Assignment3
{
    public static class StepMaker
    {
        public static List<int> MakeSteps(int[] steps, INoise noise)
        {
            List<int> resultSteps = new List<int>();
            double[] newStepDistanceAmout = new double[4];
            
            resultSteps.AddRange(steps);

            newStepDistanceAmout[0] = 0.2;
            newStepDistanceAmout[1] = 0.4;
            newStepDistanceAmout[2] = 0.6;
            newStepDistanceAmout[3] = 0.8;

            for (int i = 0; i < resultSteps.Count - 1; i++)
            {
                int difference = resultSteps[i + 1] - resultSteps[i] > 0 ? resultSteps[i + 1] - resultSteps[i] : (resultSteps[i + 1] - resultSteps[i]) * -1;

                if (difference > 10)
                {
                    resultSteps.InsertRange(i + 1, AddFourStepsRecursive(newStepDistanceAmout, 0, resultSteps[i], resultSteps[i + 1], noise));
                }
            }

            return resultSteps;
        }

        public static List<int> AddFourStepsRecursive(double[] amount, int level, int min, int max, INoise noise)
        {
            List<int> steps = new List<int>();

            steps.Add(min);

            for (int i = 0; i < amount.Length; i++)
            {
                int newStep = (int)(min + (max - min) * amount[i]);
                //int newStep = (int)(min * amount[amount.Length - 1 - i] + max * amount[i]);
                steps.Add(newStep + noise.GetNext(level));
            }

            steps.Add(max);

            for (int i = 0; i < steps.Count - 1; i++)
            {
                int difference = steps[i + 1] - steps[i] > 0 ? steps[i + 1] - steps[i] : (steps[i + 1] - steps[i]) * -1;

                if (difference > 10)
                {
                    steps.InsertRange(i + 1, AddFourStepsRecursive(amount, level + 1, steps[i], steps[i + 1], noise));
                }
            }

            steps.RemoveAt(0);
            steps.RemoveAt(steps.Count - 1);

            return steps;
        }
    }
}