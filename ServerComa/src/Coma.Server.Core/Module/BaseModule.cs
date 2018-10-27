using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Threading;
using Coma.Common.Message;
using Coma.Server.Model.Entity;

namespace Coma.Server.Core.Module
{
    public abstract class BaseModule : IModule
    {
        #region public properties

        public long Interval { get; private set; }
        public DateTime LastUpdate { get; private set; }

        #endregion
        
        private bool stop = false;

        #region constructor

        public BaseModule(long interval)
        {
            Interval = interval;
            LastUpdate = DateTime.Now;
        }

        #endregion

        #region public methods

        public abstract void Update(TimeSpan elapsed);

        public void Start()
        {
            var moduleThread = new Thread(ExecuteUpdate);
            moduleThread.Start();
        }

        public void Stop()
        {
            stop = true;
        }

        private void ExecuteUpdate()
        {
            var updateTime = 0;
            while (!stop)
            {
                var sleep = (int)Interval - updateTime;
                if (sleep > 0)
                {
                    Thread.Sleep(sleep);
                }

                var elapsed = DateTime.Now - LastUpdate;
                this.LastUpdate = DateTime.Now;
                Update(elapsed);
                /*try
                {
                    Update(elapsed);
                } catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }*/
                updateTime = (int)(DateTime.Now - LastUpdate).TotalMilliseconds;
            }
        }

        #endregion

        #region protected methods

        protected void SendMessage(Player player, BaseMessage message)
        {
            try
            {
                player.RemoteConnection.Send(message.ToString());
            } catch(SocketException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        #endregion
    }
}