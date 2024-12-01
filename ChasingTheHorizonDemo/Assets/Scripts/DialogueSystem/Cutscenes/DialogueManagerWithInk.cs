using UnityEngine;
using TMPro;
using Ink.Runtime;
using UnityEngine.EventSystems;
using UnityEditor;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public class DialogueManagerWithInk : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField] private float typingSpeed = 0.02f; // the lower, the faster

    [Header("Dialogue UI")]
    [SerializeField] private GameObject dialogueHolder = null;
    [SerializeField] private GameObject continueIcon = null;
    [SerializeField] private TextMeshProUGUI dialogueText = null;
    [SerializeField] private TextMeshProUGUI speakerText = null;
    [SerializeField] private Animator portraitAnimator = null;
    [SerializeField] private Animator facingAnimator = null;

    [Header("Choices UI")]
    [SerializeField] private GameObject[] choices = null;
    private TextMeshProUGUI[] choicesText = null;

    [Header("Audio")]
    [SerializeField] private bool playTypingSound = false;
    [SerializeField] private AudioClip dialogueTypingSoundClip = null;
    [Range(1, 20)][SerializeField] private int frequencyLevel = 2; // sound to play every [value] step when typing the dialogue
    [SerializeField] private bool stopAudioSource = false; // typing sound can overlap, so tick this if you don't want it to
    private AudioSource audioSource;

    [Header("Ink JSON")]
    [SerializeField] private TextAsset inkJSON = null;

    private PlayerInput input = null;

    private Story currentStory;

    public bool dialogueIsPlaying { get; private set; }
    private bool nextIsPressed = false;
    private bool canContinueToNextLine = false; // use as a condition for whenever a button is pressed to proceed
    private Coroutine displayLineCoroutine = null; // used to make it so no more than one coroutine display a line at a time 

    public static DialogueManagerWithInk instance { get; private set; }

    private const string SPEAKER_TAG = "speaker"; // the same as left from the ":" in the ink file
    private const string PORTRAIT_TAG = "portrait"; // tags can be used for the portraits, speaker, position, etc.
    private const string FACING_TAG = "facing";

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one Dialogue Manager in the scene!");
        }
        instance = this;
        input = GetComponent<PlayerInput>();
        audioSource = this.gameObject.AddComponent<AudioSource>();
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
        if (input.actions["Next"].WasPressedThisFrame())
        {
            nextIsPressed = true;
        }

        if (!dialogueIsPlaying) return;

        if (canContinueToNextLine && currentStory.currentChoices.Count == 0 && nextIsPressed)
        {
            nextIsPressed = false;
            ContinueStory();
        }
    }

    public void EnterDialogueMode(TextAsset inkJSON)
    {
        currentStory = new Story(inkJSON.text);
        dialogueIsPlaying = true;
        dialogueHolder.SetActive(true);

        // reset values from the tags
        speakerText.text = "???";
        portraitAnimator.Play("default");
        facingAnimator.Play("face_right");

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
            if (displayLineCoroutine != null)
            {
                StopCoroutine(displayLineCoroutine);
            }
            displayLineCoroutine = StartCoroutine(DisplayLine(currentStory.Continue()));
            // handle tags
            HandleTags(currentStory.currentTags);
        }
        else
        {
            StartCoroutine(ExitDialogueMode());
        }
    }

    private IEnumerator DisplayLine(string line)
    {
        // empty dialogue text
        dialogueText.text = line;
        dialogueText.maxVisibleCharacters = 0;

        // actions before the line starts
        continueIcon.SetActive(false);
        HideChoices();

        canContinueToNextLine = false;

        bool isAddingRichTextTag = false;

        foreach (char letter in line.ToCharArray())
        {
            // finish line immediately when next is pressed
            if (nextIsPressed) // a little buggy
            {
                nextIsPressed = false;
                dialogueText.maxVisibleCharacters = line.Length;
                break;
            }

            if (letter == '<' || isAddingRichTextTag)
            {
                isAddingRichTextTag = true;
                if (letter == '>')
                {
                    isAddingRichTextTag = false;
                }
            }
            else
            {
                if (playTypingSound) PlayDialogueSound(dialogueText.maxVisibleCharacters);
                dialogueText.maxVisibleCharacters++;
                yield return new WaitForSeconds(typingSpeed);
            }
        }

        // actions when the line is finished
        continueIcon.SetActive(true);
        DisplayChoices();

        canContinueToNextLine = true;
    }

    private void PlayDialogueSound(int currentDisplayedCharacterCount)
    {
        if (currentDisplayedCharacterCount % frequencyLevel == 0)
        {
            if (stopAudioSource) audioSource.Stop();
            audioSource.PlayOneShot(dialogueTypingSoundClip);
        }
    }

    private void HideChoices()
    {
        foreach (GameObject choice in choices)
        {
            choice.SetActive(false);
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
                case FACING_TAG:
                    facingAnimator.Play(tagValue);
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
        if (canContinueToNextLine)
        {
            currentStory.ChooseChoiceIndex(choiceIndex);
            ContinueStory();
        }
    }

    public void ContinueButtonPress() // for testing purposes, drag to a button
    {
        if (canContinueToNextLine && currentStory.currentChoices.Count == 0)
        {
            ContinueStory();
        }
    }
}
