using Coma.Common;
using Coma.Server.Model.Map;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Coma.Server.Model.Item.Fonctions
{
    public class GeneratorFunction : Function
    {
        public int Rate { get; set; }
        public ResourceType Resource { get; set; }

        public GeneratorFunction(int rate, ResourceType resource)
        {
            Rate = rate;
            Resource = resource;
        }

        public override void Execute(PlayerType mapType, Position pos, int synergy)
        {
            switch (Resource)
            {
                case ResourceType.CELLS:
                    GameModel.Instance.Bank.Cells += Rate * (synergy + 1);
                    break;
                case ResourceType.NUTRIENTS:
                    GameModel.Instance.Bank.Nutrients += Rate * (synergy + 1);
                    break;
                case ResourceType.THOUGHTS:
                    GameModel.Instance.Bank.Thoughts += Rate * (synergy + 1);
                    break;
                case ResourceType.IDEAS:
                    GameModel.Instance.Bank.Ideas += Rate * (synergy + 1);
                    break;
                default:
                    break;
            }
           
        }
    }
}
