using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Tile", menuName = "Tiles")]

public class Tile : ScriptableObject
{

    public Sprite tileImage;
    public Sprite tileImageFlooded;
    public Sprite tileImageSunk;

    public bool isFoolsLanding;

    public enum TileState {normal, flooded, sunk}
    [HideInInspector] public TileState state;

    public enum TileType {normal, gate, treasure}


    public TileType type;

    public enum TreasureType {none, earthStone, windStatue, fireCrystal, oceanChalice}
    public TreasureType treasure;


}
