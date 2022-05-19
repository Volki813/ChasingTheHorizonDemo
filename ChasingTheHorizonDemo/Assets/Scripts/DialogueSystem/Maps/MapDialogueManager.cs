using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MapDialogueManager : MonoBehaviour
{
    public static MapDialogueManager instance;
    
    [SerializeField] private GameObject combatReadout = null;
    private CursorController cursorController;
    private bool busy = false;
    private bool buttonPressed = false;
    private bool multipleLines = false;

    public ScreenDim screenDim = null;
    public bool dialogueFinished = false;

    [SerializeField] private GameObject dialogueCursorTop = null;
    [SerializeField] private GameObject dialogueCursorBot = null;

    [Header("Top Textbox")]
    [SerializeField] private MapTextbox textBox1 = null;

    [Header("Bottom Textbox")]
    [SerializeField] private MapTextbox textBox2 = null;

    private MapDialogue lastDialogue = null;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        cursorController = FindObjectOfType<CursorController>();
    }

    private IEnumerator WriteDialogue(MapDialogue dialogue)
    {
        busy = true;
        
        if(dialogue.scriptDialogue && !screenDim.gameObject.activeSelf){
            screenDim.gameObject.SetActive(true);
        }

        if(dialogue.textBox1){
            textBox1.namePlate.text = "";
            textBox1.portrait.sprite = null;
            textBox1.textBox.text = "";

            textBox1.namePlate.text = dialogue.unit.unitName;
            textBox1.portrait.gameObject.SetActive(true);
            textBox1.portrait.sprite = dialogue.unit.portrait;
            textBox1.portraitRect.localScale = new Vector3(-1, 1, 1);
        }
        else{
            textBox2.namePlate.text = "";
            textBox2.portrait.sprite = null;
            textBox2.textBox.text = "";

            textBox2.namePlate.text = dialogue.unit.unitName;
            textBox2.portrait.gameObject.SetActive(true);
            textBox2.portrait.sprite = dialogue.unit.portrait;
        }

        for(int i = 0; i < dialogue.dialogue.Length; i++)
        {
            if(dialogue.textBox1){
                textBox1.textBox.text += dialogue.dialogue[i];
                SoundManager.instance.PlayFX(7);
                yield return new WaitForSeconds(0.01f);
            }
            else{
                textBox2.textBox.text += dialogue.dialogue[i];
                SoundManager.instance.PlayFX(7);
                yield return new WaitForSeconds(0.01f);
            }
        }
        if (dialogue.textBox1)
            dialogueCursorTop.SetActive(true);
        else
            dialogueCursorBot.SetActive(true);

        dialogue.finished = true;
        lastDialogue = null;
        lastDialogue = dialogue;
        busy = false;
        yield return null;
    }

    public void WriteSingle(MapDialogue dialogue)
    {
        StartCoroutine(WriteSingleDialogue(dialogue));
    }
    private IEnumerator WriteSingleDialogue(MapDialogue dialogue)
    {
        busy = true;
        //Waits until combat is over
        yield return new WaitUntil(() => combatReadout.activeSelf == false);
        yield return new WaitForSeconds(0.01f);

        textBox1.textBox.text = "";
        textBox2.textBox.text = "";
        textBox1.namePlate.text = "";
        textBox2.namePlate.text = "";

        if(dialogue.scriptDialogue && !screenDim.gameObject.activeSelf){
            screenDim.gameObject.SetActive(true);
            screenDim.animator.SetTrigger("FadeIn");
        }
        
        if(dialogue.textBox1){
            textBox1.namePlate.text = dialogue.unit.unitName;
            textBox1.portrait.gameObject.SetActive(true);
            textBox1.portrait.sprite = dialogue.unit.portrait;
            textBox1.portraitRect.localScale = new Vector3(-1, 1, 1);
        }
        else{
            textBox2.namePlate.text = dialogue.unit.unitName;
            textBox2.portrait.gameObject.SetActive(true);
            textBox2.portrait.sprite = dialogue.unit.portrait;
        }

        for(int i = 0; i < dialogue.dialogue.Length; i++) {
            if(dialogue.textBox1){
                textBox1.textBox.text += dialogue.dialogue[i];
                SoundManager.instance.PlayFX(7);
                yield return new WaitForSeconds(0.01f);
            }
            else{
                textBox2.textBox.text += dialogue.dialogue[i];
                SoundManager.instance.PlayFX(7);
                yield return new WaitForSeconds(0.01f);
            }
        }

        if(dialogue.textBox1)
           dialogueCursorTop.SetActive(true);
        else
           dialogueCursorBot.SetActive(true);

        dialogue.finished = true;
        lastDialogue = null;
        lastDialogue = dialogue;
        busy = false;
    }

    public void WriteMultiple(MapDialogue[] allDialogue)
    {
        StartCoroutine(WriteMultipleDialogue(allDialogue));
    }
    private IEnumerator WriteMultipleDialogue(MapDialogue[] allDialogue)
    {
        dialogueFinished = false;
        multipleLines = true;
        busy = true;
        screenDim.gameObject.SetActive(true);
        screenDim.animator.SetTrigger("FadeIn");
        yield return new WaitForSeconds(0.5f);
        for (int i = 0; i < allDialogue.Length; i++)
        {
            buttonPressed = false;
            StartCoroutine(WriteDialogue(allDialogue[i]));
            yield return new WaitUntil(() => buttonPressed == true);
            allDialogue[i].finished = true;
        }
        textBox1.textBox.text = "";
        textBox1.namePlate.text = "";
        textBox1.portrait.sprite = null;
        textBox1.portrait.gameObject.SetActive(false);
        textBox2.textBox.text = "";
        textBox2.namePlate.text = "";
        textBox2.portrait.sprite = null;
        textBox2.portrait.gameObject.SetActive(false);
        screenDim.animator.SetTrigger("FadeOut");
        dialogueFinished = true;
        yield return null;
    }

    public void ClearDialogue()
    {
        if(!busy && !multipleLines){
            textBox1.textBox.text = "";
            textBox1.namePlate.text = "";
            textBox1.portrait.sprite = null;
            textBox1.portrait.gameObject.SetActive(false);
            screenDim.animator.SetTrigger("FadeOut");
            dialogueCursorBot.SetActive(false);
            dialogueCursorTop.SetActive(false);
        }
    }
    public void NextDialogue()
    {
        if(!busy && multipleLines){
            buttonPressed = true;
            dialogueCursorTop.SetActive(false);
            dialogueCursorBot.SetActive(false);
        }

        else if(!busy && !multipleLines && lastDialogue && lastDialogue.scriptDialogue)
        {
            textBox1.textBox.text = "";
            textBox1.namePlate.text = "";
            textBox1.portrait.sprite = null;
            textBox1.portrait.gameObject.SetActive(false);
            screenDim.animator.SetTrigger("FadeOut");
            dialogueCursorBot.SetActive(false);
            dialogueCursorTop.SetActive(false);
        }
    }    
}
