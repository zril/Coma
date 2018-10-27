using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Coma.Common.Map;
using Coma.Common.Map.Item;

public class TileView : MonoBehaviour {

    [SerializeField]
    public Tile currentTile;

    public List<Sprite> TileSprites;
    public List<Sprite> TileItemSprites;
    public List<Color> TileItemColors;

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
        TileRenderer.sprite = TileSprites[(int)currentTile.Type];
        if(currentTile.Type == TileType.NONE)
        {
            // Erase all
            TileItemRenderer.sprite = null;
        }
        else
        {
            if(currentTile.Item == null)
            {
                // Remove Tileitem sprites
                TileItemRenderer.sprite = null;
            }
            else
            {
                // Replace with constant TileItem
                TileItem item = TileItemInfo.Get(currentTile.Item.ItemType);
                TileItemRenderer.sprite = TileItemSprites[(int)item.Fonction];
                TileItemRenderer.color = TileItemColors[(int)item.SynergyMode];
            }
        }
    }
}
