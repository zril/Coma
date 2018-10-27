using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Coma.Common.Map
{
    public abstract class TileItem
    {
        string Name { get; set; }
        int Id { get; set; }
        bool Synergy { get; set; }

        public abstract string ToMessage();

        public abstract void FromMessage();
    }
}
