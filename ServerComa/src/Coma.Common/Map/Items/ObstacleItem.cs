using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Coma.Common.Map.Item.Items
{
    public class ObstacleItem : TileItem
    {

        public ObstacleItem()
        {
            ItemType = TileItemType.OBSTACLE;
        }

        public override void FromMessage(string message)
        {
            //rien
        }

        public override string ToMessage()
        {
            return string.Format("{0}", (int)ItemType);
        }

        public override TileItem Clone()
        {
            ObstacleItem clone = new ObstacleItem();
            clone.ItemType = ItemType;

            return clone;
        }
    }
}
