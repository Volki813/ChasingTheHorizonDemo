using UnityEngine;
public class Event : MonoBehaviour
{
    [Header("Event Context")]
    public int turn;
    public bool playerTurn;
    public bool enemyTurn;
    public bool hardModeOnly;
    [HideInInspector]
    public bool finished;

    [Header("Key References")]
    public ScreenDim screenDim;
    public CursorController cursor;
    public Camera mainCamera;
    public LoseManager loseManager;

    public virtual void StartEvent()
    {
    }
}
