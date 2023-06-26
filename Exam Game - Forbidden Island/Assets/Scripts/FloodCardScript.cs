using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloodCardScript : MonoBehaviour
{
    public static TileManager tInstance;

    [SerializeField]
    public Sprite[] floodCardSprites;

    [SerializeField]
    private Image cardImage;

    private string cardName;

    public int DrawLimit;
    public int drawCount = 0;

    private WaterLevelScript waterlevelScript;
    public bool hasInitialDraw = false;



    private void Start()
    {
        waterlevelScript = FindObjectOfType<WaterLevelScript>();
        if (waterlevelScript != null)
        {
            DrawLimit = 6;
            waterlevelScript.OnWaterLevelChanged += UpdateDrawLimit;
        }
        Button drawButtom = GetComponentInChildren<Button>();
        // drawButtom.onClick.AddListener(DrawCardOnClick);

        for (int i = 0; i < 6; i++)
        {
            DrawCardOnClick();
        }
        hasInitialDraw = true;

    }


    public Sprite DrawCard()
    {


        if (drawCount >= DrawLimit)
        {
            Debug.Log("Draw limit has been reached");
            hasInitialDraw = true;
            return null;

        }
        int randomIndex = Random.Range(0, floodCardSprites.Length);
        Sprite drawnCard = floodCardSprites[randomIndex];

        // drawCount++;
        return drawnCard;
    }


    public void DrawCardOnClick()
    {
        if (drawCount >= DrawLimit)
        {
            Debug.Log("Draw limit has been reached");
            hasInitialDraw = true;
            return;
        }

        Sprite drawnCard = DrawCard();
        cardImage.sprite = drawnCard;

        cardName = drawnCard.name;

        if (drawnCard != null)
        {
            TileManager.tInstance.TileFlood(cardName);
        }

        // Increment draw count after successfully drawing a card
        drawCount++;

        if (drawCount >= DrawLimit)
        {
            hasInitialDraw = true;
            ResetCount();
        }
    }


    public void UpdateDrawLimit(int waterlevel)
    {
        DrawLimit = waterlevel;
    }

    public void ResetCount()
    {
        drawCount = 0;
        DrawLimit = waterlevelScript.GetWaterLevel();
    }
}
