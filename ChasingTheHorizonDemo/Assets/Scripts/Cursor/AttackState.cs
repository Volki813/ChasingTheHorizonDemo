using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : CursorState
{
    public AttackState(CursorController cursor):base(cursor)
    {
    }

    public override void Confirm()
    {
        cursorController.SelectTarget();        
    }

    public override void Cancel()
    {
        cursorController.UndoAttack();
    }
}
