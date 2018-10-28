using Coma.Common;
using Coma.Server.Model.Map;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Coma.Server.Model.Item.Fonctions
{
    public class RadianceFunction : Function
    {
        public int Radius { get; set; }
        public int Power { get; set; }

        public RadianceFunction(int radius, int power)
        {
            Radius = radius;
            Power = power;
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
                            var power = Power - (Power / Radius) * dist;
                            map.GetTiles()[tmpPos.X, tmpPos.Y].Influence += power;

                            if (power > 0)
                            {
                                map.GetTiles()[tmpPos.X, tmpPos.Y].Radiance = true;
                            }
                        }
                    }
                }
            }
        }
    }
}
