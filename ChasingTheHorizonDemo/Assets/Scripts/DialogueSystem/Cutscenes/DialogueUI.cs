using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueUI : MonoBehaviour
{
    public Text speakerText;
    public Text dialogueText;
    public AudioSource music;
    public AudioSource soundEffect;
    public Image portrait;
    public GameObject choicesPanel;
    public Button[] choiceButtons;

    private int currentLineIndex = 0;
    [SerializeField] private string activeBranchId = null; // name this whatever the branchId of the first line is
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
        while (currentLineIndex < dialogueManager.currentDialogue.lines.Length)
        {
            DialogueLine line = dialogueManager.currentDialogue.lines[currentLineIndex];

            if (line.branchId == activeBranchId)
            {
                speakerText.text = line.speaker;
                dialogueText.text = line.text;

                if (line.musicName != null)
                {
                    AudioClip musicClip = Resources.Load<AudioClip>(line.musicName);
                    music.clip = musicClip;
                    music.Play();
                }
                else if (line.musicName == "Stop") // stops music if musicName in the json File is "Stop"
                {
                    music.Stop();
                }

                if (line.soundName != null)
                {
                    AudioClip soundClip = Resources.Load<AudioClip>(line.soundName);
                    soundEffect.clip = soundClip;
                    soundEffect.Play();
                }

                Sprite portraitSprite = Resources.Load<Sprite>(line.spriteName);
                if (portraitSprite != null)
                {
                    portrait.sprite = portraitSprite;
                }
                else
                {
                    Debug.LogWarning("Sprite not found: " + line.spriteName);
                }

                if (line.choices != null && line.choices.Length > 0)
                {
                    ShowChoices(line.choices);
                }
                else
                {
                    currentLineIndex++;
                    choicesPanel.SetActive(false);
                }
                return; // exit loop
            }

            // skips line if branchId doesn't match
            currentLineIndex++;
        }
        speakerText.text = "";
        dialogueText.text = "End of dialogue.";
        portrait.sprite = null;
        choicesPanel.SetActive(false);
    }

    private void ShowChoices(DialogueChoice[] choices)
    {
        choicesPanel.SetActive(true);

        for (int i = 0; i < choiceButtons.Length; i++)
        {
            if (i < choices.Length)
            {
                choiceButtons[i].gameObject.SetActive(true);
                choiceButtons[i].GetComponentInChildren<Text>().text = choices[i].choiceText;

                int nextLineIndex = choices[i].nextLineIndex;
                string newBranchId = choices[i].branchId;
                choiceButtons[i].onClick.RemoveAllListeners();
                choiceButtons[i].onClick.AddListener(() => OnChoiceSelected(nextLineIndex, newBranchId));
            }
            else
            {
                choiceButtons[i].gameObject.SetActive(false);
            }
        }
    }

    private void OnChoiceSelected(int nextLineIndex, string newBranchId)
    {
        currentLineIndex = nextLineIndex;
        activeBranchId = newBranchId;

        choicesPanel.SetActive(false);
        DisplayNextLine();
    }
}
