using Coma.Common.Map;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Coma.Server.Model.Map
{
    public class Tile
    {
        public TileType tileType { get; set; }
        public TileItem tileItem { get; set; }
        public int Influence { get; set; }
        bool Contructable { get; set; }
        bool Radiance { get; set; }
    }
}
