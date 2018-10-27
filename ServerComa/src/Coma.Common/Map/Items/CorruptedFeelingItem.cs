using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Coma.Common.Map.Item.Items
{
    public class CorruptedFeelingItem : TileItem
    {
        public CorruptedFeelingItem()
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
            CorruptedFeelingItem clone = new CorruptedFeelingItem();

            return clone;
        }
    }
}
