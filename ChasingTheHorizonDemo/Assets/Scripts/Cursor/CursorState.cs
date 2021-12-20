using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CursorState
{
    protected readonly CursorController cursorController;
    public CursorState(CursorController cursor)
    {
        cursorController = cursor;
    }

    public virtual void Start()
    {

    }

    public virtual void Confirm()
    {

    }

    public virtual void Cancel()
    {

    }
}
