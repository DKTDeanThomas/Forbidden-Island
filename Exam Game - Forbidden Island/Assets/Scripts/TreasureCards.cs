using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TreasureCards : MonoBehaviour
{

    [SerializeField]
    public Sprite[] treasureCardsSprite;

    [SerializeField]
    private Image treasureCardImage;

    public int maxTreasureDrawCount = 30;
    private int treasureDrawCount = 0;

    private WaterLevelScript _waterLevelScript;

    public Sprite DrawTreasureCard()
    {
        if(treasureDrawCount >= maxTreasureDrawCount)
        {
            Debug.Log("Treasure card limit reached");
            return null;
        }
        int Index = Random.Range(0, treasureCardsSprite.Length);
        treasureDrawCount++;
        return treasureCardsSprite[Index];
    }

    private void Start()
    {
        Button treasureDrawbutton = GetComponentInChildren<Button>();
    }

    public void DrawTreasureOnClick()
    {
        Sprite cardDrawn = DrawTreasureCard();
        treasureCardImage.sprite = cardDrawn;

        if(cardDrawn != null)
        {
            Debug.Log("Player Drew:" + cardDrawn.name);
        }

        if(cardDrawn.name == "Water Levels Rise")
        {
            _waterLevelScript = FindObjectOfType<WaterLevelScript>();
            if(_waterLevelScript != null)
            {
                _waterLevelScript.IncreaseWaterLevel(1);
            }
        }
    }



}
