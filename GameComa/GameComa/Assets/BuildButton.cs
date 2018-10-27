using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Coma.Common.Map.Item;

public class BuildButton : MonoBehaviour {

    public TileItemType ItemType;

    private InputControl InputController;

	// Use this for initialization
	void Start () {
        InputController = FindObjectOfType<InputControl>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Select()
    {
        InputController.SelectForBuild(ItemType);
    }
}
