using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Coma.Common.Map.Item.Items
{
    public class ResourceBodyRareItem : TileItem
    {
        public int Capacity {get; set;}
        public int Count { get; set; }

        public ResourceBodyRareItem(int capacity)
        {
            ItemType = TileItemType.RESOURCE_RARE_BODY;
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
            ResourceBodyRareItem clone = new ResourceBodyRareItem(Capacity);
            clone.Capacity = Capacity;
            clone.Count = Count;

            return clone;
        }
    }
}
