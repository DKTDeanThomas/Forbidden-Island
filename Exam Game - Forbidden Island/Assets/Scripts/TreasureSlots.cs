using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TreasureSlots : MonoBehaviour
{
    public static TreasureSlots tsInstance;
    public GameObject[] playerPanels;
    public Image[] cardSlots;

    public void Start()
    {
        tsInstance = this;
    }
    public void SetCardImage(Sprite card, int playerIndex, int slotIndex)
    {
        GameObject panel = playerPanels[playerIndex];
        cardSlots = panel.GetComponentsInChildren<Image>();

        if (slotIndex >= 0 && slotIndex < cardSlots.Length)
        {
            cardSlots[slotIndex].sprite = card;
        }
    }

    public void RemoveCardImage(Sprite card, int playerIndex)
    {
        GameObject panel = playerPanels[playerIndex];
        Image[] cardSlots = panel.GetComponentsInChildren<Image>();

        for (int i = 0; i < cardSlots.Length; i++)
        {
            if (cardSlots[i].sprite == card)
            {
                cardSlots[i].sprite = null;
                break;
            }
        }
    }

}

