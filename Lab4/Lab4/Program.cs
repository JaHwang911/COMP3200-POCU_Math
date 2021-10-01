using System;
using System.Diagnostics;
using System.Collections.Generic;

namespace Lab4
{
    class Program
    {
        static void Main(string[] args)
        {
            //MultiSet set = new MultiSet();

            //set.Add("cattle");
            //set.Add("bee");
            //set.Add("cattle");
            //set.Add("bee");
            //set.Add("happy");
            //set.Add("zachariah");

            //Debug.Assert(set.Remove("zachariah"));
            //Debug.Assert(!set.Remove("fun"));

            //Debug.Assert(set.GetMultiplicity("cattle") == 2);

            //List<string> expectedList = new List<string> { "bee", "bee", "cattle", "cattle", "happy" };
            //List<string> list = set.ToList();
            List<string> list = new List<string>();
            //Debug.Assert(list.Count == 5);

            //for (int i = 0; i < expectedList.Count; i++)
            //{
            //    Debug.Assert(expectedList[i] == list[i]);
            //}

            //MultiSet set2 = new MultiSet();

            //set2.Add("cattle");
            //set2.Add("cattle");
            //set2.Add("bee");

            //list = set.Union(set2).ToList();
            //Debug.Assert(list.Count == 5);

            //for (int i = 0; i < expectedList.Count; i++)
            //{
            //    Debug.Assert(expectedList[i] == list[i]);
            //}

            //expectedList = new List<string> { "bee", "cattle", "cattle" };
            //list = set.Intersect(set2).ToList();
            //Debug.Assert(list.Count == 3);

            //for (int i = 0; i < expectedList.Count; i++)
            //{
            //    Debug.Assert(expectedList[i] == list[i]);
            //}

            //expectedList = new List<string> { "bee", "happy" };
            //list = set.Subtract(set2).ToList();
            //Debug.Assert(list.Count == 2);

            //for (int i = 0; i < expectedList.Count; i++)
            //{
            //    Debug.Assert(expectedList[i] == list[i]);
            //}

            //List<MultiSet> expectedPowerset = getExpectedPowerset();
            //List<MultiSet> set2PowerSet = set2.FindPowerSet();
            //Debug.Assert(set2PowerSet.Count == expectedPowerset.Count);

            //for (int i = 0; i < expectedPowerset.Count; i++)
            //{
            //    expectedList = expectedPowerset[i].ToList();
            //    list = set2PowerSet[i].ToList();

            //    Debug.Assert(expectedList.Count == list.Count);

            //    for (int j = 0; j < expectedList.Count; j++)
            //    {
            //        Debug.Assert(expectedList[j] == list[j]);
            //    }
            //}

            //Debug.Assert(!set.IsSubsetOf(set2));
            //Debug.Assert(set.IsSupersetOf(set2));

            MultiSet set3 = new MultiSet();
            set3.Add("a");
            set3.Add("a");
            set3.Add("b");
            set3.Add("b");
            set3.Add("c");

            List<MultiSet> testExpected = testSet();
            List<MultiSet> set3PowerSet = set3.FindPowerSet();
            Debug.Assert(set3PowerSet.Count == testExpected.Count);

            for (int i = 0; i < testExpected.Count; i++)
            {
                List<string> testList = testExpected[i].ToList();
                list = set3PowerSet[i].ToList();

                Debug.Assert(testList.Count == list.Count);

                for (int j = 0; j < testList.Count; j++)
                {
                    Debug.Assert(testList[j] == list[j]);
                }
            }

            Console.WriteLine("No probs");
        }

        private static List<MultiSet> testSet()
        {
            List<MultiSet> testExpected = new List<MultiSet>();
            MultiSet testSet = new MultiSet();
            testExpected.Add(testSet);

            testSet = new MultiSet();
            testSet.Add("b");

            testExpected.Add(testSet);

            testSet = new MultiSet();
            testSet.Add("b");
            testSet.Add("c");

            testExpected.Add(testSet);

            testSet = new MultiSet();
            testSet.Add("b");
            testSet.Add("c");
            testSet.Add("c");

            testExpected.Add(testSet);

            testSet = new MultiSet();
            testSet.Add("b");
            testSet.Add("c");
            testSet.Add("c");
            testSet.Add("d");

            testExpected.Add(testSet);

            testSet = new MultiSet();
            testSet.Add("c");

            testExpected.Add(testSet);

            testSet = new MultiSet();
            testSet.Add("c");
            testSet.Add("c");

            testExpected.Add(testSet);

            testSet = new MultiSet();
            testSet.Add("c");
            testSet.Add("c");
            testSet.Add("d");

            testExpected.Add(testSet);

            testSet = new MultiSet();
            testSet.Add("c");
            testSet.Add("d");

            testExpected.Add(testSet);

            testSet = new MultiSet();
            testSet.Add("d");

            testExpected.Add(testSet);

            return testExpected;
        }

        private static List<MultiSet> getExpectedPowerset()
        {
            List<MultiSet> powerset = new List<MultiSet>();

            MultiSet set = new MultiSet();
            powerset.Add(set);

            set = new MultiSet();
            set.Add("bee");

            powerset.Add(set);

            set = new MultiSet();
            set.Add("bee");
            set.Add("cattle");

            powerset.Add(set);

            set = new MultiSet();
            set.Add("bee");
            set.Add("cattle");
            set.Add("cattle");

            powerset.Add(set);

            set = new MultiSet();
            set.Add("cattle");

            powerset.Add(set);

            set = new MultiSet();
            set.Add("cattle");
            set.Add("cattle");

            powerset.Add(set);

            return powerset;
        }
    }
}
