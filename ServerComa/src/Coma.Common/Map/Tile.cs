using Coma.Common.Map;
using Coma.Common.Map.Item;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Coma.Common.Map
{
    public class Tile
    {
        public TileType tileType { get; set; }
        public TileItemType tileItem { get; set; }
        public int Influence { get; set; }
        bool Contructable { get; set; }
        bool Radiance { get; set; }

        public string ToMessage()
        {
            return "";
        }

        public void FromMessage(String message)
        {

        }
    }
}
