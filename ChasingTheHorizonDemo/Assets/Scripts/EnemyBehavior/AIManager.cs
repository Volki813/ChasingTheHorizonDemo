using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIManager : MonoBehaviour
{
    public static AIManager instance { get; private set; }

    public UnitLoader currentEnemy;

    public UnitLoader targetUnit;
    public TileLoader targetTile;

    [SerializeField]
    private List<UnitLoader> enemyOrder = new List<UnitLoader>();

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
            //Checks if it's a blitz type
            if(enemyOrder[i].GetComponent<BehaviorTag>().blitz == true)
            {
                //Move towards allies to get into attacking range
                //Check if there are any allies in attacking range
                    //Attack allies, rest
                //Look for allie that would take the most expected damage from an attack
                //Move to the tile closest to that unit

            }
            //For now, else implies the defense type behavior until there are more
            else
            {
                //Only move to attack allies in range
            }
        }

        yield return null;
    }

    private void SetEnemyOrder()
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
            if(Mathf.Abs(currentEnemy.transform.position.x - tile.transform.position.x) + Mathf.Abs(currentEnemy.transform.position.y - tile.transform.position.y) + tile.tileCost <= currentEnemy.unit.movement && tile.occupied == false)
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
                }
            }
        }
    }

    private void MoveTowardsTarget()
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
            else
            {
                Move(currentEnemy, targetTile.transform.position);
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
            currentEnemy.transform.position = Vector2.MoveTowards(currentEnemy.transform.position, new Vector2(targetPosition.x, currentEnemy.transform.position.y), 1f * Time.deltaTime);
            yield return null;
        }
        while (currentEnemy.transform.position.y != targetPosition.y)
        {
            currentEnemy.transform.position = Vector2.MoveTowards(currentEnemy.transform.position, new Vector2(currentEnemy.transform.position.x, targetPosition.y), 1f * Time.deltaTime);
            yield return null;
        }
        GetEnemies();
        if (enemiesInRange.Count > 0)
        {
            Debug.Log("Attack");
            currentEnemy.Rest();
        }
        else
            currentEnemy.Rest();
    }
}
