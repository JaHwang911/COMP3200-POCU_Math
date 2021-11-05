using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab7
{
    public static class FilterEngine
    {
        public static List<Frame> FilterFrames(List<Frame> frames, EFeatureFlags features)
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
            List<EFeatureFlags> tempFeatures = new List<EFeatureFlags>(features.Count);
            int index = frames.Count;

            tempFrames.AddRange(frames);
            tempFeatures.AddRange(features);

            setPriorityRecursive(tempFrames, tempFeatures, ref index);

            foreach (var item in frames)
            {
                priority.Add(item.Priority);
            }

            return priority;
        }

        private static void setPriorityRecursive(List<Frame> frames, List<EFeatureFlags> features, ref int index)
        {
            if (features.Count == 0)
            {
                foreach (var item in frames)
                {
                    item.Priority = index;
                    index--;
                }

                return;
            }
            else if (frames.Count == 1)
            {
                frames[0].Priority = index;
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

            if (currentFeatureItems.Count == 0)
            {
                currentFeatureItems.AddRange(tempFrames);
                tempFrames = new List<Frame>();
            }

            tempFeatures.Remove(currentFeature);

            setPriorityRecursive(currentFeatureItems, tempFeatures, ref index);

            if (tempFrames.Count != 0)
            {
                setPriorityRecursive(tempFrames, tempFeatures, ref index);
            }

            return;
        }
    }
}
