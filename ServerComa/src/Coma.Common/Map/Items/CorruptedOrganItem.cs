using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Coma.Common.Map.Item.Items
{
    public class CorruptedOrganItem : TileItem
    {
        public CorruptedOrganItem()
        {
            ItemType = TileItemType.CORRUPTED_ORGAN;
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
            CorruptedOrganItem clone = new CorruptedOrganItem();

            return clone;
        }
    }
}
