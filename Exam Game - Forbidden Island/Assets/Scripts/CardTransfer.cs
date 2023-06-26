using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardTransfer : MonoBehaviour
{
    public static TileManager tInstance;
    public static TreasureSlots tsInstance;


    public void TransferCards()
    {

         Image thisCard = gameObject.GetComponent<Image>();
        Debug.Log(thisCard.sprite);


            Player p1 = TileManager.tInstance.players[0].GetComponent<Player>();
            Player p2 = TileManager.tInstance.players[1].GetComponent<Player>();

        for (int x = 0; x < p1.drawnCards.Count; x++)
        {
            if (p1.drawnCards[x] == thisCard.sprite && p2.drawnCards.Count <= 5)
            {
                p2.drawnCards.Add(thisCard.sprite);

                TreasureSlots.tsInstance.SetCardImage(thisCard.sprite, p2.PlayerIndex, p2.GetNextSlotIndex());
                TreasureSlots.tsInstance.RemoveCardImage(thisCard.sprite, p1.PlayerIndex);
            }

        }

        TileManager.tInstance.players[0].GetComponent<TokenMovement>().moveCount++;
        UI.uiInstance.UpdateMoveCount(TileManager.tInstance.players[0].GetComponent<TokenMovement>().moveCount);
        TileManager.tInstance.DisableHands();
    }

    
}
