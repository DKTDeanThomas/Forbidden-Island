using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Snapto : MonoBehaviour
{
    public static TileManager tInstance;
    public int indexnum;
    public static TokenMovement tmInst;

    public void Moveto()
    {

        foreach (var player in TileManager.tInstance.players)
        {
            if (player.GetComponent<Player>().isCurrentPlayer)    
            
            {

                player.transform.position = gameObject.GetComponent<Transform>().position;

                for (int i = 0; i < TileManager.tInstance.spawnedTiles.Length; i++)
                {
                    if (gameObject.transform.position == TileManager.tInstance.spawnedTiles[i].transform.position)
                    {
                        indexnum = i;
                    }
                }


                player.GetComponent<TokenMovement>().PositionUpdate(indexnum);
                player.GetComponent<TokenMovement>().DeselectToken();

            }


        }


    }
}



