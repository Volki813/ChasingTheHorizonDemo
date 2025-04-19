using UnityEngine;
using TMPro;
using Ink.Runtime;
using UnityEngine.EventSystems;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public class DialogueManager : MonoBehaviour
{
    [Header("Parameters")]
    [Tooltip("the lower, the faster")][SerializeField] private float typingSpeed = 0.02f; // the lower, the faster
    [SerializeField] private float defaultDialogueFontSize = 36;
    [SerializeField] private float defaultSpeakerFontSize = 54;

    [Header("Dialogue UI")]
    [SerializeField] private GameObject dialogueHolder = null;
    [SerializeField] private GameObject continueIcon = null;
    private Tween<float> continueIconTween = null;
    [SerializeField] private TextMeshProUGUI dialogueText = null;
    [SerializeField] private TextMeshProUGUI speakerText = null;
    [SerializeField] private SerializableDictionary<string, Vector3> positions = null;

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

    private PlayerInput input = null;
    private Story currentStory;
    private Actor currentActor = null;

    public bool dialogueIsPlaying { get; private set; }
    public bool nextIsPressed { get; private set; }
    private bool canContinueToNextLine = false; // use as a condition for whenever a button is pressed to proceed
    private Coroutine displayLineCoroutine = null; // used to make it so no more than one coroutine display a line at a time 

    public static DialogueManager instance { get; private set; }

    [Header("Queues")]
    private Queue<Action> startLineFunctions = new Queue<Action>(); // Functions called at the beginning of a line
    private Queue<Action> endLineFunctions = new Queue<Action>(); // Functions called at the end of a line

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

        // set default font sizes
        dialogueText.fontSize = defaultDialogueFontSize;
        speakerText.fontSize = defaultSpeakerFontSize;

        continueIconTween = 
            TweenManager.TweenAnchoredPositionY(continueIcon, 10, -10, 0.2f).SetPingPong(-1);
        continueIconTween.Pause();
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

    /// <summary>
    /// Call this to start a cutscene with the passed in JSON file.
    /// </summary>
    /// <param name="inkJSON"></param>
    public void EnterDialogueMode(TextAsset inkJSON)
    {
        currentStory = new Story(inkJSON.text);
        dialogueIsPlaying = true;
        dialogueHolder.SetActive(true);

        // reset values from the tags
        currentActor = null;
        speakerText.text = "???";
        foreach (Actor actor in ActorManager.instance.actorsDictionary.Values) // reset portrait for each actor
        {
            actor.portrait.sprite = null;
        }

        BindExternalFunctions();

        ContinueStory();
    }

    private IEnumerator ExitDialogueMode()
    {
        yield return new WaitForSeconds(0.2f);

        UnbindExternalFunctions();

        foreach (Actor actor in ActorManager.instance.actorsDictionary.Values) // destroy each actor
        {
            Destroy(actor.transform.parent.gameObject);
            Destroy(actor.gameObject);
        }
        ActorManager.instance.actorsDictionary.Clear();

        speakerText.fontSize = defaultSpeakerFontSize;
        dialogueText.fontSize = defaultDialogueFontSize;

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
        continueIconTween.Pause();
        HideChoices();

        while (startLineFunctions.Count > 0) // invokes all queued eternal functions with timing "start"
        {
            Action action = startLineFunctions.Dequeue();
            action?.Invoke();
        }

        canContinueToNextLine = false;

        bool isAddingRichTextTag = false;

        foreach (char letter in line.ToCharArray())
        {
            // finish line immediately when next is pressed
            if (nextIsPressed)
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

        // if you press next when the line finished typing while the characters are still moving,
        // they would keep moving while the next line is typing if next is pressed. This is to prevent this from happening
        yield return new WaitUntil(() => ActorManager.instance.AreActorsNotMoving());

        // to prevent the next line from typing immediately if characters are moving and line is finished typing
        nextIsPressed = false;

        // actions when the line is finished
        while (endLineFunctions.Count > 0) // ivokes all queued external functions with timing "end"
        {
            Action action = endLineFunctions.Dequeue();
            action?.Invoke();
        }

        yield return null;

        // the same could happen at the end of a line
        yield return new WaitUntil(() => ActorManager.instance.AreActorsNotMoving());

        // to prevent the next line from typing immediately if characters are moving and line is finished typing
        nextIsPressed = false;

        continueIcon.SetActive(true);
        continueIconTween.Resume();
        DisplayChoices();

        canContinueToNextLine = true;
    }

    private void BindExternalFunctions()
    {
        currentStory.BindExternalFunction("CurrentSpeaker", (string timing, string actorName) =>
        {
            Action action = () =>
            {
                currentActor = ActorManager.instance.GetActorByName(actorName);
                
                // substring(1) basically removes the first letter of a string,
                // so this way the first letter doesn't have to be written in uppercase but will still show up as such
                speakerText.text = String.Concat(currentActor.name[0].ToString().ToUpper(), 
                    currentActor.name.Substring(1)); 
            };

            CheckTiming(timing, action);
        });

        currentStory.BindExternalFunction("SetPortrait", (string timing, string actorName, string portrait) =>
        {
            Action action = () =>
            {
                Actor actor = ActorManager.instance.GetActorByName(actorName);
                actor.SetPortrait(PORTRAIT_PATH, portrait);
            };

            CheckTiming(timing, action);
        });

        currentStory.BindExternalFunction("SetFacingDirection", (string timing, string actorName, string direction, bool withBounce) =>
        {
            Action action = () =>
            {
                Actor actor = ActorManager.instance.GetActorByName(actorName);
                actor.SetFacingDirection(direction);

                if (withBounce)
                {
                    StartCoroutine(actor.Bounce());
                }
            };

            CheckTiming(timing, action);
        });

        currentStory.BindExternalFunction("PlaySound", (string timing, string soundName) =>
        {
            Action action = () =>
            {
                AudioClip soundClip = Resources.Load<AudioClip>(SFX_PATH + soundName);
                soundSource.clip = soundClip;
                soundSource.Play();
            };

            CheckTiming(timing, action);
        });

        currentStory.BindExternalFunction("PlayMusic", (string timing, string musicName) =>
        {
            Action action = () =>
            {
                AudioClip musicClip = Resources.Load<AudioClip>(MUSIC_PATH + musicName);
                musicSource.clip = musicClip;
                musicSource.Play();
                if (musicName == "stop") musicSource.Stop(); // stop music if musicName is "stop"
            };

            CheckTiming(timing, action);
        });

        currentStory.BindExternalFunction("EditFontSize", (string timing, float fontSize, string speakerOrDialogue) =>
        {
            Action action = () =>
            {
                switch (speakerOrDialogue)
                {
                    case "speaker":
                        if (fontSize < 0) speakerText.fontSize = defaultSpeakerFontSize;
                        else speakerText.fontSize = fontSize;
                        break;
                    case "dialogue":
                        if (fontSize < 0) dialogueText.fontSize = defaultDialogueFontSize;
                        else dialogueText.fontSize = fontSize;
                        break;
                    default:
                        Debug.LogError("speakerOrDialogue has to be either 'speaker' or 'dialogue'");
                        break;
                }
            };

            CheckTiming(timing, action);
        });

        currentStory.BindExternalFunction("PlaceActor", (string timing, string actorName, string position) =>
        {
            Action action = () =>
            {
                try
                {
                    Actor actor = ActorManager.instance.GetActorByName(actorName);
                    actor.gameObject.SetActive(true);
                    Vector3 positionToPlaceIn = positions[position];

                    StartCoroutine(ActorManager.instance.PlaceActorAtPosition(actor, position, positionToPlaceIn));
                }
                catch (Exception e)
                {
                    Debug.LogError("position has to be 'far left', 'near left', 'center left', 'center right', " +
                    "'near right' or 'far right");
                }
            };

            CheckTiming(timing, action);
        });

        currentStory.BindExternalFunction("MoveActor", (string timing, string actorName, string destination,
            float timeToComplete) =>
        {
            Action action = () =>
            {
                try
                {
                    Actor actor = ActorManager.instance.GetActorByName(actorName);
                    Vector3 destinationPos = positions[destination];

                    StartCoroutine(ActorManager.instance.MoveActorToPosition(actor, destination, destinationPos, timeToComplete));
                }
                catch (Exception e)
                {
                    Debug.LogError("destination has to be 'far left', 'near left', 'center left', 'center right', " +
                                            "'near right' or 'far right");
                }
            };

            CheckTiming(timing, action);
        });

        currentStory.BindExternalFunction("RemoveActor", (string timing, string actorName) =>
        {
            Action action = () =>
            {
                Actor actor = ActorManager.instance.GetActorByName(actorName);
                actor.gameObject.SetActive(false);
            };

            CheckTiming(timing, action);
        });
    }

    private void UnbindExternalFunctions()
    {
        currentStory.UnbindExternalFunction("CurrentSpeaker");

        currentStory.UnbindExternalFunction("SetPortrait");

        currentStory.UnbindExternalFunction("SetFacingDirection");

        currentStory.UnbindExternalFunction("PlaySound");

        currentStory.UnbindExternalFunction("PlayMusic");

        currentStory.UnbindExternalFunction("EditFontSize");

        currentStory.UnbindExternalFunction("PlaceActor");

        currentStory.UnbindExternalFunction("MoveActor");

        currentStory.UnbindExternalFunction("RemoveActor");
    }

    private void CheckTiming(string timing, Action action)
    {
        if (timing == "start") startLineFunctions.Enqueue(action);
        else if (timing == "end") endLineFunctions.Enqueue(action);
        else Debug.LogError("timing has to be either 'start' or 'end'");
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

            // to prevent text from being skipped immediately if next is pressed while choice screen is active
            nextIsPressed = false;
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