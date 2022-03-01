using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InventorySystem;

//UnitLoader script loads all the data and functions each Unit needs
//Each unit regardless of affiliation requires this script
//I think everything else in here is fairly self explanatory
public class UnitLoader : MonoBehaviour
{
    public List<Node> currentPath = new List<Node>();

    //VARIABLES
    public int currentHealth = 0;
    public bool hasMoved = false;
    public bool hasAttacked = false;
    public bool rested = false;
    public bool attackable = false;
    public Vector2 originalPosition = new Vector2(0, 0);

    //REFERENCES
    private CursorController cursor;
    public TileMap map;
    public Unit unit;
    public UnitLoader target;
    public SpriteRenderer spriteRenderer = null;
    public Animator animator = null;
    public Inventory inventory = null;
    public GameObject actionMenu = null;
    public Weapon equippedWeapon = null;
    public List<UnitLoader> enemiesInRange = new List<UnitLoader>();
    public Transform actionMenuSpawn = null;
    public MapDialogue attackedDialogue = null;
    public MapDialogue defeatedDialogue = null;

    private void Start()
    {
        map = FindObjectOfType<TileMap>();
        cursor = FindObjectOfType<CursorController>();
        currentHealth = unit.statistics.health;
        currentPath = null;
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
        enemiesInRange.Clear();
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
        map.DehighlightTiles();
        map.walkableTiles = map.GenerateRange((int)(transform.localPosition.x), (int)(transform.localPosition.y), unit.statistics.movement, this);
        map.HighlightTiles();
    }
    public void GetEnemies()
    {
        Vector2 currentPosition = new Vector2(transform.localPosition.x, transform.localPosition.y);
        Vector2 enemyPosition = new Vector2(0, 0);

        foreach(UnitLoader unit in FindObjectsOfType<UnitLoader>())
        {
            if(!unit.unit.allyUnit)
            {
                enemyPosition = new Vector2(unit.transform.localPosition.x, unit.transform.localPosition.y);
                if(Vector2.Distance(currentPosition, enemyPosition) <= equippedWeapon.range)
                {
                    unit.AttackableHighlight();
                    if(!enemiesInRange.Contains(unit)){
                        enemiesInRange.Add(unit);
                    }
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
        originalPosition = transform.localPosition;
        yield return new WaitForEndOfFrame();

        while(transform.localPosition.x != targetPosition.x)
        {
            if (transform.localPosition.x > targetPosition.x)
            {
                animator.SetBool("Left", true);
            }
            else
            {
                animator.SetBool("Right", true);
            }
            transform.localPosition = Vector2.MoveTowards(transform.localPosition, new Vector2(targetPosition.x, transform.localPosition.y), 3f * Time.deltaTime);
            yield return null;
        }
        while(transform.localPosition.y != targetPosition.y)
        {
            if (transform.localPosition.y > targetPosition.y)
            {
                animator.SetBool("Down", true);
            }
            else
            {
                animator.SetBool("Up", true);
            }
            transform.localPosition = Vector2.MoveTowards(transform.localPosition, new Vector2(transform.localPosition.x, targetPosition.y), 3f * Time.deltaTime);
            yield return null;
        }
        hasMoved = true;
        ActionMenu();
        actionMenu.transform.position = actionMenuSpawn.position;
        map.DehighlightTiles();

        animator.SetBool("Up", false);
        animator.SetBool("Down", false);
        animator.SetBool("Selected", false);
        animator.SetBool("Left", false);
        animator.SetBool("Right", false);        
    }

    private IEnumerator NodeMovement()
    {
        if(currentPath != null)
        {
            Vector3 finalNode = new Vector3(currentPath[currentPath.Count - 1].x, currentPath[currentPath.Count - 1].y);
            while (transform.localPosition != finalNode)
            {
                Vector3 nextNode = new Vector3(currentPath[1].x, currentPath[1].y);

                while (transform.localPosition != nextNode)
                {
                    transform.localPosition = Vector2.MoveTowards(transform.localPosition, nextNode, 3f * Time.deltaTime);
                    yield return null;
                }

                currentPath.RemoveAt(0);
            }
        }
        hasMoved = true;
        ActionMenu();
        actionMenu.transform.position = actionMenuSpawn.position;
        GetEnemies();
        map.DehighlightTiles();
        cursor.SetState(new ActionMenuState(cursor));
        cursor.controls.MapScene.Disable();
        cursor.controls.UI.Enable();

        animator.SetBool("Up", false);
        animator.SetBool("Down", false);
        animator.SetBool("Selected", false);
        animator.SetBool("Left", false);
        animator.SetBool("Right", false);
    }
    public void FollowPath()
    {
        StartCoroutine(NodeMovement());
    }

    public void DelayedDeath()
    {        
        Invoke("Death", 0.1f);
    }
    private void Death()
    {        
        if (currentHealth <= 0){
            if(unit.allyUnit){
                TurnManager.instance.allyUnits.Remove(this);                
                Destroy(gameObject);
            }
            else{
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
        }
        else
        {
            actionMenu.SetActive(true);
        }
    }

    private void OnDestroy()
    {
        if(GameObject.Find("LoseManager")){
            if(GetComponent<BattleDialogue>()){
                if(GetComponent<BattleDialogue>().deathQuote){
                    MapDialogueManager.instance.WriteSingle(GetComponent<BattleDialogue>().deathQuote);
                }
            }
        }
        TurnManager.instance.RefreshTiles();
    }
}
