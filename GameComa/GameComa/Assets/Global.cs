
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
    }

    public ConcurrentQueue<PlayerMessage> PlayerMessages { get; private set; }

    public ISocketClient Client { get; private set; }
    public int PlayerId { get; private set; }

    public string CurrentScene { get; set; }

    private int messageCount = 0;
    private int port = 4242;
    private string serverIp = "127.0.0.1";
    private string userName = "jpiji";
    //private string userName = "jpiji2";

    public void InitClient()
    {
        if (PlayerId == 0)
        {
            var randomport = UnityEngine.Random.Range(0, 1000);
            Settings.Default.ClientPort = port + randomport;
            Client = new TcpSocketClient();
            string rep = Client.Connect(serverIp, 1337, MessageReceived, userName);
            //string rep = Client.Connect("192.168.1.23", 1337, MessageReceived, "jpiji");
            //string rep = Client.Connect("192.168.1.31", 1337, MessageReceived, "jpiji");

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
        

        var prefix = "pla:";
        if (message.StartsWith(prefix))
        {
            PlayerMessage playerMessage = new PlayerMessage();
            playerMessage.DeserializeArguments(message.Remove(0, prefix.Length));

            PlayerMessages.Enqueue(playerMessage);
        }

    }

}
