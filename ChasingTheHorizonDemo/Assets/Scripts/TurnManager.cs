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

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        FindAllys();
        FindEnemies();

        Debug.Log("Ally Turn");
    }

    private void Update()
    {
        turnNumberText.text = turnNumber.ToString();

        if(allyTurn == false)
        {
            AllyTurn();
        }
        else
        {
            EnemyTurn();
        }
    }

    private void AllyTurn()
    {
        for(int i=0; i<enemyUnits.Count; i++)
        {
            if(enemyUnits[i].rested == false)
            {
                return;
            }
        }

        turnNumber++;
        RefreshAllys();
        allyTurn = true;
        Debug.Log("Ally Turn");
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

        allyTurn = false;
        Debug.Log("Enemy Turn");
        RefreshEnemies();
        AIManager.instance.StartAI();
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
}
