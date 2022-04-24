using System.Collections;
using UnityEngine;
public class TurnBeginState : TurnState
{
    public TurnBeginState(TurnManager manager) : base(manager)
    {
        stateType = StateType.Begin;
    }
    public override IEnumerator Begin()
    {
        DisableCursor();
        turnManager.FindAllys();
        turnManager.FindEnemies();
        turnManager.SetState(new PlayerTurnState(turnManager));
        yield return null;
    }
}
