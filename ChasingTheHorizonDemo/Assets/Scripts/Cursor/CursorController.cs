using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
//Handles the functionality of the Cursor
//There should only be 1 CursorController script in any given scene
public class CursorController : MonoBehaviour
{
    public Sprite highlight = null;
    public Vector2 currentPosition = new Vector3(0, 0);
    public UnitLoader selectedUnit = null;
    public bool enemyTurn = false;

    [Header("Movement Contraints")] //These variables keep the cursor from being able to move off the map
    [SerializeField] private float topMost = 0;
    [SerializeField] private float bottomMost = 0;
    [SerializeField] private float leftMost = 0;
    [SerializeField] private float rightMost = 0;
    [Header("Camera Constraints")] //These variables make sure the Camera doesnt move too far when moving with the cursor
    [SerializeField] private float cameraTop = 0;
    [SerializeField] private float cameraBottom = 0;
    [SerializeField] private float cameraLeft = 0;
    [SerializeField] private float cameraRight = 0;

    //REFERENCES
    public CursorControls controls;
    [SerializeField] private GameObject menu = null;    
    [SerializeField] private Camera mapCamera = null;
    [Header("Map Frame Points")] //These variables make sure the Camera always moves when your cursor reaches any of these points
    [SerializeField] private Transform frameTop = null;
    [SerializeField] private Transform frameBottom = null;
    [SerializeField] private Transform frameLeft = null;
    [SerializeField] private Transform frameRight = null;

    private CursorState currentState;
    public CursorState previousState;

    private InputAction.CallbackContext moveContext;
    private bool movePressed = false;

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

        controls.MapScene.Movement.performed += ctx => MapMovement(ctx.ReadValue<Vector2>());
    }
    private void Start()
    {
        currentPosition = transform.position;
        highlight = GetComponent<SpriteRenderer>().sprite;
        enemyTurn = false;
        controls.MapScene.Enable();
        SetState(new MapState(this));
    }

    private void CursorCameraMovement()
    {
        //Vertical Movement
        if (transform.position.y >= frameTop.position.y)
        {
            if (mapCamera.transform.position.y < cameraTop)
            {
                mapCamera.transform.position += new Vector3(0, 1, 0);
            }
        }
        if (transform.position.y <= frameBottom.position.y)
        {
            if (mapCamera.transform.position.y > cameraBottom)
            {
                mapCamera.transform.position -= new Vector3(0, 1, 0);
            }
        }

        //Horizontal Movement
        if (transform.position.x >= frameRight.position.x)
        {
            if (mapCamera.transform.position.x < cameraRight)
            {
                mapCamera.transform.position += new Vector3(1, 0, 0);
            }
        }
        if (transform.position.x <= frameLeft.position.x)
        {
            if (mapCamera.transform.position.x > cameraLeft)
            {
                mapCamera.transform.position -= new Vector3(1, 0, 0);
            }
        }
    }
    private void MapMovement(Vector2 direction)
    {
        CursorCameraMovement();
        if (direction == new Vector2(-1, 0))
        {
            if (currentPosition.x > leftMost)
            {
                currentPosition.x -= 1;
                transform.position = currentPosition;                
            }
        }
        if (direction == new Vector2(1, 0))
        {
            if (currentPosition.x < rightMost)
            {
                currentPosition.x += 1;
                transform.position = currentPosition;
            }
        }
        if (direction == new Vector2(0, 1))
        {
            if (currentPosition.y < topMost)
            {
                currentPosition.y += 1;
                transform.position = currentPosition;
            }
        }
        if (direction == new Vector2(0, -1))
        {
            if (currentPosition.y > bottomMost)
            {
                currentPosition.y -= 1;
                transform.position = currentPosition;
            }
        }
        SoundManager.instance.PlayFX(2);
    }   

    private void MoveCursor(Vector2 direction, InputAction.CallbackContext context)
    {        
        StartCoroutine(CursorMovement(direction, context));
    }
    private IEnumerator CursorMovement(Vector2 direction, InputAction.CallbackContext context)
    {
        yield return null;
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
            if(transform.position == unit.transform.position && selectedUnit == null)
            {
                if(unit.unit.allyUnit && unit.rested == false)
                {
                    selectedUnit = unit;
                    unit.Selected();
                    SetState(new UnitState(this));
                }
            }
        }
    }
    public void DeselectUnit()
    {
        if(selectedUnit != null)
        {
            selectedUnit.animator.SetBool("Selected", false);
            selectedUnit.ResetTiles();
            selectedUnit = null;
            SetState(new MapState(this));
        }
    }
    public void SelectEnemy()
    {
        foreach(UnitLoader unit in FindObjectsOfType<UnitLoader>())
        {
            if(transform.position == unit.transform.position && !unit.unit.allyUnit)
            {
                unit.GetWalkableTiles();
                unit.GetComponent<SpriteRenderer>().color = Color.red;
            }
        }
    }
    public void ResetTiles()
    {
        foreach(TileLoader tile in FindObjectsOfType<TileLoader>())
        {
            tile.ResetTiles();
        }
        foreach(UnitLoader unit in FindObjectsOfType<UnitLoader>())
        {
            if(!unit.unit.allyUnit)
            {
                unit.GetComponent<SpriteRenderer>().color = Color.white;
            }
        }
    }
    public void MoveUnit()
    {
        foreach(TileLoader tile in FindObjectsOfType<TileLoader>())
        {
            if(transform.position == tile.transform.position)
            {
                if(tile.walkable == true && selectedUnit.hasMoved == false)
                {
                    selectedUnit.Move(tile.transform.position);
                    SetState(new ActionMenuState(this));
                    controls.MapScene.Disable();
                    controls.UI.Enable();
                }
            }
        }
    }
    public void UndoMove()
    {
        foreach(UnitLoader unit in FindObjectsOfType<UnitLoader>())
        {
            if(!unit.unit.allyUnit)
            {
                unit.spriteRenderer.color = Color.white;
            }
        }
        selectedUnit.transform.position = selectedUnit.originalPosition;
        selectedUnit.hasMoved = false;
        TurnManager.instance.RefreshTiles();
        selectedUnit.ActionMenu();
        selectedUnit.target = null;
        selectedUnit = null;
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
            if(transform.position == unit.transform.position && selectedUnit.enemiesInRange.Contains(unit))
            {
                selectedUnit.target = unit;
                ActionMenuManager.instance.combatPreview.SetActive(true);
                controls.MapScene.Disable();
                controls.UI.Enable();
                SetState(new CombatPreviewState(this));
            }
        }
    }    
    public void CancelAttack()
    {
        selectedUnit.target = null;
        ActionMenuManager.instance.combatPreview.SetActive(false);        
        controls.UI.Disable();
        controls.MapScene.Enable();
        SetState(new AttackState(this));
    }
    public void AttackTarget()
    {
        CombatManager.instance.EngageAttack(selectedUnit, selectedUnit.target);
        selectedUnit.target.GetComponent<SpriteRenderer>().color = Color.white;
        selectedUnit = null;
        ActionMenuManager.instance.combatPreview.SetActive(false);
        ActionMenuManager.instance.gameObject.SetActive(false);
    }
    public void DisplayMenu()
    {
        foreach(TileLoader tile in FindObjectsOfType<TileLoader>())
        {
            if(transform.position == tile.transform.position && tile.occupied == false)
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
        menu.SetActive(false);
        SetState(new MapState(this));
        controls.UI.Disable();
        controls.MapScene.Enable();
    }    
}
