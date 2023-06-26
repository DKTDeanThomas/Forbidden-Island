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

    [SerializeField]
    private List<Sprite> availableCards = new List<Sprite>();

    public int maxTreasureDrawCount = 2;
    public int treasureDrawCount = 0;

    private WaterLevelScript _waterLevelScript;

    private Button treasureDrawButton;

    private TreasureSlots treasureSlots;


    private void Start()
    {

        availableCards.AddRange(treasureCardsSprite);

        treasureSlots = FindObjectOfType<TreasureSlots>();
        // Button treasureDrawbutton = GetComponentInChildren<Button>();
        // treasureDrawButton.onClick.AddListener(DrawTreasureOnClick);
    }

    public Sprite DrawTreasureCard()
    {
        if (availableCards.Count == 0)
        {
            Debug.Log("No more treasure cards available");
            return null;
        }

        int index = Random.Range(0, availableCards.Count);
        Sprite cardDrawm = availableCards[index];
        availableCards.RemoveAt(index);
        return cardDrawm;
    }

    public void DrawTreasureOnClick(Player player)
    {
       
        Sprite cardDrawn = DrawTreasureCard();


        if (cardDrawn != null)
        {
            //Debug.Log("Player " + player.playerType + " Drew: " + cardDrawn.name);
            player.AddDrawnCard(cardDrawn);
            treasureSlots.SetCardImage(cardDrawn, player.PlayerIndex, player.GetNextSlotIndex());
        }

        if (cardDrawn.name == "Water Levels Rise")
        {
            _waterLevelScript = FindObjectOfType<WaterLevelScript>();
            if (_waterLevelScript != null)
            {
                _waterLevelScript.IncreaseWaterLevel(1);
            }
        }
        
        treasureDrawCount++;
    }

    public void GiveCardOnClick(Player giver, Sprite card, int playerIndex)
    {
        GameManager gameManager = FindObjectOfType<GameManager>();
        if (gameManager != null)
        {
            // gameManager.GiveTreasureCard(giver, card);
        }
    }


}
