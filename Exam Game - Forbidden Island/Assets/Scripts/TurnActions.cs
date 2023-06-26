using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnActions : MonoBehaviour
{
    public static TurnActions taInstance;
    public static GameManager gameManager;

    public static TileManager tInstance;

    public GameObject selectObj;
    public GameObject selectPrefab;


    public void Start()
    {
        taInstance = this;
    }


    public void EnableTrade()
    {
        if (TileManager.tInstance.players[0].GetComponent<Player>().isCurrentPlayer == true)
        {

            if (TileManager.tInstance.players[0].GetComponent<TokenMovement>().moveCount  < 3  )
            {
                for (int i = 0; i < TileManager.tInstance.player1hand.Length; i++)
                {
                    if (TileManager.tInstance.player1hand[i].GetComponent<Button>() != null)
                    {
                        GameObject tempObject = TileManager.tInstance.player1hand[i];
                        Button btn = tempObject.GetComponent<Button>();


                        btn.enabled = true;
                    }

                    
                }
            }

            else
                Debug.Log("out of moves");



        }



        else if (TileManager.tInstance.players[1].GetComponent<Player>().isCurrentPlayer == true)
        {
            if (TileManager.tInstance.players[1].GetComponent<TokenMovement>().moveCount < 3)
            {

                for (int i = 0; i < TileManager.tInstance.player2hand.Length; i++)
                {
                    if (TileManager.tInstance.player2hand[i].GetComponent<Button>() != null)
                    {
                        GameObject tempObject = TileManager.tInstance.player2hand[i];
                        Button btn = tempObject.GetComponent<Button>();


                        btn.enabled = true;
                    }

                }
            }

            else
                Debug.Log("out of moves");

        }

        
    }









}


