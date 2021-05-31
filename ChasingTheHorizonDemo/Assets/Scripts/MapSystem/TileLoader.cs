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
    public bool occupied;

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

    private void Update()
    {
        UpdateOccupationStatus();
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

    private void UpdateOccupationStatus()
    {        
        foreach(UnitLoader unit in FindObjectsOfType<UnitLoader>())
        {
            if(unit.transform.position == transform.position)
            {
                occupied = true;
            }
        }
    }
}
