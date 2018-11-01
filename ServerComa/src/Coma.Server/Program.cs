using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Net.Sockets;
using Anjril.Common.Network;
using Coma.Server.Model.Entity;
using Coma.Server.Core.Module;
using Anjril.Common.Network.TcpImpl;
using Coma.Server.Core;
using Coma.Server.Core.Command;
using Coma.Server.Model;
using Coma.Common;

namespace Coma.Server
{
    class Program
    {
        private static Dictionary<IRemoteConnection, Player> PLAYERS = new Dictionary<IRemoteConnection, Player>();
        private static List<IModule> MODULES = new List<IModule>();

        static void Main(string[] args)
        {
            Console.WriteLine("----------------------------------");
            Console.WriteLine("----------------------------------");
            Console.WriteLine("-------- Template Server ---------");
            Console.WriteLine("----------------------------------");
            Console.WriteLine("----------------------------------");
            Console.WriteLine("");

            using (var socket = new TcpSocket())
            {
                #region start module loop

                var updateLoop = new Thread(new ThreadStart(StartModules));
                updateLoop.Name = "UpdateLoop";
                updateLoop.Start();

                #endregion

                #region set network on

                socket.StartListening(ConnectionRequested, MessageReceived, Disconnected);

                Console.WriteLine();
                Console.WriteLine("Server listening on port " + socket.Port);
                Console.WriteLine();

                #endregion

                Console.WriteLine("Press space to restart a new game.");
                Console.WriteLine("Press escape to stop the server...");

                Console.WriteLine();
                ConsoleKeyInfo key = Console.ReadKey(); ;
                

                while (key.Key != ConsoleKey.Escape)
                {
                    key = Console.ReadKey();
                    if (key.Key == ConsoleKey.Spacebar)
                    {
                        Console.WriteLine("restarting a new game...");
                        var bodyplayer = GameModel.Instance.BodyPlayer;
                        var soulplayer = GameModel.Instance.SoulPlayer;
                        GameModel.Instance.Reset();
                        GameModel.Instance.BodyPlayer = bodyplayer;
                        GameModel.Instance.SoulPlayer = soulplayer;
                        StopModules();
                        StartModules();
                    }
                }

                Console.WriteLine();

                #region stop module loop

                StopModules();

                while (updateLoop.IsAlive)
                {
                    Thread.Sleep(10);
                }

                #endregion

                #region set network off

                socket.StopListening();

                #endregion
            }

            Console.WriteLine("----------------------------------");
            Console.WriteLine("----------------------------------");
            Console.WriteLine("-------- Template Server ---------");
            Console.WriteLine("----------------------------------");
            Console.WriteLine("----------------------------------");

            Console.WriteLine();
            Console.WriteLine("Server stopped. Press any key to exit...");
            Console.WriteLine();

            Console.ReadKey();
        }

        #region socket management

        private static bool ConnectionRequested(IRemoteConnection sender, string request, out string response)
        {
            Player player = null;
            switch (request)
            {
                case "body":
                    player = new Player(request, sender, PlayerType.BODY);
                    PLAYERS.Add(sender, player);
                    GlobalServer.Instance.AddPlayer(player.Id, sender);
                    GameModel.Instance.BodyPlayer = player;
                    GameModel.Instance.BodyCamInit = false;
                    break;
                case "soul":
                    player = new Player(request, sender, PlayerType.SOUL);
                    PLAYERS.Add(sender, player);
                    GlobalServer.Instance.AddPlayer(player.Id, sender);
                    GameModel.Instance.SoulPlayer = player;
                    GameModel.Instance.SoulCamInit = false;
                    break;
                default:
                    response = "KO";
                    return false;
            }
            
            

            response = "OK:" + player.Id;

            return true;
        }

        private static void MessageReceived(IRemoteConnection sender, string message)
        {
            Console.WriteLine("Message received from {0}:{1} : {2}.", sender.IPAddress, sender.Port, message);

            var player = PLAYERS[sender];

            var splitedArgument = message.Split('/');

            var cmd = splitedArgument[0];
            var args = splitedArgument[1];

            ICommand command = CommandFactory.GetCommand(cmd);

            Object param;
            if (command.CanRun(player, args, out param))
            {
                Task.Run(() => command.Run(player, param));
            }
            else
            {
                Console.WriteLine("Can't run the command {0} with the arguments {1}", cmd, args);
            }
        }

        private static void Disconnected(IRemoteConnection remote, string justification)
        {
            var player = PLAYERS[remote];
            
            GlobalServer.Instance.RemovePlayer(player.Id);
            PLAYERS.Remove(remote);

            Console.WriteLine("disconnected ({0} - {1})", player.Id, player.Name);
        }

        #endregion

        #region private methods

        private static void StartModules()
        {
            MODULES.Add(new MapModule());
            MODULES.Add(new VirusModule());
            MODULES.Add(new NightmareModule());
            MODULES.Add(new EnemyPowerModule());
            MODULES.Add(new EnemyRadiusModule());

            foreach (IModule mod in MODULES)
            {
                mod.Start();
            }
        }

        private static void StopModules()
        {
            foreach (IModule mod in MODULES)
            {
                mod.Stop();
            }
        }

        #endregion
    }
}
