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
        public int Nutrients { get; set; }
        public int Thoughts { get; set; }
        public int Ideas { get; set; }

        public Bank()
        {
            Cells = 200;
            Nutrients = 200;
            Thoughts = 200;
            Ideas = 200;
        }
    }
}
