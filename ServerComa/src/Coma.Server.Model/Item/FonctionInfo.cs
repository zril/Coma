using Coma.Server.Model.Item.Fonctions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coma.Server.Model.Item
{
    public class FonctionInfo
    {
        public Fonction MainFonction { get; set; }
        public Fonction SynergyFonction { get; set; }
        public Fonction SynergyEffectFonction { get; set; }

        public FonctionInfo()
        {
            MainFonction = new NoFonction();
            SynergyFonction = new NoFonction();
            SynergyEffectFonction = new NoFonction();
        }
    }
}
