using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Coma.Common.Message;
using Coma.Server.Model;
using Coma.Server.Model.Entity;

namespace Coma.Server.Core.Module
{
    public class PlayerModule : BaseModule
    {
        public PlayerModule()
            : base(25)
        { }

        public override void Update(TimeSpan elapsed)
        {
        }

        private void updatePlayer(Player player)
        {
            var moveSpeed = 0.2;
            if (player.MovingDown)
            {
                player.Position.Y -= moveSpeed;
            }
            if (player.MovingUp)
            {
                player.Position.Y += moveSpeed;
            }
            if (player.MovingLeft)
            {
                player.Position.X -= moveSpeed;
            }
            if (player.MovingRight)
            {
                player.Position.X += moveSpeed;
            }
        }
    }
}
