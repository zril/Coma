﻿using Coma.Common;
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
    public class VirusModule : BaseModule
    {
        public VirusModule()
            : base(10000)
        { }

        public override void Update(TimeSpan elapsed)
        {
            if (GameModel.Instance.BodyPlayer != null)
            {
                AddVirus(GameModel.Instance.GetMap(PlayerType.BODY));
            }
        }

        private void AddVirus(WorldMap map)
        {
            var random = new Random();
            var virusPosList = new List<Position>();
            for (int j = 0; j < map.GetTiles().GetLength(1); j++)
            {
                for (int i = 0; i < map.GetTiles().GetLength(0); i++)
                {
                    if (map.GetTiles()[i, j].Influence < 0 && map.GetTiles()[i, j].Influence > -10)
                    {
                        virusPosList.Add(new Position(i, j));
                    }
                }
            }
            var viruspos = virusPosList[random.Next(virusPosList.Count)];
            map.GetTiles()[viruspos.X, viruspos.Y].Item = TileItemInfo.GetClone(TileItemType.VIRUS);
        }
    }
}
