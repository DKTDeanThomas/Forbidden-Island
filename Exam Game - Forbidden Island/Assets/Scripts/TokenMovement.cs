using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TokenMovement : MonoBehaviour
{
    public Color selectedColor ;
    private Color originalColor;
    private bool isSelected = false;
    public GameObject token;

    private Vector3 offset;

    public int moveCount = 0;
    public int maxMoveLimit = 6;
   

    private void Start()
    {
        originalColor = GetComponent<SpriteRenderer>().color;
        
    }

    private void Update()
    {
        if(isSelected && Input.GetMouseButtonDown(0))
        {
            if(!IsMouseOverToken())
            {
                DeselectToken();
            }

            MoveTokenWithMouse();
        }
    }

    private void OnMouseDown()
    {
        if(!isSelected)
        {
            SelectToken();
        }
    }


    public void SelectToken()
    {
        GetComponent<SpriteRenderer>().color = selectedColor;

        isSelected = true;

        offset = transform.position - WorldPosition();
    }

    void DeselectToken()
    {
        GetComponent<SpriteRenderer>().color = originalColor;
        isSelected = false;


       
    }

    private bool IsMouseOverToken()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint( Input.mousePosition );
        mousePosition.z = 0f;
        Collider2D collider = GetComponent<Collider2D>();
        return collider.bounds.Contains( mousePosition );
    }

    void MoveTokenWithMouse()
    {
       

        if(moveCount < maxMoveLimit)
        {

            Vector3 newPosition = WorldPosition() + offset;
            transform.position = newPosition;
            moveCount++;
        }
        else
        {
            DeselectToken();
            offset = Vector3.zero;    //prevents movement when limit is reached
            Debug.Log("Player movement limit reached");

        }


    }

   private  Vector3 WorldPosition()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        return mousePosition;
    }



   


}
