using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class OpheliaEvent : Event
{
    [SerializeField] private GameObject opheliaObject = null;
    [SerializeField] private GameObject screenDim = null;
    [SerializeField] private GameObject opheliaCG = null;
    [SerializeField] private UnitLoader rolandObject = null;
    [SerializeField] private MapDialogue[] opheliaDialogue = null;
    [SerializeField] private MapDialogue[] opheliaDialogue2 = null;
    [SerializeField] private GameObject thanksForPlaying = null;
    [SerializeField] private GameObject restartButton = null;
    private CursorController cursor;
    private LoseManager loseManager;
    private bool eventPlayed = false;

    private void Start()
    {
        cursor = FindObjectOfType<CursorController>();
        loseManager = FindObjectOfType<LoseManager>();
    }
    private void Update()
    {
        if(TurnManager.instance.enemyUnits.Count <= 0 && eventPlayed == false)
        {
            StartCoroutine(Event());
        }
    }

    private IEnumerator Event()
    {
        cursor.GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 0);
        TurnManager.instance.gameObject.SetActive(false);
        eventPlayed = true;
        loseManager.gameObject.SetActive(false);
        yield return new WaitUntil(() => !screenDim.activeSelf);
        yield return new WaitForSeconds(1f);
        cursor.cursorControls.DeactivateInput();
        //Move Ophelia on screen
        opheliaObject.SetActive(true);
        StartCoroutine(MoveUnit(opheliaObject.GetComponent<UnitLoader>(), new Vector2(2f, 10f)));
        yield return new WaitUntil(() => opheliaObject.transform.localPosition == new Vector3(2, 10));
        yield return new WaitUntil(() => !screenDim.activeSelf);
        MapDialogueManager.instance.ClearDialogue();
        yield return new WaitForSeconds(0.4f);
        //Turn the music off
        MusicPlayer.instance.PauseTrack();
        //Send dialogue to dialogue manager
        MapDialogueManager.instance.WriteMultiple(opheliaDialogue);
        yield return new WaitUntil(() => !screenDim.activeSelf);
        //Ophelia attacks Roland
        yield return new WaitForSeconds(0.5f);
        //Move next to Roland
        StartCoroutine(MoveUnit(opheliaObject.GetComponent<UnitLoader>(), new Vector2(rolandObject.transform.localPosition.x - 1, rolandObject.transform.localPosition.y)));
        yield return new WaitUntil(() => (Vector2)opheliaObject.transform.localPosition == new Vector2(rolandObject.transform.localPosition.x - 1, rolandObject.transform.localPosition.y));
        yield return new WaitForSeconds(0.5f);
        //Attack Roland
        CombatManager.instance.EngageAttack(opheliaObject.GetComponent<UnitLoader>(), rolandObject);
        //Ophelia CG appears and the rest of the dialogue is displayed
        yield return new WaitForSeconds(2f);
        opheliaCG.SetActive(true);
        yield return new WaitForSeconds(1f);
        MapDialogueManager.instance.WriteMultiple(opheliaDialogue2);
        yield return new WaitUntil(() => MapDialogueManager.instance.dialogueFinished);
        thanksForPlaying.SetActive(true);
        yield return new WaitForSeconds(1f);
        EventSystem.current.SetSelectedGameObject(null);
        yield return new WaitForEndOfFrame();
        cursor.cursorControls.ActivateInput();
        cursor.cursorControls.SwitchCurrentActionMap("UI");
        EventSystem.current.SetSelectedGameObject(restartButton.gameObject);
    }
    private IEnumerator MoveUnit(UnitLoader currentEnemy, Vector2 targetPosition)
    {
        while(currentEnemy.transform.localPosition.x != targetPosition.x)
        {
            if (currentEnemy.transform.localPosition.x > targetPosition.x)
            {
                currentEnemy.GetComponent<Animator>().SetBool("Left", true);
            }
            else
            {
                currentEnemy.GetComponent<Animator>().SetBool("Right", true);
            }
            currentEnemy.transform.localPosition = Vector2.MoveTowards(currentEnemy.transform.localPosition, new Vector2(targetPosition.x, currentEnemy.transform.localPosition.y), 2f * Time.deltaTime);
            yield return null;
        }
        while(currentEnemy.transform.localPosition.y != targetPosition.y)
        {
            if (currentEnemy.transform.localPosition.y > targetPosition.y)
            {
                currentEnemy.GetComponent<Animator>().SetBool("Down", true);
            }
            else
            {
                currentEnemy.GetComponent<Animator>().SetBool("Up", true);
            }
            currentEnemy.transform.localPosition = Vector2.MoveTowards(currentEnemy.transform.localPosition, new Vector2(currentEnemy.transform.localPosition.x, targetPosition.y), 2f * Time.deltaTime);
            yield return null;
        }
        currentEnemy.GetComponent<Animator>().SetBool("Right", false);
    }
}
