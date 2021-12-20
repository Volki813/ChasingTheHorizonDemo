using System.Collections;
using UnityEngine;
public class EnemyTurnState : TurnState
{
    public EnemyTurnState(TurnManager manager) : base(manager)
    {
        stateType = StateType.Enemy;
    }

    public override IEnumerator Begin()
    {
        DisableCursor();
        turnManager.allyTurnGraphic.SetActive(false);
        
        //Checks for Events
        if(EventManager.instance.EnemyEventCheck()){
            EventManager.instance.ActivateEvent();
            yield return new WaitUntil(() => EventManager.instance.currentEvent.finished);
        }

        turnManager.enemyTurnGraphic.SetActive(true);
        turnManager.UpdateTiles();
        yield return new WaitForSeconds(2f);
        AIManager.instance.enemyOrder.Clear();
        AIManager.instance.SetEnemyOrder();
        turnManager.RefreshEnemies();
    }
}
