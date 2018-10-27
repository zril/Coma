
using Anjril.Common.Network;
using Anjril.Common.Network.TcpImpl;
using Anjril.Common.Network.TcpImpl.Properties;
using Coma.Common.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Coma.Common.Parameter;

class Global
{
    public static Global Instance
    {
        get
        {
            if (_Instance == null)
                _Instance = new Global();
            return _Instance;
        }
    }

    private static Global _Instance;

    private Global()
    {
        PlayerMessages = new ConcurrentQueue<PlayerMessage>();
        MapMessages = new ConcurrentQueue<MapMessage>();
    }

    public ConcurrentQueue<PlayerMessage> PlayerMessages { get; private set; }
    public ConcurrentQueue<MapMessage> MapMessages { get; private set; }

    public ISocketClient Client { get; private set; }
    public int PlayerId { get; private set; }

    public string CurrentScene { get; set; }

    private int messageCount = 0;
    private int port = 4242;
    private string serverIp = "127.0.0.1";
    private string userName = "soul";

    public void InitClient()
    {
        if (PlayerId == 0)
        {
            var randomport = UnityEngine.Random.Range(0, 1000);
            Settings.Default.ClientPort = port + randomport;
            Client = new TcpSocketClient();
            string rep = Client.Connect(serverIp, 1337, MessageReceived, userName);

            Debug.Log("connect " + rep);
            PlayerId = Int32.Parse(rep.Split(':')[1]);
        }
    }

    public void Login(string serverip, string username)
    {
        serverIp = serverip;
        userName = username;
    }

    public void SendCommand(BaseParam param)
    {
        Client.Send(param.ToString());
    }

    private void MessageReceived(IRemoteConnection sender, string message)
    {
        messageCount++;
        Debug.Log("received " + message);
        
        
        if (message.StartsWith(MessagePrefix.PLAYER))
        {
            PlayerMessage playerMessage = new PlayerMessage();
            playerMessage.DeserializeArguments(message.Remove(0, MessagePrefix.PLAYER.Length));
            PlayerMessages.Enqueue(playerMessage);
        }


        if (message.StartsWith(MessagePrefix.MAP))
        {
            MapMessage mapMessage = new MapMessage();
            mapMessage.DeserializeArguments(message.Remove(0, MessagePrefix.MAP.Length));
            MapMessages.Enqueue(mapMessage);
        }

    }

}
