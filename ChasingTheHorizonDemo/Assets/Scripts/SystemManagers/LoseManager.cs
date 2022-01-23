using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class LoseManager : MonoBehaviour
{
    [SerializeField] private GameObject combatReadout = null;
    [SerializeField] private GameObject restartButton = null;
    [SerializeField] private GameObject fadeOut = null;
    [Header("Check this box if you want the player to lose when all ally units die")]
    public bool allAllies = false;
    [Header("Check this box if you want the player to lose when any ally unit dies")]
    public bool anyAlly = false;
    [Header("Check this box if you want the player to lose when a specific ally unit dies")]
    public bool specificAlly = false;
    [Header("Drop the specific allies who's deaths will trigger a loss")]
    public UnitLoader[] specificAllies = null;
    private int amountOfAllies = 0;
    private bool gameOver = false;
    private CursorController cursor = null;

    private void Start()
    {
        cursor = FindObjectOfType<CursorController>();
        amountOfAllies = specificAllies.Length;
    }
    private void Update()
    {
        if(!gameOver){
            if(allAllies){
                if(TurnManager.instance.allyUnits.Count <= 0 && !combatReadout.activeSelf){
                    StartCoroutine(GameOver());
                    gameOver = true;
                }
            }
            if(anyAlly){
                foreach(UnitLoader unit in TurnManager.instance.allyUnits){
                    if(unit.currentHealth <= 0 && !combatReadout.activeSelf){
                        StartCoroutine(GameOver());
                        gameOver = true;
                    }
                }
            }
            if(specificAlly){
                if(specificAllies.Length < amountOfAllies && !combatReadout.activeSelf){
                    StartCoroutine(GameOver());
                    gameOver = true;
                }
            }
        }
    }

    public void RestartMap()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }
    public void TitleScreen()
    {
        SceneManager.LoadScene("StartScreen");
    }
    public void GiveUp()
    {
        Application.Quit();
    }

    private IEnumerator GameOver()
    {
        cursor.controls.MapScene.Disable();
        cursor.controls.UI.Disable();
        GameObject.Find("SystemsManager").SetActive(false);
        fadeOut.SetActive(true);
        yield return new WaitForSeconds(2f);
        EventSystem.current.SetSelectedGameObject(null);
        yield return new WaitForEndOfFrame();
        EventSystem.current.SetSelectedGameObject(restartButton.gameObject);
        yield return null;
    }
}
