using Coma.Common.Map.Item;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GlobalUI : MonoBehaviour {

    public List<TileItemType> ItemTypes;
    public Transform ButtonContainer;
    public int maxItemsPerRow;
    public GameObject ButtonPrefab;

	// Use this for initialization
	void Start () {

        int buttonRow = 0;
        int buttonColumn = 0;
        int paddings = 0;

		foreach(TileItemType type in ItemTypes)
        {

            if(type == TileItemType.NONE)
            {
                buttonRow++;
                buttonColumn = 0;
                paddings++;
            }
            else
            {
                if(buttonColumn >= maxItemsPerRow)
                {
                    buttonRow++;
                    buttonColumn = 0;
                }

                int x = buttonColumn * 75;
                int y = buttonRow * 85 + 25 * paddings;

                GameObject btnObject = Instantiate<GameObject>(ButtonPrefab, ButtonContainer);
                BuildButton btn = btnObject.GetComponent<BuildButton>();
                btn.transform.localPosition = new Vector3(x, -y);
                btn.ItemType = type;
                btn.UpdateDisplay();

                buttonColumn++;
            }
        }
	}
	
	// Update is called once per frame
	void Update () {

	}
    
    

}
