using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Coma.Common.Map.Item.Items
{
    public class ResourceSoulItem : TileItem
    {
        public int Capacity {get; set;}
        public int Count { get; set; }

        public ResourceSoulItem(int capacity)
        {
            ItemType = TileItemType.RESOURCE_COMMON_SOUL;
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
            ResourceSoulItem clone = new ResourceSoulItem(Capacity);
            clone.Capacity = Capacity;
            clone.Count = Count;

            return clone;
        }
    }
}
