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
        turnManager.map.selectedUnit = null;
        
        //Checks for Events
        if(EventManager.instance.EnemyEventCheck()){
            EventManager.instance.ActivateEvent();
            yield return new WaitUntil(() => EventManager.instance.currentEvent.finished);
        }

        turnManager.enemyTurnGraphic.SetActive(true);
        turnManager.screenDim.SetActive(true);
        turnManager.screenDim.GetComponent<Animator>().SetTrigger("FadeIn");
        turnManager.UpdateTiles();
        yield return new WaitForSeconds(0.85f);
        turnManager.screenDim.GetComponent<Animator>().SetTrigger("FadeOut");
        AIManager.instance.enemyOrder.Clear();
        AIManager.instance.SetEnemyOrder();
        turnManager.RefreshEnemies();
    }
}
