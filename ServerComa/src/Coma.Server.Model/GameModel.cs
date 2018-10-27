using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Coma.Server.Model.Entity;
using Coma.Server.Model.Map;
using Coma.Common;

namespace Coma.Server.Model
{
    public class GameModel
    {
        private const int mapSize = 100;

        #region singleton

        private static GameModel instance;
        public static GameModel Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new GameModel();
                }

                return instance;
            }
        }

        #endregion

        public Player BodyPlayer { get; set; }
        public Player SoulPlayer { get; set; }

        public WorldMap BodyMap { get; set; }
        public WorldMap SoulMap { get; set; }

        private GameModel()
        {
            BodyMap = new WorldMap(mapSize, PlayerType.BODY);
            SoulMap = new WorldMap(mapSize, PlayerType.SOUL);
        }
    }
}
