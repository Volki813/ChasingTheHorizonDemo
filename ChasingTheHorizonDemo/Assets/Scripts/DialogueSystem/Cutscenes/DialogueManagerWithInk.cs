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
    [SerializeField] private GameObject dialogueHolder = null;
    [SerializeField] private TextMeshProUGUI dialogueText = null;
    [SerializeField] private TextMeshProUGUI speakerText = null;
    [SerializeField] private Animator portraitAnimator = null;

    [Header("Choices UI")]
    [SerializeField] private GameObject[] choices = null;
    private TextMeshProUGUI[] choicesText = null;

    [Header("Ink JSON")]
    [SerializeField] private TextAsset inkJSON = null;

    private PlayerInput input = null;

    private Story currentStory;

    public bool dialogueIsPlaying { get; private set; }

    public static DialogueManagerWithInk instance { get; private set; }

    private const string SPEAKER_TAG = "speaker"; // the same as left from the ":" in the ink file
    private const string PORTRAIT_TAG = "portrait"; // tags can be used for the portraits, speaker, position, etc.

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
        dialogueHolder.SetActive(false);

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
        dialogueHolder.SetActive(true);

        ContinueStory();
    }

    private IEnumerator ExitDialogueMode()
    {
        yield return new WaitForSeconds(0.2f);

        dialogueIsPlaying = false;
        dialogueHolder.SetActive(false);
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
            // handle tags
            HandleTags(currentStory.currentTags);
        }
        else
        {
            StartCoroutine(ExitDialogueMode());
        }
    }

    private void HandleTags(List<string> currentTags)
    {
        foreach (string tag in currentTags)
        {
            // tags in ink turn into strings in unity which can be used to create a key/value system
            string[] splitTag = tag.Split(":"); // can change what symbol we use for splitting the key and the value
            if (splitTag.Length != 2) // Error if e.g. a tag has more than one ":" in it
            {
                Debug.LogError("Tag couldn't be parsed: " + tag);
            }
            string tagKey = splitTag[0].Trim();
            string tagValue = splitTag[1].Trim();

            switch (tagKey)
            {
                case SPEAKER_TAG:
                    speakerText.text = tagValue;
                    break;
                case PORTRAIT_TAG:
                    portraitAnimator.Play(tagValue); // name the tag the same as the animation
                    break;
                default:
                    Debug.LogWarning("Tag came in but is not being handled: " + tag);
                    break;
            }
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
