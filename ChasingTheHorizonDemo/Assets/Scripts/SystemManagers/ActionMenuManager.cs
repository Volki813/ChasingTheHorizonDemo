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

    [SerializeField] private GameObject inventoryMenu = null;
    public GameObject combatPreview = null;

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
            cursor.ResetState();
            cursor.controls.AttackCursor.Enable();
            GetComponent<RectTransform>().localPosition = new Vector2(-1058, 40.1f);
            EventSystem.current.SetSelectedGameObject(null);
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
        cursor.controls.ActionMenuCursor.Disable();
        cursor.controls.MapCursor.Enable();
    }

    public void PreviewBackButton()
    {
        combatPreview.SetActive(false);
        actionMenuButton.Select();
        cursor.ResetState();
        cursor.controls.AttackCursor.Enable();
    }

    public void PreviewAttackButton()
    {
        CombatManager.instance.EngageAttack(cursor.selectedUnit, cursor.selectedUnit.target);
        cursor.selectedUnit.target.GetComponent<SpriteRenderer>().color = Color.white;
        cursor.selectedUnit = null;
        combatPreview.SetActive(false);
        gameObject.SetActive(false);
        cursor.controls.ActionMenuCursor.Disable();
        cursor.controls.Disable();
    }

    private IEnumerator HighlightButton()
    {
        EventSystem.current.SetSelectedGameObject(null);
        yield return null;
        EventSystem.current.SetSelectedGameObject(actionMenuButton.gameObject);
    }
}
