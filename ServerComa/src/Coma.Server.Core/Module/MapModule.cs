using Coma.Common;
using Coma.Common.Map.Item;
using Coma.Common.Message;
using Coma.Server.Model;
using Coma.Server.Model.Item;
using Coma.Server.Model.Map;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coma.Server.Core.Module
{
    public class MapModule : BaseModule
    {
        private bool bodyInit = false;
        private bool soulInit = false;

        public MapModule()
            : base(1000)
        { }

        public override void Update(TimeSpan elapsed)
        {
            if (GameModel.Instance.BodyPlayer != null)
            {
                if (!bodyInit)
                {
                    var camMessage = new CameraMessage(GameModel.Instance.BodyMap.Start.X, GameModel.Instance.BodyMap.Start.Y);
                    GlobalServer.Instance.SendMessage(GameModel.Instance.BodyPlayer.Id, camMessage.ToString());
                    bodyInit = true;
                }

                UpdateTiles(PlayerType.BODY);
                MapMessage bodyMessage = new MapMessage(GameModel.Instance.BodyMap.GetTiles());
                GlobalServer.Instance.SendMessage(GameModel.Instance.BodyPlayer.Id, bodyMessage.ToString());
            }

            if (GameModel.Instance.SoulPlayer != null)
            {
                if (!soulInit)
                {
                    var camMessage = new CameraMessage(GameModel.Instance.SoulMap.Start.X, GameModel.Instance.SoulMap.Start.Y);
                    GlobalServer.Instance.SendMessage(GameModel.Instance.SoulPlayer.Id, camMessage.ToString());
                    soulInit = true;
                }

                UpdateTiles(PlayerType.SOUL);
                MapMessage soulMessage = new MapMessage(GameModel.Instance.SoulMap.GetTiles());
                GlobalServer.Instance.SendMessage(GameModel.Instance.SoulPlayer.Id, soulMessage.ToString());
            }
        }

        private void UpdateTiles(PlayerType mapType)
        {
            var map = GameModel.Instance.GetMap(mapType);

            //reset
            for (int j = 0; j < map.GetTiles().GetLength(1); j++)
            {
                for (int i = 0; i < map.GetTiles().GetLength(0); i++)
                {
                    map.GetTiles()[i, j].Influence = 0;
                    map.GetTiles()[i, j].Contructable = false;
                }
            }

            //execution fonctions
            for (int j = 0; j < map.GetTiles().GetLength(1); j++)
            {
                for (int i = 0; i < map.GetTiles().GetLength(0); i++)
                {
                    var tmppos = new Position(i, j);
                    if (tmppos.IsInMap(map.GetTiles().GetLength(1)))
                    {
                        FunctionInfo fonctions = TileItemFunctionInfo.Get(map.GetTiles()[i, j].Item.ItemType);
                        fonctions.MainFunction.Execute(mapType, tmppos);

                        //todo condition
                        fonctions.SynergyFunction.Execute(mapType, tmppos);

                        //maintenance
                        GameModel.Instance.Bank.Cells -= map.GetTiles()[i, j].Item.MaintenanceCellCostRate;
                        GameModel.Instance.Bank.Thoughts -= map.GetTiles()[i, j].Item.MaintenanceThoughtCostRate;
                    }
                }
            }

            for (int j = 0; j < map.GetTiles().GetLength(1); j++)
            {
                for (int i = 0; i < map.GetTiles().GetLength(0); i++)
                {
                    if (map.GetTiles()[i, j].Influence < 0)
                    {
                        switch (map.GetTiles()[i, j].Item.ItemType)
                        {
                            case TileItemType.GENERATOR_SOUL:
                            case TileItemType.GENERATOR_BODY:
                            case TileItemType.HARVESTOR_BODY:
                            case TileItemType.HARVESTOR_SOUL:
                            case TileItemType.BUILD_AREA_BODY:
                            case TileItemType.BUILD_AREA_SOUL:
                            case TileItemType.RADIANCE_AREA_BODY:
                            case TileItemType.RADIANCE_AREA_SOUL:
                                map.GetTiles()[i, j].Item = TileItemInfo.GetClone(TileItemType.NONE);
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
        }
    }
}
