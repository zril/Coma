using Coma.Common;
using Coma.Common.Map.Item;
using Coma.Common.Map.Item.Items;
using Coma.Server.Model.Map;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Coma.Server.Model.Item.Fonctions
{
    public class HarvestFunction : Function
    {
        public int Radius { get; set; }
        public int Rate { get; set; }

        public HarvestFunction(int radius, int rate)
        {
            Radius = radius;
            Rate = rate;
        }

        public override void Execute(PlayerType mapType, Position pos)
        {
            WorldMap map = GameModel.Instance.GetMap(mapType);

            for (int i = pos.X - Radius; i <= pos.X + Radius; i++)
            {
                for (int j = pos.Y - Radius; j <= pos.Y + Radius; j++)
                {
                    var tmpPos = new Position(i, j);
                    if (tmpPos.IsInMap(map.GetTiles().GetLength(0)))
                    {
                        var dist = tmpPos.Dist(pos);
                        if (dist <= Radius)
                        {
                            var tileType = map.GetTiles()[tmpPos.X, tmpPos.Y].Item.ItemType;
                            var bank = GameModel.Instance.Bank;
                            
                            switch (tileType)
                            {
                                case TileItemType.RESOURCE_COMMON_BODY:
                                    ResourceBodyItem item1 = map.GetTiles()[tmpPos.X, tmpPos.Y].Item as ResourceBodyItem;
                                    item1.Count -= Rate;
                                    bank.Cells += Rate;
                                    if (item1.Count < 0)
                                    {
                                        bank.Cells += item1.Count;
                                        map.GetTiles()[tmpPos.X, tmpPos.Y].Item = TileItemInfo.GetClone(TileItemType.NONE);
                                    }
                                    break;
                                case TileItemType.RESOURCE_COMMON_SOUL:
                                    ResourceSoulItem item2 = map.GetTiles()[tmpPos.X, tmpPos.Y].Item as ResourceSoulItem;
                                    item2.Count -= Rate;
                                    bank.Thoughts += Rate;
                                    if (item2.Count < 0)
                                    {
                                        bank.Thoughts += item2.Count;
                                        map.GetTiles()[tmpPos.X, tmpPos.Y].Item = TileItemInfo.GetClone(TileItemType.NONE);
                                    }
                                    break;
                                case TileItemType.RESOURCE_RARE_BODY:
                                    ResourceBodyRareItem item3 = map.GetTiles()[tmpPos.X, tmpPos.Y].Item as ResourceBodyRareItem;
                                    item3.Count -= Rate;
                                    bank.Nutrients += Rate;
                                    bank.Ideas += Rate;
                                    if (item3.Count < 0)
                                    {
                                        bank.Nutrients += item3.Count;
                                        map.GetTiles()[tmpPos.X, tmpPos.Y].Item = TileItemInfo.GetClone(TileItemType.NONE);
                                    }
                                    break;
                                case TileItemType.RESOURCE_RARE_SOUL:
                                    ResourceSoulRareItem item4 = map.GetTiles()[tmpPos.X, tmpPos.Y].Item as ResourceSoulRareItem;
                                    item4.Count -= Rate;
                                    bank.Ideas += Rate;
                                    bank.Nutrients += Rate;
                                    if (item4.Count < 0)
                                    {
                                        bank.Ideas += item4.Count;
                                        map.GetTiles()[tmpPos.X, tmpPos.Y].Item = TileItemInfo.GetClone(TileItemType.NONE);
                                    }
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
}
