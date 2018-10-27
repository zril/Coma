using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Coma.Common.Map.Item.Items
{
    public class RadianceAreaBodyItem : TileItem
    {
        public RadianceAreaBodyItem()
        {
            ItemType = TileItemType.RADIANCE_AREA_BODY;
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
            RadianceAreaBodyItem clone = new RadianceAreaBodyItem();

            return clone;
        }
    }
}
