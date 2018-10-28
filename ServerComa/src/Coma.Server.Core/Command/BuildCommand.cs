using Coma.Server.Core.Command;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Coma.Common;
using Coma.Common.Parameter;
using Coma.Server.Model.Entity;
using Coma.Server.Model;
using Coma.Server.Model.Map;
using Coma.Common.Map.Item;

namespace Coma.Server.Core.Command
{
    [Description("bld")]
    class BuildCommand : BaseCommand<BuildParam>
    {
        public override void RunWithCast(Player player, BuildParam param)
        {
            

            var cellcost = TileItemInfo.Get(param.Id).CostCells;
            var nutrientcost = TileItemInfo.Get(param.Id).CostNutrients;
            var thoughtcost = TileItemInfo.Get(param.Id).CostThoughts;
            var ideacost = TileItemInfo.Get(param.Id).CostIdeas;

            if (GameModel.Instance.Bank.Cells < cellcost)
            {
                return;
            }
            if (GameModel.Instance.Bank.Nutrients < nutrientcost)
            {
                return;
            }
            if (GameModel.Instance.Bank.Thoughts < thoughtcost)
            {
                return;
            }
            if (GameModel.Instance.Bank.Ideas < ideacost)
            {
                return;
            }


            WorldMap map = null;
            if (player.Type == PlayerType.BODY)
            {
                map = GameModel.Instance.BodyMap;
            }

            if (player.Type == PlayerType.SOUL)
            {
                map = GameModel.Instance.SoulMap;
            }

            if (param.Id == TileItemType.NONE)
            {
                switch (map.GetTiles()[param.Position.X, param.Position.Y].Item.ItemType)
                {
                    case TileItemType.GENERATOR_SOUL:
                    case TileItemType.GENERATOR_BODY:
                    case TileItemType.BUILD_AREA_BODY:
                    case TileItemType.BUILD_AREA_SOUL:
                    case TileItemType.HARVESTOR_BODY:
                    case TileItemType.HARVESTOR_SOUL:
                    case TileItemType.RADIANCE_AREA_BODY:
                    case TileItemType.RADIANCE_AREA_SOUL:
                        map.GetTiles()[param.Position.X, param.Position.Y].Item = TileItemInfo.GetClone(param.Id);
                        return;
                    default:
                        return;
                }
            } 
            

            if (map.GetTiles()[param.Position.X, param.Position.Y].Contructable && map.GetTiles()[param.Position.X, param.Position.Y].Item.ItemType == TileItemType.NONE)
            {
                map.GetTiles()[param.Position.X, param.Position.Y].Item = TileItemInfo.GetClone(param.Id);
                GameModel.Instance.Bank.Cells -= cellcost;
                GameModel.Instance.Bank.Nutrients -= nutrientcost;
                GameModel.Instance.Bank.Thoughts -= thoughtcost;
                GameModel.Instance.Bank.Ideas -= ideacost;
            }
        }
    }
}
