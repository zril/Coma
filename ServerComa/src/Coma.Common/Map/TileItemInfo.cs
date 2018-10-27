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

            //RESSOURCE
            type = TileItemType.BUILD_AREA;
            items[(int)type] = new BuildAreaItem();

            //VIRUS
            type = TileItemType.VIRUS;
            items[(int)type] = new VirusItem();

            //NIGHTMARE
            type = TileItemType.NIGHTMARE;
            items[(int)type] = new NightmareItem();

            //ORGAN
            type = TileItemType.ORGAN;
            items[(int)type] = new NightmareItem();

            //CORRUPTED ORGAN
            type = TileItemType.CORRUPTED_ORGAN;
            items[(int)type] = new NightmareItem();

            //FEELING
            type = TileItemType.FEELING;
            items[(int)type] = new NightmareItem();

            //CORRUPTED FEELING
            type = TileItemType.CORRUPTED_FEELING;
            items[(int)type] = new NightmareItem();

            //TRUC 2
            type = TileItemType.GENERATOR;
            //items[(int)type] =
        }
    }
}
