using Coma.Common.Map.Item;
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
        public TileItemType SynergyTrigger { get; set; }

        public FunctionInfo()
        {
            MainFunction = new NoFonction();
            SecondaryFunction = new NoFonction();
            SynergyFunction = new NoFonction();
            SynergyTrigger = TileItemType.NONE;
        }
    }
}
