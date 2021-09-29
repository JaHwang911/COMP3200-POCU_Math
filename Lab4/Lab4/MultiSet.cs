using System.Collections.Generic;

namespace Lab4
{
    public sealed class MultiSet
    {
        private List<string> mSet = new List<string>(256);
        private Dictionary<string, uint> mMultiplicity = new Dictionary<string, uint>();

        public void Add(string element)
        {
            if (mSet.Contains(element))
            {
                mMultiplicity[element]++;
            }
            else
            {
                mMultiplicity.Add(element, 1);
            }

            mSet.Add(element);
        }

        public bool Remove(string element)
        {
            if (mMultiplicity.ContainsKey(element))
            {
                mMultiplicity[element]--;
            }

            return mSet.Remove(element);
        }

        public uint GetMultiplicity(string element)
        {
            if (!mMultiplicity.ContainsKey(element))
            {
                return 0;
            }

            return mMultiplicity[element];
        }

        public List<string> ToList()
        {
            mSet.Sort();
            return mSet;
        }

        public MultiSet Union(MultiSet other)
        {
            MultiSet resultSet = new MultiSet();
            List<string> intersectionSet = getIntersectionSet(other);
            List<string> tempThisSet = new List<string>();
            List<string> otherSet = new List<string>();
            List<string> sumSet = new List<string>();
            tempThisSet.AddRange(ToList());
            otherSet.AddRange(other.ToList());
            sumSet.AddRange(tempThisSet);
            sumSet.AddRange(otherSet);
            
            for (int i = 0; i < sumSet.Count; i++)
            {
                if (intersectionSet.Contains(sumSet[i]))
                {
                    intersectionSet.Remove(sumSet[i]);
                }
                else
                {
                    resultSet.Add(sumSet[i]);
                }
            }

            return resultSet;
        }

        public MultiSet Intersect(MultiSet other)
        {
            MultiSet resultSet = new MultiSet();
            List<string> intersectionSet = getIntersectionSet(other);

            foreach (var element in intersectionSet)
            {
                resultSet.Add(element);
            }

            return resultSet;
        }

        public MultiSet Subtract(MultiSet other)
        {
            MultiSet resultSet = new MultiSet();
            List<string> intersectionSet = getIntersectionSet(other);
            
            foreach (var element in mSet)
            {
                if (intersectionSet.Contains(element))
                {
                    continue;
                }
                else
                {
                    resultSet.Add(element);
                }
            }

            return resultSet;
        }

        public void AddRange(List<string> elements)
        {
            mSet.AddRange(elements);
        }

        public List<MultiSet> FindPowerSet()
        {
            List<MultiSet> result = new List<MultiSet>();
            MultiSet emptySet= new MultiSet();
            result.Add(emptySet);
            List<string> tempList = new List<string>();

            foreach (var element in mMultiplicity)
            {
                for (int i = 0; i < element.Value; i++)
                {
                    MultiSet tempSet = new MultiSet();
                    tempList.Add(element.Key);
                    tempSet.AddRange(tempList);
                    result.Add(tempSet);
                }
            }

            for (int i = 0; i < tempList.Count; i++)
            {
                tempList.RemoveAt(0);
                MultiSet tempSet = new MultiSet();
                tempSet.AddRange(tempList);
                result.Add(tempSet);
            }

            return result;
        }

        public bool IsSubsetOf(MultiSet other)
        {
            return false;
        }

        public bool IsSupersetOf(MultiSet other)
        {
            return false;
        }

        private List<string> getIntersectionSet(MultiSet other)
        {
            List<string> intersectionSet = new List<string>();
            List<string> tempThisSet = new List<string>();
            List<string> otherSet = new List<string>();
            tempThisSet.AddRange(ToList());
            otherSet.AddRange(other.ToList());

            foreach (var element in otherSet)
            {
                if (tempThisSet.Contains(element))
                {
                    tempThisSet.Remove(element);
                    intersectionSet.Add(element);
                }
            }

            return intersectionSet;
        }
    }
}