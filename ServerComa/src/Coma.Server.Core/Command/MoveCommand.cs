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

namespace Coma.Server.Core.Command
{
    [Description("mov")]
    class MoveCommand : BaseCommand<MoveParam>
    {
        public override void RunWithCast(Player player, MoveParam param)
        {
            lock (player)
            {
                if (param.Direction == Direction.Up)
                {
                    player.MovingUp = param.Active;
                }
                if (param.Direction == Direction.Down)
                {
                    player.MovingDown = param.Active;
                }
                if (param.Direction == Direction.Right)
                {
                    player.MovingRight = param.Active;
                }
                if (param.Direction == Direction.Left)
                {
                    player.MovingLeft = param.Active;
                }
            }
        }
    }
}
