using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Coma.Common.Map.Item.Items
{
    public class VirusItem : TileItem
    {
        public VirusItem()
        {
            ItemType = TileItemType.VIRUS;
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
            VirusItem clone = new VirusItem();

            return clone;
        }
    }
}
