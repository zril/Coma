using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Coma.Common.Map.Item.Items
{
    public class BuildAreaItem : TileItem
    {
        public BuildAreaItem()
        {
            ItemType = TileItemType.BUILD_AREA;
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
            BuildAreaItem clone = new BuildAreaItem();

            return clone;
        }
    }
}
