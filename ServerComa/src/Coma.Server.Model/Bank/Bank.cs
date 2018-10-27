using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coma.Server.Model
{
    public class Bank
    {
        public int ResourceBody1 { get; set; }
        public int ResourceBody2 { get; set; }
        public int ResourceSoul1 { get; set; }
        public int ResourceSoul2 { get; set; }

        public Bank()
        {
            ResourceBody1 = 0;
            ResourceBody2 = 0;
            ResourceSoul1 = 0;
            ResourceSoul2 = 0;
        }
    }
}
