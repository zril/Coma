using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Coma.Common
{
    public static class Utils
    {
        public static string BoolToString(bool b)
        {
            return b ? "1" : "0";
        }

        public static bool StringToBool(string s)
        {
            if (s == "0")
            {
                return false;
            } else
            {
                return true;
            }
        }
        
        public static string PosToString(Position pos)
        {
            return pos.X + "," + pos.Y;
        }

        public static Position PosFromString(string pos)
        {
            var split = pos.Split(',');

            return new Position(int.Parse(split[0]), int.Parse(split[1]));
        }
    }
}
