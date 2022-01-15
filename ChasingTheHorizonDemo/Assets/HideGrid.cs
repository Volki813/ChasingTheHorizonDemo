using UnityEngine;

public class HideGrid : MonoBehaviour
{
    private bool gridHidden = false;
    public void HideTileGrid()
    {
        if(!gridHidden){
            foreach (TileLoader tile in FindObjectsOfType<TileLoader>()){
                tile.spriteRenderer.color = new Color32(255, 255, 255, 0);
                gridHidden = true;
            }
        }
        else{
            foreach (TileLoader tile in FindObjectsOfType<TileLoader>()){
                tile.spriteRenderer.color = new Color32(255, 255, 255, 255);
                gridHidden = false;
            }
        }
    }
}