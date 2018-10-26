using Anjril.Common.Network;
using Coma.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coma.Server.Core
{
    public class GlobalServer
    {
        #region singleton

        private static GlobalServer instance;
        public static GlobalServer Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new GlobalServer();
                }

                return instance;
            }
        }

        #endregion

        #region private fields

        private Dictionary<int, IRemoteConnection> playerConnectionMap = new Dictionary<int, IRemoteConnection>();

        #endregion

        #region constructor

        private GlobalServer()
        {
            playerConnectionMap = new Dictionary<int, IRemoteConnection>();
        }

        #endregion

        #region player management

        public void AddPlayer(int playerId, IRemoteConnection remote)
        {
            playerConnectionMap.Add(playerId, remote);
        }

        public void RemovePlayer(int playerId)
        {
            playerConnectionMap.Remove(playerId);
        }

        public List<int> GetPlayers()
        {
            return playerConnectionMap.Keys.ToList();
        }

        #endregion

        #region network management

        public IRemoteConnection GetConnection(int playerId)
        {
            if (playerConnectionMap.ContainsKey(playerId))
            {
                return playerConnectionMap[playerId];
            }
            else
            {
                return null;
            }
        }

        public void SendMessage(int playerId, string message)
        {
            var conn = GetConnection(playerId);
            if (conn != null)
            {
                conn.Send(message);
            }
        }

        #endregion
    }
}
