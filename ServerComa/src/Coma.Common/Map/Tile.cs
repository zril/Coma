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
        public TileType Type { get; set; }
        public int Influence { get; set; }
        bool Contructable { get; set; }
        bool Radiance { get; set; }
        public TileItem Item { get; set; }

        public Tile()
        {
            Influence = 0;
            Contructable = false;
            Radiance = false;
            Item = TileItemInfo.Get(TileItemType.NONE);
        }

        public void FromMessage(String message)
        {
            string[] splitargs = message.Split(';');

            Type = (TileType) int.Parse(splitargs[0]);
            Influence = int.Parse(splitargs[1]);
            Contructable = Utils.StringToBool(splitargs[2]);
            Radiance = Utils.StringToBool(splitargs[3]);

            string[] itemargs = splitargs[4].Split(',');
            TileItemType itemtype = (TileItemType)(int.Parse(itemargs[0]));
            Item = TileItemInfo.Get(itemtype);
            Item.FromMessage(splitargs[4]);
        }

        public string ToMessage()
        {
            return string.Format("{0};{1};{2};{3};{4}", (int)Type, Influence, Utils.BoolToString(Contructable), Utils.BoolToString(Radiance), Item.ToMessage());
        }
    }
}
