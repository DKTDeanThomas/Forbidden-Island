using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;


public class TileManager : MonoBehaviour
{

    public GameObject highlightPrefab;
    private GameObject highlightObject;


    public static TurnActions taInstance;
    public static GameManager gameManager;
    public static TileManager tInstance;
    public static UI uiInstance;
    public Tile tile;
    public Player player;

    public Transform TilePanel;
    public GameObject TileInst;

    public GameObject playerToken;

    public List<Tile> gameTiles = new List<Tile>();
    public List<Tile> boardTiles = new List<Tile>();
    public List<Tile> Treasures = new List<Tile>();

    public Tile placeholderTile;

    public GameObject[] spawnedTiles;
    public List<GameObject> players = new List<GameObject>();
    public List<Sprite> playerCards = new List<Sprite>();

    public bool isP1Inst = false;
    public bool isP2Inst = false;

    public GameObject[] player1hand;
    public GameObject[] player2hand;


    void Start()
    {
        tInstance = this;

       ShuffleTiles();

        TransfertoBoard();

        SpawnTiles();

        ResetTileStates();

        FindAllTiles();

        EnableAll();

        DisableHands();


    }

    public void DisableHands()
    {
        FindAllHands();


        for (int i = 0; i < player1hand.Length; i++)
        {
            if (player1hand[i].GetComponent<Button>() != null)
            {
                GameObject tempObject = player1hand[i];
                Button btn = tempObject.GetComponent<Button>();


                btn.enabled = false;
            }

        }


        for (int i = 0; i < player2hand.Length; i++)
        {
            if (player2hand[i].GetComponent<Button>() != null)
            {
                GameObject tempObject = player2hand[i];
                Button btn = tempObject.GetComponent<Button>();


                btn.enabled = false;
            }

        }

    }
    


    public void EnableAll()
    {
        for (int i = 0; i < spawnedTiles.Length; i++)
        {
            GameObject tempObject = spawnedTiles[i];
            Button btn = tempObject.GetComponent<Button>();


            btn.enabled = false;

        }
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

            boardTiles[i].tilePos = new Vector2( i%6, i/6);



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
        int TI = 0;


        for (int i = 0; i < gameTiles.Count; i++)
        {
            for (int x = 0; x < boardTiles.Count; x++)
            {
                if (x == 0 || x == 5 || x == 30 || x == 35)
                {
                    boardTiles[x] = Treasures[TI];
                    TI++;
                    skipAssignment = true;
                }
                else if (x == 1 || x == 4 || x == 6 || x == 11 || x == 24 || x == 29 || x == 31 || x == 34)
                {
                   // Debug.Log("skip these");
                    boardTiles[x] = placeholderTile;
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

    public void TileFlood(string cardName)
    {
        spawnedTiles = GameObject.FindGameObjectsWithTag("Tile");

        int floodedTileIndex = -1;

        for (int i = 0; i < spawnedTiles.Length; i++)
        {
            var tileImage = spawnedTiles[i].transform.Find("TileImg").GetComponent<Image>();

            if (boardTiles[i] != null && boardTiles[i].tileImage != null && cardName == boardTiles[i].tileName)
            {
                Debug.Log(boardTiles[i].name);

                if (boardTiles[i].state == Tile.TileState.flooded)
                {
                    tileImage.sprite = null; 
                    boardTiles[i].state = Tile.TileState.sunk; 
                }

                if (boardTiles[i].state == Tile.TileState.sunk)
                {
                    tileImage.sprite = null;
                    tileImage.color = Color.clear;
                }

                else
                {
                    tileImage.sprite = boardTiles[i].tileImageFlooded;
                    boardTiles[i].state = Tile.TileState.flooded;
                    floodedTileIndex = i;
                }
            }

        }

        if (floodedTileIndex != -1)
        {
            Debug.Log("Tile is flooded: " + boardTiles[floodedTileIndex].name);
        }
    }

    public void ResetTileStates()
    {
        for (int i = 0; i < boardTiles.Count; i++)
        {
            if (boardTiles[i] != null)
            {
                boardTiles[i].state = Tile.TileState.normal;
            }
        }
    }

    public void FindGate(int ptype)
    {
        GameObject p1PC = GameObject.FindGameObjectWithTag("P1Card");
        GameObject p2PC = GameObject.FindGameObjectWithTag("P2Card");

        switch (ptype)
        {
            case 0:
                for (int i = 0; i < spawnedTiles.Length; i++)
                {
                    if (boardTiles[i] != null && boardTiles[i].tileImage != null && boardTiles[i].playerType == Tile.PlayerType.diver)
                    {
                        if (!isP1Inst)
                        {
                            InstantiatePlayer(Player.PlayerType.diver, Player.PlayerNum.Player1, i, Color.black, true);
                            isP1Inst = true;
                            p1PC.GetComponent<Image>().sprite = playerCards[0];


                        }
                        else if (isP1Inst && CheckPlayerAvailability(0))
                        {
                
                            InstantiatePlayer(Player.PlayerType.diver, Player.PlayerNum.Player2, i, Color.black, false);
                            isP2Inst = true;
                            p2PC.GetComponent<Image>().sprite = playerCards[0];
                        }
                    }
                }
                break;

            case 1:
                for (int i = 0; i < spawnedTiles.Length; i++)
                {
                    if (boardTiles[i] != null && boardTiles[i].tileImage != null && boardTiles[i].playerType == Tile.PlayerType.explorer)
                    {
                        if (!isP1Inst)
                        {
                            InstantiatePlayer(Player.PlayerType.explorer, Player.PlayerNum.Player1, i, Color.green, true);
                            isP1Inst = true;
                            p1PC.GetComponent<Image>().sprite = playerCards[1];


                        }
                        else if (isP1Inst && CheckPlayerAvailability(1))
                        {
                
                            InstantiatePlayer(Player.PlayerType.explorer, Player.PlayerNum.Player2, i, Color.green, false);
                            isP2Inst = true;
                            p2PC.GetComponent<Image>().sprite = playerCards[1];
                        }
                    }
                }
                break;

            case 2:
                for (int i = 0; i < spawnedTiles.Length; i++)
                {
                    if (boardTiles[i] != null && boardTiles[i].tileImage != null && boardTiles[i].playerType == Tile.PlayerType.pilot)
                    {
                        if (!isP1Inst)
                        {
                            InstantiatePlayer(Player.PlayerType.pilot, Player.PlayerNum.Player1, i, Color.blue, true);
                            isP1Inst = true;
                            p1PC.GetComponent<Image>().sprite = playerCards[2];

                        }
                        else if (isP1Inst && CheckPlayerAvailability(2))
                        {
                
                            InstantiatePlayer(Player.PlayerType.pilot, Player.PlayerNum.Player2, i, Color.blue, false);
                            isP2Inst = true;
                            p2PC.GetComponent<Image>().sprite = playerCards[2];
                        }
                    }
                }
                break;

            case 3:
                for (int i = 0; i < spawnedTiles.Length; i++)
                {
                    if (boardTiles[i] != null && boardTiles[i].tileImage != null && boardTiles[i].playerType == Tile.PlayerType.messenger)
                    {
                        if (!isP1Inst)
                        {
                            InstantiatePlayer(Player.PlayerType.messenger, Player.PlayerNum.Player1, i, Color.white, true);
                            isP1Inst = true;
                            p1PC.GetComponent<Image>().sprite = playerCards[3];

                        }
                        else if (isP1Inst && CheckPlayerAvailability(3))
                        {
       
                            InstantiatePlayer(Player.PlayerType.messenger, Player.PlayerNum.Player2, i, Color.white, false);
                            isP2Inst = true;
                            p2PC.GetComponent<Image>().sprite = playerCards[3];
                        }
                    }
                }
                break;

            case 4:
                for (int i = 0; i < spawnedTiles.Length; i++)
                {
                    if (boardTiles[i] != null && boardTiles[i].tileImage != null && boardTiles[i].playerType == Tile.PlayerType.engineer)
                    {
                        if (!isP1Inst)
                        {
                            InstantiatePlayer(Player.PlayerType.engineer, Player.PlayerNum.Player1, i, Color.red, true);
                            isP1Inst = true;
                            p1PC.GetComponent<Image>().sprite = playerCards[4];

                        }
                        else if (isP1Inst && CheckPlayerAvailability(4))
                        {

                            InstantiatePlayer(Player.PlayerType.engineer, Player.PlayerNum.Player2, i, Color.red, false);
                            isP2Inst = true;
                            p2PC.GetComponent<Image>().sprite = playerCards[4];
                        }
                    }
                }
                break;

            case 5:
                for (int i = 0; i < spawnedTiles.Length; i++)
                {
                    if (boardTiles[i] != null && boardTiles[i].tileImage != null && boardTiles[i].playerType == Tile.PlayerType.navigator)
                    {
                        if (!isP1Inst)
                        {
                            InstantiatePlayer(Player.PlayerType.navigator, Player.PlayerNum.Player1, i, Color.yellow, true);
                            isP1Inst = true;
                            p1PC.GetComponent<Image>().sprite = playerCards[5];

                        }
                        else if (isP1Inst && CheckPlayerAvailability(5))
                        {
                            
                            InstantiatePlayer(Player.PlayerType.navigator, Player.PlayerNum.Player2, i, Color.yellow, false);
                            isP2Inst = true;
                            p2PC.GetComponent<Image>().sprite = playerCards[5];
                        }
                    }
                }
                break;

            default:
                break;
        }

        if (isP1Inst)
        {
            UI.uiInstance.playerChooseText(true);
        }

        if (isP1Inst && isP2Inst)
        {
            UI.uiInstance.playerMovementCanvas(false);
            UI.uiInstance.TurnCanvas(true);
        }

        GameManager.gameManager.SetCurrentToken();
        GameManager.gameManager.EnableCurrentToken();
        GameManager.gameManager.InitializeAll();


    }

    private void InstantiatePlayer(Player.PlayerType type, Player.PlayerNum number, int tileIndex, Color color, bool izztru)
    {
        GameObject player = Instantiate(playerToken, new Vector3(spawnedTiles[tileIndex].transform.position.x, spawnedTiles[tileIndex].transform.position.y, 0f), Quaternion.identity);
        player.GetComponent<Player>().UpdatePlayerType(type);
        player.GetComponent<Player>().UpdatePlayerNum(number);
        player.GetComponent<Player>().playerPos = boardTiles[tileIndex].tilePos;
        player.GetComponent<SpriteRenderer>().color = color;
        player.GetComponent<Player>().UpdateCurrent(izztru);
        players.Add(player);
    }

    public void ShowAdjacentTiles(Vector2 pLoc)
    {

        DeselectTiles();


        for (int i = 0; i < boardTiles.Count; i++)
        {
            if (boardTiles[i] != null && boardTiles[i].tileImage != null)
            {
                Vector2 tilePos = boardTiles[i].tilePos;
                Vector2 PosRight = new Vector2(pLoc.x + 1, pLoc.y);
                Vector2 PosLeft = new Vector2(pLoc.x - 1, pLoc.y);
                Vector2 PosUp = new Vector2(pLoc.x, pLoc.y + 1);
                Vector2 PosDown = new Vector2(pLoc.x, pLoc.y - 1);

                if (tilePos == PosRight || tilePos == PosLeft || tilePos == PosUp || tilePos == PosDown)
                {
                    if (boardTiles[i].state == Tile.TileState.sunk)
                    {

                    }
                    else
                    {
                        highlightObject = Instantiate(highlightPrefab, spawnedTiles[i].transform.position, Quaternion.identity);
                        boardTiles[i].canMoveto = true;
                    }

                }

                else
                {
                    boardTiles[i].canMoveto = false;
                }

            }

        }

        CheckMoveability();
    }

    public void DeselectTiles()
    {
        GameObject[] highlightObjects = GameObject.FindGameObjectsWithTag("Highlight");
        foreach (GameObject highlightObject in highlightObjects)
        {
            Destroy(highlightObject);
        }
    }

    public void CheckMoveability()
    {
       

        for (int i = 0; i < spawnedTiles.Length; i++)
        {
            GameObject tempObject = spawnedTiles[i];
            Button btn = tempObject.GetComponent<Button>();

            if (boardTiles[i].canMoveto == false)
            {
                btn.enabled = false;
            }
            
            else
                btn.enabled = true;
        }
    }

    public bool CheckPlayerAvailability(int type)
    {
        Player.PlayerType targetPlayerType;

        switch (type)
        {
            case 0:
                targetPlayerType = Player.PlayerType.diver;
                break;
            case 1:
                targetPlayerType = Player.PlayerType.explorer;
                break;
            case 2:
                targetPlayerType = Player.PlayerType.pilot;
                break;
            case 3:
                targetPlayerType = Player.PlayerType.messenger;
                break;
            case 4:
                targetPlayerType = Player.PlayerType.engineer;
                break;
            case 5:
                targetPlayerType = Player.PlayerType.navigator;
                break;
            default:
                return false; 
        }

        foreach (var player in players)
        {
            if (player.GetComponent<Player>().playerType == targetPlayerType)
            {
                Debug.Log("Already chosen. Please choose another");
                return false;
            }
        }
        return true;
    }

    public void FindAllTiles()
    {
        spawnedTiles = GameObject.FindGameObjectsWithTag("Tile");
    }

    public void FindAllHands()
    {
       player1hand = GameObject.FindGameObjectsWithTag("Hand1");
       player2hand = GameObject.FindGameObjectsWithTag("Hand2");
    }
}




