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

            //NONE
            type = TileItemType.OBSTACLE;
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
            items[(int)type].SynergyFunction = new RadianceFunction(7, 7, true);
            items[(int)type].SynergyTrigger = TileItemType.GENERATOR_BODY;

            //GENERATOR
            type = TileItemType.GENERATOR_BODY;
            items[(int)type] = new FunctionInfo();
            items[(int)type].MainFunction = new GeneratorFunction(4, ResourceType.CELLS);
            items[(int)type].SynergyFunction = new GeneratorFunction(1, ResourceType.CELLS);
            items[(int)type].SynergyTrigger = TileItemType.HARVESTOR_BODY;

            //HARVESTOR
            type = TileItemType.HARVESTOR_BODY;
            items[(int)type] = new FunctionInfo();
            items[(int)type].MainFunction = new HarvestFunction(1, 4);

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
            items[(int)type].SecondaryFunction = new GeneratorFunction(1, ResourceType.IDEAS);
            items[(int)type].SynergyFunction = new RadianceFunction(9, 9, true);
            items[(int)type].SynergyTrigger = TileItemType.RADIANCE_AREA_SOUL;

            //HARVESTOR
            type = TileItemType.HARVESTOR_SOUL;
            items[(int)type] = new FunctionInfo();
            items[(int)type].MainFunction = new HarvestFunction(2, 2);

            //RADIANCE AREA
            type = TileItemType.RADIANCE_AREA_SOUL;
            items[(int)type] = new FunctionInfo();
            items[(int)type].MainFunction = new RadianceFunction(5, 35);
            items[(int)type].SynergyFunction = new RadianceFunction(10, 5);
            items[(int)type].SynergyTrigger = TileItemType.BUILD_AREA_SOUL;

            //VIRUS
            type = TileItemType.VIRUS;
            items[(int)type] = new FunctionInfo();
            items[(int)type].MainFunction = new RadianceFunction(5, -30);
            items[(int)type].SecondaryFunction = new RadianceFunction(5, -8, true);

            //NIGHTMARE
            type = TileItemType.NIGHTMARE;
            items[(int)type] = new FunctionInfo();
            items[(int)type].MainFunction = new RadianceFunction(10, -20);
            items[(int)type].SecondaryFunction = new RadianceFunction(10, -5, true);

            //ORGAN
            type = TileItemType.ORGAN;
            items[(int)type] = new FunctionInfo();
            items[(int)type].MainFunction = new RadianceFunction(15, 80);
            items[(int)type].SecondaryFunction = new BuildAreaFunction(20);

            //CORRUPTED ORGAN
            type = TileItemType.CORRUPTED_ORGAN;
            items[(int)type] = new FunctionInfo();
            items[(int)type].MainFunction = new RadianceFunction(20, -60);

            //FEELING
            type = TileItemType.FEELING;
            items[(int)type] = new FunctionInfo();
            items[(int)type].MainFunction = new RadianceFunction(15, 80);
            items[(int)type].SecondaryFunction = new BuildAreaFunction(20);

            //CORRUPTED FEELING
            type = TileItemType.CORRUPTED_FEELING;
            items[(int)type] = new FunctionInfo();
            items[(int)type].MainFunction = new RadianceFunction(18, -75);

            //TRUC 2
            //type = TileItemType.GENERATOR_BODY;
            //items[(int)type] =
        }
    }
}
