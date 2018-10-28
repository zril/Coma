using Coma.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Coma.Server.Model.Item
{
    public abstract class Function
    {

        public void Execute(PlayerType mapType, Position pos)
        {
            Execute(mapType, pos, 0);
        }

        public abstract void Execute(PlayerType mapType, Position pos, int synergy);
    }
}
