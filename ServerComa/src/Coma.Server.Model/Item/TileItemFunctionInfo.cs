using Coma.Common.Map.Item.Items;
using Coma.Server.Model.Item;
using Coma.Server.Model.Item.Fonctions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Coma.Common.Map.Item
{
    public class TileItemFunctionInfo
    {
        private static FunctionInfo[] items;
        private static bool init = false;

        public static FunctionInfo Get(TileItemType type)
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
            items = new FunctionInfo[999];

            //NONE
            type = TileItemType.NONE;
            items[(int)type] = new FunctionInfo();

            //RESSOURCE
            type = TileItemType.RESOURCE_COMMON;
            items[(int)type] = new FunctionInfo();

            //RESSOURCE
            type = TileItemType.BUILD_AREA;
            items[(int)type] = new FunctionInfo();
            items[(int)type].MainFunction = new BuildAreaFunction(10);

            //VIRUS
            type = TileItemType.VIRUS;
            items[(int)type] = new FunctionInfo();
            items[(int)type].MainFunction = new RadianceFunction(5, -30);

            //VIRUS
            type = TileItemType.NIGHTMARE;
            items[(int)type] = new FunctionInfo();
            items[(int)type].MainFunction = new RadianceFunction(10, -20);

            //ORGAN
            type = TileItemType.ORGAN;
            items[(int)type] = new FunctionInfo();
            items[(int)type].MainFunction = new RadianceFunction(20, 50);
            items[(int)type].SynergyFunction = new BuildAreaFunction(20);

            //CORRUPTED ORGAN
            type = TileItemType.CORRUPTED_ORGAN;
            items[(int)type] = new FunctionInfo();
            items[(int)type].MainFunction = new RadianceFunction(20, -50);

            //FEELING
            type = TileItemType.FEELING;
            items[(int)type] = new FunctionInfo();
            items[(int)type].MainFunction = new RadianceFunction(20, 50);
            items[(int)type].SynergyFunction = new BuildAreaFunction(20);

            //CORRUPTED FEELING
            type = TileItemType.CORRUPTED_FEELING;
            items[(int)type] = new FunctionInfo();
            items[(int)type].MainFunction = new RadianceFunction(20, -50);

            //TRUC 2
            type = TileItemType.GENERATOR;
            //items[(int)type] =
        }
    }
}
