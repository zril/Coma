
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
        MapMessages = new ConcurrentQueue<MapMessage>();
        BankMessages = new ConcurrentQueue<BankMessage>();
        CameraMessages = new ConcurrentQueue<CameraMessage>();
    }

    public ConcurrentQueue<MapMessage> MapMessages { get; private set; }
    public ConcurrentQueue<BankMessage> BankMessages { get; private set; }
    public ConcurrentQueue<CameraMessage> CameraMessages { get; private set; }

    public ISocketClient Client { get; private set; }
    public int PlayerId { get; private set; }

    public string CurrentScene { get; set; }

    private int messageCount = 0;
    private int port = 4242;
    private string serverIp = "192.168.1.125";
    private string userName = "nope";

    public void InitClient(bool body, string serverIp)
    {
        if (PlayerId == 0)
        {
            userName = body ? "body" : "soul";
            this.serverIp = serverIp;
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


        if (message.StartsWith(MessagePrefix.MAP))
        {
            MapMessage mapMessage = new MapMessage();
            mapMessage.DeserializeArguments(message.Remove(0, MessagePrefix.MAP.Length));
            MapMessages.Enqueue(mapMessage);
        }
        else if (message.StartsWith(MessagePrefix.BANK))
        {
            BankMessage bankMessage = new BankMessage();
            bankMessage.DeserializeArguments(message.Remove(0, MessagePrefix.BANK.Length));
            BankMessages.Enqueue(bankMessage);
        }
        else if(message.StartsWith(MessagePrefix.CAMERA))
        {
            Debug.Log("received " + message);
            CameraMessage camMessage = new CameraMessage();
            camMessage.DeserializeArguments(message.Remove(0, MessagePrefix.CAMERA.Length));
            CameraMessages.Enqueue(camMessage);
        }

    }

}
