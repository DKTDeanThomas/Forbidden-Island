using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static TileManager tInstance;
    public Vector2 playerPos;

    public enum PlayerNum {Player1, Player2}
    public PlayerNum playerNum;

    public enum PlayerType {none, diver, navigator, pilot, messenger, explorer, engineer }
    public PlayerType playerType;

    public bool isCurrentPlayer;

    public List<Sprite> drawnCards = new List<Sprite>();

    private int currentSlotIndex = 0;

    private TreasureSlots treasureSlots;

    public int playerIndex;

    public int PlayerIndex { get; private set; }

    private GameManager gameManager;




    public void UpdatePlayerType(PlayerType newType)
    {
        playerType = newType;
 
    }

    public void UpdatePlayerNum(PlayerNum newType)
    {
        playerNum = newType;
  
    }

    public void UpdateLocation(Vector2 pos)
    {
        playerPos = pos;
    }

    public void UpdateCurrent(bool izztru)
    {
        if (izztru)
        {
            isCurrentPlayer = true;
        }

        if (!izztru)
        {
            isCurrentPlayer = false;
        }
    }

    public void AddDrawnCard(Sprite card)
    {
        drawnCards.Add(card);

        // treasureSlots.SetCardImage(card, PlayerIndex, currentSlotIndex);
        //currentSlotIndex = (currentSlotIndex + 1) % 5;


    }

    public int GetNextSlotIndex()
    {
        int nextSlotIndex = currentSlotIndex;
        currentSlotIndex = (currentSlotIndex + 1) % 5;
        return nextSlotIndex;
    }

    public void Initialize(int index)
    {
        PlayerIndex = index;
    }

    public void RemoveCard(Sprite card)
    {
        drawnCards.Remove(card);
        //treasureSlots.RemoveCardImage(card, PlayerIndex);
    }

    public bool HasCard(Sprite card)
    {
        return drawnCards.Contains(card);
    }

}
