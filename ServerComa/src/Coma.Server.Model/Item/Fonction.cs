using Coma.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Coma.Server.Model.Item
{
    public abstract class Fonction
    {
        public abstract void Execute(PlayerType mapType, Position pos);
    }
}
