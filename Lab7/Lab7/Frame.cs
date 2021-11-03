using System;
namespace Lab7
{
    public class Frame
    {
        public EFeatureFlags Features { get; private set; }
        public uint ID { get; private set; }
        public string Name { get; private set; }
        public int priority { get; set; }

        public Frame(uint id, string name)
        {
            ID = id;
            Name = name;
            Features = EFeatureFlags.Default;
        }

        public void ToggleFeatures (EFeatureFlags features)
        {
            Features ^= features;
        }

        public void TurnOnFeatures(EFeatureFlags features)
        {
            Features |= features;
        }

        public void TurnOffFeatures(EFeatureFlags features)
        {
            features = ~features;
            Features &= features;
        }
    }
}
