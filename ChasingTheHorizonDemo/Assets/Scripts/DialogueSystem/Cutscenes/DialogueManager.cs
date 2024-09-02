using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public Dialogue currentDialogue;

    private void Start()
    {
        LoadDialogue(currentDialogue.dialogueName); // use name of the file (must be placed in Resources folder)
        DisplayDialogue();
    }

    public void LoadDialogue(string fileName) 
    {
        TextAsset jsonFile = Resources.Load<TextAsset>(fileName);
        if(jsonFile != null)
        {
            currentDialogue = JsonUtility.FromJson<Dialogue>(jsonFile.text);
        }
        else
        {
            Debug.LogError("File not found");
        }
    }

    public void DisplayDialogue()
    {
        foreach(DialogueLine line in currentDialogue.lines)
        {
            Debug.Log(line.speaker + ": " + line.text);
        }
    }
}
