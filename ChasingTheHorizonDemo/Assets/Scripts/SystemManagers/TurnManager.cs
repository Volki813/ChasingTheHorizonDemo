using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Handles turn switching in Map Scenes automatically
//I feel like this script uses a lot of memory when it doesn't need to, could be changed to be more efficient but I haven't thought about it
//There should only be 1 TurnManager in a given scene
public class TurnManager : MonoBehaviour
{
    public static TurnManager instance { get; private set; }

    //VARIABLES
    public int turnNumber = 0;
    public bool allyTurn = true;
    public bool gameOver = false;
    //REFERENCES
    CursorController cursor;
    public Text turnNumberText;
    public List<UnitLoader> allyUnits = new List<UnitLoader>();
    public List<UnitLoader> enemyUnits = new List<UnitLoader>();
    [SerializeField] private GameObject combatReadout = null;
    [SerializeField] private GameObject allyTurnGraphic = null;
    [SerializeField] private GameObject enemyTurnGraphic = null;
    [SerializeField] private GameObject screenDim = null;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        cursor = FindObjectOfType<CursorController>();

        FindAllys();
        FindEnemies();

        allyTurnGraphic.SetActive(true);
        cursor.ResetState();
        Invoke("MapCursor", 1f);
    }

    private void Update()
    {
        turnNumberText.text = turnNumber.ToString();

        if(enemyUnits.Count <= 0)
        {
            gameOver = true;
        }

        if(combatReadout.activeSelf == false)
        {
            if(!gameOver)
            {
                if (allyTurn == false)
                {
                    AllyTurn();
                }
                else
                {
                    EnemyTurn();
                }
            }
            else
            {
                allyTurn = false;
                Invoke("GameOver", 1f);
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

    private void GameOver()
    {
        cursor.controls.Disable();
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
        RefreshEnemies();
        allyTurn = true;
        UpdateTiles();
        allyTurnGraphic.SetActive(true);
        enemyTurnGraphic.SetActive(false);
        cursor.GetComponent<SpriteRenderer>().sprite = cursor.highlight;
        Invoke("MapCursor", 1f);
        cursor.enemyTurn = false;
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
        cursor.GetComponent<SpriteRenderer>().sprite = null;
        allyTurn = false;
        allyTurnGraphic.SetActive(false);
        enemyTurnGraphic.SetActive(true);
        UpdateTiles();

        AIManager.instance.enemyOrder.Clear();
        AIManager.instance.SetEnemyOrder();
        RefreshEnemies();
        AIManager.instance.StopAllCoroutines();
        AIManager.instance.Invoke("StartAI", 1.5f);
        cursor.enemyTurn = true;
    }

    private void MapCursor()
    {
        if(screenDim.activeSelf)
        {
            cursor.controls.Disable();
        }
        else
        {
            cursor.controls.MapCursor.Enable();
        }
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
