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
            for (int j = 0; j < size; j++)
            {
                for (int i = 0; i < size; i++)
                {
                    tiles[i, j] = new Tile();
                    tiles[i, j].Type = TileType.NORMAL;

                    if (j == 0)
                    {
                        tiles[i, j].Item = TileItemInfo.GetClone(TileItemType.RESOURCE_COMMON);
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
