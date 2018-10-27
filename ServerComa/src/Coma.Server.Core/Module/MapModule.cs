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
        public MapModule()
            : base(1000)
        { }

        public override void Update(TimeSpan elapsed)
        {
            if (GameModel.Instance.BodyPlayer != null)
            {
                
                UpdateTiles(PlayerType.BODY);
                MapMessage bodyMessage = new MapMessage(GameModel.Instance.BodyMap.GetTiles());
                GlobalServer.Instance.SendMessage(GameModel.Instance.BodyPlayer.Id, bodyMessage.ToString());
            }

            if (GameModel.Instance.SoulPlayer != null)
            {
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
                    }
                }
            }
        }
    }
}
