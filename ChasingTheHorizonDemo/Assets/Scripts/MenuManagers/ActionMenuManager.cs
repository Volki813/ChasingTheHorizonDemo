using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

//Manages the Action Menu as well as the Inventory and the Combat Preview menu
//I want to seperate the Inventory and Combat Preview menu into their own scripts eventually but for now all of that is handled here
//There should only be 1 ActionMenuManager script per scene
public class ActionMenuManager : MonoBehaviour
{
    public static ActionMenuManager instance;

    [Header("Other References")]
    CursorController cursor;    
    [SerializeField] private Button actionMenuButton = null;

    public GameObject inventoryMenu = null;
    public GameObject combatPreview = null;
    public GameObject weaponSelection = null;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        cursor = FindObjectOfType<CursorController>();
    }

    private void OnEnable()
    {
        StartCoroutine(HighlightButton());
    }
    private void OnDisable()
    {
        inventoryMenu.SetActive(false);
    }

    public void Attack()
    {
        if(cursor.selectedUnit.enemiesInRange.Count >= 1)
        {
            cursor.SetState(new ActionMenuState(cursor));
            GetComponent<RectTransform>().anchoredPosition = new Vector2(-1400, 40);
            EventSystem.current.SetSelectedGameObject(null);
            cursor.controls.UI.Disable();
            cursor.controls.MapScene.Enable();
            cursor.SetState(new AttackState(cursor));
        }
        else if(cursor.selectedUnit.enemiesInRange.Count == 0)
        {
            Debug.Log("test");
        }
    }
    public void Items()
    {
        if(inventoryMenu.activeSelf == false)
        {
            inventoryMenu.SetActive(true);
        }
        else
        {
            inventoryMenu.SetActive(false);
        }
    }
    public void Wait()
    {
        cursor.selectedUnit.Rest();
        cursor.selectedUnit = null;
        gameObject.SetActive(false);
        cursor.controls.UI.Disable();
        cursor.controls.MapScene.Enable();
        cursor.SetState(new MapState(cursor));
    }

    public void CloseInventory()
    {
        inventoryMenu.SetActive(false);
        StartCoroutine(HighlightButton());
    }
    private IEnumerator HighlightButton()
    {
        EventSystem.current.SetSelectedGameObject(null);
        yield return null;
        EventSystem.current.SetSelectedGameObject(actionMenuButton.gameObject);
    }
}
