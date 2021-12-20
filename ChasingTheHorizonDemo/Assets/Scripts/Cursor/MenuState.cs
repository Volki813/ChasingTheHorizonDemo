using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuState : CursorState
{
    public MenuState(CursorController cursor) : base(cursor)
    {
    }

    public override void Confirm()
    {
        return;
    }

    public override void Cancel()
    {
        cursorController.CloseMenu();
    }
}
