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

        public WorldMap(int mapsize, PlayerType playerType)
        {
            Random random = new Random();

            tiles = new Tile[mapsize, mapsize];
            for (int j = 0; j < mapsize; j++)
            {
                for (int i = 0; i < mapsize; i++)
                {
                    tiles[i, j] = new Tile();
                    tiles[i, j].Type = TileType.NORMAL;
                }
            }

            for (int n = 0; n < 8; n++)
            {
                AddResourcePatch(30, mapsize, random);
            }



            //Body
            if (playerType == PlayerType.BODY)
            {

                var startX = random.Next(mapsize / 4);
                var startY = random.Next(mapsize / 4);

                tiles[0, 0].Item = TileItemInfo.GetClone(TileItemType.VIRUS);
                tiles[0, mapsize - 1].Item = TileItemInfo.GetClone(TileItemType.VIRUS);
                tiles[mapsize - 1, 0].Item = TileItemInfo.GetClone(TileItemType.VIRUS);
                tiles[mapsize - 1, mapsize - 1].Item = TileItemInfo.GetClone(TileItemType.VIRUS);
                
                tiles[startX, startY].Item = TileItemInfo.GetClone(TileItemType.ORGAN);

                var endX = startX + 50;
                var endY = startY + 50;

                tiles[endX, endY].Item = TileItemInfo.GetClone(TileItemType.CORRUPTED_ORGAN);
            }

            //Soul
            if (playerType == PlayerType.SOUL)
            {
                var startX = random.Next(mapsize / 4);
                var startY = random.Next(mapsize / 4);

                tiles[startX, startY].Item = TileItemInfo.GetClone(TileItemType.FEELING);

                var endX = startX + 50;
                var endY = startY + 50;

                tiles[endX, endY].Item = TileItemInfo.GetClone(TileItemType.CORRUPTED_FEELING);
            }
        }

        public Tile[,] GetTiles()
        {
            return tiles;
        }


        private void AddResourcePatch(int size, int mapsize, Random random)
        {
            var patch = GenUtils.MazeGen(size, 5, false, 1, 1, 1, 1);

            Position center = new Position(random.Next(mapsize), random.Next(mapsize));

            foreach(Position pos in patch)
            {
                Position newpos = new Position(center.X + pos.X, center.Y + pos.Y);
                if (newpos.IsInMap(mapsize))
                {
                    tiles[newpos.X, newpos.Y].Item = TileItemInfo.GetClone(TileItemType.RESOURCE_COMMON);
                }
            }
        }
    }
}
