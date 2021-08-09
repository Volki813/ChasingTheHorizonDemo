using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MenuManager : MonoBehaviour
{
    [SerializeField]
    private Button primaryButton;

    private void OnEnable()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(primaryButton.gameObject);
    }

    public void UnitMenu()
    {

    }

    public void Options()
    {

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
}
