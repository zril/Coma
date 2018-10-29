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

        Random random;

        public WorldMap(int mapsize, PlayerType playerType)
        {
            random = new Random();

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

                //tiles

                for (int n = 0; n < 15; n++)
                {
                    Position patchCenter = new Position(random.Next(mapsize), random.Next(mapsize));
                    for (int k = 0; k < 8; k++)
                    {
                        patchCenter = AddResourcePatch(40, mapsize, patchCenter, TileItemType.OBSTACLE);
                    }
                }

                for (int n = 0; n < 25; n++)
                {
                    Position patchCenter = new Position(random.Next(mapsize), random.Next(mapsize));
                    for (int k = 0; k < 3; k++)
                    {
                        AddResourcePatch(20, mapsize, patchCenter, TileItemType.RESOURCE_COMMON_BODY);
                    }
                }

                for (int n = 0; n < 10; n++)
                {
                    Position patchCenter = new Position(random.Next(mapsize), random.Next(mapsize));
                    AddResourcePatch(18, mapsize, patchCenter, TileItemType.RESOURCE_RARE_BODY);
                }


                //start
                var startX = 10 + random.Next(mapsize / 4);
                var startY = 10 + random.Next(mapsize / 4);

                /*tiles[0, 0].Item = TileItemInfo.GetClone(TileItemType.VIRUS);
                tiles[0, mapsize - 1].Item = TileItemInfo.GetClone(TileItemType.VIRUS);
                tiles[mapsize - 1, 0].Item = TileItemInfo.GetClone(TileItemType.VIRUS);
                tiles[mapsize - 1, mapsize - 1].Item = TileItemInfo.GetClone(TileItemType.VIRUS);
                tiles[mapsize/2, 0].Item = TileItemInfo.GetClone(TileItemType.VIRUS);
                tiles[0, mapsize / 2].Item = TileItemInfo.GetClone(TileItemType.VIRUS);
                tiles[mapsize - 1, mapsize / 2].Item = TileItemInfo.GetClone(TileItemType.VIRUS);
                tiles[mapsize / 2, mapsize - 1].Item = TileItemInfo.GetClone(TileItemType.VIRUS);*/


                Position startResource1 = new Position(startX - 10 + random.Next(20), startY - 10 + random.Next(20));
                AddResourcePatch(10, mapsize, startResource1, TileItemType.RESOURCE_COMMON_BODY);
                Position startResource2 = new Position(startX - 10 + random.Next(20), startY - 10 + random.Next(20));
                AddResourcePatch(5, mapsize, startResource1, TileItemType.RESOURCE_RARE_BODY);

                tiles[startX, startY].Item = TileItemInfo.GetClone(TileItemType.ORGAN);
                ClearObstacle(10, mapsize, new Position(startX, startY));


                //objective
                var endX = startX + 5 + random.Next(10);
                var endY = startY + 50 + random.Next(10);

                tiles[endX, endY].Item = TileItemInfo.GetClone(TileItemType.CORRUPTED_ORGAN);
                ClearObstacle(10, mapsize, new Position(endX, endY));

                endX = startX + 50 + random.Next(10);
                endY = startY + 5 + random.Next(10);

                tiles[endX, endY].Item = TileItemInfo.GetClone(TileItemType.CORRUPTED_ORGAN);
                ClearObstacle(10, mapsize, new Position(endX, endY));

                Start = new Position(startX, startY);

            }

            //Soul
            if (playerType == PlayerType.SOUL)
            {


                //tiles
                for (int n = 0; n < 50; n++)
                {
                    Position patchCenter = new Position(random.Next(mapsize), random.Next(mapsize));
                    AddResourcePatch(35, mapsize, patchCenter, TileItemType.RESOURCE_COMMON_SOUL);
                }

                for (int n = 0; n < 15; n++)
                {
                    Position patchCenter = new Position(random.Next(mapsize), random.Next(mapsize));
                    for (int k = 0; k < 4; k++)
                    {
                        patchCenter = AddResourcePatch(40, mapsize, patchCenter, TileItemType.OBSTACLE);
                    }
                }

                for (int n = 0; n < 20; n++)
                {
                    Position patchCenter = new Position(random.Next(mapsize), random.Next(mapsize));
                    AddResourcePatch(10, mapsize, patchCenter, TileItemType.RESOURCE_RARE_SOUL);
                }

                //Start
                var startX = 10 + random.Next(mapsize / 4);
                var startY = 10 + random.Next(mapsize / 4);

                Position startResource1 = new Position(startX - 10 + random.Next(20), startY - 10 + random.Next(20));
                AddResourcePatch(10, mapsize, startResource1, TileItemType.RESOURCE_COMMON_SOUL);
                Position startResource2 = new Position(startX - 10 + random.Next(20), startY - 10 + random.Next(20));
                AddResourcePatch(5, mapsize, startResource1, TileItemType.RESOURCE_RARE_SOUL);

                tiles[startX, startY].Item = TileItemInfo.GetClone(TileItemType.FEELING);
                ClearObstacle(10, mapsize, new Position(startX, startY));


                //objectives
                var endX = startX + 5 + random.Next(10);
                var endY = startY + 50 + random.Next(10);

                tiles[endX, endY].Item = TileItemInfo.GetClone(TileItemType.CORRUPTED_FEELING);
                ClearObstacle(10, mapsize, new Position(endX, endY));

                endX = startX + 50 + random.Next(10);
                endY = startY + 5 + random.Next(10);

                tiles[endX, endY].Item = TileItemInfo.GetClone(TileItemType.CORRUPTED_FEELING);
                ClearObstacle(10, mapsize, new Position(endX, endY));

                Start = new Position(startX, startY);
            }
        }

        public Tile[,] GetTiles()
        {
            return tiles;
        }


        private Position AddResourcePatch(int size, int mapsize, Position center, TileItemType tileType)
        {
            var square = tileType == TileItemType.RESOURCE_COMMON_SOUL;

            var mazeness = 0.5;
            mazeness = (tileType == TileItemType.RESOURCE_COMMON_BODY) ? 1 : mazeness;
            mazeness = (tileType == TileItemType.RESOURCE_RARE_BODY) ? 10 : mazeness;
            mazeness = (tileType == TileItemType.RESOURCE_COMMON_SOUL) ? 10 : mazeness;
            mazeness = (tileType == TileItemType.RESOURCE_RARE_SOUL) ? 0.1 : mazeness;

            var patch = GenUtils.MazeGen(size, mazeness, square, 1, 1, 1, 1);
            Position furthestPos = center;

            foreach (Position pos in patch)
            {
                Position newpos = new Position(center.X + pos.X, center.Y + pos.Y);
                if (newpos.IsInMap(mapsize))
                {
                    tiles[newpos.X, newpos.Y].Item = TileItemInfo.GetClone(tileType);
                }
                if (newpos.Dist(center) > furthestPos.Dist(center)){
                    furthestPos = newpos;
                }
            }
            
            return furthestPos;
        }

        private void ClearObstacle(int radius, int mapsize, Position center)
        {

            for (int i = center.X - radius; i <= center.X + radius; i++)
            {
                for (int j = center.Y - radius; j <= center.Y + radius; j++)
                {
                    Position pos = new Position(i, j);
                    if (pos.IsInMap(mapsize) && center.Dist(pos) <= radius)
                    {
                        if (tiles[pos.X, pos.Y].Item.ItemType == TileItemType.OBSTACLE)
                        {
                            tiles[pos.X, pos.Y].Item = TileItemInfo.GetClone(TileItemType.NONE);
                        }

                    }
                }
            }
        }
    }
}
