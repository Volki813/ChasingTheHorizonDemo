using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

//Manages the Start Screen
//Need to create an options menu
//Eventually there will be an option to load a save or start a new game
public class StartManager : MonoBehaviour
{
    [SerializeField] private Button startButton = null;

    private void Start()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(startButton.gameObject);
    }

    //BUTTONS
    public void Play()
    {
        SceneManager.LoadScene("Cutscene 1");
    }
    public void Options()
    {

    }
    public void Exit()
    {
        Application.Quit();
    }
}
