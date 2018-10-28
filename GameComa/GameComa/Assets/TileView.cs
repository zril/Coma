using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Coma.Common.Map;
using Coma.Common.Map.Item;
using DG.Tweening;

public class TileView : MonoBehaviour
{

    [SerializeField]
    public Tile currentTile;

    public Main MainController;

    public TileUI currentTileUI;

    public SpriteRenderer TileItemRenderer;
    public SpriteRenderer TileRenderer;
    public SpriteRenderer TileConstructRenderer;
    public SpriteRenderer TileInfluCoverRenderer;
    public SpriteRenderer TileInfluValueRenderer;

    public Color TileColorBody;
    public Color TileColorSoul;
    public Color TileColorInfluOKBody;
    public Color TileColorInfluOKSoul;
    public Color TileColorInfluKOBody;
    public Color TileColorInfluKOSoul;
    public Color TileColorSynergyBody;
    public Color TileColorSynergySoul;

    public float ColorCapValue;

    public Transform TileUICanvas;

    public GameObject SynergyMarkerPrefab;
    private GameObject SynergyMarker;
    private Tween SynergyTween;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnBecameVisible()
    {
        if (MainController.IsTileUIVisible)
        {
            UpdateUI();
            currentTileUI.gameObject.SetActive(true);
        }
    }

    private void OnBecameInvisible()
    {
        DisableTileUI();
    }

    public void UpdateTile(Tile tile)
    {
        this.currentTile = tile;
        UpdateDisplay();
    }

    public void DisableTileUI()
    {
        if (currentTileUI != null)
            currentTileUI.gameObject.SetActive(false);
    }

    public void EnableTileUI()
    {
        if (TileRenderer.isVisible)
        {
            UpdateUI();
            currentTileUI.gameObject.SetActive(true);
        }
    }

    public void UpdateDisplay()
    {
        TileItem item = null;

        TileRenderer.sprite = MainController.TileSprites[(int)currentTile.Type];
        TileRenderer.color = MainController.PlayerIsBody ? TileColorBody : TileColorSoul;
        if (currentTile == null || currentTile.Type == TileType.NONE)
        {
            // Erase all
            TileItemRenderer.sprite = null;

            Color consColor = TileConstructRenderer.color;
            consColor.a = 0;
            TileConstructRenderer.color = consColor;

            Color influColor = TileInfluCoverRenderer.color;
            influColor.a = 0;
            TileInfluCoverRenderer.color = influColor;

            TileInfluValueRenderer.color = Color.clear;
        }
        else
        {
            if (currentTile.Item == null || currentTile.Item.ItemType == TileItemType.NONE)
            {
                if (SynergyMarker != null)
                {
                    Destroy(SynergyMarker);
                }
                // Remove Tileitem sprites
                if (TileItemRenderer.sprite != null)
                {
                    TileItemRenderer.DOFade(0f, 0.5f).onComplete = () =>
                     {
                         TileItemRenderer.sprite = null;
                     };
                }
                else
                {
                    TileItemRenderer.sprite = null;
                }
            }
            else
            {
                // Replace with constant TileItem
                item = TileItemInfo.Get(currentTile.Item.ItemType);

                bool wasEmpty = TileItemRenderer.sprite == null;
                Color color = Color.white;
                TileItemRenderer.sprite = MainController.TileItemSprites[(int)item.Fonction];
                if(wasEmpty)
                {
                    color.a = 0f;
                }
                TileItemRenderer.color = color;
                var tween = TileItemRenderer.DOFade(1f, 0.5f);

                if(currentTile.Item.Synergy > 0 && SynergyMarker == null)
                {
                    //DoTween + Instantiate marker
                    color.a = 1f;
                    tween.OnComplete(() =>
                    {
                        SynergyTween = TileItemRenderer.DOColor(MainController.PlayerIsBody ? TileColorSynergyBody : TileColorSynergySoul, .5f).SetLoops(-1, LoopType.Yoyo);
                    });
                    SynergyMarker = Instantiate<GameObject>(SynergyMarkerPrefab, transform);
                    SynergyMarker.GetComponent<SynergyMarker>().SynergyMode = item.SynergyMode;
                    Debug.Log(item.SynergyMode);
                }
                else if(currentTile.Item.Synergy == 0 && SynergyMarker != null)
                {
                    // Stop Tweening + Destroy marker
                    SynergyTween.Complete();
                    //Destroy(SynergyMarker);
                    TileItemRenderer.color = color;
                }

            }
            Color consColor = TileConstructRenderer.color;
            consColor.a = currentTile.Contructable ? 0.25f : 0;
            TileConstructRenderer.color = consColor;

            Color influColor = TileInfluCoverRenderer.color;
            influColor.a = currentTile.Radiance ? 0.25f : 0;
            TileInfluCoverRenderer.color = influColor;

            Color influValueColor = Color.black;

            float alpha = 0;
            if (currentTile.Influence > 0)
            {
                influValueColor = MainController.PlayerIsBody ? TileColorInfluOKBody : TileColorInfluOKSoul;
                alpha = 0.05f + 0.85f * Mathf.Min(1f, Mathf.Abs(currentTile.Influence / ColorCapValue));
            }
            else if (currentTile.Influence < 0)
            {
                influValueColor = MainController.PlayerIsBody ? TileColorInfluKOBody : TileColorInfluKOSoul;
                alpha = 0.35f + 0.5f * Mathf.Min(1f, Mathf.Abs(currentTile.Influence / ColorCapValue));
            }

            influValueColor.a = alpha;
            TileInfluValueRenderer.DOColor(influValueColor, 0.5f);
        }

        if (TileItemRenderer.isVisible && currentTileUI != null && MainController.IsTileUIVisible)
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
