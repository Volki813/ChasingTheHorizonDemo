using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnManager : MonoBehaviour
{
    public static TurnManager instance { get; private set; }

    public int turnNumber;
    public Text turnNumberText;

    public bool allyTurn = true;

    public List<UnitLoader> allyUnits = new List<UnitLoader>();
    public List<UnitLoader> enemyUnits = new List<UnitLoader>();

    CursorController cursor;

    public GameObject combatReadout;

    public GameObject allyTurnObject;
    public GameObject enemyTurnObject;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        cursor = FindObjectOfType<CursorController>();

        FindAllys();
        FindEnemies();

        allyTurnObject.SetActive(true);
        cursor.ResetState();
        Invoke("MapCursor", 1f);
        Debug.Log("Ally Turn");
    }

    private void Update()
    {
        turnNumberText.text = turnNumber.ToString();

        if(combatReadout.activeSelf == false)
        {
            if(allyTurn == false)
            {
                AllyTurn();
            }
            else
            {
                EnemyTurn();
            }
        }
    }

    public void UpdateTiles()
    {
        foreach(TileLoader tile in FindObjectsOfType<TileLoader>())
        {
            tile.UpdateOccupationStatus();
        }
    }

    
    private void AllyTurn()
    {
        for (int i=0; i<enemyUnits.Count; i++)
        {
            if(enemyUnits[i].rested == false)
            {
                return;
            }
        }

        StopAllCoroutines();
        cursor.ResetState();
        turnNumber++;
        RefreshAllys();
        allyTurn = true;
        UpdateTiles();
        allyTurnObject.SetActive(true);
        enemyTurnObject.SetActive(false);
        Invoke("MapCursor", 1f);
    }  

    private void MapCursor()
    {
        cursor.controls.MapCursor.Enable();
    }


    private void EnemyTurn()
    {
        for (int i = 0; i < allyUnits.Count; i++)
        {
            if(allyUnits[i].rested == false)
            {
                return;
            }
        }

        StopAllCoroutines();
        cursor.ResetState();
        cursor.controls.Disable();
        allyTurn = false;
        allyTurnObject.SetActive(false);
        enemyTurnObject.SetActive(true);
        UpdateTiles();

        AIManager.instance.enemyOrder.Clear();
        AIManager.instance.SetEnemyOrder();
        RefreshEnemies();
        AIManager.instance.StopAllCoroutines();
        AIManager.instance.Invoke("StartAI", 1.5f);
    }
    public void FindAllys()
    {
        foreach (UnitLoader unit in FindObjectsOfType<UnitLoader>())
        {
            if (unit.unit.allyUnit)
            {
                allyUnits.Add(unit);
            }
        }
    }
    private void FindEnemies()
    {
        foreach (UnitLoader unit in FindObjectsOfType<UnitLoader>())
        {
            if (!unit.unit.allyUnit)
            {
                enemyUnits.Add(unit);
            }
        }
    }
    private void RefreshAllys()
    {
        foreach(UnitLoader unit in allyUnits)
        {
            unit.Stand();
        }
    }
    private void RefreshEnemies()
    {
        foreach (UnitLoader unit in enemyUnits)
        {
            unit.Stand();
        }
    }
    public void RefreshTiles()
    {
        foreach (TileLoader tile in FindObjectsOfType<TileLoader>())
        {
            tile.UpdateOccupationStatus();
        }
    }
}
