﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Coma.Common.Map.Item.Items
{
    public class BuildAreaSoulItem : TileItem
    {
        public BuildAreaSoulItem()
        {
            ItemType = TileItemType.BUILD_AREA_SOUL;
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
            BuildAreaSoulItem clone = new BuildAreaSoulItem();

            return clone;
        }
    }
}
