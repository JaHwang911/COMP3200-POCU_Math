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
            List<List<string>> powerSetList = new List<List<string>>();
            List<string> thisSetList = new List<string>();
            thisSetList.AddRange(ToList());
            int addIndex = 0;

            powerSetRecursive(powerSetList, thisSetList, ref addIndex);
            powerSetList = powerSetSort(powerSetList);

            for (int i = 0; i < powerSetList.Count; i++)
            {
                MultiSet tempSet = new MultiSet();
                for (int j = 0; j < powerSetList[i].Count; j++)
                {
                    tempSet.Add(powerSetList[i][j]);
                }
                result.Add(tempSet);
            }

            result[0] = new MultiSet();

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

        private List<List<string>> powerSetSort(List<List<string>> powerSetList)
        {
            List<string> combineList = new List<string>();
            List<List<string>> result = new List<List<string>>();

            for (int i = 0; i < powerSetList.Count; i++)
            {
                string elements = string.Join("|", powerSetList[i]);
                combineList.Add(elements);
            }
            var sorted = combineList.OrderBy(e => e);

            foreach (var element in sorted)
            {
                var convertedArray = element.Split('|');
                result.Add(convertedArray.ToList());
            }

            return result;
        }

        private void powerSetRecursive(List<List<string>> powerSetList, List<string> thisSetList, ref int addIndex)
        {
            List<string> tempList = new List<string>();

            if (thisSetList.Count == 0)
            {
                powerSetList.Add(tempList);
                return;
            }

            bool bEqualNextElement = false;

            if (thisSetList.Count > 1 && thisSetList[0] == thisSetList[1])
            {
                bEqualNextElement = true;
            }

            string currentElement = thisSetList[0];
            tempList.AddRange(thisSetList.GetRange(1, thisSetList.Count - 1));
            powerSetRecursive(powerSetList, tempList, ref addIndex);
            List<List<string>> tempPower = new List<List<string>>();
            int startIndex = 0;

            if (bEqualNextElement)
            {
                startIndex = addIndex;
            }

            for (int i = startIndex; i < powerSetList.Count; i++)
            {
                tempList = new List<string>();
                tempList.Add(currentElement);
                tempList.AddRange(powerSetList[i]);
                tempPower.Add(tempList);
            }

            addIndex = powerSetList.Count;
            powerSetList.AddRange(tempPower);
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