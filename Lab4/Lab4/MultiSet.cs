using System.Collections.Generic;
using System.Linq;

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
            List<string> otherSetList = new List<string>();
            List<string> sumSet = new List<string>();
            tempThisSet.AddRange(ToList());
            otherSetList.AddRange(other.ToList());
            sumSet.AddRange(tempThisSet);
            sumSet.AddRange(otherSetList);
            
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
                    intersectionSet.Remove(element);
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
            MultiSet emptySet = new MultiSet();
            result.Add(emptySet);
            List<string> thisSetList = new List<string>();
            thisSetList.AddRange(ToList());
            List<List<string>> power = new List<List<string>>();

            powerSetRecursive(power, thisSetList);
            powerSetSort(power);

            return result;
        }

        public bool IsSubsetOf(MultiSet other)
        {
            List<string> intersectionSet = getIntersectionSet(other);
            List<string> thisSetList = new List<string>();
            thisSetList.AddRange(ToList());

            if (intersectionSet.Count != thisSetList.Count)
            {
                return false;
            }

            for (int i = 0; i < intersectionSet.Count; i++)
            {
                if (intersectionSet[i] != thisSetList[i])
                {
                    return false;
                }
            }

            return true;
        }

        public bool IsSupersetOf(MultiSet other)
        {
            List<string> intersectionSet = getIntersectionSet(other);
            List<string> otherSetList = new List<string>();
            otherSetList.AddRange(other.ToList());

            if (intersectionSet.Count != otherSetList.Count)
            {
                return false;
            }

            for (int i = 0; i < intersectionSet.Count; i++)
            {
                if (intersectionSet[i] != otherSetList[i])
                {
                    return false;
                }
            }

            return true;
        }

        private void powerSetSort(List<List<string>> powerSet)
        {
            List<List<string>> sorted = new List<List<string>>(powerSet.Count);
        }

        private void powerSetRecursive(List<List<string>> power, List<string> setList)
        {
            MultiSet tempSet = new MultiSet();
            List<string> tempList = new List<string>();

            if (setList.Count == 0)
            {
                power.Add(tempList);
                return;
            }

            bool bEqualNextElement = false;

            if (setList.Count > 1 && setList[0] == setList[1])
            {
                bEqualNextElement = true;
            }

            string currentElement = setList[0];
            tempList.AddRange(setList.GetRange(1, setList.Count - 1));
            powerSetRecursive(power, tempList);
            List<List<string>> tempPower = new List<List<string>>();

            if (!bEqualNextElement)
            {
                for (int i = 0; i < power.Count; i++)
                {
                    tempList = new List<string>();
                    tempList.Add(currentElement);
                    tempList.AddRange(power[i]);
                    tempPower.Add(tempList);
                }
                power.AddRange(tempPower);
            }
            else
            {
                for (int i = 0; i < power.Count; i++)
                {
                    if (power[i].Count != 0 && currentElement == power[i][0])
                    {
                        tempList = new List<string>();
                        tempList.Add(currentElement);
                        tempList.AddRange(power[i]);
                        tempPower.Add(tempList);
                    }
                }
                power.AddRange(tempPower);
            }
        }

        private List<string> getIntersectionSet(MultiSet other)
        {
            List<string> intersectionSet = new List<string>();
            List<string> tempThisSet = new List<string>();
            List<string> otherSetList = new List<string>();
            tempThisSet.AddRange(ToList());
            otherSetList.AddRange(other.ToList());

            foreach (var element in otherSetList)
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