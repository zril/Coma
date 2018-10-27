using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Coma.Common.Map.Item;

public class TileUI : MonoBehaviour
{

    public TextMeshProUGUI CategoryText;
    public TextMeshProUGUI CategorySynText;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpdateDisplay(TileItem item)
    {
        if (item == null || item.ItemType == TileItemType.NONE)
        {
            CategoryText.text = "" + this.transform.position.x +"," + this.transform.position.y;
            CategorySynText.text = "";
        }
        else
        {
            CategoryText.text = "" + item.Category;
            string catsyns = "";
            if (item.SynergyCaterories != null)
            {
                foreach (var cat in item.SynergyCaterories)
                    catsyns += cat + " ";
            }
            CategorySynText.text = catsyns.Trim();
        }
    }
}
