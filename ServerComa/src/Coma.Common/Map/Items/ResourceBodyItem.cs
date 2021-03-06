﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Coma.Common.Map.Item.Items
{
    public class ResourceBodyItem : TileItem
    {
        public int Capacity {get; set;}
        public int Count { get; set; }

        public ResourceBodyItem(int capacity)
        {
            ItemType = TileItemType.RESOURCE_COMMON_BODY;
            Capacity = capacity;
            Count = capacity;
        }

        public override void FromMessage(string message)
        {
            string[] splitargs = message.Split(',');

            Count = int.Parse(splitargs[1]);
        }

        public override string ToMessage()
        {
            return string.Format("{0},{1}", (int)ItemType, Count);
        }

        public override TileItem Clone()
        {
            ResourceBodyItem clone = new ResourceBodyItem(Capacity);
            clone.Capacity = Capacity;
            clone.Count = Count;

            return clone;
        }
    }
}
