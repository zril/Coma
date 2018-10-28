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
            type = TileItemType.RESOURCE_COMMON_BODY;
            items[(int)type] = new FunctionInfo();

            //RESSOURCE
            type = TileItemType.RESOURCE_COMMON_SOUL;
            items[(int)type] = new FunctionInfo();

            //RESSOURCE
            type = TileItemType.RESOURCE_RARE_BODY;
            items[(int)type] = new FunctionInfo();

            //RESSOURCE
            type = TileItemType.RESOURCE_RARE_SOUL;
            items[(int)type] = new FunctionInfo();

            //BUILD AREA
            type = TileItemType.BUILD_AREA_BODY;
            items[(int)type] = new FunctionInfo();
            items[(int)type].MainFunction = new BuildAreaFunction(6);
            
            //GENERATOR
            type = TileItemType.GENERATOR_BODY;
            items[(int)type] = new FunctionInfo();
            items[(int)type].MainFunction = new GeneratorFunction(4, ResourceType.CELLS);
            
            //HARVESTOR
            type = TileItemType.HARVESTOR_BODY;
            items[(int)type] = new FunctionInfo();
            items[(int)type].MainFunction = new HarvestFunction(1, 3);

            //RADIANCE AREA
            type = TileItemType.RADIANCE_AREA_BODY;
            items[(int)type] = new FunctionInfo();
            items[(int)type].MainFunction = new RadianceFunction(8, 40);

            //BUILD AREA
            type = TileItemType.BUILD_AREA_SOUL;
            items[(int)type] = new FunctionInfo();
            items[(int)type].MainFunction = new BuildAreaFunction(11);

            //GENERATOR
            type = TileItemType.GENERATOR_SOUL;
            items[(int)type] = new FunctionInfo();
            items[(int)type].MainFunction = new GeneratorFunction(3, ResourceType.THOUGHTS);

            //HARVESTOR
            type = TileItemType.HARVESTOR_SOUL;
            items[(int)type] = new FunctionInfo();
            items[(int)type].MainFunction = new HarvestFunction(2, 5);

            //RADIANCE AREA
            type = TileItemType.RADIANCE_AREA_SOUL;
            items[(int)type] = new FunctionInfo();
            items[(int)type].MainFunction = new RadianceFunction(5, 25);

            //VIRUS
            type = TileItemType.VIRUS;
            items[(int)type] = new FunctionInfo();
            items[(int)type].MainFunction = new RadianceFunction(5, -30);

            //NIGHTMARE
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
            //type = TileItemType.GENERATOR_BODY;
            //items[(int)type] =
        }
    }
}
