using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coma.Server.Model
{
    public class Position
    {
        public double X { get; set; }
        public double Y { get; set; }

        public Position(float x, float y)
        {
            X = x;
            Y = y;
        }

    }
}
