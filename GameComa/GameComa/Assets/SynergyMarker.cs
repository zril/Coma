using Coma.Common.Map.Item;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SynergyMarker : MonoBehaviour {

    private Main MainController;

    public TileItemSynergyMode SynergyMode;
    // Use this for initialization
    void Start () {
        MainController = FindObjectOfType<Main>();
        var SynergyRenderer = GetComponent<SpriteRenderer>();
        SynergyRenderer.sprite = MainController.TileItemSynergySprites[(int)SynergyMode];
        Debug.Log(SynergyMode);
        switch(SynergyMode)
        {
            case TileItemSynergyMode.NONE:
                //Destroy(this.gameObject);
                break;
            default:
                SynergyRenderer.DOFade(.25f, 1.3f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutFlash);
                break;
        }
	}

	// Update is called once per frame
	void Update () {
		
	}
}
