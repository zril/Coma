using Coma.Common.Map.Item;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class InputControl : MonoBehaviour
{

    public Camera MainCamera;
    public float CameraSpeed;
    public TileItemType SelectedBuildItem = TileItemType.NONE;

    public RectTransform SelectImage;
    public Main MainController;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftAlt))
        {
            MainCamera.cullingMask |= 1 << LayerMask.NameToLayer("TileConstr");
        }

        if (Input.GetKeyUp(KeyCode.LeftAlt) && SelectedBuildItem == TileItemType.NONE)
        {
            MainCamera.cullingMask &= ~(1 << LayerMask.NameToLayer("TileConstr"));
        }

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            MainCamera.cullingMask |= 1 << LayerMask.NameToLayer("TileInflu");
        }

        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            MainCamera.cullingMask &= ~(1 << LayerMask.NameToLayer("TileInflu"));
        }

        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.Z))
        {
            MainCamera.transform.position += new Vector3(0, 1) * Time.deltaTime * CameraSpeed;
        }

        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            MainCamera.transform.position += new Vector3(0, -1) * Time.deltaTime * CameraSpeed;
        }

        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.Q))
        {
            MainCamera.transform.position += new Vector3(-1, 0) * Time.deltaTime * CameraSpeed;
        }

        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            MainCamera.transform.position += new Vector3(1, 0) * Time.deltaTime * CameraSpeed;
        }

        if (Input.mouseScrollDelta.y > 0 && MainCamera.orthographicSize > 4)
        {
            MainCamera.orthographicSize -= 1;
            CameraSpeed -= 1f;
            if (MainCamera.orthographicSize == 4 && !MainController.IsTileUIVisible)
            {
                MainController.EnableTileUI();
            }
        }

        if (Input.mouseScrollDelta.y < 0 && MainCamera.orthographicSize < 25)
        {
            MainCamera.orthographicSize += 1;
            CameraSpeed += 1f;
            if (MainController.IsTileUIVisible)
            {
                MainController.DisableTileUI();
            }
        }


        if (SelectedBuildItem != TileItemType.NONE)
        {
            Vector3 pos = Input.mousePosition;
            pos.z = 5;
            pos = Camera.main.ScreenToWorldPoint(pos);
            pos.x = Mathf.Max(0, Mathf.Min(MainController.TileMap.GetLength(0) - 1, Mathf.FloorToInt(pos.x + 0.5f)));
            pos.y = Mathf.Max(0, Mathf.Min(MainController.TileMap.GetLength(1) - 1, Mathf.FloorToInt(pos.y + 0.5f)));
            SelectImage.position = pos;
        }

        if ((Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.GetKeyDown(KeyCode.Escape)) && SelectedBuildItem != TileItemType.NONE)
        {
            //RequestForBuild
            if (Input.GetMouseButtonDown(0))
            {
                if (SelectedBuildItem == TileItemType.OBSTACLE)
                    SelectedBuildItem = TileItemType.NONE;

                MainController.RequestForBuild(SelectedBuildItem, (int)SelectImage.position.x, (int)SelectImage.position.y);
            }
            SelectedBuildItem = TileItemType.NONE;
            if (!Input.GetKey(KeyCode.LeftAlt))
            {
                MainCamera.cullingMask &= ~(1 << LayerMask.NameToLayer("TileConstr"));
            }
            SelectImage.GetComponent<Image>().sprite = null;
            SelectImage.GetComponent<Image>().color = Color.clear;
        }


    }

    public void MoveCamera(int x, int y)
    {
        MainCamera.transform.DOMove(new Vector3(x, y, -10),1f);
    }

    public void SelectForBuild(TileItemType type)
    {
        MainCamera.cullingMask |= 1 << LayerMask.NameToLayer("TileConstr");
        TileItem item = TileItemInfo.Get(type);
        SelectImage.GetComponent<Image>().sprite = MainController.TileItemSprites[(int)item.Fonction];
        Color col = MainController.TileItemColors[(int)item.Synergy];
        col.a = 0.2f;
        SelectImage.GetComponent<Image>().color = col;
        SelectedBuildItem = type;
    }

    public void SelectForDelete(Sprite spriteToUse)
    {
        SelectImage.GetComponent<Image>().sprite = spriteToUse;
        Color col = Color.red;
        col.a = 0.2f;
        SelectImage.GetComponent<Image>().color = col;
        SelectedBuildItem = TileItemType.OBSTACLE;
    }

}
