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
    private TileMap map;
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
        map = FindObjectOfType<TileMap>();
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
        if(map.selectedUnit.enemiesInRange.Count >= 1)
        {
            cursor.SetState(new ActionMenuState(cursor));
            GetComponent<RectTransform>().anchoredPosition = new Vector2(-1400, 40);
            EventSystem.current.SetSelectedGameObject(null);
            cursor.controls.UI.Disable();
            cursor.controls.MapScene.Enable();
            cursor.SetState(new AttackState(cursor));
            map.DehighlightTiles();
            map.attackableTiles = map.GenerateRange((int)map.selectedUnit.transform.localPosition.x, (int)map.selectedUnit.transform.localPosition.y, map.selectedUnit.equippedWeapon.range, map.selectedUnit);
            map.HighlightTiles();
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
            CloseInventory();
        }
    }
    public void Wait()
    {
        map.selectedUnit.Rest();
        map.selectedUnit = null;
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
