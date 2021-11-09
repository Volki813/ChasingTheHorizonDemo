using UnityEngine;

//TileLoader script loads all the data and functions every tile needs
//Every tile in a scene requires this script to function properly
public class TileLoader : MonoBehaviour
{
    //VARIABLES
    public string tileName = null;
    public int tileCost = 0;
    public bool walkable = true;
    public bool occupied = false;
    
    //REFERENCES
    public Tile tile = null;
    public SpriteRenderer spriteRenderer = null;
    private Sprite originalSprite = null;
    [SerializeField] private Sprite highlightSprite = null;
    [SerializeField] private Sprite enemyHighlight = null;


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


    public void HighlightTile(Unit unit)
    {
        if(unit.allyUnit)
        {
            walkable = true;
            spriteRenderer.sprite = highlightSprite;
        }
        else
        {
            spriteRenderer.sprite = enemyHighlight;
        }
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
