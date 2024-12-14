using UnityEngine;
using TMPro;
using Ink.Runtime;
using UnityEngine.EventSystems;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public class DialogueManagerWithInk : MonoBehaviour
{
    [Header("Parameters")]
    [Tooltip("the lower, the faster")][SerializeField] private float typingSpeed = 0.02f; // the lower, the faster
    [SerializeField] private float defaultDialogueFontSize = 36;
    [SerializeField] private float defaultSpeakerFontSize = 54;

    [Header("Dialogue UI")]
    [SerializeField] private GameObject dialogueHolder = null;
    [SerializeField] private GameObject continueIcon = null;
    [SerializeField] private TextMeshProUGUI dialogueText = null;
    [SerializeField] private TextMeshProUGUI speakerText = null;

    [Header("Choices UI")]
    [SerializeField] private GameObject[] choices = null;
    private TextMeshProUGUI[] choicesText = null;

    [Header("Audio")]
    [Tooltip("only play typing sound if this is ticked")][SerializeField] private bool playTypingSound = false;
    [SerializeField] private AudioClip dialogueTypingSoundClip = null;
    [Tooltip("sound to play every [value] step when typing the dialogue")]
    [Range(1, 20)][SerializeField] private int frequencyLevel = 2; // sound to play every [value] step when typing the dialogue
    [Tooltip("typing sound can overlap, so tick this if you don't want it to")]
    [SerializeField] private bool stopAudioSource = false; // typing sound can overlap, so tick this if you don't want it to
    private AudioSource audioSource; // for typing sound
    [SerializeField] private AudioSource soundSource = null;
    [SerializeField] private AudioSource musicSource = null;

    [Header("Ink JSON")]
    [SerializeField] private TextAsset inkJSON = null;

    [SerializeField] private ActorManager actorManager = null;

    private PlayerInput input = null;

    private Story currentStory;
    private Actor currentActor = null;

    public bool dialogueIsPlaying { get; private set; }
    private bool nextIsPressed = false;
    private bool canContinueToNextLine = false; // use as a condition for whenever a button is pressed to proceed
    private Coroutine displayLineCoroutine = null; // used to make it so no more than one coroutine display a line at a time 

    public static DialogueManagerWithInk instance { get; private set; }

    [Header("Tags")]
    private const string ACTOR_TAG = "actor"; // actor = the character who's currently being handled
    // the same as left from the ":" in the ink file
    private const string PORTRAIT_TAG = "portrait"; // tags can be used for the portraits, speaker, position, etc.
    private const string FACING_TAG = "facing";
    private const string SOUND_TAG = "sound";
    private const string MUSIC_TAG = "music"; // name the music and sound in inky the same as the file name in assets
    private const string DIALOGUE_FONT_SIZE_TAG = "dialogue_font_size"; // set to -1 if default size
    private const string SPEAKER_FONT_SIZE_TAG = "speaker_font_size"; // set to -1 if default size

    [Header("Resources paths")]
    private const string SFX_PATH = "Sound/SFX/";
    private const string MUSIC_PATH = "Sound/Music/";
    private const string PORTRAIT_PATH = "Portraits/";


    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one Dialogue Manager in the scene!");
        }
        instance = this;
        input = GetComponent<PlayerInput>();
        audioSource = this.gameObject.AddComponent<AudioSource>();
        actorManager = GetComponent<ActorManager>();
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

        // set default fons sizes
        dialogueText.fontSize = defaultDialogueFontSize;
        speakerText.fontSize = defaultSpeakerFontSize;
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
        currentActor = null;
        speakerText.text = "???";
        foreach (Actor actor in actorManager.actors) // reset portrait for each actor
        {
            actor.portrait.sprite = null;
        }

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
            HandleTags(currentStory.currentTags, true);
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
        HandleTags(currentStory.currentTags, false); // for end tags 

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

    private void HandleTags(List<string> currentTags, bool startTags)
    {
        foreach (string tag in currentTags)
        {
            // tags in ink turn into strings in unity which can be used to create a key/value system
            string[] splitTag = tag.Split(":"); // can change what symbol we use for splitting the key and the value
            if (splitTag.Length != 3) // Error if e.g. a tag has more than two ":" in it
            {
                Debug.LogError("Tag couldn't be parsed: " + tag);
            }
            string tagStart = splitTag[0].Trim();
            string tagKey = splitTag[1].Trim();
            string tagValue = splitTag[2].Trim();

            if (tagStart == "start" && startTags)
            {
                ProcessTags(tagKey, tagValue, tag);
            }
            else if (tagStart == "end" && !startTags)
            {
                ProcessTags(tagKey, tagValue, tag);
            }
        }
    }

    private void ProcessTags(string tagKey, string tagValue, string tag)
    {
        switch (tagKey)
        {
            case ACTOR_TAG:
                currentActor = actorManager.GetActorByName(tagValue);
                speakerText.text = String.Concat(currentActor.name[0].ToString().ToUpper(), currentActor.name.Substring(1));
                break;
            case PORTRAIT_TAG:
                SetPortrait(tagValue, currentActor); // name the tag the same as the portrait
                break;
            case FACING_TAG:
                SetFacingDirection(tagValue, currentActor);
                break;
            case SOUND_TAG:
                AudioClip soundClip = Resources.Load<AudioClip>(SFX_PATH + tagValue);
                soundSource.clip = soundClip;
                soundSource.Play();
                break;
            case MUSIC_TAG:
                AudioClip musicClip = Resources.Load<AudioClip>(MUSIC_PATH + tagValue);
                musicSource.clip = musicClip;
                musicSource.Play();
                if (tagValue == "stop") musicSource.Stop(); // stop music if music tag is "stop"
                break;
            case DIALOGUE_FONT_SIZE_TAG:
                if (float.TryParse(tagValue, out float dialogue_font_size))
                {
                    if (dialogue_font_size == -1) dialogueText.fontSize = defaultDialogueFontSize;
                    else dialogueText.fontSize = dialogue_font_size;
                }
                else
                {
                    Debug.LogWarning("float not parsed correctly");
                }
                break;
            case SPEAKER_FONT_SIZE_TAG:
                if (float.TryParse(tagValue, out float speaker_font_size))
                {
                    if (speaker_font_size == -1) speakerText.fontSize = defaultSpeakerFontSize;
                    else speakerText.fontSize = speaker_font_size;
                }
                else
                {
                    Debug.LogWarning("float not parsed correctly");
                }
                break;
            default:
                Debug.LogWarning("Tag came in but is not being handled: " + tag);
                break;
        }
    }

    private void SetPortrait(string tagValue, Actor actor)
    {
        actor.portrait.sprite = Resources.Load<Sprite>(PORTRAIT_PATH + actor.name + "/" + tagValue);
    }

    private void SetFacingDirection(string tagValue, Actor actor)
    {
        switch (tagValue) // tagvalue is either "left" or "right"
        {
            case "left":
                actor.position.localScale = new Vector3(-1,1,1);
                break;
            case "right":
                actor.position.localScale = Vector3.one;
                break;
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
