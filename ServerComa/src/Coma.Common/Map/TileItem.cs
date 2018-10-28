using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Coma.Common.Map.Item
{
    public abstract class TileItem
    {
        public TileItemType ItemType { get; set; }
        public int Synergy { get; set; }
        public TileItemCategory Category { get; set; }
        public TileItemFonction Fonction { get; set; }
        public TileItemSynergyMode SynergyMode { get; set; }
        public List<TileItemCategory> SynergyCaterories { get; set; }
        public int CostCells { get; set; }
        public int CostNutrients { get; set; }
        public int CostThoughts { get; set; }
        public int CostIdeas { get; set; }

        public int MaintenanceCellCostRate { get; set; }
        public int MaintenanceThoughtCostRate { get; set; }

        public abstract string ToMessage();

        public abstract void FromMessage(string message);

        public abstract TileItem Clone();
    }
}
