using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Lab6
{
    class Program
    {
        static void Main(string[] args)
        {
            Item item1 = new Item(EType.Furniture, 1.99999, 1.245, true);
            Item item2 = new Item(EType.Furniture, 0.8752, 11.23, false);
            Item item3 = new Item(EType.Paper, 5.234, 16.9, false);
            Item item4 = new Item(EType.Paper, 444, 34, true);
            Item item5 = new Item(EType.Electronics, 5.00001, 11, true);
            Item item6 = new Item(EType.Electronics, 6.121, 10, false);

            Recyclebot bot = new Recyclebot();
            List<Item> itemList = new List<Item>
            {
                item1,
                item2,
                item3,
                item4,
                item5,
                item6
            };

            foreach (var item in itemList)
            {
                bot.Add(item);
            }

            List<Item> expectedDumps = new List<Item>
            {
                item1,
                item2,
                item3,
                item4,
                item5,
                item6
            };

            List<Item> dumps = bot.Dump();

            Debug.Assert(dumps.Count == expectedDumps.Count);

            for (int i = 0; i < expectedDumps.Count; i++)
            {
                Debug.Assert(itemEquals(dumps[i], expectedDumps[i]));
            }

            Console.WriteLine("No prob");
        }

        static bool itemEquals(Item item1, Item item2)
        {
            return (
                item1.Type == item2.Type
                && item1.Weight == item2.Weight
                && item1.Volume == item2.Volume
                && item1.IsToxicWaste == item2.IsToxicWaste
            );
        }
    }
}
