using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Coma.Common.Map.Item.Items
{
    public class GeneratorSoulItem : TileItem
    {
        public GeneratorSoulItem()
        {
            ItemType = TileItemType.GENERATOR_SOUL;
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
            GeneratorSoulItem clone = new GeneratorSoulItem();

            return clone;
        }
    }
}
