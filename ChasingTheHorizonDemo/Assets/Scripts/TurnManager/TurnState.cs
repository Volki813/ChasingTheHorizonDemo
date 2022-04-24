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
        turnManager.cursor.cursorControls.DeactivateInput();
        turnManager.cursor.GetComponent<Animator>().SetBool("Invisible", true);
    }

    protected void EnableCursor()
    {
        turnManager.cursor.GetComponent<Animator>().SetBool("Invisible", false);
        turnManager.cursor.cursorControls.ActivateInput();
        turnManager.cursor.cursorControls.SwitchCurrentActionMap("MapScene");
        turnManager.cursor.SetState(new MapState(turnManager.cursor));
    }

    protected void Test()
    {
        Debug.Log("test");
    }
}
