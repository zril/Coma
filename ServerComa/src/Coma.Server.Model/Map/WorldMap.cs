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

        public Position Start { get; set; }

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
            

            //Body
            if (playerType == PlayerType.BODY)
            {

                for (int n = 0; n < 40; n++)
                {
                    Position patchCenter = new Position(random.Next(mapsize), random.Next(mapsize));
                    AddResourcePatch(28, mapsize, patchCenter, TileItemType.RESOURCE_COMMON_BODY);
                }

                for (int n = 0; n < 12; n++)
                {
                    Position patchCenter = new Position(random.Next(mapsize), random.Next(mapsize));
                    AddResourcePatch(12, mapsize, patchCenter, TileItemType.RESOURCE_RARE_BODY);
                }

                for (int n = 0; n < 20; n++)
                {
                    Position patchCenter = new Position(random.Next(mapsize), random.Next(mapsize));
                    AddResourcePatch(40, mapsize, patchCenter, TileItemType.OBSTACLE);
                }

                var startX = 5 + random.Next(mapsize / 2);
                var startY = 5 + random.Next(mapsize / 2);

                tiles[0, 0].Item = TileItemInfo.GetClone(TileItemType.VIRUS);
                tiles[0, mapsize - 1].Item = TileItemInfo.GetClone(TileItemType.VIRUS);
                tiles[mapsize - 1, 0].Item = TileItemInfo.GetClone(TileItemType.VIRUS);
                tiles[mapsize - 1, mapsize - 1].Item = TileItemInfo.GetClone(TileItemType.VIRUS);
                tiles[mapsize/2, 0].Item = TileItemInfo.GetClone(TileItemType.VIRUS);
                tiles[0, mapsize / 2].Item = TileItemInfo.GetClone(TileItemType.VIRUS);
                tiles[mapsize - 1, mapsize / 2].Item = TileItemInfo.GetClone(TileItemType.VIRUS);
                tiles[mapsize / 2, mapsize - 1].Item = TileItemInfo.GetClone(TileItemType.VIRUS);

                tiles[startX, startY].Item = TileItemInfo.GetClone(TileItemType.ORGAN);

                Position center = new Position(startX - 10 + random.Next(20), startY - 10 + random.Next(20));
                AddResourcePatch(20, mapsize, center, TileItemType.RESOURCE_COMMON_BODY);

                var endX = startX + 40;
                var endY = startY + 40;

                tiles[endX, endY].Item = TileItemInfo.GetClone(TileItemType.CORRUPTED_ORGAN);

                Start = new Position(startX, startY);

            }

            //Soul
            if (playerType == PlayerType.SOUL)
            {
                for (int n = 0; n < 20; n++)
                {
                    Position patchCenter = new Position(random.Next(mapsize), random.Next(mapsize));
                    AddResourcePatch(50, mapsize, patchCenter, TileItemType.RESOURCE_COMMON_SOUL);
                }

                for (int n = 0; n < 10; n++)
                {
                    Position patchCenter = new Position(random.Next(mapsize), random.Next(mapsize));
                    AddResourcePatch(12, mapsize, patchCenter, TileItemType.RESOURCE_RARE_SOUL);
                }

                for (int n = 0; n < 10; n++)
                {
                    Position patchCenter = new Position(random.Next(mapsize), random.Next(mapsize));
                    AddResourcePatch(40, mapsize, patchCenter, TileItemType.OBSTACLE);
                }


                var startX = 5 + random.Next(mapsize / 2);
                var startY = 5 + random.Next(mapsize / 2);

                Position center = new Position(startX - 10 + random.Next(20), startY - 10 + random.Next(20));
                AddResourcePatch(20, mapsize, center, TileItemType.RESOURCE_COMMON_SOUL);

                tiles[startX, startY].Item = TileItemInfo.GetClone(TileItemType.FEELING);

                var endX = startX + 40;
                var endY = startY + 40;

                tiles[endX, endY].Item = TileItemInfo.GetClone(TileItemType.CORRUPTED_FEELING);

                Start = new Position(startX, startY);
            }
        }

        public Tile[,] GetTiles()
        {
            return tiles;
        }


        private void AddResourcePatch(int size, int mapsize, Position center, TileItemType tileType)
        {
            var square = tileType == TileItemType.RESOURCE_COMMON_SOUL;
            var mazeness = tileType == TileItemType.RESOURCE_COMMON_SOUL ? 20 : 5;
            var patch = GenUtils.MazeGen(size, mazeness, square, 1, 1, 1, 1);

            foreach(Position pos in patch)
            {
                Position newpos = new Position(center.X + pos.X, center.Y + pos.Y);
                if (newpos.IsInMap(mapsize))
                {
                    tiles[newpos.X, newpos.Y].Item = TileItemInfo.GetClone(tileType);
                }
            }
        }
    }
}
