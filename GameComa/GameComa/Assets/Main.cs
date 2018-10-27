using System.Collections;
using System.Collections.Generic;
using Coma.Common.Message;
using Coma.Common.Map;
using UnityEngine;

public class Main : MonoBehaviour
{
    Dictionary<int, GameObject> players;

    public List<Sprite> TileSprites;
    public List<Sprite> TileItemSprites;
    public List<Color> TileItemColors;

    public GameObject TilePrefab;
    public Transform TileParent;
    public int UpdateFrames;
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

    }

    IEnumerator MessagesCoroutine()
    {
        while (true)
        {
            while (Global.Instance.MapMessages.Count > 0)
            {
                MapMessage mapMessage = Global.Instance.MapMessages.Dequeue();
                int counter = 0;
                int maxElementsPerUpdate = mapMessage.TileMap.Length / UpdateFrames;
                if (TileMap == null)
                {

                    TileMap = new TileView[mapMessage.TileMap.GetLength(0), mapMessage.TileMap.GetLength(1)];
                }

                for (int y = 0; y < TileMap.GetLength(1); y++)
                {
                    for (int x = 0; x < TileMap.GetLength(0); x++)
                    {
                        counter++;
                        if (TileMap[x, y] == null)
                        {
                            //Instantiate here
                            GameObject tileObj = GameObject.Instantiate<GameObject>(TilePrefab, new Vector3(x, y, 0), Quaternion.identity, TileParent);

                            TileMap[x, y] = tileObj.GetComponent<TileView>();
                            TileMap[x, y].MainController = this;
                        }

                        TileMap[x, y].UpdateTile(mapMessage.TileMap[x, y]);
                        if(counter == maxElementsPerUpdate)
                        {
                            counter = 0;
                            yield return null;
                        }
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
