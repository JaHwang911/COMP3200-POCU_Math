using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab7
{
    public static class FilterEngine
    {
        public static List<Frame> FilterFrames (List<Frame> frames, EFeatureFlags features)
        {
            List<Frame> filtered = new List<Frame>(frames.Count);

            foreach (var item in frames)
            {
                if ((features & item.Features) != 0)
                {
                    filtered.Add(item);
                }
            }

            return filtered;
        }

        public static List<Frame> FilterOutFrames(List<Frame> frames, EFeatureFlags features)
        {
            List<Frame> filteredOut = new List<Frame>(frames.Count);

            foreach (var item in frames)
            {
                if ((features & item.Features) == 0)
                {
                    filteredOut.Add(item);
                }
            }

            return filteredOut;
        }

        public static List<Frame> Intersect(List<Frame> frames1, List<Frame> frames2)
        {
            List<Frame> filtered = new List<Frame>(frames1.Count + frames2.Count);

            foreach (var item1 in frames1)
            {
                Frame sameFrame = frames2.Where(i => i.ID == item1.ID).FirstOrDefault();

                if (sameFrame != null)
                {
                    filtered.Add(sameFrame);
                }
            }

            return filtered;
        }
        
        public static List<int> GetSortKeys(List<Frame> frames, List<EFeatureFlags> features)
        {
            List<int> priority = new List<int>(frames.Count);
            List<Frame> tempFrames = new List<Frame>(frames.Count);
            int index = frames.Count;

            tempFrames.AddRange(frames);

            // 먼저 우선 순위로만 나눔
            for (int i = 0; i < features.Count; i++)
            {
                List<Frame> currentFeatureItems = new List<Frame>(frames.Count);

                for (int j = 0; j < tempFrames.Count; j++)
                {
                    if ((tempFrames[j].Features & features[i]) != 0)
                    {
                        currentFeatureItems.Add(tempFrames[j]);
                        tempFrames.RemoveAt(j);
                        j--;
                    }
                }

                List<EFeatureFlags> tempFeatures = new List<EFeatureFlags>(features.Count);
                tempFeatures.AddRange(features.GetRange(i + 1, features.Count - 1 - i));

                if (currentFeatureItems.Count != 0)
                {
                    setPriorityRecursive(currentFeatureItems, tempFeatures, ref index);
                }

                if (i == features.Count - 1 && tempFrames.Count > 0)
                {
                    currentFeatureItems = new List<Frame>(frames.Count);
                    currentFeatureItems.AddRange(tempFrames);
                    setPriorityRecursive(currentFeatureItems, tempFeatures, ref index);
                }
            }

            foreach (var item in frames)
            {
                priority.Add(item.priority);
            }

            return priority;
        }

        private static void setPriorityRecursive (List<Frame> frames, List<EFeatureFlags> features, ref int index)
        {
            if (features.Count == 0)
            {
                foreach (var item in frames)
                {
                    item.priority = index;
                    index--;
                }

                return;
            }
            else if (frames.Count == 1)
            {
                frames[0].priority = index;
                index--;
                return;
            }

            List<Frame> tempFrames = new List<Frame>(frames.Count);
            List<Frame> currentFeatureItems = new List<Frame>(frames.Count);
            List<EFeatureFlags> tempFeatures = new List<EFeatureFlags>(features.Count);

            tempFrames.AddRange(frames);
            tempFeatures.AddRange(features);
            EFeatureFlags currentFeature = tempFeatures[0];

            for (int i = 0; i < tempFrames.Count; i++)
            {
                if ((tempFrames[i].Features & currentFeature) != 0)
                {
                    currentFeatureItems.Add(tempFrames[i]);
                    tempFrames.Remove(tempFrames[i]);
                    i--;
                }
            }

            tempFeatures.Remove(currentFeature);

            if (currentFeatureItems.Count == 0)
            {
                currentFeatureItems.AddRange(tempFrames);
            }

            setPriorityRecursive(currentFeatureItems, tempFeatures, ref index);

            foreach (var item in tempFrames)
            {
                item.priority = index;
                index--;
            }

            return;
        }
    }
}
