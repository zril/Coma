using Coma.Common;
using Coma.Common.Map;
using Coma.Common.Map.Item;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coma.Server.Model.Map
{
    public class WorldMap
    {
        private Tile[,] tiles;

        public WorldMap(int size, PlayerType playerType)
        {
            tiles = new Tile[size, size];
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    tiles[i, j] = new Tile();
                    tiles[i, j].Type = TileType.NORMAL;

                    if (i == 0)
                    {
                        tiles[i, j].Item = TileItemInfo.Get(TileItemType.RESOURCE_COMMON);
                    }
                }
            }
        }

        public Tile[,] GetTiles()
        {
            return tiles;
        }
    }
}
