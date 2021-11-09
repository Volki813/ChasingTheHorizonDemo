using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

//Handles the Main Menu of Map Scenes
//Additional functionality can be added
//Options Menu coming soon

public class MenuManager : MonoBehaviour
{
    [SerializeField] private Button primaryButton = null;
    [SerializeField] private GameObject unitsMenu = null;

    private void OnEnable()
    {
        StartCoroutine(HighlightButton());
    }

    private void OnDisable()
    {
        unitsMenu.SetActive(false);
    }

    public void UnitMenu()
    {
        if(unitsMenu.activeSelf == false)
        {
            unitsMenu.SetActive(true);
        }
        else
        {
            unitsMenu.SetActive(false);
        }
    }

    public void Options()
    {
        //Will bring up a screen that will let you change your controls and various other settings
    }

    public void EndTurn()
    {
        foreach(UnitLoader unit in FindObjectsOfType<UnitLoader>())
        {
            if(unit.unit.allyUnit == true)
            {
                unit.Rest();
            }
            gameObject.SetActive(false);
        }
    }

    private IEnumerator HighlightButton()
    {
        EventSystem.current.SetSelectedGameObject(null);
        yield return null;
        EventSystem.current.SetSelectedGameObject(primaryButton.gameObject);
    }
}
