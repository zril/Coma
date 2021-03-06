﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Coma.Common.Map.Item;
using TMPro;
using UnityEngine.UI;

public class BuildButton : MonoBehaviour
{

    public TileItemType ItemType;
    public TileItem Item;
    public TextMeshProUGUI Text;
    public Image ButtonImage;

    private InputControl InputController;
    private Main MainController;

    // Use this for initialization
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Select()
    {
        InputController.SelectForBuild(ItemType);
    }

    public void Delete()
    {
        InputController.SelectForDelete(ButtonImage.sprite);
    }

    private void Init()
    {
        InputController = FindObjectOfType<InputControl>();
        MainController = FindObjectOfType<Main>();
    }

    public void UpdateDisplay()
    {
        Init();

        if (Item == null)
        {
            Item = TileItemInfo.Get(ItemType);
        }

        ButtonImage.sprite = MainController.TileItemSprites[(int)Item.Fonction];
        ButtonImage.color = MainController.TileItemColors[(int)Item.Synergy];

        string cost = "";
        if (Item.CostCells > 0)
            cost += Item.CostCells + "C  ";

        if (Item.CostThoughts > 0)
            cost += Item.CostThoughts + "T";

        if (cost != "")
            cost = cost.Trim() + "\n";

        if (Item.CostNutrients > 0)
            cost += Item.CostNutrients + "N  ";

        if (Item.CostIdeas > 0)
            cost += Item.CostIdeas + "I";

        Text.text = cost.Trim();
    }
}
