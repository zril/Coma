using Coma.Common;
using Coma.Common.Map.Item;
using Coma.Server.Model.Map;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coma.Server.Model.Item.Fonctions
{
    public static class Synergy
    {

        public static int CheckSynergyTrigger(WorldMap map, Position center, TileItemSynergyMode mode, TileItemType trigger)
        {
            var res = 0;
            var mapsize = map.GetTiles().GetLength(0);
            if (trigger != TileItemType.NONE)
            {
                if (mode == TileItemSynergyMode.ADJACENT)
                {
                    Position postmp;
                    postmp = new Position(center.X + 1, center.Y);
                    if (postmp.IsInMap(mapsize) && map.GetTiles()[postmp.X, postmp.Y].Item.ItemType == trigger) res++;
                    postmp = new Position(center.X - 1, center.Y);
                    if (postmp.IsInMap(mapsize) && map.GetTiles()[postmp.X, postmp.Y].Item.ItemType == trigger) res++;
                    postmp = new Position(center.X, center.Y + 1);
                    if (postmp.IsInMap(mapsize) && map.GetTiles()[postmp.X, postmp.Y].Item.ItemType == trigger) res++;
                    postmp = new Position(center.X, center.Y - 1);
                    if (postmp.IsInMap(mapsize) && map.GetTiles()[postmp.X, postmp.Y].Item.ItemType == trigger) res++;
                    postmp = new Position(center.X + 1, center.Y + 1);
                    if (postmp.IsInMap(mapsize) && map.GetTiles()[postmp.X, postmp.Y].Item.ItemType == trigger) res++;
                    postmp = new Position(center.X - 1, center.Y - 1);
                    if (postmp.IsInMap(mapsize) && map.GetTiles()[postmp.X, postmp.Y].Item.ItemType == trigger) res++;
                    postmp = new Position(center.X - 1, center.Y + 1);
                    if (postmp.IsInMap(mapsize) && map.GetTiles()[postmp.X, postmp.Y].Item.ItemType == trigger) res++;
                    postmp = new Position(center.X + 1, center.Y - 1);
                    if (postmp.IsInMap(mapsize) && map.GetTiles()[postmp.X, postmp.Y].Item.ItemType == trigger) res++;
                }

                if (mode == TileItemSynergyMode.VERTICAL)
                {
                    Position postmp;
                    for (int j = 1; j <= 8; j++)
                    {
                        postmp = new Position(center.X, center.Y + j);
                        if (postmp.IsInMap(mapsize) && map.GetTiles()[postmp.X, postmp.Y].Item.ItemType == trigger) res++;
                    }
                    for (int j = 1; j <= 8; j++)
                    {
                        postmp = new Position(center.X, center.Y - j);
                        if (postmp.IsInMap(mapsize) && map.GetTiles()[postmp.X, postmp.Y].Item.ItemType == trigger) res++;
                    }
                }

                if (mode == TileItemSynergyMode.HORIZONTAL)
                {
                    Position postmp;
                    for (int i = 1; i <= 8; i++)
                    {
                        postmp = new Position(center.X, center.Y + i);
                        if (postmp.IsInMap(mapsize) && map.GetTiles()[postmp.X, postmp.Y].Item.ItemType == trigger) res++;
                    }
                    for (int i = 1; i <= 8; i++)
                    {
                        postmp = new Position(center.X, center.Y - i);
                        if (postmp.IsInMap(mapsize) && map.GetTiles()[postmp.X, postmp.Y].Item.ItemType == trigger) res++;
                    }
                }

                if (mode == TileItemSynergyMode.DIAGONAL)
                {
                    Position postmp;
                    for (int i = 1; i <= 3; i++)
                    {
                        postmp = new Position(center.X + i, center.Y + i);
                        if (postmp.IsInMap(mapsize) && map.GetTiles()[postmp.X, postmp.Y].Item.ItemType == trigger) res++;
                    }
                    for (int i = 1; i <= 3; i++)
                    {
                        postmp = new Position(center.X - i, center.Y - i);
                        if (postmp.IsInMap(mapsize) && map.GetTiles()[postmp.X, postmp.Y].Item.ItemType == trigger) res++;
                    }
                    for (int i = 1; i <= 3; i++)
                    {
                        postmp = new Position(center.X - i, center.Y + i);
                        if (postmp.IsInMap(mapsize) && map.GetTiles()[postmp.X, postmp.Y].Item.ItemType == trigger) res++;
                    }
                    for (int i = 1; i <= 3; i++)
                    {
                        postmp = new Position(center.X - i, center.Y + i);
                        if (postmp.IsInMap(mapsize) && map.GetTiles()[postmp.X, postmp.Y].Item.ItemType == trigger) res++;
                    }
                }
            }

            if (res > 4)
            {
                res = 4;
            }
            return res;
        }
    }
}
