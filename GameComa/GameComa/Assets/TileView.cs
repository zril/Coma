using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Coma.Common.Map;

public class TileView : MonoBehaviour {

    public Tile currentTile;
    public GameObject TileItem;


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
        TileRenderer.sprite = TileSprites[(int)currentTile.tileType];
        if(currentTile.tileType == TileType.NONE)
        {
            // Erase all
            TileItemRenderer.sprite = null;
        }
        else
        {
            if(currentTile.tileItem == null)
            {
                // Remove Tileitem sprites
                TileItemRenderer.sprite = null;
            }
            else
            {
                // Replace with constant TileItem
                TileItem item = null;
                TileItemRenderer.sprite = TileItemSprites[0];
                TileItemRenderer.color = TileItemColors[0];
            }
        }
    }
}
