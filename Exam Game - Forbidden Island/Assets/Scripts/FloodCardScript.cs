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

    public int maxDrawLimit = 30;
    private int drawCount = 0;

    public Sprite DrawCard()
    {
        if(drawCount >= maxDrawLimit)
        {
            Debug.Log("Draw limit has been reached");
            return null;    
        }
        int randomIndex = Random.Range(0, floodCardSprites.Length);
        drawCount++;
        return floodCardSprites[randomIndex];     
    }

    private void Start()
    {
        Button drawButtom = GetComponentInChildren<Button>();
       // drawButtom.onClick.AddListener(DrawCardOnClick);

    }

    
   public void DrawCardOnClick()
    {
        Sprite drawnCard = DrawCard();
        cardImage.sprite= drawnCard;

        cardName = drawnCard.name;


        if (drawnCard != null)
        {
            //Debug.Log("Player Drew:" + drawnCard.name);
            TileManager.tInstance.TileFlood(cardName);
        }
            
    }


}
