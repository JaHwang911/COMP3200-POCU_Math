using System;
using System.Diagnostics;
using System.Collections.Generic;

namespace Lab4
{
    class Program
    {
        static void Main(string[] args)
        {
            MultiSet set1 = new MultiSet();
            MultiSet set2 = new MultiSet();
            
            set1.Add("d");
            set1.Add("k");
            set1.Add("f");
            set1.Add("e"); // set1: { d, e, f, k }

            set2.Add("g");
            set2.Add("e");
            set2.Add("f"); // set2: { e, f, g }

            List<string> difference = set1.Subtract(set2).ToList(); // difference: { d, k }
            List<string> expectedList = new List<string> { "d", "k"};

            for (int i = 0; i < difference.Count; i++)
            {
                Debug.Assert(difference[i] == expectedList[i]);
            }

            MultiSet set3 = new MultiSet();

            set3.Add("a");
            set3.Add("b");
            set3.Add("a");

            List<MultiSet> powersetList = set3.FindPowerSet();

            Console.WriteLine("No probs");
        }
    }
}
