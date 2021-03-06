﻿using System.Collections;
using System.Collections.Generic;
using Coma.Common.Message;
using Coma.Common.Map;
using UnityEngine;
using Coma.Common.Parameter;
using Coma.Common.Map.Item;

public class Main : MonoBehaviour
{
    public bool PlayerIsBody;

    public List<Sprite> TileSprites;
    public List<Sprite> TileItemSprites;
    public List<Sprite> TileItemSynergySprites;
    public List<Color> TileItemColors;


    public GameObject TilePrefab;
    public GameObject TileUIPrefab;
    public Transform TileParent;
    public int UpdateFrames;
    public TileView[,] TileMap;

    public bool IsTileUIVisible = false;

    private GlobalUI globalUI;

    public int BankCells = 0;
    public int BankNutrients = 0;
    public int BankThoughts = 0;
    public int BankIdeas = 0;

    // Use this for initialization
    void Start()
    {
        PlayerIsBody = FindObjectOfType<StaticController>().isBody;
        string ip = FindObjectOfType<StaticController>().ip;
        Global.Instance.InitClient(PlayerIsBody, ip);
        TileMap = null;
        StartCoroutine("MapMessagesCoroutine");
        StartCoroutine("GeneralMessagesCoroutine");
        globalUI = FindObjectOfType<GlobalUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            Global.Instance.SendCommand(new BuildParam(new Coma.Common.Position(1, 1), TileItemType.BUILD_AREA_BODY));
        }

    }



    IEnumerator GeneralMessagesCoroutine()
    {
        while (true)
        {
            while (Global.Instance.BankMessages.Count > 0)
            {
                BankMessage bankMessage = Global.Instance.BankMessages.Dequeue();
                SetBank(bankMessage.Cells, bankMessage.Nutrients, bankMessage.Thoughts, bankMessage.Ideas);

            }
            while (Global.Instance.CameraMessages.Count > 0)
            {
                CameraMessage cameraMessage = Global.Instance.CameraMessages.Dequeue();
                GetComponent<InputControl>().MoveCamera(cameraMessage.X, cameraMessage.Y);

            }
            yield return null;
        }
    }

    IEnumerator MapMessagesCoroutine()
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
                        if (counter == maxElementsPerUpdate)
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

    public void RequestForBuild(TileItemType item, int x, int y)
    {
        if (x >= 0 && x < TileMap.GetLength(0) && y >= 0 && y < TileMap.GetLength(1))
        {
            Global.Instance.SendCommand(new BuildParam(new Coma.Common.Position(x, y), item));
            TileView tile = TileMap[x, y];
            if (tile.currentTile.Item != null || tile.currentTile.Item.ItemType == TileItemType.NONE)
            {
                //Add temp construct image
            }
        }
    }

    public void DisableTileUI()
    {
        IsTileUIVisible = false;
        foreach (var tile in TileMap)
        {
            if (tile != null)
                tile.DisableTileUI();
        }
    }

    public void EnableTileUI()
    {
        IsTileUIVisible = true;
        foreach (var tile in TileMap)
        {
            if (tile != null)
                tile.EnableTileUI();
        }
    }

    private void SetBank(int cells, int nutrients, int thoughts, int ideas)
    {
        BankCells = cells;
        BankNutrients = nutrients;
        BankThoughts = thoughts;
        BankIdeas = ideas;
        globalUI.UpdateBankAccounts(cells, nutrients, thoughts, ideas);
    }

    private void OnApplicationQuit()
    {
        Debug.Log("quit");
        Global.Instance.Client.Disconnect(Global.Instance.PlayerId.ToString());
    }
}
