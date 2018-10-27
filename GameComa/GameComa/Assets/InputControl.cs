using Coma.Common.Map.Item;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
            MainCamera.cullingMask |= 1 << LayerMask.NameToLayer("TileUIAlt");
        }

        if (Input.GetKeyUp(KeyCode.LeftAlt))
        {
            MainCamera.cullingMask &= ~(1 << LayerMask.NameToLayer("TileUIAlt"));
        }

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            MainCamera.cullingMask |= 1 << LayerMask.NameToLayer("TileUI");
        }

        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            MainCamera.cullingMask &= ~(1 << LayerMask.NameToLayer("TileUI"));
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            MainCamera.transform.position += new Vector3(0, 1) * Time.deltaTime * CameraSpeed;
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            MainCamera.transform.position += new Vector3(0, -1) * Time.deltaTime * CameraSpeed;
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            MainCamera.transform.position += new Vector3(-1, 0) * Time.deltaTime * CameraSpeed;
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            MainCamera.transform.position += new Vector3(1, 0) * Time.deltaTime * CameraSpeed;
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

        if (Input.GetMouseButtonDown(0) && SelectedBuildItem != TileItemType.NONE)
        {
            //RequestForBuild
            MainController.RequestForBuild(SelectedBuildItem, (int)SelectImage.position.x, (int)SelectImage.position.y);
            SelectedBuildItem = TileItemType.NONE;
            SelectImage.GetComponent<Image>().sprite = null;
        }

    }


    public void SelectForBuild(TileItemType type)
    {
        TileItem item = TileItemInfo.Get(type);
        SelectImage.GetComponent<Image>().sprite = MainController.TileItemSprites[(int)type];
        SelectedBuildItem = type;
    }



}
