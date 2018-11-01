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
        
        public bool BodyCamInit { get; set; }
        public bool SoulCamInit { get; set; }

        public WorldMap BodyMap { get; set; }
        public WorldMap SoulMap { get; set; }

        public int EnemyPowerBonus { get; set; }
        public int EnemyRadiusBonus { get; set; }

        public Bank Bank { get; set; }

        private GameModel()
        {
            BodyMap = new WorldMap(mapSize, PlayerType.BODY);
            SoulMap = new WorldMap(mapSize, PlayerType.SOUL);

            BodyCamInit = false;
            SoulCamInit = false;

            EnemyPowerBonus = 0;
            EnemyRadiusBonus = 0;

            Bank = new Bank();
        }

        public WorldMap GetMap(PlayerType type)
        {
            if (type == PlayerType.BODY)
            {
                return BodyMap;
            }
            if (type == PlayerType.SOUL)
            {
                return SoulMap;
            }

            return null;
        }

        public void Reset()
        {
            instance = new GameModel();
        }
    }
}
