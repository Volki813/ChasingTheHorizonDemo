using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Handles turn switching in Map Scenes automatically
//I feel like this script uses a lot of memory when it doesn't need to, could be changed to be more efficient but I haven't thought about it
//There should only be 1 TurnManager in a given scene
public enum BattleState {BeginStep, PlayerTurn, EnemyTurn, Win, Lose }

public class TurnManager : MonoBehaviour
{
    public static TurnManager instance { get; private set; }

    //VARIABLES
    public int turnNumber = 0;
    public bool allyTurn = true;
    public bool gameOver = false;
    //REFERENCES
    public CursorController cursor;
    public Camera mainCamera;
    public Text turnNumberText;
    public ScreenDim screenDim = null;
    public GameObject allyTurnGraphic = null;
    public GameObject enemyTurnGraphic = null; 

    public List<UnitLoader> allyUnits = new List<UnitLoader>();
    public List<UnitLoader> enemyUnits = new List<UnitLoader>();

    public TurnState currentState = null;

    public TileMap map;

    private void Awake()
    {
        instance = this;
    }

    public void SetState(TurnState state)
    {
        currentState = state;
        StartCoroutine(currentState.Begin());
    }

    private void Start()
    {
        map = FindObjectOfType<TileMap>();
        cursor = FindObjectOfType<CursorController>();
        mainCamera = FindObjectOfType<Camera>();
        DisableCursor();
        SetState(new TurnBeginState(this));
    }

    private void DisableCursor()
    {
        cursor.cursorControls.DeactivateInput();
    }

    private void Update()
    {
        turnNumberText.text = turnNumber.ToString();

        //Switches To Enemy Turn
        foreach(UnitLoader unit in allyUnits) {
            if (unit.rested == false) return;
        }
        if(currentState.stateType == TurnState.StateType.Player) {
            SetState(new EnemyTurnState(this));
            Invoke("StartAi", 1.2f);
        }

        //Switches To Player Turn        
        foreach(UnitLoader unit in enemyUnits) {
            if (unit.rested == false) return;
        }
        if(cursor.enemyTurn == false) {
            SetState(new PlayerTurnState(this));
        }
    }

    private void StartAi()
    {
        AIManager.instance.StopAllCoroutines();
        AIManager.instance.StartAI();
    }
    private void PlayerTurn()
    {
        SetState(new PlayerTurnState(this));
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
    public void FindEnemies()
    {
        foreach (UnitLoader unit in FindObjectsOfType<UnitLoader>())
        {
            if (!unit.unit.allyUnit)
            {
                enemyUnits.Add(unit);
            }
        }
    }
    public void RefreshAllys()
    {
        foreach(UnitLoader unit in allyUnits)
        {
            unit.Stand();
        }
    }
    public void RefreshEnemies()
    {
        foreach (UnitLoader unit in enemyUnits)
        {
            unit.Stand();
        }
    }

    public void RefreshAllySprites()
    {
        foreach(UnitLoader unit in TurnManager.instance.allyUnits)
        {
            unit.spriteRenderer.color = new Color32(255, 255, 255, 255);
        }
    }
}
