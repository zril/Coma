using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Coma.Common.Map.Item.Items
{
    public class RadianceAreaSoulItem : TileItem
    {
        public RadianceAreaSoulItem()
        {
            ItemType = TileItemType.RADIANCE_AREA_SOUL;
        }

        public override void FromMessage(string message)
        {
            string[] splitargs = message.Split(',');

            Synergy = int.Parse(splitargs[1]);
        }

        public override string ToMessage()
        {
            return string.Format("{0},{1}", (int)ItemType, Synergy);
        }

        public override TileItem Clone()
        {
            RadianceAreaSoulItem clone = new RadianceAreaSoulItem();

            return clone;
        }
    }
}
