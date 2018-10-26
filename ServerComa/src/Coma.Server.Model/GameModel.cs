using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Coma.Server.Model.Entity;

namespace Coma.Server.Model
{
    public class GameModel
    {
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

        private Dictionary<int, Player> players; 

        private GameModel()
        {
            players = new Dictionary<int, Player>();
        }

        public List<Player> GetPlayers()
        {
            return players.Values.ToList();
        }

        public void AddPlayer(Player player)
        {
            players.Add(player.Id, player);
        }

        public void RemovePlayer(int playerId)
        {
            players.Remove(playerId);
        }
    }
}
