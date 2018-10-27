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
    public class NightmareModule : BaseModule
    {
        public NightmareModule()
            : base(15000)
        { }

        public override void Update(TimeSpan elapsed)
        {
            if (GameModel.Instance.SoulPlayer != null)
            {
                AddNightmare(GameModel.Instance.GetMap(PlayerType.SOUL));
            }
        }

        private void AddNightmare(WorldMap map)
        {
            var random = new Random();
            var nightmarePosList = new List<Position>();
            for (int j = 0; j < map.GetTiles().GetLength(1); j++)
            {
                for (int i = 0; i < map.GetTiles().GetLength(0); i++)
                {
                    if (map.GetTiles()[i, j].Influence == 0)
                    {
                        nightmarePosList.Add(new Position(i, j));
                    }
                }
            }
            var nightmarepos = nightmarePosList[random.Next(nightmarePosList.Count)];
            map.GetTiles()[nightmarepos.X, nightmarepos.Y].Item = TileItemInfo.GetClone(TileItemType.NIGHTMARE);
        }
    }
}
