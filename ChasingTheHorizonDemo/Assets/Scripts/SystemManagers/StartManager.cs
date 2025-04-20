using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using System.Collections;

//Manages the Start Screen
//Need to create an options menu
//Eventually there will be an option to load a save or start a new game
public class StartManager : MonoBehaviour
{
    [SerializeField] private Text[] titleTexts = null;
    [SerializeField] private Button playButton = null;
    [SerializeField] private Button optionsButton = null;
    [SerializeField] private Button quitButton = null;

    [SerializeField] private GameObject volumeButton = null;

    [SerializeField] private GameObject optionsMenu = null;
    [SerializeField] private GameObject allButtons = null;

    [SerializeField] private GameObject normalMode = null;
    [SerializeField] private GameObject hardMode = null;

    [SerializeField] private Slider musicSlider = null;
    [SerializeField] private Slider sfxSlider = null;

    [SerializeField] private AudioSource musicVolume = null;
    [SerializeField] private AudioSource sfxVolume = null;

    private void Start()
    {
        foreach (Text titleText in titleTexts) TweenManager.TweenTextAlpha(titleText, 0, 1, 0.5f).SetEase(EaseType.ExpoEaseOut);
        TweenManager.TweenAnchoredPositionX(playButton.gameObject, -10.54f, -5.3f, 0.25f).SetStartDelay(0.5f).SetEase(EaseType.ExpoEaseIn);
        TweenManager.TweenAnchoredPositionX(optionsButton.gameObject, -10.54f, -5.3f, 0.5f).SetStartDelay(0.5f).SetEase(EaseType.ExpoEaseIn);
        TweenManager.TweenAnchoredPositionX(quitButton.gameObject, -10.54f, -5.3f, 0.75f).SetStartDelay(0.5f).SetEase(EaseType.ExpoEaseIn)
            .SetOnComplete(() =>
            {
                playButton.interactable = true;
                optionsButton.interactable = true;
                quitButton.interactable = true;
                EventSystem.current.SetSelectedGameObject(playButton.gameObject);
            });
    }

    private void Update()
    {
        if (EventSystem.current.currentSelectedGameObject == null)
        {
            EventSystem.current.SetSelectedGameObject(playButton.gameObject);
        }

        if (optionsMenu.activeSelf)
        {
            musicVolume.volume = musicSlider.value;
            sfxVolume.volume = sfxSlider.value;
        }
    }

    //BUTTONS
    public void Play()
    {
        if (!normalMode.activeSelf)
        {
            normalMode.SetActive(true);
            hardMode.SetActive(true);
            StartCoroutine(Highlight(normalMode));
        }
        else
        {
            normalMode.SetActive(false);
            hardMode.SetActive(false);
        }
    }
    public void Options()
    {
        if (!optionsMenu.activeSelf)
        {
            optionsMenu.SetActive(true);
            SetupOptions();
            StartCoroutine(Highlight(volumeButton));
        }
        else
        {
            optionsMenu.SetActive(false);
        }
    }
    public void Exit()
    {
        Application.Quit();
    }

    public void NormalMode()
    {
        PlayerPrefs.SetString("Difficulty", "Normal");
        SceneManager.LoadScene("Cutscene 1");
    }

    public void HardMode()
    {
        PlayerPrefs.SetString("Difficulty", "Hard");
        SceneManager.LoadScene("Cutscene 1");
    }

    private void SetupOptions()
    {
        musicSlider.value = musicVolume.volume;
        sfxSlider.value = sfxVolume.volume;
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    private IEnumerator Highlight(GameObject button)
    {
        EventSystem.current.SetSelectedGameObject(null);
        yield return new WaitForSeconds(0.05f);
        EventSystem.current.SetSelectedGameObject(button);
    }
}
