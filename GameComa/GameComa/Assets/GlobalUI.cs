using Coma.Common.Map.Item;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class GlobalUI : MonoBehaviour {

    public List<TileItemType> ItemTypes;
    public Transform ButtonContainer;
    public int maxItemsPerRow;
    public GameObject ButtonPrefab;

    public TextMeshProUGUI BodyBankText;
    public TextMeshProUGUI SoulBankText;

    private int DisplayBankCells = 0;
    private int DisplayBankNutrients = 0;
    private int DisplayBankThoughts = 0;
    private int DisplayBankIdeas = 0;

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
	void Update ()
    {
        string BodyBank = "€ " + ("" + DisplayBankCells).PadLeft(5, '0') + "   -   $ " + ("" + DisplayBankNutrients).PadLeft(5, '0');
        string SoulBank = ("" + DisplayBankIdeas).PadLeft(5, '0') + " ¤   -   " + ("" + DisplayBankThoughts).PadLeft(5, '0') + " £";
        BodyBankText.text = BodyBank;
        SoulBankText.text = SoulBank;
    }
    
    public void UpdateBankAccounts(int cells, int nutrients, int thoughts, int ideas)
    {
        DOTween.To(() => DisplayBankCells, x => DisplayBankCells = x, cells, 0.5f);
        DOTween.To(() => DisplayBankNutrients, x => DisplayBankNutrients = x, nutrients, 0.5f);
        DOTween.To(() => DisplayBankThoughts, x => DisplayBankThoughts = x, thoughts, 0.5f);
        DOTween.To(() => DisplayBankIdeas, x => DisplayBankIdeas = x, ideas, 0.5f);
    }

}
