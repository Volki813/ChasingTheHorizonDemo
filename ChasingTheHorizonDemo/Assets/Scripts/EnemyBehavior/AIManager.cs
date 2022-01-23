using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//The AI Manager handles all the enemy unit AI in a Map scene automatically given there are enemy units in the scene
//There should be 1 AI Manager per Map Scene
public class AIManager : MonoBehaviour
{
    public static AIManager instance { get; private set; }

    //REFERENCES
    private Camera mainCamera = null;
    private CursorController cursor;
    private Animator enemyAnimator = null;
    [SerializeField] private UnitLoader currentEnemy = null;
    [SerializeField] private GameObject combatReadout = null;

    public List<UnitLoader> enemyOrder = new List<UnitLoader>();
    [SerializeField] private List<TileLoader> walkableTiles = new List<TileLoader>();
    [SerializeField] private List<UnitLoader> enemiesInRange = new List<UnitLoader>();

    private Vector3 cameraTarget = new Vector3(0, 0, 0);

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        mainCamera = FindObjectOfType<Camera>();
        cursor = FindObjectOfType<CursorController>();
        Invoke("SetEnemyOrder", 1f);
    }

    public void StartAI()
    {
        StartCoroutine(BehaviorSystem());
    }

    private IEnumerator BehaviorSystem()
    {
        cursor.enemyTurn = true;
        List<UnitLoader> enemies = enemyOrder;
        //Iterates through each enemy in the enemy list
        for(int i = 0; i < enemies.Count; i++)
        {
            currentEnemy = enemies[i];
            enemyAnimator = currentEnemy.GetComponent<Animator>();

            StartCoroutine(MoveCamera(enemies[i]));
            yield return new WaitUntil(() => mainCamera.transform.position == cameraTarget);

            if(enemies[i].GetComponent<BehaviorTag>().blitz)
            {                
                Blitz(currentEnemy);
                yield return new WaitUntil(() => currentEnemy.rested || !currentEnemy);
                yield return new WaitForSeconds(0.5f);
                walkableTiles.Clear();
                enemiesInRange.Clear();
            }
            else if(enemies[i].GetComponent<BehaviorTag>().defensive)
            {
                Defensive();
                if(IsLastEnemy() && combatReadout.activeSelf == false){
                    yield return new WaitForSeconds(0.5f);
                }
                else{
                    yield return new WaitUntil(() => currentEnemy.rested || !currentEnemy);
                    yield return new WaitForSeconds(0.5f);
                }
                walkableTiles.Clear();
                enemiesInRange.Clear();
            }
            yield return new WaitUntil(() => combatReadout.activeSelf == false);
        }
        enemyOrder.Clear();
        SetEnemyOrder();
        cursor.enemyTurn = false;
        yield return null;
    }

    private void Blitz(UnitLoader currentEnemy)
    {
        GetEnemies();
        if(enemiesInRange.Count == 0)
        {
            GetWalkableTiles();
            UnitLoader closestAlly = FindClosestAlly();
            TileLoader closestTile = FindClosestTile(closestAlly);
            Move(currentEnemy, closestTile.transform.position);
        }
        else if(enemiesInRange.Count == 1)
        {
            CombatManager.instance.EngageAttack(currentEnemy, enemiesInRange[0]);
        }
        else if(enemiesInRange.Count >= 2)
        {
            CombatManager.instance.EngageAttack(currentEnemy, DetermineWeakestUnit());
        }
    }
    private void Defensive()
    {
        GetEnemies();
        if(enemiesInRange.Count == 0)
        {
            currentEnemy.Rest();
        }
        else if(enemiesInRange.Count == 1)
        {
            CombatManager.instance.EngageAttack(currentEnemy, enemiesInRange[0]);
        }
        else if(enemiesInRange.Count >= 2)
        {
            UnitLoader weakestUnit = DetermineWeakestUnit();
            CombatManager.instance.EngageAttack(currentEnemy, weakestUnit);
        }
    }
    public void SetEnemyOrder()
    {
        int i = 0;
        foreach(UnitLoader unit in TurnManager.instance.enemyUnits)
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
            if (tile.transform.position == transform.position)
            {
                tile.HighlightTile(currentEnemy.unit);
            }
            if (Mathf.Abs(currentEnemy.transform.position.x - tile.transform.position.x) + Mathf.Abs(currentEnemy.transform.position.y - tile.transform.position.y) + tile.tileCost <= currentEnemy.unit.statistics.movement && tile.occupied == false)
            {
                walkableTiles.Add(tile);
            }
        }
    }
    private void GetEnemies()
    {
        foreach(UnitLoader unit in TurnManager.instance.allyUnits)
        {
            if(Mathf.Abs(currentEnemy.transform.position.x - unit.transform.position.x) + Mathf.Abs(currentEnemy.transform.position.y - unit.transform.position.y) <= currentEnemy.equippedWeapon.range)
            {
                enemiesInRange.Add(unit);
            }
        }
    }

    private UnitLoader DetermineWeakestUnit()
    {
        UnitLoader weakestUnit = null;
        int highestDamage = 0;
        foreach(UnitLoader unit in enemiesInRange)
        {
            if (CombatManager.instance.Hit(currentEnemy, unit) > highestDamage)
            {
                highestDamage = CombatManager.instance.Hit(currentEnemy, unit);
                weakestUnit = unit;
            }
        }
        return weakestUnit;
    }
    private UnitLoader FindClosestAlly()
    {
        float closestSoFar = 100;
        UnitLoader closestUnit = null;
        foreach(UnitLoader unit in TurnManager.instance.allyUnits)
        {
            if(Mathf.Abs(currentEnemy.transform.position.x - unit.transform.position.x) + Mathf.Abs(currentEnemy.transform.position.y - unit.transform.position.y) <= closestSoFar)
            {
                closestSoFar = Mathf.Abs(currentEnemy.transform.position.x - unit.transform.position.x) + Mathf.Abs(currentEnemy.transform.position.y - unit.transform.position.y);
                closestUnit = unit;
            }
        }
        return closestUnit;
    }
    private TileLoader FindClosestTile(UnitLoader unit)
    {
        float lowestSoFar = 100;
        TileLoader closestTile = null;
        foreach(TileLoader tile in walkableTiles)
        {
            if(Mathf.Abs(tile.transform.position.x - unit.transform.position.x) + Mathf.Abs(tile.transform.position.y - unit.transform.position.y) <= lowestSoFar)
            {
                lowestSoFar = Mathf.Abs(tile.transform.position.x - unit.transform.position.x) + Mathf.Abs(tile.transform.position.y - unit.transform.position.y);
                closestTile = tile;
            }
        }
        return closestTile;
    }

    private bool IsLastEnemy()
    {
        foreach(UnitLoader unit in TurnManager.instance.enemyUnits) {
            if (unit.rested == false) return false;
        }
        return true;
    }

    public void Move(UnitLoader currentEnemy, Vector2 targetPosition)
    {
        StartCoroutine(Movement(currentEnemy, targetPosition));
    }
    private IEnumerator Movement(UnitLoader currentEnemy, Vector2 targetPosition)
    {
        while (currentEnemy.transform.position.x != targetPosition.x)
        {
            if (currentEnemy.transform.position.x > targetPosition.x)
            {
                enemyAnimator.SetBool("Left", true);
            }
            else
            {
                enemyAnimator.SetBool("Right", true);
            }
            currentEnemy.transform.position = Vector2.MoveTowards(currentEnemy.transform.position, new Vector2(targetPosition.x, currentEnemy.transform.position.y), 2f * Time.deltaTime);
            yield return null;
        }
        while (currentEnemy.transform.position.y != targetPosition.y)
        {
            if (currentEnemy.transform.position.y > targetPosition.y)
            {
                enemyAnimator.SetBool("Down", true);
            }
            else
            {
                enemyAnimator.SetBool("Up", true);
            }
            currentEnemy.transform.position = Vector2.MoveTowards(currentEnemy.transform.position, new Vector2(currentEnemy.transform.position.x, targetPosition.y), 2f * Time.deltaTime);
            yield return null;
        }
        GetEnemies();
        if(enemiesInRange.Count == 1)
        {
            CombatManager.instance.EngageAttack(currentEnemy, enemiesInRange[0]);
        }
        else if (enemiesInRange.Count >= 2)
        {
            CombatManager.instance.EngageAttack(currentEnemy, DetermineWeakestUnit());
        }
        else
        {
            currentEnemy.Rest();
        }
        enemyAnimator.SetBool("Up", false);
        enemyAnimator.SetBool("Down", false);
        enemyAnimator.SetBool("Left", false);
        enemyAnimator.SetBool("Right", false);

        TurnManager.instance.UpdateTiles();
    }
    private IEnumerator MoveCamera(UnitLoader enemy)
    {
        cameraTarget = new Vector3(enemy.transform.position.x, enemy.transform.position.y, -10);
        if(cameraTarget.x > cursor.cameraRight){
            cameraTarget.x = cursor.cameraRight;
        }
        if(cameraTarget.x < cursor.cameraLeft){
            cameraTarget.x = cursor.cameraLeft;
        }
        if(cameraTarget.y > cursor.cameraTop){
            cameraTarget.y = cursor.cameraTop;
        }
        if(cameraTarget.y < cursor.cameraBottom){
            cameraTarget.y = cursor.cameraBottom;
        }

        while(mainCamera.transform.position != cameraTarget)
        {
            mainCamera.transform.position = Vector3.MoveTowards(mainCamera.transform.position, cameraTarget, 5f * Time.deltaTime);
            yield return null;
        }
    }
}
