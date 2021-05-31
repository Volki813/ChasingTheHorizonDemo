using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitLoader : MonoBehaviour
{
    public Unit unit;

    public UnitLoader target;

    [SerializeField] private Transform actionMenuSpawn;
    public Inventory inventory;

    public SpriteRenderer spriteRenderer;
    public bool hasMoved = false;
    public bool hasAttacked = false;
    public bool rested = false;
    public bool attackable = false;

    public GameObject actionMenu;

    public Weapon equippedWeapon;

    [Header("Combat Stats")]
    public int hp;
    public int attack;
    public int attackSpeed;
    public int protection;
    public int resilience;
    public int hit;
    public int avoid;
    public int crit;
    public int vigilance;

    public int hitChance;
    public int critChance;
    public int damage;

    private void Start()
    {
        hp = unit.health;

        spriteRenderer = GetComponent<SpriteRenderer>();

        spriteRenderer.sprite = unit.sprite;
    }
    private void Update()
    {
        attack = unit.strength + equippedWeapon.might;
        attackSpeed = unit.agility - ((unit.strength - equippedWeapon.weight) / 4);
        protection = unit.defense; //plus any shield
        resilience = unit.resistance; //plus any shield
        hit = equippedWeapon.hit + (unit.proficiency / 2) + (unit.motivation / 4);
        avoid = attackSpeed + unit.motivation / 5;
        crit = equippedWeapon.crit + (unit.proficiency / 2) + (unit.motivation / 5);
        vigilance = (unit.proficiency / 3) + (unit.motivation / 5);
    }


    public void Selected()
    {
        GetWalkableTiles();
    }
    public void ResetTiles()
    {
        foreach (TileLoader tile in FindObjectsOfType<TileLoader>())
        {
            tile.ResetTiles();
        }
    }
    public void Move(Vector2 targetPosition)
    {
        StartCoroutine(Movement(targetPosition));
    }
    public void Rest()
    {
        foreach (UnitLoader unit in FindObjectsOfType<UnitLoader>())
        {
            if(unit.unit.allyUnit == false && unit.GetComponent<SpriteRenderer>().color == Color.red)
            {
                unit.GetComponent<SpriteRenderer>().color = Color.white;
            }
        }
        hasMoved = true;
        rested = true;
        GetComponent<SpriteRenderer>().color = Color.grey;
        
    }
    public void Stand()
    {
        hasMoved = false;
        rested = false;
        GetComponent<SpriteRenderer>().color = Color.white;
    }
    public void Attack(UnitLoader enemy)
    {
        CombatManager.instance.EngageAttack(this, enemy);
    }
    private void GetWalkableTiles()
    {
        foreach(TileLoader tile in FindObjectsOfType<TileLoader>())
        {
            if(Mathf.Abs(transform.position.x - tile.transform.position.x) + Mathf.Abs(transform.position.y - tile.transform.position.y) + tile.tileCost <= unit.movement && tile.occupied == false)
            {
                tile.HighlightTile();
            }
        }
    }
    private void GetEnemies()
    {
        foreach(UnitLoader unit in FindObjectsOfType<UnitLoader>())
        {
            if(!unit.unit.allyUnit)
            {
                if(Mathf.Abs(transform.position.x - unit.transform.position.x) + Mathf.Abs(transform.position.y - unit.transform.position.y) <= equippedWeapon.range)
                {
                    unit.AttackableHighlight();
                    target = unit;
                }
            }
        }
    }
    private void AttackableHighlight()
    {
        GetComponent<SpriteRenderer>().color = Color.red;
    }
    private IEnumerator Movement(Vector2 targetPosition)
    {
        while(transform.position.x != targetPosition.x)
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(targetPosition.x, transform.position.y), 3f * Time.deltaTime);
            yield return null;
        }
        while(transform.position.y != targetPosition.y)
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x, targetPosition.y), 3f * Time.deltaTime);
            yield return null;
        }
        ResetTiles();
        hasMoved = true;
        actionMenu.SetActive(true);
        actionMenu.transform.position = actionMenuSpawn.position;
        GetEnemies();
    }
}
