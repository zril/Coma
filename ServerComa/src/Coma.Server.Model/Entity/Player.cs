using Anjril.Common.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coma.Server.Model.Entity
{
    public class Player
    {
        #region public properties

        private static int sequenceId = 0;

        public int Id { get; private set; }

        public string Name { get; private set; }

        public bool MovingUp { get; set; }
        public bool MovingDown { get; set; }
        public bool MovingLeft { get; set; }
        public bool MovingRight { get; set; }

        public IRemoteConnection RemoteConnection { get; private set; }

        public Position Position { get; private set; }

        #endregion

        #region constructor

        public Player(string name, IRemoteConnection remote)
        {
            Name = name;
            Id = sequenceId++;
            RemoteConnection = remote;

            Position = new Position(0, 0);
            MovingUp = false;
            MovingDown = false;
            MovingLeft = false;
            MovingRight = false;
        }

        #endregion
    }
}
