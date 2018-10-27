﻿using System;
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
            Cells = 100;
            Nutrients = 0;
            Thoughts = 100;
            Ideas = 0;
        }
    }
}
