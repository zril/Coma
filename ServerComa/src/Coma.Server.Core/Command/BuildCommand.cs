using Coma.Server.Core.Command;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Coma.Common;
using Coma.Common.Parameter;
using Coma.Server.Model.Entity;
using Coma.Server.Model;
using Coma.Server.Model.Map;
using Coma.Common.Map.Item;

namespace Coma.Server.Core.Command
{
    [Description("bld")]
    class BuildCommand : BaseCommand<BuildParam>
    {
        public override void RunWithCast(Player player, BuildParam param)
        {
            WorldMap map = null;
            if (player.Type == PlayerType.BODY)
            {
                map = GameModel.Instance.BodyMap;
            }

            if (player.Type == PlayerType.SOUL)
            {
                map = GameModel.Instance.SoulMap;
            }

            if (map.GetTiles()[param.Position.X, param.Position.Y].Item.ItemType == TileItemType.NONE)
            {
                map.GetTiles()[param.Position.X, param.Position.Y].Item = TileItemInfo.GetClone(param.Id);
            }
        }
    }
}
