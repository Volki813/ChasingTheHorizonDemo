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

    [Header("Top Textbox")]
    [SerializeField] private Text textBox1 = null;
    [SerializeField] private Text namePlate1 = null;
    [SerializeField] private Image portrait1 = null;

    [Header("Bottom Textbox")]
    [SerializeField] private Text textBox2 = null;
    [SerializeField] private Text namePlate2 = null;
    [SerializeField] private Image portrait2 = null;

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
        dialogue.finished = true;
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
        }
        
        if(dialogue.textBox1){
            namePlate1.text = dialogue.unit.unitName;
            portrait1.gameObject.SetActive(true);
            portrait1.sprite = dialogue.unit.portrait;
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
        dialogue.finished = true;
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
        for(int i = 0; i < allDialogue.Length; i++)
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
        screenDim.SetActive(false);
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
            screenDim.SetActive(false);
        }
    }
    public void NextDialogue()
    {
        if(!busy){
            buttonPressed = true;
        }
    }    
}
