using System;
using System.Collections.Generic;

namespace Lab4
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            MultiSet set = new MultiSet();
            set.Add("a");
            set.Add("b");
            set.Add("c");
            set.Add("a");
            set.Add("a");

            uint mul = set.GetMultiplicity("a");
            Console.WriteLine(mul);

            bool tryRemove = set.Remove("a");
            Console.WriteLine(tryRemove);

            mul = set.GetMultiplicity("a");
            Console.WriteLine(mul);

            tryRemove = set.Remove("d");
            Console.WriteLine(tryRemove);

            MultiSet set1 = new MultiSet();

            set1.Add("apple");
            set1.Add("watermelon");
            set1.Add("plum");
            set1.Add("apple");

            List<string> list = set1.ToList();
            Console.WriteLine(list);
        }
    }
}
