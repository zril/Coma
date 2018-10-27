﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Coma.Common.Map.Item.Items
{
    public class ResourceItem : TileItem
    {
        public int Capacity {get; set;}
        public int Count { get; set; }

        public ResourceItem(int capacity)
        {
            ItemType = TileItemType.RESOURCE_COMMON;
            Capacity = capacity;
            Count = capacity;
        }

        public override void FromMessage(string message)
        {
            string[] splitargs = message.Split(',');
            
            Synergy = int.Parse(splitargs[1]);
            Count = int.Parse(splitargs[2]);
        }

        public override string ToMessage()
        {
            return string.Format("{0},{1},{2}", (int)ItemType, Synergy, Count);
        }
    }
}
