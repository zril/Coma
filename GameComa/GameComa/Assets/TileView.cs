using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Coma.Common.Map;
using Coma.Common.Map.Item;

public class TileView : MonoBehaviour {

    [SerializeField]
    public Tile currentTile;

    public Main MainController;

    public TileUI currentTileUI;

    public SpriteRenderer TileItemRenderer;
    public SpriteRenderer TileRenderer;

    public Transform TileUICanvas;
    
	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {

    }

    private void OnBecameVisible()
    {

        UpdateUI();
        currentTileUI.gameObject.SetActive(true);
    }

    private void OnBecameInvisible()
    {
        currentTileUI.gameObject.SetActive(false);
    }

    public void UpdateTile(Tile tile)
    {
        this.currentTile = tile;
        UpdateDisplay();
    }

    public void UpdateDisplay()
    {
        TileItem item = null;

        TileRenderer.sprite = MainController.TileSprites[(int)currentTile.Type];
        if(currentTile == null || currentTile.Type == TileType.NONE)
        {
            // Erase all
            TileItemRenderer.sprite = null;
        }
        else
        {
            if(currentTile.Item == null || currentTile.Item.ItemType == TileItemType.NONE)
            {
                // Remove Tileitem sprites
                TileItemRenderer.sprite = null;
            }
            else
            {
                // Replace with constant TileItem
                item = TileItemInfo.Get(currentTile.Item.ItemType);
                TileItemRenderer.sprite = MainController.TileItemSprites[(int)item.Fonction];
                TileItemRenderer.color = MainController.TileItemColors[(int)item.SynergyMode];
            }
        }

        if(TileItemRenderer.isVisible)
        {
            currentTileUI.UpdateDisplay(item);
        }
    }

    void UpdateUI()
    {
        if (currentTileUI == null)
        {
            TileUICanvas = GameObject.FindGameObjectWithTag("TileUICanvas").transform;
            GameObject tileUIObj = Instantiate<GameObject>(MainController.TileUIPrefab, transform.localPosition, Quaternion.identity, TileUICanvas);
            currentTileUI = tileUIObj.GetComponent<TileUI>();
        }

        TileItem item = null;
        if (currentTile != null && currentTile.Type != TileType.NONE && currentTile.Item != null && currentTile.Item.ItemType != TileItemType.NONE)
        {
            item = TileItemInfo.Get(currentTile.Item.ItemType);
        }

        currentTileUI.UpdateDisplay(item);
    }
}
