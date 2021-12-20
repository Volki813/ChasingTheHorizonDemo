using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionMenuState : CursorState
{
    public ActionMenuState(CursorController cursor):base(cursor)
    {
    }

    public override void Confirm()
    {
        return;
    }

    public override void Cancel()
    {
        if(ActionMenuManager.instance.inventoryMenu.activeSelf) {
            cursorController.CloseInventory();
        }
        else {
            cursorController.UndoMove();
        }
    }
}
