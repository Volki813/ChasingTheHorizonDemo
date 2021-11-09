using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Tile", menuName = "MapTile")]
public class Tile : ScriptableObject
{
    public string tileName;
    public int tileCost;
    public bool walkable;

    public int defenseBonus;
    public int avoidBonus;
}
