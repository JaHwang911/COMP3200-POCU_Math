using System.Collections.Generic;

namespace Lab4
{
    public sealed class MultiSet
    {
        private List<string> set = new List<string>(256);
        private Dictionary<string, uint> multiplicity = new Dictionary<string, uint>();

        public void Add(string element)
        {
            if (set.Contains(element))
            {
                multiplicity[element]++;
            }
            else
            {
                multiplicity.Add(element, 1);
            }

            set.Add(element);
        }

        public bool Remove(string element)
        {
            if (multiplicity.ContainsKey(element))
            {
                multiplicity[element]--;
            }

            return set.Remove(element);
        }

        public uint GetMultiplicity(string element)
        {
            if (!multiplicity.ContainsKey(element))
            {
                return 0;
            }

            return multiplicity[element];
        }

        public List<string> ToList()
        {
            set.Sort();
            return set;
        }

        public MultiSet Union(MultiSet other)
        {
            List<string> otherList = other.ToList();
            MultiSet resultSet = new MultiSet();

            return null;
        }

        public MultiSet Intersect(MultiSet other)
        {
            return null;
        }

        public MultiSet Subtract(MultiSet other)
        {
            return null;
        }

        public List<MultiSet> FindPowerSet()
        {
            return null;
        }

        public bool IsSubsetOf(MultiSet other)
        {
            return false;
        }

        public bool IsSupersetOf(MultiSet other)
        {
            return false;
        }
    }
}