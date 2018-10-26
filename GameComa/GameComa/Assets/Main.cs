using System.Collections;
using System.Collections.Generic;
using Coma.Common.Message;
using UnityEngine;

public class Main : MonoBehaviour {

    Dictionary<int, GameObject> players;

	// Use this for initialization
	void Start () {
        Global.Instance.InitClient();
        players = new Dictionary<int, GameObject>();
    }
	
	// Update is called once per frame
	void Update () {
		
        while (Global.Instance.PlayerMessages.Count > 0)
        {
            PlayerMessage message = Global.Instance.PlayerMessages.Dequeue();

            GameObject player;

            if (players.ContainsKey(message.Id))
            {
                player = players[message.Id];
                player.transform.position = new Vector3((float)message.X, (float)message.Y, 0);
                Debug.Log("update " + message.Id + " " + message.X + " " + message.Y);
            } else
            {
                player = GameObject.Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/Player"), new Vector3((float)message.X, (float)message.Y, 0), Quaternion.identity);
                players.Add(message.Id, player);
                player.GetComponent<Player>().playerId = message.Id;
                Debug.Log("new " + message.Id + " " + message.X + " " + message.Y);
            }

        }
	}

    private void OnApplicationQuit()
    {
        Debug.Log("quit");
        Global.Instance.Client.Disconnect(Global.Instance.PlayerId.ToString());
    }
}
