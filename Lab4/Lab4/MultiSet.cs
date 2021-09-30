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
            MultiSet emptySet= new MultiSet();
            result.Add(emptySet);
            List<string> thisSetList = new List<string>();
            thisSetList.AddRange(ToList());

            for (int i = 0; i < thisSetList.Count; i++)
            {
                MultiSet tempSet = new MultiSet();
                List<string> powerSet = new List<string>(thisSetList.Count);
                powerSet = thisSetList.GetRange(i, thisSetList.Count - i);

                if (i == 0 || thisSetList[i] != thisSetList[i - 1])
                {
                    powerSetRecursive(result, powerSet);
                }
                else if (thisSetList[i] == thisSetList[i - 1] && i == thisSetList.Count - 1)
                {
                    continue;
                }
                else
                {
                    tempSet.AddRange(powerSet);
                    result.Add(tempSet);
                }
            }

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

        private void powerSetRecursive(List<MultiSet> result, List<string> setList)
        {
            MultiSet tempSet = new MultiSet();
            if (setList.Count == 1)
            {
                tempSet.AddRange(setList);
                result.Add(tempSet);
                return;
            }

            List<string> thisLevelSet = new List<string>();
            thisLevelSet.AddRange(setList);
            setList.RemoveAt(setList.Count - 1);
            powerSetRecursive(result, setList);
            tempSet.AddRange(thisLevelSet);
            result.Add(tempSet);
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