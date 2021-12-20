using UnityEngine;
using System.Collections;

public abstract class TurnState
{
    public enum StateType { Begin, Player, Enemy}
    public StateType stateType;

    protected readonly TurnManager turnManager;

    public TurnState(TurnManager manager)
    {
        turnManager = manager;
    }

    public virtual void Start()
    {

    }

    public virtual IEnumerator Begin()
    {
        yield return null;
    }

    protected void DisableCursor()
    {
        turnManager.cursor.controls.Disable();
        turnManager.cursor.controls.UI.Disable();
        turnManager.cursor.controls.MapScene.Disable();
        turnManager.cursor.GetComponent<SpriteRenderer>().sprite = null;
    }

    protected void EnableCursor()
    {
        turnManager.cursor.controls.Enable();
        turnManager.cursor.controls.MapScene.Enable();
        turnManager.cursor.SetState(new MapState(turnManager.cursor));
        turnManager.cursor.GetComponent<SpriteRenderer>().sprite = turnManager.cursor.highlight;
    }

    protected void Test()
    {
        Debug.Log("test");
    }
}
