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
            int index = 0;

            tempFrames.AddRange(frames);

            return priority;
        }

        private static void setPriorityRecursive (List<Frame> frames, List<EFeatureFlags> features, ref int index)
        {
            List<Frame> tempItem = new List<Frame>(frames.Count);

            if (features.Count == 0)
            {
                foreach (var item in frames)
                {
                    item.priority = index;
                    index++;
                }

                return;
            }
        }
    }
}
