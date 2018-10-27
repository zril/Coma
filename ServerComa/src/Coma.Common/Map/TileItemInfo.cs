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
            type = TileItemType.RESOURCE_COMMON_BODY;
            items[(int)type] = new ResourceBodyItem(100);

            //RESSOURCE
            type = TileItemType.RESOURCE_COMMON_SOUL;
            items[(int)type] = new ResourceSoulItem(100);

            //RESSOURCE
            type = TileItemType.RESOURCE_RARE_BODY;
            items[(int)type] = new ResourceBodyRareItem(100);

            //RESSOURCE
            type = TileItemType.RESOURCE_RARE_SOUL;
            items[(int)type] = new ResourceSoulRareItem(100);

            //BUILD
            type = TileItemType.BUILD_AREA_BODY;
            items[(int)type] = new BuildAreaBodyItem();

            //BUILD
            type = TileItemType.BUILD_AREA_SOUL;
            items[(int)type] = new BuildAreaSoulItem();

            //GENERATOR
            type = TileItemType.GENERATOR_SOUL;
            items[(int)type] = new GeneratorSoulItem();

            //GENERATOR
            type = TileItemType.GENERATOR_BODY;
            items[(int)type] = new GeneratorBodyItem();

            //RADIANCE
            type = TileItemType.RADIANCE_AREA_BODY;
            items[(int)type] = new RadianceAreaBodyItem();

            //RADIANCE
            type = TileItemType.RADIANCE_AREA_SOUL;
            items[(int)type] = new RadianceAreaSoulItem();

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
            //type = TileItemType.GENERATOR_BODY;
            //items[(int)type] =
        }
    }
}
