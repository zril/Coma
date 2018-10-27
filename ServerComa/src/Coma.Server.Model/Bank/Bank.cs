using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coma.Server.Model
{
    public class Bank
    {
        public int Cells { get; set; }
        public int Nutrient { get; set; }
        public int Thoughts { get; set; }
        public int Ideas { get; set; }

        public Bank()
        {
            Cells = 0;
            Nutrient = 0;
            Thoughts = 0;
            Ideas = 0;
        }
    }
}
