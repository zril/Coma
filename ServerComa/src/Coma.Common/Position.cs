using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Coma.Common
{
    public class Position
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Position(int x, int y)
        {
            X = x;
            Y = y;
        }

        public bool Equals(Position other)
        {
            if (other == null) return false;
            return this.Dist(other) == 0;
        }

        public override int GetHashCode()
        {
            return X * 13457 + Y * 18947;
        }

        public int Dist(Position other)
        {
            return Math.Abs(this.X - other.X) + Math.Abs(this.Y - other.Y);
        }

        public bool IsInMap(int mapsize)
        {
            if (X < 0) return false;
            if (Y < 0) return false;
            if (X >= mapsize) return false;
            if (Y >= mapsize) return false;
            return true;
        } 
    }
}
