using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapDialogueManager : MonoBehaviour
{
    public static MapDialogueManager instance;
    
    [SerializeField] private GameObject combatReadout = null;
    public GameObject screenDim = null;
    private CursorController cursorController;
    private bool busy = false;
    private bool buttonPressed = false;
    private bool multipleLines = false;
    public bool dialogueFinished = false;

    [SerializeField] private GameObject dialogueCursorTop = null;
    [SerializeField] private GameObject dialogueCursorBot = null;

    [Header("Top Textbox")]
    [SerializeField] private Text textBox1 = null;
    [SerializeField] private Text namePlate1 = null;
    [SerializeField] private Image portrait1 = null;

    [Header("Bottom Textbox")]
    [SerializeField] private Text textBox2 = null;
    [SerializeField] private Text namePlate2 = null;
    [SerializeField] private Image portrait2 = null;

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
        
        if(dialogue.scriptDialogue && !screenDim.activeSelf){
            screenDim.SetActive(true);
        }

        if(dialogue.textBox1){
            namePlate1.text = "";
            portrait1.sprite = null;
            textBox1.text = "";

            namePlate1.text = dialogue.unit.unitName;
            portrait1.gameObject.SetActive(true);
            portrait1.sprite = dialogue.unit.portrait;
            portrait1.GetComponent<RectTransform>().localScale = new Vector3(-1, 1, 1);
        }
        else{
            namePlate2.text = "";
            portrait2.sprite = null;
            textBox2.text = "";

            namePlate2.text = dialogue.unit.unitName;
            portrait2.gameObject.SetActive(true);
            portrait2.sprite = dialogue.unit.portrait;
        }

        for(int i = 0; i < dialogue.dialogue.Length; i++)
        {
            if(dialogue.textBox1){
                textBox1.text += dialogue.dialogue[i];
                SoundManager.instance.PlayFX(7);
                yield return new WaitForSeconds(0.01f);
            }
            else{
                textBox2.text += dialogue.dialogue[i];
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

        textBox1.text = "";
        textBox2.text = "";
        namePlate1.text = "";
        namePlate2.text = "";

        if(dialogue.scriptDialogue && !screenDim.activeSelf){
            screenDim.SetActive(true);
            screenDim.GetComponent<Animator>().SetTrigger("FadeIn");
        }
        
        if(dialogue.textBox1){
            namePlate1.text = dialogue.unit.unitName;
            portrait1.gameObject.SetActive(true);
            portrait1.sprite = dialogue.unit.portrait;
            portrait1.GetComponent<RectTransform>().localScale = new Vector3(-1, 1, 1);
        }
        else{
            namePlate2.text = dialogue.unit.unitName;
            portrait2.gameObject.SetActive(true);
            portrait2.sprite = dialogue.unit.portrait;
        }

        for(int i = 0; i < dialogue.dialogue.Length; i++) {
            if(dialogue.textBox1){
                textBox1.text += dialogue.dialogue[i];
                SoundManager.instance.PlayFX(7);
                yield return new WaitForSeconds(0.01f);
            }
            else{
                textBox2.text += dialogue.dialogue[i];
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
        screenDim.SetActive(true);
        screenDim.GetComponent<Animator>().SetTrigger("FadeIn");
        yield return new WaitForSeconds(0.5f);
        for (int i = 0; i < allDialogue.Length; i++)
        {
            buttonPressed = false;
            StartCoroutine(WriteDialogue(allDialogue[i]));
            yield return new WaitUntil(() => buttonPressed == true);
            allDialogue[i].finished = true;
        }
        textBox1.text = "";
        namePlate1.text = "";
        portrait1.sprite = null;
        portrait1.gameObject.SetActive(false);
        textBox2.text = "";
        namePlate2.text = "";
        portrait2.sprite = null;
        portrait2.gameObject.SetActive(false);
        screenDim.GetComponent<Animator>().SetTrigger("FadeOut");
        dialogueFinished = true;
        yield return null;
    }

    public void ClearDialogue()
    {
        if(!busy && !multipleLines){
            textBox1.text = "";
            namePlate1.text = "";
            portrait1.sprite = null;
            portrait1.gameObject.SetActive(false);
            screenDim.GetComponent<Animator>().SetTrigger("FadeOut");
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
            textBox1.text = "";
            namePlate1.text = "";
            portrait1.sprite = null;
            portrait1.gameObject.SetActive(false);
            screenDim.GetComponent<Animator>().SetTrigger("FadeOut");
            dialogueCursorBot.SetActive(false);
            dialogueCursorTop.SetActive(false);
        }
    }    
}
