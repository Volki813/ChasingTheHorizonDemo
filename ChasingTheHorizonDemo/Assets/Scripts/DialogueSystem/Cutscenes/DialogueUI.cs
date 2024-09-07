using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueUI : MonoBehaviour
{
    public Text speakerText;
    public Text dialogueText;
    public Image portrait;

    private int currentLineIndex = 0;
    private DialogueManager dialogueManager;

    private void Start()
    {
        dialogueManager = GetComponent<DialogueManager>();
        StartCoroutine(InitializeDialogue()); // because the manager loads the dialogue in start too, so UI has to wait until it's finished
    }

    IEnumerator InitializeDialogue()
    {
        // wait until the dialogue is loaded
        yield return new WaitUntil(() => dialogueManager.currentDialogue != null);

        DisplayNextLine();
    }

    public void DisplayNextLine()
    {
        if(currentLineIndex < dialogueManager.currentDialogue.lines.Length)
        {
            DialogueLine line = dialogueManager.currentDialogue.lines[currentLineIndex];

            speakerText.text = line.speaker;
            dialogueText.text = line.text;

            Sprite portraitSprite = Resources.Load<Sprite>(line.spriteName);
            if(portraitSprite != null)
            {
                portrait.sprite = portraitSprite;
            }
            else
            {
                Debug.LogWarning("Sprite not found: " + line.spriteName);
            }

            currentLineIndex++;
        }
        else
        {
            speakerText.text = "";
            dialogueText.text = "End of dialogue.";
            portrait.sprite = null;
        }
    }
}
