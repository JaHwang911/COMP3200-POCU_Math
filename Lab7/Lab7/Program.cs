using System;
using System.Collections.Generic;

namespace Lab7
{
    class Program
    {
        static void Main(string[] args)
        {
            Frame frame = new Frame(1, "Ray-ban");
            frame.ToggleFeatures(EFeatureFlags.Men);

            List<Frame> frames = new List<Frame>
            {
                new Frame(1, "Glasses1"),
                new Frame(2, "Glasses2"),
                new Frame(3, "Glasses3"),
                new Frame(4, "Glasses4")
            };

            frames[0].TurnOnFeatures(EFeatureFlags.Men | EFeatureFlags.Women);
            frames[1].TurnOnFeatures(EFeatureFlags.Women | EFeatureFlags.Aviator | EFeatureFlags.Black);
            frames[2].TurnOnFeatures(EFeatureFlags.Rectangle | EFeatureFlags.Black);
            frames[3].TurnOnFeatures(EFeatureFlags.Round | EFeatureFlags.Red);

            List<Frame> filteredFrames = FilterEngine.FilterFrames(frames, EFeatureFlags.Black | EFeatureFlags.Men);
            foreach (var item in filteredFrames)
            {
                Console.WriteLine(item.Name);
            }

            Console.WriteLine("==========");

            List<Frame> filteredOutFrames = FilterEngine.FilterOutFrames(frames, EFeatureFlags.Red | EFeatureFlags.Women);
            foreach (var item in filteredOutFrames)
            {
                Console.WriteLine(item.Name);
            }

            List<Frame> frames1 = new List<Frame>
            {
                new Frame(1, "Glasses 1"),
                new Frame(2, "Glasses 2"),
                new Frame(3, "Glasses 3"),
                new Frame(4, "Glasses 4")
            };

            List<Frame> frames2 = new List<Frame>
            {
                new Frame(1, "Glasses 1"),
                new Frame(5, "Glasses 5"),
                new Frame(6, "Glasses 6"),
                new Frame(3, "Glasses 3")
            };
            List<Frame> intersect = FilterEngine.Intersect(frames1, frames2);
        }
    }
}
