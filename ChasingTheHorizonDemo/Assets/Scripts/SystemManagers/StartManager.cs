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
    [SerializeField] private GameObject volumeButton = null;

    [SerializeField] private Button startButton = null;
    [SerializeField] private GameObject optionsMenu = null;
    [SerializeField] private GameObject allButtons = null;

    [SerializeField] private Slider musicSlider = null;
    [SerializeField] private Slider sfxSlider = null;

    [SerializeField] private AudioSource musicVolume = null;
    [SerializeField] private AudioSource sfxVolume = null;

    private void Start()
    {
        StartCoroutine(HighlightButton(startButton.gameObject));
    }

    private void Update()
    {        
        if(EventSystem.current.currentSelectedGameObject == null)
        {
            EventSystem.current.SetSelectedGameObject(startButton.gameObject);
        }

        if(optionsMenu.activeSelf)
        {
            musicVolume.volume = musicSlider.value;            
            sfxVolume.volume = sfxSlider.value;
        }
    }

    //BUTTONS
    public void Play()
    {
        SceneManager.LoadScene("Cutscene 1");
    }
    public void Options()
    {
        if(!optionsMenu.activeSelf){
            optionsMenu.SetActive(true);
            SetupOptions();
            StartCoroutine(HighlightButton(volumeButton));
        }
        else{
            optionsMenu.SetActive(false);
        }
    }
    public void Exit()
    {
        Application.Quit();
    }

    private void SetupOptions()
    {
        musicSlider.value = musicVolume.volume;
        sfxSlider.value = sfxVolume.volume;
    }

    private IEnumerator HighlightButton(GameObject button)
    {
        allButtons.GetComponent<Animator>().SetTrigger("Enter");

        EventSystem.current.SetSelectedGameObject(null);
        yield return new WaitForSeconds(0.05f);
        EventSystem.current.SetSelectedGameObject(button);
    }
}
