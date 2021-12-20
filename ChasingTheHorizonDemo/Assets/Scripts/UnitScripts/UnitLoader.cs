using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueSystem;
using InventorySystem;

//UnitLoader script loads all the data and functions each Unit needs
//Each unit regardless of affiliation requires this script
//I think everything else in here is fairly self explanatory
public class UnitLoader : MonoBehaviour
{
    //VARIABLES
    public int currentHealth = 0;
    public bool hasMoved = false;
    public bool hasAttacked = false;
    public bool rested = false;
    public bool attackable = false;
    public Vector2 originalPosition = new Vector2(0, 0);

    //REFERENCES
    public Unit unit;
    public UnitLoader target;
    public SpriteRenderer spriteRenderer = null;
    public Animator animator = null;
    public Inventory inventory = null;
    public GameObject actionMenu = null;
    public Weapon equippedWeapon = null;
    public List<UnitLoader> enemiesInRange = new List<UnitLoader>();
    [SerializeField] private Transform actionMenuSpawn = null;

    private void Start()
    {
        currentHealth = unit.statistics.health;        
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = unit.sprite;
        EquipWeapon();
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

    private void EquipWeapon()
    {
        if (!equippedWeapon)
        {
            foreach (Item item in inventory.inventory)
            {
                if (item.type == ItemType.Weapon)
                {
                    equippedWeapon = (Weapon)item;
                    return;
                }
            }
        }
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
    public void GetWalkableTiles()
    {
        foreach(TileLoader tile in FindObjectsOfType<TileLoader>())
        {
            if (tile.transform.position == transform.position)
            {
                tile.HighlightTile(unit);
            }
            if (Mathf.Abs(transform.position.x - tile.transform.position.x) + Mathf.Abs(transform.position.y - tile.transform.position.y) + tile.tileCost <= unit.statistics.movement && tile.occupied == false)
            {
                tile.HighlightTile(unit);
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
                    enemiesInRange.Add(unit);
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
    
    public void DelayedDeath()
    {
        Invoke("Death", 0.1f);
    }

    private void Death()
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

    private void OnDestroy()
    {
        TurnManager.instance.RefreshTiles();
    }
}
