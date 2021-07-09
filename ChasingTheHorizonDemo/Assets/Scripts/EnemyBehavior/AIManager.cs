using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIManager : MonoBehaviour
{
    public static AIManager instance { get; private set; }

    public UnitLoader currentEnemy;

    public UnitLoader targetUnit;
    public TileLoader targetTile;

    public GameObject combatReadout;

    private Animator animator;

    [SerializeField]
    public List<UnitLoader> enemyOrder = new List<UnitLoader>();

    [SerializeField]
    private List<TileLoader> walkableTiles = new List<TileLoader>();

    [SerializeField]
    private List<UnitLoader> enemiesInRange = new List<UnitLoader>();

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        Invoke("SetEnemyOrder", 2f);
    }

    public void StartAI()
    {
        StartCoroutine(BehaviorSystem());
    }

    private IEnumerator BehaviorSystem()
    {
        //Iterates through each enemy in the enemy list
        for (int i = 0; i < enemyOrder.Count; i++)
        {
            currentEnemy = enemyOrder[i];
            animator = currentEnemy.GetComponent<Animator>();
            if (enemyOrder[i].GetComponent<BehaviorTag>().blitz)
            {
                Blitz(currentEnemy);
                yield return new WaitForSeconds(3f);
                walkableTiles.Clear();
                enemiesInRange.Clear();
                targetTile = null;
                targetUnit = null;
            }
            else if(enemyOrder[i].GetComponent<BehaviorTag>().defensive)
            {
                Defensive();
                yield return new WaitForSeconds(3f);
                walkableTiles.Clear();
                enemiesInRange.Clear();
                targetTile = null;
                targetUnit = null;
            }
            yield return new WaitUntil(() => combatReadout.activeSelf == false);
        }
        yield return null;
    }

    private void Blitz(UnitLoader currentEnemy)
    {
        GetEnemies();
        GetWalkableTiles();
        if(enemiesInRange.Count > 0)
        {
            CombatManager.instance.EngageAttack(currentEnemy, targetUnit);
        }
        else
        {
            targetUnit = GetTarget();
            FindClosestTile();
            Move(currentEnemy, targetTile.transform.position);
            GetEnemies();
            if(enemiesInRange.Count > 0)
            {
                CombatManager.instance.EngageAttack(currentEnemy, targetUnit);
            }
        }
    }

    private void Defensive()
    {
        GetEnemies();
        if(enemiesInRange.Count > 0)
        {
            CombatManager.instance.EngageAttack(currentEnemy, targetUnit);
        }
        else
        {
            currentEnemy.Rest();
        }
    }


    public void SetEnemyOrder()
    {
        int i = 0;
        foreach (UnitLoader unit in TurnManager.instance.enemyUnits)
        {
            enemyOrder.Insert(i, TurnManager.instance.enemyUnits[i]);
            i++;
        }
        enemyOrder.Sort((x, y) => x.GetComponent<BehaviorTag>().order.CompareTo(y.GetComponent<BehaviorTag>().order));
    }
    
    private void GetWalkableTiles()
    {
        foreach(TileLoader tile in FindObjectsOfType<TileLoader>())
        {
            if(Mathf.Abs(currentEnemy.transform.position.x - tile.transform.position.x) + Mathf.Abs(currentEnemy.transform.position.y - tile.transform.position.y) + tile.tileCost <= currentEnemy.unit.statistics.movement && tile.occupied == false)
            {
                walkableTiles.Add(tile);
            }
        }
    }
    private void GetEnemies()
    {
        foreach (UnitLoader unit in FindObjectsOfType<UnitLoader>())
        {
            if(unit.unit.allyUnit)
            {
                if(Mathf.Abs(currentEnemy.transform.position.x - unit.transform.position.x) + Mathf.Abs(currentEnemy.transform.position.y - unit.transform.position.y) <= currentEnemy.equippedWeapon.range)
                {
                    enemiesInRange.Add(unit);

                    if(enemiesInRange.Count > 1)
                    {
                        targetUnit = GetTarget();
                    }
                    else
                    {
                        targetUnit = enemiesInRange[0];
                    }
                }
            }
        }
    }

    //changed variable names
    private UnitLoader GetTarget()
    {
        // any reason this loop isn't a foreach loop? I'm too lazy to think ab it rn.
        for (int i = 0; i < TurnManager.instance.allyUnits.Count; i++)
        {
            int expectedDamage = CombatManager.instance.Hit(currentEnemy, TurnManager.instance.allyUnits[i]);
            if(TurnManager.instance.allyUnits[i].currentHealth <= expectedDamage)
            {
                return TurnManager.instance.allyUnits[i];
            }
            else if(TurnManager.instance.allyUnits[i].currentHealth - expectedDamage <= 0)
            {
                return TurnManager.instance.allyUnits[i];
            }
            else if(TurnManager.instance.allyUnits[i].currentHealth - expectedDamage == 1)
            {
                return TurnManager.instance.allyUnits[i];
            }
        }
        return FindMostVulernerableUnit();
    }
    private UnitLoader FindMostVulernerableUnit()
    {
        int highestSoFar = 0;
        for (int i = 0; i < TurnManager.instance.allyUnits.Count; i++)
        {
            int expectedDamage = CombatManager.instance.Hit(currentEnemy, TurnManager.instance.allyUnits[i]);
            if(highestSoFar <= expectedDamage)
            {
                highestSoFar = expectedDamage;
                targetUnit = TurnManager.instance.allyUnits[i];
            }
        }
        return targetUnit;
    }
    private void FindClosestTile()
    {
        float lowestSoFar = 100;

        for (int i = 0; i < walkableTiles.Count; i++)
        {
            float distance = (Mathf.Abs(targetUnit.transform.position.x - walkableTiles[i].transform.position.x) + Mathf.Abs(targetUnit.transform.position.y - walkableTiles[i].transform.position.y));
            if(distance < lowestSoFar)
            {
                lowestSoFar = distance;
                targetTile = walkableTiles[i];
            }
        }
    }
    private void Move(UnitLoader currentEnemy, Vector2 targetPosition)
    {
        StartCoroutine(Movement(currentEnemy, targetPosition));
    }
    private IEnumerator Movement(UnitLoader currentEnemy, Vector2 targetPosition)
    {
        while (currentEnemy.transform.position.x != targetPosition.x)
        {
            if (currentEnemy.transform.position.x > targetPosition.x)
            {
                animator.SetBool("Left", true);
            }
            else
            {
                animator.SetBool("Right", true);
            }
            currentEnemy.transform.position = Vector2.MoveTowards(currentEnemy.transform.position, new Vector2(targetPosition.x, currentEnemy.transform.position.y), 2f * Time.deltaTime);
            yield return null;
        }
        while (currentEnemy.transform.position.y != targetPosition.y)
        {
            if (currentEnemy.transform.position.y > targetPosition.y)
            {
                animator.SetBool("Down", true);
            }
            else
            {
                animator.SetBool("Up", true);
            }
            currentEnemy.transform.position = Vector2.MoveTowards(currentEnemy.transform.position, new Vector2(currentEnemy.transform.position.x, targetPosition.y), 2f * Time.deltaTime);
            yield return null;
        }
        currentEnemy.Rest();
        TurnManager.instance.UpdateTiles();
        
        animator.SetBool("Up", false);
        animator.SetBool("Down", false);
        animator.SetBool("Selected", false);
        animator.SetBool("Left", false);
        animator.SetBool("Right", false);
    }
}
