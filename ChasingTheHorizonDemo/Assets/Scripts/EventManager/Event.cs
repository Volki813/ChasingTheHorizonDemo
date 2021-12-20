using UnityEngine;
public class Event : MonoBehaviour
{
    public int turn;
    public bool playerTurn;
    public bool enemyTurn;
    [HideInInspector]
    public bool finished;

    public virtual void StartEvent()
    {
    }
}
