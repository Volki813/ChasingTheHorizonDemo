using UnityEngine;
using TMPro;
using Ink.Runtime;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public class DialogueManagerWithInk : MonoBehaviour
{
    [Header("Dialogue UI")]
    [SerializeField] private GameObject dialoguePanel = null;
    [SerializeField] private TextMeshProUGUI dialogueText = null;

    [Header("Choices UI")]
    [SerializeField] private GameObject[] choices = null;
    private TextMeshProUGUI[] choicesText = null;

    [Header("Ink JSON")]
    [SerializeField] private TextAsset inkJSON = null;

    private PlayerInput input = null;

    private Story currentStory;

    public bool dialogueIsPlaying { get; private set; }

    public static DialogueManagerWithInk instance { get; private set; }

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one Dialogue Manager in the scene!");
        }
        instance = this;
        input = GetComponent<PlayerInput>();
    }

    private void Start()
    {
        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);

        // Get choices texts
        choicesText = new TextMeshProUGUI[choices.Length];
        int index = 0;
        foreach (GameObject choice in choices)
        {
            choicesText[index] = choice.GetComponentInChildren<TextMeshProUGUI>();
            index++;
        }
    }

    private void Update()
    {
        if (!dialogueIsPlaying) return;

        if (input.actions["Next"].WasPressedThisFrame())
        {
            ContinueStory();
        }

        // using skip to start the dialogue for now
        if (input.actions["Skip"].WasPressedThisFrame()) // this doesn't work for some reason lol
        {
            EnterDialogueMode(inkJSON);
        }
    }

    public void EnterDialogueMode(TextAsset inkJSON)
    {
        currentStory = new Story(inkJSON.text);
        dialogueIsPlaying = true;
        dialoguePanel.SetActive(true);

        ContinueStory();
    }

    private IEnumerator ExitDialogueMode()
    {
        yield return new WaitForSeconds(0.2f);

        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);
        dialogueText.text = "End of Dialogue";
    }

    public void ContinueStory()
    {
        if (currentStory.canContinue)
        {
            // set text for current line
            dialogueText.text = currentStory.Continue();
            // display choices for current line
            DisplayChoices();
        }
        else
        {
            StartCoroutine(ExitDialogueMode());
        }
    }

    private void DisplayChoices()
    {
        List<Choice> currentChoices = currentStory.currentChoices;

        // make sure the UI can support the amount of choices
        if (currentChoices.Count > choices.Length)
        {
            Debug.LogError("More choices than the UI can support. Number of choices: " + currentChoices.Count);
        }

        int index = 0;
        foreach (Choice choice in currentChoices)
        {
            choices[index].SetActive(true);
            choicesText[index].text = choice.text;
            index++;
        }
        // disable remaining choices
        for (int i = index; i < choices.Length; i++)
        {
            choices[i].SetActive(false);
        }

        StartCoroutine(SelectFirstChoice());
    }

    private IEnumerator SelectFirstChoice()
    {
        EventSystem.current.SetSelectedGameObject(null);
        yield return new WaitForEndOfFrame();
        EventSystem.current.SetSelectedGameObject(choices[0].gameObject);
    }

    public void MakeChoice(int choiceIndex) // set choiceIndex like an array, e.g. first choice has index 0, second is 1, etc.
    {
        currentStory.ChooseChoiceIndex(choiceIndex);
        foreach (GameObject choice in choices)
        {
            choice.SetActive(false);
        }
        ContinueStory();
    }
}
