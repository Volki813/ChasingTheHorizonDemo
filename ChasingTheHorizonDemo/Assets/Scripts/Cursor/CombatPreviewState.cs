using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatPreviewState : CursorState
{
    public CombatPreviewState(CursorController cursor) : base(cursor)
    {
    }

    public override void Confirm()
    {

    }

    public override void Cancel()
    {
        cursorController.CancelAttack();
    }
}
