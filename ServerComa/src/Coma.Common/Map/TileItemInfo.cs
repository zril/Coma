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
            items[(int)type].Fonction = TileItemFonction.NONE;

            //RESSOURCE
            type = TileItemType.RESOURCE_COMMON_BODY;
            items[(int)type] = new ResourceBodyItem(100);
            items[(int)type].Fonction = TileItemFonction.RESOURCE;

            //RESSOURCE
            type = TileItemType.RESOURCE_COMMON_SOUL;
            items[(int)type] = new ResourceSoulItem(100);
            items[(int)type].Fonction = TileItemFonction.RESOURCE;

            //RESSOURCE
            type = TileItemType.RESOURCE_RARE_BODY;
            items[(int)type] = new ResourceBodyRareItem(100);
            items[(int)type].Fonction = TileItemFonction.RESOURCE;

            //RESSOURCE
            type = TileItemType.RESOURCE_RARE_SOUL;
            items[(int)type] = new ResourceSoulRareItem(100);
            items[(int)type].Fonction = TileItemFonction.RESOURCE;

            //BUILD
            type = TileItemType.BUILD_AREA_BODY;
            items[(int)type] = new BuildAreaBodyItem();
            items[(int)type].Fonction = TileItemFonction.BUILD_AREA;
            items[(int)type].MaintenanceCellCostRate = 2;
            items[(int)type].CostCells = 30;

            //HARVEST
            type = TileItemType.HARVESTOR_BODY;
            items[(int)type] = new HarvestorBodyItem();
            items[(int)type].Fonction = TileItemFonction.HARVEST;
            items[(int)type].CostCells = 50;

            //GENERATOR
            type = TileItemType.GENERATOR_BODY;
            items[(int)type] = new GeneratorBodyItem();
            items[(int)type].Fonction = TileItemFonction.GENERATE;
            items[(int)type].CostCells = 50;
            items[(int)type].CostNutrients = 30;

            //RADIANCE
            type = TileItemType.RADIANCE_AREA_BODY;
            items[(int)type] = new RadianceAreaBodyItem();
            items[(int)type].Fonction = TileItemFonction.RADIANCE_AREA;
            items[(int)type].MaintenanceCellCostRate = 1;
            items[(int)type].CostCells = 60;
            items[(int)type].CostNutrients = 20;

            //BUILD
            type = TileItemType.BUILD_AREA_SOUL;
            items[(int)type] = new BuildAreaSoulItem();
            items[(int)type].Fonction = TileItemFonction.BUILD_AREA;
            items[(int)type].MaintenanceThoughtCostRate = 5;
            items[(int)type].CostThoughts = 30;
            items[(int)type].CostIdeas = 15;

            //GENERATOR
            type = TileItemType.GENERATOR_SOUL;
            items[(int)type] = new GeneratorSoulItem();
            items[(int)type].Fonction = TileItemFonction.GENERATE;
            items[(int)type].CostThoughts = 150;

            //HARVEST
            type = TileItemType.HARVESTOR_SOUL;
            items[(int)type] = new HarvestorSoulItem();
            items[(int)type].Fonction = TileItemFonction.HARVEST;
            items[(int)type].CostThoughts = 30;
            items[(int)type].CostIdeas = 10;

            //RADIANCE
            type = TileItemType.RADIANCE_AREA_SOUL;
            items[(int)type] = new RadianceAreaSoulItem();
            items[(int)type].Fonction = TileItemFonction.RADIANCE_AREA;
            items[(int)type].MaintenanceThoughtCostRate = 3;
            items[(int)type].CostThoughts = 40;

            //VIRUS
            type = TileItemType.VIRUS;
            items[(int)type] = new VirusItem();
            items[(int)type].Fonction = TileItemFonction.HOSTILE;

            //NIGHTMARE
            type = TileItemType.NIGHTMARE;
            items[(int)type] = new NightmareItem();
            items[(int)type].Fonction = TileItemFonction.HOSTILE;

            //ORGAN
            type = TileItemType.ORGAN;
            items[(int)type] = new NightmareItem();
            items[(int)type].Fonction = TileItemFonction.POI;

            //CORRUPTED ORGAN
            type = TileItemType.CORRUPTED_ORGAN;
            items[(int)type] = new CorruptedOrganItem();
            items[(int)type].Fonction = TileItemFonction.POI;

            //FEELING
            type = TileItemType.FEELING;
            items[(int)type] = new FeelingItem();
            items[(int)type].Fonction = TileItemFonction.POI;

            //CORRUPTED FEELING
            type = TileItemType.CORRUPTED_FEELING;
            items[(int)type] = new CorruptedFeelingItem();
            items[(int)type].Fonction = TileItemFonction.POI;

            //TRUC 2
            //type = TileItemType.GENERATOR_BODY;
            //items[(int)type] =
        }
    }
}
