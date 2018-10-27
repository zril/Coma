using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Coma.Common.Map.Item.Items
{
    public class BuildAreaItem : TileItem
    {
        public int Radius { get; set; }

        public BuildAreaItem(int radius)
        {
            ItemType = TileItemType.BUILD_AREA;
            Radius = radius;
        }

        public override void FromMessage(string message)
        {
            //rien
        }

        public override string ToMessage()
        {
            return string.Format("{0},{1}", (int)ItemType);
        }

        public override TileItem Clone()
        {
            ResourceItem clone = new ResourceItem(Radius);

            return clone;
        }
    }
}
