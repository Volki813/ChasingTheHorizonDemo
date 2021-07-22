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

    public Animator animator;

    public int currentHealth;
    public Vector2 originalPosition;

    private void Start()
    {
        currentHealth = unit.statistics.health;

        spriteRenderer = GetComponent<SpriteRenderer>();

        spriteRenderer.sprite = unit.sprite;
    }

    public BattleStatistics CombatStatistics()
    {
        //Instead when we need the battle stats we can call this function to run the calculation.
        //It will return a battle stats struct containing the calculated stats for us to use
        BattleStatistics stats = new BattleStatistics();

        //Just use the same calculations here but make them apart of the struct
        stats.attack = unit.statistics.strength + equippedWeapon.might;
        stats.attackSpeed = unit.statistics.agility - ((unit.statistics.strength - equippedWeapon.weight) / 4);
        stats.protection = unit.statistics.defense; //plus any shield
        stats.resilience = unit.statistics.resistance; //plus any shield
        stats.hit = equippedWeapon.hit + (unit.statistics.proficiency / 2) + (unit.statistics.motivation / 4);
        stats.avoid = stats.attackSpeed + unit.statistics.motivation / 5;
        stats.crit = equippedWeapon.crit + (unit.statistics.proficiency / 2) + (unit.statistics.motivation / 5);
        stats.vigilance = (unit.statistics.proficiency / 3) + (unit.statistics.motivation / 5);

        
        return stats;
    }

    public void Selected()
    {
        animator.SetBool("Selected", true);
        GetWalkableTiles();
    }

    public void ResetTiles()
    {
        foreach (TileLoader tile in FindObjectsOfType<TileLoader>())
        {
            tile.ResetTiles();
        }
    }
    public void UpdateTiles()
    {
        foreach(TileLoader tile in FindObjectsOfType<TileLoader>())
        {
            tile.UpdateOccupationStatus();
        }
    }
    public void Move(Vector2 targetPosition)
    {
        originalPosition = transform.position;
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
            if (tile.transform.position == transform.position)
            {
                tile.HighlightTile();
            }
            if (Mathf.Abs(transform.position.x - tile.transform.position.x) + Mathf.Abs(transform.position.y - tile.transform.position.y) + tile.tileCost <= unit.statistics.movement && tile.occupied == false)
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
        if(GetComponent<SpriteRenderer>().color == Color.red)
        {
            GetComponent<SpriteRenderer>().color = Color.white;
        }
        else
        {
            GetComponent<SpriteRenderer>().color = Color.red;
        }
    }
    private IEnumerator Movement(Vector2 targetPosition)
    {
        while(transform.position.x != targetPosition.x)
        {
            if(transform.position.x > targetPosition.x)
            {
                animator.SetBool("Left", true);
            }
            else
            {
                animator.SetBool("Right", true);
            }
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(targetPosition.x, transform.position.y), 3f * Time.deltaTime);
            yield return null;
        }
        while(transform.position.y != targetPosition.y)
        {
            if (transform.position.y > targetPosition.y)
            {
                animator.SetBool("Down", true);
            }
            else
            {
                animator.SetBool("Up", true);
            }
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x, targetPosition.y), 3f * Time.deltaTime);
            yield return null;
        }
        ResetTiles();
        UpdateTiles();
        TurnManager.instance.RefreshTiles();
        hasMoved = true;
        ActionMenu();
        actionMenu.transform.position = actionMenuSpawn.position;
        GetEnemies();

        animator.SetBool("Up", false);
        animator.SetBool("Down", false);
        animator.SetBool("Selected", false);
        animator.SetBool("Left", false);
        animator.SetBool("Right", false);
    }
    public void Death()
    {
        if(currentHealth <= 0)
        {
            if(unit.allyUnit)
            {
                TurnManager.instance.allyUnits.Remove(this);
                Destroy(gameObject);
            }
            else
            {
                TurnManager.instance.enemyUnits.Remove(this);
                AIManager.instance.enemyOrder.Remove(this);
                Destroy(gameObject);
            }
        }
    }
    public void ActionMenu()
    {
        if(actionMenu.activeSelf == true)
        {
            actionMenu.SetActive(false);
            if(target != null)
            {
                target.AttackableHighlight();
            }
        }
        else
        {
            actionMenu.SetActive(true);
        }
    }
}
