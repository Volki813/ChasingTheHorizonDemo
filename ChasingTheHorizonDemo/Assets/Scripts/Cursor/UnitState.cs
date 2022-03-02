﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitState : MapState
{
    public UnitState(CursorController cursor):base(cursor)
    {
    }

    public override void Confirm()
    {
        cursorController.MoveUnit();
    }

    public override void Cancel()
    {
        cursorController.DeselectUnit();
    }
}
