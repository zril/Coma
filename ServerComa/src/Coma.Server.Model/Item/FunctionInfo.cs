using Coma.Server.Model.Item.Fonctions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coma.Server.Model.Item
{
    public class FunctionInfo
    {
        public Function MainFunction { get; set; }
        public Function SecondaryFunction { get; set; }
        public Function SynergyFunction { get; set; }
        public Function SynergyEffectFunction { get; set; }

        public FunctionInfo()
        {
            MainFunction = new NoFonction();
            SecondaryFunction = new NoFonction();
            SynergyFunction = new NoFonction();
            SynergyEffectFunction = new NoFonction();
        }
    }
}
