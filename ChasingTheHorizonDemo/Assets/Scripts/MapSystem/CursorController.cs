using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CursorController : MonoBehaviour
{
    private Vector3 currentPosition;

    public UnitLoader selectedUnit;

    [SerializeField]
    private GameObject menu;

    [Header("Cursor State Machine")]
    public bool MapCursor = false;
    public bool UnitCursor = false;
    public bool MenuCursor = false;
    public bool ActionMenuCursor = false;
    public bool DialogueCursor = false;

    [Header("Main Camera")]
    [SerializeField]
    private Camera mapCamera;

    [Header("Movement Contraints")]
    [SerializeField]
    private Vector2 topMost;
    [SerializeField]
    private Vector2 bottomMost;
    [SerializeField]
    private Vector2 leftMost;
    [SerializeField]
    private Vector2 rightMost;

    [Header("Camera Constraints")]
    [SerializeField]
    private float cameraTop;
    [SerializeField]
    private float cameraBottom;
    [SerializeField]
    private float cameraLeft;
    [SerializeField]
    private float cameraRight;

    [Header("Map Frame Points")]
    [SerializeField]
    private Transform frameTop;
    [SerializeField]
    private Transform frameBottom;
    [SerializeField]
    private Transform frameLeft;
    [SerializeField]
    private Transform frameRight;

    [Header("Tile Info UI")]
    [SerializeField]
    private Text tileName;
    [SerializeField]
    private Text tileCost;

    private void Start()
    {
        MapCursor = true;
        currentPosition = transform.position;
    }

    private void Update()
    {
        StateManager();
    }

    //Manages all the Cursor states
    private void StateManager()
    {
        CursorCameraMovement();
        if(MapCursor == true)
        {
            MapMovement();
            SelectUnit();
            DisplayMenu();
        }
        else if(UnitCursor == true)
        {
            MapMovement();
            DeselectUnit();
            MoveUnit();
        }
        else if(MenuCursor == true)
        {
            CloseMenu();
        }    
        else if(ActionMenuCursor == true)
        {

        }
        else if(DialogueCursor == true)
        {

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
    private void MapMovement()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (currentPosition.x > leftMost.x)
            {
                currentPosition.x -= 1;
                transform.position = currentPosition;
            }
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (currentPosition.x < rightMost.x)
            {
                currentPosition.x += 1;
                transform.position = currentPosition;
            }
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (currentPosition.y < topMost.y)
            {
                currentPosition.y += 1;
                transform.position = currentPosition;
            }
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (currentPosition.y > bottomMost.y)
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
            if(Input.GetKeyDown(Controls.instance.confirmButton) && transform.position == unit.transform.position && selectedUnit == null)
            {
                if(unit.unit.allyUnit && unit.rested == false)
                {
                    selectedUnit = unit;
                    unit.Selected();

                    MapCursor = false;
                    UnitCursor = true;
                }
            }
        }
    }

    //This lets you move the selected unit to a new tile
    private void MoveUnit()
    {
        foreach(TileLoader tile in FindObjectsOfType<TileLoader>())
        {
            if(Input.GetKeyDown(Controls.instance.confirmButton) && transform.position == tile.transform.position)
            {
                if(tile.walkable == true && selectedUnit.hasMoved == false)
                {
                    selectedUnit.Move(tile.transform.position);

                    UnitCursor = false;
                    ActionMenuCursor = true;
                }
            }
        }
    }

    //This deselects any selected unit 0
    private void DeselectUnit()
    {
        if(Input.GetKeyDown(Controls.instance.cancelButton) && selectedUnit != null)
        {
            selectedUnit.ResetTiles();
            selectedUnit = null;

            UnitCursor = false;
            MapCursor = true;
        }
    }

    //Opens the Menu
    private void DisplayMenu()
    {
        foreach(TileLoader tile in FindObjectsOfType<TileLoader>())
        {
            if(transform.position == tile.transform.position && tile.occupied == false)
            {
                if(Input.GetKeyDown(Controls.instance.confirmButton))
                {
                    MapCursor = false;
                    UnitCursor = false;
                    MenuCursor = true;
                    menu.SetActive(true);
                }
            }
        }
    }

    //Closes the Menu
    private void CloseMenu()
    {
        if(Input.GetKeyDown(Controls.instance.cancelButton))
        {
            menu.SetActive(false);
            MenuCursor = false;
            MapCursor = true;
        }
    } 

    //Turns all the cursor booleans false making changing the cursor state less tedious
    public void ResetState()
    {
        MapCursor = false;
        UnitCursor = false;
        MenuCursor = false;
        ActionMenuCursor = false;
        DialogueCursor = false;
    }
}
