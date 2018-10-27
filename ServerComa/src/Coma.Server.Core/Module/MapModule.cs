using Coma.Common.Message;
using Coma.Server.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coma.Server.Core.Module
{
    public class MapModule : BaseModule
    {
        public MapModule()
            : base(1000)
        { }

        public override void Update(TimeSpan elapsed)
        {
            if (GameModel.Instance.BodyPlayer != null)
            {
                MapMessage bodyMessage = new MapMessage(GameModel.Instance.BodyMap.GetTiles());
                GlobalServer.Instance.SendMessage(GameModel.Instance.BodyPlayer.Id, bodyMessage.ToString());
            }

            if (GameModel.Instance.SoulPlayer != null)
            {
                MapMessage soulMessage = new MapMessage(GameModel.Instance.SoulMap.GetTiles());
                GlobalServer.Instance.SendMessage(GameModel.Instance.SoulPlayer.Id, soulMessage.ToString());
            }
        }
    }
}
