using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TokenMovement : MonoBehaviour
{
    public static UI uiInstance;
    public static TileManager tInstance;
    public Color selectedColor ;
    private Color originalColor;
    private bool isSelected = false;
    public GameObject token;

    private Vector3 offset;

    public int moveCount = 0;
    public const int maxMoveLimit = 3;

    public Vector2 playerLocation;


    private void Start()
    {
        originalColor = GetComponent<SpriteRenderer>().color;
    }

    private void Update()
    {
        
       /* if (isSelected && Input.GetMouseButtonDown(0))
        {
            if (!IsMouseOverToken())
            {
                DeselectToken();
            }
                MoveTokenWithMouse();          
        }*/
    }

    private void MoveTokenWithMouse()
    {
        if (moveCount < maxMoveLimit)
        {
           // Vector3 newPosition = WorldPosition() + offset;
           //transform.position = newPosition;
            moveCount++;
            UI.uiInstance.UpdateMoveCount(moveCount);
        }
        else
        {
            DeselectToken();
            offset = Vector3.zero;    // prevents movement when limit is reached
            Debug.Log("Player movement limit reached");
        }
    }

    private void OnMouseDown()
    {
        ReceiveLocation();
       
        if (moveCount < 3)
        {
            if (isSelected)
            {
                DeselectToken();
            }

            else if (!isSelected)
            {
                SelectToken();
            }

        }

        else
        {
            DeselectToken();
            offset = Vector3.zero;    // prevents movement when limit is reached
            Debug.Log("Player movement limit reached");
        }
        
    }

    private void SelectToken()
    {
        GetComponent<SpriteRenderer>().color = selectedColor;
        TileManager.tInstance.ShowAdjacentTiles(playerLocation);

        isSelected = true;
        offset = transform.position - WorldPosition();
       
    }

    public void DeselectToken()
    {
        GetComponent<SpriteRenderer>().color = originalColor;
        TileManager.tInstance.DeselectTiles();
        isSelected = false;

        if (moveCount < maxMoveLimit)
        {
            moveCount++;
            UI.uiInstance.UpdateMoveCount(moveCount);
        }

    }

    private bool IsMouseOverToken()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f;
        Collider2D collider = GetComponent<Collider2D>();
        return collider.bounds.Contains(mousePosition);
    }

   

    private Vector3 WorldPosition()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        return mousePosition;
    }

    private void ReceiveLocation()
    {
        playerLocation = gameObject.GetComponent<Player>().playerPos;
    }

    public void PositionUpdate(int indexnum)
    {
        gameObject.GetComponent<Player>().playerPos = TileManager.tInstance.boardTiles[indexnum].tilePos;
        TileManager.tInstance.DeselectTiles();
    }

    public void ResetMoves()
    {
        moveCount = 0;
        UI.uiInstance.UpdateMoveCount(moveCount);
    }
}

