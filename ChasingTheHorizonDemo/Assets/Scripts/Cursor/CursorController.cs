using System.Collections;
using UnityEngine;
//Handles the functionality of the Cursor
//There should only be 1 CursorController script in any given scene
public class CursorController : MonoBehaviour
{
    public Sprite highlight = null;
    public Vector2 currentPosition = new Vector3(0, 0);
    public bool enemyTurn = false;
    public GameObject enemyInventory = null;

    [Header("Movement Contraints")] //These variables define the limits of the cursors range of movement
    [SerializeField] private float topMost = 0;
    [SerializeField] private float bottomMost = 0;
    [SerializeField] private float leftMost = 0;
    [SerializeField] private float rightMost = 0;
    [Header("Camera Constraints")] //These variables define the limits where the camera can move
    public float cameraTop = 0;
    public float cameraBottom = 0;
    public float cameraLeft = 0;
    public float cameraRight = 0;

    //REFERENCES
    private TileMap map;
    public CursorControls controls;
    [SerializeField] private GameObject menu = null;    
    [SerializeField] private Camera mapCamera = null;
    [Header("Map Frame Points")] //These variables define at which point your cursor needs to be for the camera to move in the respective direction
    [SerializeField] private Transform frameTop = null;
    [SerializeField] private Transform frameBottom = null;
    [SerializeField] private Transform frameLeft = null;
    [SerializeField] private Transform frameRight = null;

    private CursorState currentState;
    public CursorState previousState;

    private bool cursorMoving = false;
    private Stack movementRequests = new Stack();
    public bool buttonHeld = false;
    
    private IEnumerator buttonHeldCoroutine = null;

    public void SetState(CursorState state)
    {
        previousState = currentState;
        currentState = state;
        currentState.Start();
    }
    private void Awake()
    {
        controls = new CursorControls();

        controls.MapScene.Confirm.performed += ctx => Confirm();
        controls.MapScene.Cancel.performed += ctx => Cancel();
        controls.UI.Confirm.performed += ctx => Confirm();
        controls.UI.Cancel.performed += ctx => Cancel();

        controls.MapScene.Movement.performed += ctx => RequestMove(ctx.ReadValue<Vector2>());
        controls.MapScene.Movement.canceled += ctx => ButtonReleased();
    }
    private void Start()
    {
        map = FindObjectOfType<TileMap>();
        currentPosition = transform.localPosition;
        highlight = GetComponent<SpriteRenderer>().sprite;
        enemyTurn = false;
        controls.MapScene.Enable();
        SetState(new MapState(this));
    }

    private void RequestMove(Vector2 direction)
    {
        buttonHeldCoroutine = ButtonHeld(direction);
        StartCoroutine(buttonHeldCoroutine);
        if(!cursorMoving){
            movementRequests.Push(direction);
            StartCoroutine(StartMovement(10f));
        }
    }
    private void ButtonReleased()
    {
        buttonHeld = false;
        movementRequests.Clear();
        StopCoroutine(buttonHeldCoroutine);
    }
    private IEnumerator ButtonHeld(Vector2 direction)
    {
        yield return new WaitForSeconds(0.3f);
        buttonHeld = true;
        while(buttonHeld)
        {
            movementRequests.Push(direction);
            StartCoroutine(StartMovement(20f));
            yield return new WaitForSeconds(0.2f);
        }
    }
    private IEnumerator StartMovement(float moveSpeed)
    {
        SoundManager.instance.PlayFX(2);

        Vector2 poppedRequest = (Vector2)movementRequests.Peek();
        currentPosition += (Vector2)movementRequests.Pop();        
        if(currentPosition.x < leftMost || currentPosition.x > rightMost){
            currentPosition -= poppedRequest;
            yield break;
        }
        if(currentPosition.y < bottomMost || currentPosition.y > topMost){
            currentPosition -= poppedRequest;
            yield break;
        }

        MoveCamera();

        while((Vector2)transform.localPosition != currentPosition)
        {
            cursorMoving = true;
            transform.localPosition = Vector2.MoveTowards(transform.localPosition, currentPosition, moveSpeed * Time.deltaTime);
            yield return null;
        }
        cursorMoving = false;
    }

    private void MoveCamera()
    {
        if(currentPosition.x >= frameRight.position.x)
        {
            if(mapCamera.transform.position.x < cameraRight)
            {
                StartCoroutine(StartCameraMove(new Vector3(mapCamera.transform.position.x + 1f, mapCamera.transform.position.y, -10)));
            }
        }
        if(currentPosition.x <= frameLeft.position.x)
        {
            if(mapCamera.transform.position.x > cameraLeft)
            {
                StartCoroutine(StartCameraMove(new Vector3(mapCamera.transform.position.x - 1f, mapCamera.transform.position.y, -10)));
            }
        }

        if(currentPosition.y >= frameTop.position.y)
        {
            if(mapCamera.transform.position.y < cameraTop)
            {
                StartCoroutine(StartCameraMove(new Vector3(mapCamera.transform.position.x, mapCamera.transform.position.y + 1f, -10)));
            }
        }
        if(currentPosition.y <= frameBottom.position.y)
        {
            if(mapCamera.transform.position.y > cameraBottom)
            {
                StartCoroutine(StartCameraMove(new Vector3(mapCamera.transform.position.x, mapCamera.transform.position.y - 1f, -10)));
            }
        }
    }
    private IEnumerator StartCameraMove(Vector3 targetPosition)
    {
        while(mapCamera.transform.position != targetPosition)
        {
            mapCamera.transform.position = Vector3.MoveTowards(mapCamera.transform.position, targetPosition, 10f * Time.deltaTime);
            yield return null;
        }
    }   

    private void Confirm()
    {
        currentState.Confirm();
        SoundManager.instance.PlayFX(0);
    }
    private void Cancel()
    {
        currentState.Cancel();
        SoundManager.instance.PlayFX(1);
    }

    public void SelectUnit()
    {
        foreach(UnitLoader unit in FindObjectsOfType<UnitLoader>())
        {
            if(transform.localPosition == unit.transform.localPosition && map.selectedUnit == null)
            {
                if(unit.unit.allyUnit && unit.rested == false)
                {
                    map.selectedUnit = unit;
                    unit.Selected();
                    SetState(new UnitState(this));
                }
            }
        }
    }
    public void DeselectUnit()
    {
        if(map.selectedUnit != null)
        {
            if(map.selectedUnit.currentPath == null)
            {
                map.selectedUnit.animator.SetBool("Selected", false);
                map.selectedUnit = null;
                map.DehighlightTiles();
                SetState(new MapState(this));
            }
        }
    }
    public void SelectEnemy()
    {
        foreach(UnitLoader unit in FindObjectsOfType<UnitLoader>())
        {
            if(transform.localPosition == unit.transform.localPosition && !unit.unit.allyUnit)
            {          
                unit.GetWalkableTiles();                
                unit.spriteRenderer.color = Color.red;
                enemyInventory.SetActive(true);
                enemyInventory.GetComponent<EnemyInventory>().DisplayInventory(unit);                
            }
        }
    }
    public void ResetTiles()
    {
        map.DehighlightTiles();
        foreach(UnitLoader unit in FindObjectsOfType<UnitLoader>())
        {
            if(unit.spriteRenderer.color == Color.red)
            {
                unit.spriteRenderer.color = Color.white;
                enemyInventory.SetActive(false);
            }
        }
    }
    public void MoveUnit()
    {
        foreach(Node n in map.walkableTiles)
        {
            if(new Vector3(currentPosition.x, currentPosition.y) == new Vector3(n.x, n.y) && map.CanTraverse(n.x, n.y))
            {                
                if(map.selectedUnit.hasMoved == false)
                {
                    map.selectedUnit.originalPosition = map.selectedUnit.transform.localPosition;
                    map.GeneratePathTo(n.x, n.y);
                    map.selectedUnit.FollowPath();
                    break;
                }
            }
        }
    }
    public void UndoMove()
    {
        foreach (UnitLoader unit in FindObjectsOfType<UnitLoader>())
        {
            if (!unit.unit.allyUnit)
            {
                unit.spriteRenderer.color = Color.white;
            }
        }
        map.selectedUnit.enemiesInRange.Clear();
        map.selectedUnit.transform.localPosition = map.selectedUnit.originalPosition;
        map.selectedUnit.hasMoved = false;
        map.selectedUnit.currentPath = null;
        map.DehighlightTiles();
        map.selectedUnit.ActionMenu();
        map.selectedUnit.target = null;
        map.selectedUnit = null;
        SetState(new MapState(this));
        controls.UI.Disable();
        controls.MapScene.Enable();
    }
    public void CloseInventory()
    {
        ActionMenuManager.instance.CloseInventory();
    }
    public void SelectTarget()
    {
        foreach(UnitLoader unit in FindObjectsOfType<UnitLoader>())
        {
            if(transform.localPosition == unit.transform.localPosition && map.selectedUnit.enemiesInRange.Contains(unit) && 
                Vector2.Distance(map.selectedUnit.transform.localPosition, unit.transform.localPosition) <= map.selectedUnit.equippedWeapon.range)
            {
                map.selectedUnit.target = unit;
                ActionMenuManager.instance.combatPreview.SetActive(true);
                ActionMenuManager.instance.weaponSelection.SetActive(true);
                controls.MapScene.Disable();
                controls.UI.Enable();
                SetState(new CombatPreviewState(this));
            }
        }
    }    
    public void CancelAttack()
    {
        map.selectedUnit.target = null;
        ActionMenuManager.instance.combatPreview.SetActive(false);
        ActionMenuManager.instance.weaponSelection.SetActive(false);
        controls.UI.Disable();
        controls.MapScene.Enable();
        SetState(new AttackState(this));
    }
    public void AttackTarget()
    {
        CombatManager.instance.EngageAttack(map.selectedUnit, map.selectedUnit.target);
        map.selectedUnit.target.GetComponent<SpriteRenderer>().color = Color.white;
        map.selectedUnit = null;
        ActionMenuManager.instance.combatPreview.SetActive(false);
        ActionMenuManager.instance.weaponSelection.SetActive(false);
        ActionMenuManager.instance.gameObject.SetActive(false);
    }
    public void DisplayMenu()
    {
        foreach(Node n in map.graph)
        {
            if(transform.localPosition == new Vector3(n.x, n.y) && map.IsOccupied(n.x, n.y) == false)
            {
                menu.SetActive(true);
                SetState(new MenuState(this));
                controls.MapScene.Disable();
                controls.UI.Enable();
            }
        }
    }
    public void CloseMenu()
    {
        if (menu.transform.GetChild(0).gameObject.activeSelf == true && menu.transform.GetChild(1).gameObject.activeSelf == true)
        {
            menu.transform.GetChild(0).gameObject.SetActive(false);
            menu.transform.GetChild(1).gameObject.SetActive(false);
            return;
        }
        else if(menu.transform.GetChild(0).gameObject.activeSelf == true)
        {
            menu.transform.GetChild(0).gameObject.SetActive(false);
            return;
        }
        else if(menu.transform.GetChild(1).gameObject.activeSelf == true)
        {
            menu.transform.GetChild(1).gameObject.SetActive(false);
            menu.GetComponent<MenuManager>().Highlight();
            return;
        }        
        else
        {
            menu.SetActive(false);
            SetState(new MapState(this));
            controls.UI.Disable();
            controls.MapScene.Enable();
        }
    }    
}