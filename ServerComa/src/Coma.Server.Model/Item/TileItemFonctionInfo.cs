using Coma.Common.Map.Item.Items;
using Coma.Server.Model.Item;
using Coma.Server.Model.Item.Fonctions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Coma.Common.Map.Item
{
    public class TileItemFonctionInfo
    {
        private static FonctionInfo[] items;
        private static bool init = false;

        public static FonctionInfo Get(TileItemType type)
        {
            if (!init)
            {
                initInfo();
                init = true;
            }
            return items[(int) type];
        }

        private static void initInfo()
        {
            TileItemType type;
            items = new FonctionInfo[999];

            //NONE
            type = TileItemType.NONE;
            items[(int)type] = new FonctionInfo();

            //RESSOURCE
            type = TileItemType.RESOURCE_COMMON;
            items[(int)type] = new FonctionInfo();

            //RESSOURCE
            type = TileItemType.BUILD_AREA;
            items[(int)type] = new FonctionInfo();

            //VIRUS
            type = TileItemType.VIRUS;
            items[(int)type] = new FonctionInfo();
            items[(int)type].MainFonction = new RadianceFonction(5, -30);

            //TRUC 2
            type = TileItemType.GENERATOR;
            //items[(int)type] =
        }
    }
}
