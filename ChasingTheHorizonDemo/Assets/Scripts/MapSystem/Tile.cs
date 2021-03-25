using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Tile", menuName = "MapTile")]
public class Tile : ScriptableObject
{
    [SerializeField] private Sprite sprite;
    [SerializeField] private int tileCost;
}
