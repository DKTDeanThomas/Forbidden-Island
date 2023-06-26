using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Tile", menuName = "Tiles")]

public class Tile : ScriptableObject
{
    public string tileName;
    public Sprite tileImage;
    public Sprite tileImageFlooded;
    public Sprite tileImageSunk;

    public bool isFoolsLanding;

    public bool canMoveto;

    public enum TileState {normal, flooded, sunk}
    public TileState state;

    public enum TileType {normal, gate, treasure}
    public TileType type;

    public enum PlayerType {none, diver, navigator, pilot, messenger, explorer, engineer}
    public PlayerType playerType;

    public enum TreasureType {none, earthStone, windStatue, fireCrystal, oceanChalice}
    public TreasureType treasure;

    public int ColNum;
    public int RowNum;

    public Vector2 tilePos;









}
