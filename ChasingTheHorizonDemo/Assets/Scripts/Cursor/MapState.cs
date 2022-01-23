public class MapState : CursorState
{
    public MapState(CursorController cursor) : base(cursor)
    {
    }

    public override void Start()
    {
        cursorController.controls.UI.Disable();
    }

    public override void Confirm()
    {
        cursorController.SelectUnit();
        cursorController.SelectEnemy();
        cursorController.DisplayMenu();
    }

    public override void Cancel()
    {
        cursorController.DeselectUnit();
        cursorController.ResetTiles();
    }
}
