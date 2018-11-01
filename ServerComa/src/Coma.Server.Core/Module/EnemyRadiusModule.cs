using Coma.Common;
using Coma.Common.Map.Item;
using Coma.Common.Message;
using Coma.Server.Model;
using Coma.Server.Model.Item;
using Coma.Server.Model.Map;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coma.Server.Core.Module
{
    public class EnemyRadiusModule : BaseModule
    {
        private Random random;

        public EnemyRadiusModule()
            : base(660000)
        {
            random = new Random();
        }

        public override void Update(TimeSpan elapsed)
        {
            GameModel.Instance.EnemyRadiusBonus++;
        }
    }
}
