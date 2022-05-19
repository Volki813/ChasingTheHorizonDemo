using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static EventManager instance;
    
    public List<Event> events = new List<Event>();
    public Event currentEvent;
    
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        if(events.Count > 0)
        {
            currentEvent = events[0];
        }
    }

    public void ActivateEvent()
    {
        currentEvent.StartEvent();
    }

    public void ReloadEvent()
    {
        currentEvent = null;
        events.RemoveAt(0);
        if(events.Count > 0)
        {
            currentEvent = events[0];
        }
    }

    public bool PlayerEventCheck()
    {
        if(currentEvent != null)
        {
            string difficulty = PlayerPrefs.GetString("Difficulty", "Normal");
            if(difficulty == "Normal")
            {
                if(currentEvent.turn == TurnManager.instance.turnNumber && currentEvent.playerTurn && !currentEvent.hardModeOnly)
                {
                    return true;
                }
            }
            else if(difficulty == "Hard")
            {
                if(currentEvent.turn == TurnManager.instance.turnNumber && currentEvent.playerTurn)
                {
                    return true;
                }
            }
            
        }
        return false;
    }
    public bool EnemyEventCheck()
    {
        if(currentEvent != null)
        {
            string difficulty = PlayerPrefs.GetString("Difficulty", "Normal");
            if (difficulty == "Normal")
            {
                if(currentEvent.turn == TurnManager.instance.turnNumber && currentEvent.enemyTurn && !currentEvent.hardModeOnly)
                {
                    return true;
                }
            }
            else if (difficulty == "Hard")
            {
                if (currentEvent.turn == TurnManager.instance.turnNumber && currentEvent.enemyTurn && currentEvent.hardModeOnly)
                {
                    return true;
                }
            }
        }
        return false;
    }

}
