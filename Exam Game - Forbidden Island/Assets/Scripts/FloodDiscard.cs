using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloodDiscard : MonoBehaviour
{

    public Image discardPileImage;

    private List<Sprite> discardPile = new List<Sprite>();

    public void AddToDiscard(Sprite card)
    {
        discardPile.Add(card);
        UpdateDiscardPile();
    }

    private void UpdateDiscardPile()
    {
        if (discardPile.Count > 0)
        {
            discardPileImage.sprite = discardPile[discardPile.Count - 1];
        }
    }
}
