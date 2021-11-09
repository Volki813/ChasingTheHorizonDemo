using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class BattleDialogueManager : MonoBehaviour
{
    public static BattleDialogueManager instance;

    //VARIABLES
    private bool dialogueFinished = false;
    public bool busy = false;
    //REFERENCES
    private Text dialogueHolder = null;
    [SerializeField] private Text unitName = null;
    [SerializeField] private Image unitPortrait = null;
    [SerializeField] private GameObject combatReadout = null;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        dialogueHolder = GetComponent<Text>();
    }

    public void WriteDialogue(string dialogue, UnitLoader unit)
    {
        StartCoroutine(WriteText(dialogue, unit));
    }

    private IEnumerator WriteText(string dialogue, UnitLoader unit)
    {
        dialogueHolder.text = "";
        unitName.text = "";
        dialogueFinished = false;

        yield return new WaitUntil(() => combatReadout.activeSelf == false);
        yield return new WaitForSeconds(0.2f);

        unitPortrait.gameObject.SetActive(true);
        unitName.text = unit.unit.unitName;
        unitPortrait.sprite = unit.unit.portrait;

        for(int i = 0; i < dialogue.Length; i++)
        {
            dialogueHolder.text += dialogue[i];
            yield return new WaitForSeconds(0.02f);
        }
        dialogueFinished = true;
        busy = false;
    }

    public void ClearDialogue(InputAction.CallbackContext context)
    {
        if(dialogueFinished == true)
        {
            if (context.performed)
            {
                dialogueHolder.text = null;
                unitName.text = null;
                unitPortrait.sprite = null;
                unitPortrait.gameObject.SetActive(false);
            }
        }
    }
}
