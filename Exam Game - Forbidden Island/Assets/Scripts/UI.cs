using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    public static UI uiInstance;

    public Text p1Choose;

    public GameObject PlayerSelectionScreen;
    public GameObject selectionCanvas;
    public GameObject turnCanvas;

    public GameObject p1Tog;
    public GameObject p2Tog;

    public Text waterlvl;

    public GameObject DiffSelectPanel;

    public GameObject MoveCounter;
    public Text mCount;

    public GameObject maxReached;

    public void UpdateMoveCount(int Movecount)
    {
        if (Movecount == 3)
        {
            maxReached.SetActive(true);
            MoveCounter.SetActive(false);
        }

        else
        {
            string moves = Movecount.ToString();
            mCount.text = moves;
        }
      
    }

    private void Start()
    {
        uiInstance = this;
    }

    public void DiffSelect()
    {
        DiffSelectPanel.SetActive(false);
        SelectionCanvas();
    }

    public void SelectionCanvas()
    {
        selectionCanvas.SetActive(true);
    }

    public void WaterDisplay(int lvl)
    {
        string lvltext = lvl.ToString();
        waterlvl.text = lvltext;
    }

    public void TurnToggle(bool izztru)
    {
        if (izztru)
        {
            p1Tog.GetComponent<Text>().color = Color.red;
            p2Tog.GetComponent<Text>().color = Color.green;
        }

        if (!izztru)
        {
            p1Tog.GetComponent<Text>().color = Color.green;
            p2Tog.GetComponent<Text>().color = Color.red;
        }
    }

    public void playerMovementCanvas(bool set)
    {
        if (set)
        {
            PlayerSelectionScreen.SetActive(true);
        }

        else if (!set)
        {
            PlayerSelectionScreen.SetActive(false);
        }
       
    }

    public void playerChooseText(bool set)
    {
        if (set)
        {
            p1Choose.text = "Player 2 Choose";
 
        }
        
    }

    public void TurnCanvas(bool set)
    {
        if (set)
        {
            turnCanvas.SetActive(true);
        }

        else if (!set)
        {
            turnCanvas.SetActive(false);
        }

    }
}


