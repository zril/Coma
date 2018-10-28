using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Coma.Common.Map.Item.Items
{
    public class HarvestorBodyItem : TileItem
    {
        public HarvestorBodyItem()
        {
            ItemType = TileItemType.HARVESTOR_BODY;
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
            HarvestorBodyItem clone = new HarvestorBodyItem();

            return clone;
        }
    }
}
