using UnityEngine;
using UnityEngine.InputSystem;

//Handles the functionality of the Cursor
//There should only be 1 CursorController script in any given scene
public class CursorController : MonoBehaviour
{
    //VARIABLES
    public Sprite highlight = null;
    private Vector3 currentPosition = new Vector3(0, 0, 0);
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

    private void Awake()
    {
        controls = new CursorControls();

        //MapCursor Controls
        controls.MapCursor.SelectUnit.performed += ctx => SelectUnit();
        controls.MapCursor.SelectEnemy.performed += ctx => SelectEnemy();
        controls.MapCursor.ResetTiles.performed += ctx => ResetTiles();
        controls.MapCursor.DisplayMenu.performed += ctx => DisplayMenu();
        controls.MapCursor.Movement.performed += ctx => MapMovement(ctx.ReadValue<Vector2>());
        
        //UnitCursor Controls
        controls.UnitCursor.DeselectUnit.performed += ctx => DeselectUnit();
        controls.UnitCursor.Movement.performed += ctx => MapMovement(ctx.ReadValue<Vector2>());
        controls.UnitCursor.MoveUnit.performed += ctx => MoveUnit();

        //AttackCursor Controls
        controls.AttackCursor.Attack.performed += ctx => AttackUnit();
        controls.AttackCursor.Cancel.performed += ctx => CancelAttack();
        controls.AttackCursor.Movement.performed += ctx => MapMovement(ctx.ReadValue<Vector2>());

        //ActionMenuCursor Controls
        controls.ActionMenuCursor.UndoMove.performed += ctx => UndoMove();

        //MenuCursor Controls
        controls.MenuCursor.CloseMenu.performed += ctx => CloseMenu();
    }

    private void Start()
    {
        currentPosition = transform.position;
        highlight = GetComponent<SpriteRenderer>().sprite;
        enemyTurn = false;
    }

    private void Update()
    {
        if(enemyTurn == false)
        {
            CursorCameraMovement();
        }
    }

    //This allows the cursor to move the camera when it moves to the edge of the viewport
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

    //This is for the Cursor to move along the grid of the map
    private void MapMovement(Vector2 direction)
    {
        if(direction == new Vector2(-1, 0))
        {
            if(currentPosition.x > leftMost)
            {
                currentPosition.x -= 1;
                transform.position = currentPosition;
            }
        }
        if(direction == new Vector2(1, 0))
        {
            if (currentPosition.x < rightMost)
            {
                currentPosition.x += 1;
                transform.position = currentPosition;
            }
        }
        if(direction == new Vector2(0, 1))
        {
            if (currentPosition.y < topMost)
            {
                currentPosition.y += 1;
                transform.position = currentPosition;
            }
        }
        if(direction == new Vector2(0, -1))
        {
            if (currentPosition.y > bottomMost)
            {
                currentPosition.y -= 1;
                transform.position = currentPosition;
            }
        }
    }

    //This lets you select ally units
    private void SelectUnit()
    {
        foreach(UnitLoader unit in FindObjectsOfType<UnitLoader>())
        {
            if(transform.position == unit.transform.position && selectedUnit == null)
            {
                if(unit.unit.allyUnit && unit.rested == false)
                {
                    selectedUnit = unit;
                    unit.Selected();

                    controls.MapCursor.Disable();
                    controls.UnitCursor.Enable();
                }
            }
        }
    }

    //This lets you check the movement range of an enemy
    private void SelectEnemy()
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

    //This lets you reset all the tiles when you press B in case you selected an enemy previously
    private void ResetTiles()
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

    //This deselects any selected unit
    private void DeselectUnit()
    {
        selectedUnit.animator.SetBool("Selected", false);
        selectedUnit.ResetTiles();
        selectedUnit = null;

        controls.UnitCursor.Disable();
        controls.MapCursor.Enable();
    }

    //This lets you move the selected unit to a new tile
    private void MoveUnit()
    {
        foreach(TileLoader tile in FindObjectsOfType<TileLoader>())
        {
            if(transform.position == tile.transform.position)
            {
                if(tile.walkable == true && selectedUnit.hasMoved == false)
                {
                    selectedUnit.Move(tile.transform.position);

                    controls.UnitCursor.Disable();
                    controls.ActionMenuCursor.Enable();
                }
            }
        }
    }

    //This puts the unit that just moved back to its original position
    private void UndoMove()
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
        controls.ActionMenuCursor.Disable();
        controls.MapCursor.Enable();
    }

    //This lets you choose a unit to attack
    private void AttackUnit()
    {
        foreach(UnitLoader unit in FindObjectsOfType<UnitLoader>())
        {
            if(transform.position == unit.transform.position && selectedUnit.enemiesInRange.Contains(unit))
            {
                selectedUnit.target = unit;
                ActionMenuManager.instance.combatPreview.SetActive(true);
                ResetState();
            }
        }
    }

    //This lets you cancel your attack 
    private void CancelAttack()
    {
        selectedUnit.target = null;
        ActionMenuManager.instance.combatPreview.SetActive(false);
        ResetState();
        UndoMove();
    }

    //Opens the Menu
    private void DisplayMenu()
    {
        foreach(TileLoader tile in FindObjectsOfType<TileLoader>())
        {
            if(transform.position == tile.transform.position && tile.occupied == false)
            {
                controls.MapCursor.Disable();
                controls.UnitCursor.Disable();
                controls.MenuCursor.Enable();
                menu.SetActive(true);
            }
        }
    }

    //Closes the Menu
    private void CloseMenu()
    {
        menu.SetActive(false);
        controls.MenuCursor.Disable();
        controls.MapCursor.Enable();
    } 

    //Turns all the cursor booleans false making changing the cursor state less tedious
    public void ResetState()
    {
        controls.MapCursor.Disable();
        controls.UnitCursor.Disable();
        controls.MenuCursor.Disable();
        controls.ActionMenuCursor.Disable();
        controls.NeutralCursor.Disable();
        controls.AttackCursor.Disable();
    }
}
