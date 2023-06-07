using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TileManager : MonoBehaviour
{
    public static TileManager tInstance;
    public Tile tile;

    public Transform TilePanel;
    public GameObject TileInst;

    public List<Tile> gameTiles = new List<Tile>(); 
    public List<Tile> boardTiles = new List<Tile>();

    void Start()
    {
        tInstance = this;

        ShuffleTiles();

        TransfertoBoard();

        SpawnTiles();

    }



    public void SpawnTiles()
    {
        for (int i = 0; i < boardTiles.Count; i++)
        {
            GameObject spwn = Instantiate(TileInst, TilePanel);

            var TileImage = spwn.transform.Find("TileImg").GetComponent<Image>();

            if (boardTiles[i] != null && boardTiles[i].tileImage != null)
            {
                TileImage.sprite = boardTiles[i].tileImage;
            }
            else
            {
                TileImage.sprite = null;

            }
        }
    }


    public void ShuffleTiles()
    {
        int n = gameTiles.Count;
        while (n > 1)
        {
            n--;
            int k = Random.Range(0, n + 1);
            Tile value = gameTiles[k];
            gameTiles[k] = gameTiles[n];
            gameTiles[n] = value;
        }
    }

    public void TransfertoBoard()
    {
        bool skipAssignment = false;


        for (int i = 0; i < gameTiles.Count; i++)
        {
            for (int x = 0; x < boardTiles.Count; x++)
            {
                if (x == 0 || x == 5 || x == 30 || x == 35)
                {
                   // Debug.Log("treasure goes here");
                    skipAssignment = true;
                }
                else if (x == 1 || x == 4 || x == 6 || x == 11 || x == 24 || x == 29 || x == 31 || x == 34)
                {
                   // Debug.Log("skip these");
                    boardTiles[x] = null;
                }
                else if (!skipAssignment)
                {
                    boardTiles[x] = gameTiles[i];
                    i++;
                }

                skipAssignment = false;
            }
        }
    }

}



