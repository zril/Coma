using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Coma.Common.Map;
using Coma.Common.Map.Item;

public class TileView : MonoBehaviour {

    [SerializeField]
    public Tile currentTile;

    public Main MainController;

    public SpriteRenderer TileItemRenderer;
    public SpriteRenderer TileRenderer;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void UpdateTile(Tile tile)
    {
        this.currentTile = tile;
        UpdateDisplay();
    }

    public void UpdateDisplay()
    {
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
                TileItem item = TileItemInfo.Get(currentTile.Item.ItemType);
                TileItemRenderer.sprite = MainController.TileItemSprites[(int)item.Fonction];
                TileItemRenderer.color = MainController.TileItemColors[(int)item.SynergyMode];
            }
        }
    }
}
