using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Coma.Common.Map.Item.Items
{
    public class FeelingItem : TileItem
    {
        public FeelingItem()
        {
            ItemType = TileItemType.VIRUS;
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
            FeelingItem clone = new FeelingItem();

            return clone;
        }
    }
}
