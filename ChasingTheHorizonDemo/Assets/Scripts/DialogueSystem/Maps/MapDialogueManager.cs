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

        if(dialogue.scriptDialogue){
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
                yield return new WaitForSeconds(0.02f);
            }
            else{
                textBox2.text += dialogue.dialogue[i];
                yield return new WaitForSeconds(0.02f);
            }
        }
        dialogue.finished = true;
        busy = false;
    }

    public void ClearDialogue()
    {
        if(!busy){
            textBox1.text = "";
            namePlate1.text = "";
            portrait1.sprite = null;
            portrait1.gameObject.SetActive(false);
            screenDim.SetActive(false);
        }
    }
}
