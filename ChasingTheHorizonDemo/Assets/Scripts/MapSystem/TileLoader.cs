using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileLoader : MonoBehaviour
{
    public Tile tile;

    public SpriteRenderer spriteRenderer;
    private Sprite originalSprite;
    [SerializeField]
    private Sprite highlightSprite;

    public string tileName;
    public int tileCost;
    public bool walkable;
    public bool occupied = false;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalSprite = GetComponent<SpriteRenderer>().sprite;
    }

    private void Start()
    {
        UpdateOccupationStatus();

        tileName = tile.tileName;
        tileCost = tile.tileCost;
    }

    public void HighlightTile()
    {
        walkable = true;
        spriteRenderer.sprite = highlightSprite;
    }

    public void ResetTiles()
    {
        walkable = false;
        spriteRenderer.sprite = originalSprite;        
    }

    public void UpdateOccupationStatus()
    {
        occupied = false;
        foreach(UnitLoader unit in FindObjectsOfType<UnitLoader>())
        {
            if(transform.position == unit.transform.position)
            {
                occupied = true;
            }
        }
    }
}
