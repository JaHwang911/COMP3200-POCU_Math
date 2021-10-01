using System;
using System.Diagnostics;
using System.Collections.Generic;

namespace Lab4
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> list = new List<string>();

            MultiSet set3 = new MultiSet();
            set3.Add("a");
            set3.Add("a");
            set3.Add("b");
            set3.Add("b");
            set3.Add("c");

            MultiSet set4 = new MultiSet();
            set4.Add("a");
            set4.Add("a");
            set4.Add("a");
            set4.Add("a");
            set4.Add("b");

            MultiSet set5 = new MultiSet();
            set5.Add("a");
            set5.Add("a");
            set5.Add("a");
            set5.Add("b");
            set5.Add("b");

            MultiSet set6 = new MultiSet();
            set6.Add("1");
            set6.Add("2");
            set6.Add("3");;

            List<List<string>> testExpected = new List<List<string>>()
            {
                new List<string>() {},
                new List<string>() {"a"},
                new List<string>() {"a", "a"},
                new List<string>() {"a", "a", "b"},
                new List<string>() {"a", "a", "b", "b"},
                new List<string>() {"a", "a", "b", "b", "c"},
                new List<string>() {"a", "a", "b", "c"},
                new List<string>() {"a", "a", "c"},
                new List<string>() {"a", "b"},
                new List<string>() {"a", "b", "b"},
                new List<string>() {"a", "b", "b", "c"},
                new List<string>() {"a", "b", "c"},
                new List<string>() {"a", "c"},
                new List<string>() {"b"},
                new List<string>() {"b", "b"},
                new List<string>() {"b", "b", "c"},
                new List<string>() {"b", "c"},
                new List<string>() {"c"},
            };
            List<MultiSet> set3PowerSet = set3.FindPowerSet();
            Debug.Assert(set3PowerSet.Count == testExpected.Count);

            for (int i = 0; i < testExpected.Count; i++)
            {
                List<string> testList = testExpected[i];
                list = set3PowerSet[i].ToList();

                Debug.Assert(testList.Count == list.Count);

                for (int j = 0; j < testList.Count; j++)
                {
                    Debug.Assert(testList[j] == list[j]);
                }
            }

            testExpected = new List<List<string>>()
            {
                new List<string>() { },
                new List<string>() { "a" },
                new List<string>() { "a", "a" },
                new List<string>() { "a", "a", "a" },
                new List<string>() { "a", "a", "a", "a" },
                new List<string>() { "a", "a", "a", "a", "b" },
                new List<string>() { "a", "a", "a", "b" },
                new List<string>() { "a", "a", "b" },
                new List<string>() { "a", "b" },
                new List<string>() { "b" }
            };
            List<MultiSet> set4PowerSet = set4.FindPowerSet();
            Debug.Assert(set4PowerSet.Count == testExpected.Count);

            for (int i = 0; i < testExpected.Count; i++)
            {
                List<string> testList = testExpected[i];
                list = set4PowerSet[i].ToList();

                Debug.Assert(testList.Count == list.Count);

                for (int j = 0; j < testList.Count; j++)
                {
                    Debug.Assert(testList[j] == list[j]);
                }
            }

            testExpected = new List<List<string>>()
            {
                new List<string>() {},
                new List<string>() {"a"},
                new List<string>() {"a", "a"},
                new List<string>() {"a", "a", "a"},
                new List<string>() {"a", "a", "a", "b"},
                new List<string>() {"a", "a", "a", "b", "b"},
                new List<string>() {"a", "a", "b"},
                new List<string>() {"a", "a", "b", "b"},
                new List<string>() {"a", "b"},
                new List<string>() {"a", "b", "b"},
                new List<string>() {"b"},
                new List<string>() {"b", "b"},
            };
            List<MultiSet> set5PowerSet = set5.FindPowerSet();
            Debug.Assert(set5PowerSet.Count == testExpected.Count);

            for (int i = 0; i < testExpected.Count; i++)
            {
                List<string> testList = testExpected[i];
                list = set5PowerSet[i].ToList();

                Debug.Assert(testList.Count == list.Count);

                for (int j = 0; j < testList.Count; j++)
                {
                    Debug.Assert(testList[j] == list[j]);
                }
            }

            testExpected = new List<List<string>>()
            {
                new List<string>() {},
                new List<string>() {"1"},
                new List<string>() {"1", "2"},
                new List<string>() {"1", "2", "3"},
                new List<string>() {"1", "3"},
                new List<string>() {"2"},
                new List<string>() {"2", "3"},
                new List<string>() {"3"}
            };
            List<MultiSet> set6PowerSet = set6.FindPowerSet();
            Debug.Assert(set6PowerSet.Count == testExpected.Count);

            for (int i = 0; i < testExpected.Count; i++)
            {
                List<string> testList = testExpected[i];
                list = set6PowerSet[i].ToList();

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
