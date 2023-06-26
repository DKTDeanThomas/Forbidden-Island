using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager;
    public static TileManager tInstance;
    public static UI uiInstance;

    /* public GameObject diverToken;
     public GameObject pilotToken;
     public GameObject explorerToken; */

    public TreasureSlots treasureSlots;

    private GameObject currentToken;
    public Player currentPlayer;
    private TreasureCards treasureCards;

    //public Player[] players;
    private Player selectedPlayer;
    private Sprite selectedCard;

    public bool isplayer1turn = true;
    public bool isplayer2turn = false;
    void Start()
    {
        gameManager = this;
              
        treasureCards = FindObjectOfType<TreasureCards>();

        treasureSlots = FindObjectOfType<TreasureSlots>();
         
    }

    public void SetCurrentToken()
    {
        foreach (var player in TileManager.tInstance.players)
        {
            if (player.GetComponent<Player>().isCurrentPlayer)
            {
                currentToken = player;
                
                currentPlayer = currentToken.GetComponent<Player>();
            }


        }
    }
    public void EnableCurrentToken()
    {
        currentToken.GetComponent<Player>().isCurrentPlayer = true;
        currentToken.GetComponent<TokenMovement>().enabled = true;
        currentToken.GetComponent<BoxCollider2D>().enabled = true;

        currentToken.GetComponent<Player>().UpdateCurrent(true);

        DisableOtherTokens();

        //treasureCards.DrawTreasureOnClick(currentToken.GetComponent<Player>());
    }

    private void DisableCurrentToken()
    {
        currentToken.GetComponent<TokenMovement>().enabled = false;
        currentToken.GetComponent<BoxCollider2D>().enabled = false;
        currentToken.GetComponent<Player>().UpdateCurrent(false);
    }

    private void DisableOtherTokens()
    {
        foreach (var player in TileManager.tInstance.players)
        {
            if (player.GetComponent<Player>().isCurrentPlayer == false)
            {
                player.GetComponent<TokenMovement>().enabled = false;
                player.GetComponent<BoxCollider2D>().enabled = false;
            }
        }
    }

    public void NextTurn()
    {
        SwitchTurn();

    }

    private void SwitchTurn()
    {

        if (TileManager.tInstance.players[0].GetComponent<Player>().isCurrentPlayer == true)
        {

            TileManager.tInstance.players[0].GetComponent<TokenMovement>().enabled = false;
            TileManager.tInstance.players[0].GetComponent<BoxCollider2D>().enabled = false;

            currentToken = TileManager.tInstance.players[1];

            
            TileManager.tInstance.players[0].GetComponent<Player>().UpdateCurrent(false);

            TileManager.tInstance.players[1].GetComponent<TokenMovement>().moveCount = 0;
            TileManager.tInstance.players[1].GetComponent<Player>().UpdateCurrent(true);

            Debug.Log(TileManager.tInstance.players[0].GetComponent<Player>().playerNum + " is now disabled");

            currentPlayer = currentToken.GetComponent<Player>();
            Debug.Log(currentPlayer.playerType);

            UI.uiInstance.TurnToggle(true);



        }


        else if (TileManager.tInstance.players[1].GetComponent<Player>().isCurrentPlayer == true)
        {

            TileManager.tInstance.players[1].GetComponent<TokenMovement>().enabled = false;
            TileManager.tInstance.players[1].GetComponent<BoxCollider2D>().enabled = false;

            currentToken = TileManager.tInstance.players[0];

            TileManager.tInstance.players[1].GetComponent<Player>().UpdateCurrent(false);

            TileManager.tInstance.players[0].GetComponent<TokenMovement>().moveCount = 0;
            TileManager.tInstance.players[0].GetComponent<Player>().UpdateCurrent(true);

            Debug.Log(TileManager.tInstance.players[0].GetComponent<Player>().playerNum + " is now disabled");

            currentPlayer = currentToken.GetComponent<Player>();
            Debug.Log(currentPlayer.playerType);

            UI.uiInstance.TurnToggle(false);

        }

        TileManager.tInstance.DisableHands();
        EnableCurrentToken();
    }

    public void InitializeAll()
    {
        TileManager.tInstance.players[0].GetComponent<Player>().Initialize(0);
        TileManager.tInstance.players[1].GetComponent<Player>().Initialize(1);
    }

    public void DrawTreasureCard()
    {
        treasureCards.DrawTreasureOnClick(currentPlayer);
    }

    public void SelectPlayer(Player player)
    {
        selectedPlayer = player;
    }

    public void SelectCard(Sprite card)
    {
        selectedCard = card;
    }

     public void TransferCard(Player fromPlayer, Player toPlayer)
     {
         if (fromPlayer != null && toPlayer != null && selectedCard != null)
         {
             if (fromPlayer.HasCard(selectedCard))
             {
                 fromPlayer.RemoveCard(selectedCard);
                 toPlayer.AddDrawnCard(selectedCard);
                 selectedCard = null;
                 Debug.Log("Card transferred from Player " + fromPlayer.playerIndex + " to Player " + toPlayer.playerIndex);
             }
             else
             {
                 Debug.Log("Player " + fromPlayer.playerIndex + " does not have the selected card.");
             }
         }
     }

}


