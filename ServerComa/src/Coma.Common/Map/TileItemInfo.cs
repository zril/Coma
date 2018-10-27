using Coma.Common.Map.Item.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Coma.Common.Map.Item
{
    public class TileItemInfo
    {
        private static TileItem[] items;
        private static bool init = false;

        public static TileItem Get(TileItemType type)
        {
            if (!init)
            {
                initInfo();
                init = true;
            }
            return items[(int) type];
        }

        public static TileItem GetClone(TileItemType type)
        {
            if (!init)
            {
                initInfo();
                init = true;
            }
            return items[(int)type].Clone();
        }

        private static void initInfo()
        {
            TileItemType type;
            items = new TileItem[999];

            //NONE
            type = TileItemType.NONE;
            items[(int)type] = new NoItem();

            //RESSOURCE
            type = TileItemType.RESOURCE_COMMON;
            items[(int)type] = new ResourceItem(100);

            //TRUC 2
            type = TileItemType.GENERATOR;
            //items[(int)type] =
        }
    }
}
