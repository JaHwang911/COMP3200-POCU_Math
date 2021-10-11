using System;
using System.Diagnostics;
using System.Collections.Generic;

namespace Lab6
{
    public class Recyclebot
    {
        public List<Item> RecycleItems { get; private set; } 
        public List<Item> NonRecycleItems { get; private set; }

        public Recyclebot()
        {
            RecycleItems = new List<Item>();
            NonRecycleItems = new List<Item>();
        }

        public void Add(Item item)
        {
            switch (item.Type)
            {
                case EType.Paper:
                case EType.Furniture:
                case EType.Electronics:
                    if (2.0 <= item.Weight && item.Weight < 5.0)
                    {
                        RecycleItems.Add(item);
                    }
                    else
                    {
                        NonRecycleItems.Add(item);
                    }
                    break;
                case EType.Compost:
                case EType.Glass:
                case EType.Plastic:
                    RecycleItems.Add(item);
                    break;
                default:
                    Debug.Assert(false, "Wrong item type");
                    break;
            }
        }

        public List<Item> Dump()
        {
            List<Item> dumpResult = new List<Item>(NonRecycleItems.Count);

            foreach (var item in NonRecycleItems)
            {
                // 10L, 11L, 15L 는 유독 폐기물이 아니야 10L, 11L, 15L 임에도 불구하고 유독폐기물이라면 가구나 전기 제품 이여야 해
                if (item.Volume != 10 || item.Volume != 11 || item.Volume != 15)
                {
                    if (item.IsToxicWaste || item.Type == EType.Furniture || item.Type == EType.Electronics)
                    {
                        dumpResult.Add(item);
                    }
                }
            }

            return dumpResult;
        }
    }
}
