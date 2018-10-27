using System.Collections;
using System.Collections.Generic;
using Coma.Common.Message;
using Coma.Common.Map;
using UnityEngine;
using Coma.Common.Parameter;
using Coma.Common.Map.Item;

public class Main : MonoBehaviour
{
    Dictionary<int, GameObject> players;

    public List<Sprite> TileSprites;
    public List<Sprite> TileItemSprites;
    public List<Color> TileItemColors;

    public GameObject TilePrefab;
    public Transform TileParent;
    TileView[,] TileMap;

    // Use this for initialization
    void Start()
    {
        Global.Instance.InitClient();
        players = new Dictionary<int, GameObject>();
        TileMap = null;
        StartCoroutine("MessagesCoroutine");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            Global.Instance.SendCommand(new BuildParam(new Coma.Common.Position(1,1), TileItemType.BUILD_AREA));
        }

        GameObject player;

        while (Global.Instance.PlayerMessages.Count > 0)
        {
            PlayerMessage playerMessage = Global.Instance.PlayerMessages.Dequeue();
            if (players.ContainsKey(playerMessage.Id))
            {
                player = players[playerMessage.Id];
                player.transform.position = new Vector3((float)playerMessage.X, (float)playerMessage.Y, 0);
                Debug.Log("update " + playerMessage.Id + " " + playerMessage.X + " " + playerMessage.Y);
            }
            else
            {
                player = GameObject.Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/Player"), new Vector3((float)playerMessage.X, (float)playerMessage.Y, 0), Quaternion.identity);
                players.Add(playerMessage.Id, player);
                player.GetComponent<Player>().playerId = playerMessage.Id;
                Debug.Log("new " + playerMessage.Id + " " + playerMessage.X + " " + playerMessage.Y);
            }
        }


    }

    IEnumerator MessagesCoroutine()
    {
        while (true)
        {
            while (Global.Instance.MapMessages.Count > 0)
            {
                MapMessage mapMessage = Global.Instance.MapMessages.Dequeue();
                if (TileMap == null)
                {
                    // Remove current Tiles ?

                    TileMap = new TileView[mapMessage.TileMap.GetLength(0), mapMessage.TileMap.GetLength(1)];
                }

                for (int y = 0; y < TileMap.GetLength(1); y++)
                {
                    for (int x = 0; x < TileMap.GetLength(0); x++)
                    {
                        if (TileMap[x, y] == null)
                        {
                            //Instantiate here
                            GameObject tileObj = GameObject.Instantiate<GameObject>(TilePrefab, new Vector3(x, y, 0), Quaternion.identity, TileParent);

                            TileMap[x, y] = tileObj.GetComponent<TileView>();
                            TileMap[x, y].MainController = this;
                        }

                        TileMap[x, y].UpdateTile(mapMessage.TileMap[x, y]);
                    }
                }
            }
            yield return null;
        }
    }

    private void OnApplicationQuit()
    {
        Debug.Log("quit");
        Global.Instance.Client.Disconnect(Global.Instance.PlayerId.ToString());
    }
}
